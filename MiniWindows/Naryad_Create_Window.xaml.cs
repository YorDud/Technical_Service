using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для Naryad_Create_Window.xaml
	/// </summary>
	public partial class Naryad_Create_Window : Window
	{
		private MainWindow mainWindow;

		public Naryad_Create_Window(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;

			LoadData_ComboBox();

			DateStart.SelectedDate = DateTime.Now;
			DateEnd.SelectedDate = DateTime.Now.AddYears(1);

		}

		private void AddNaryad_Click(object sender, RoutedEventArgs e)
		{
			// Получение данных из полей формы
			var deviceName = DeviceName.Text;
			var typesTOName = TypesTOName.Text;
			var typesTOWorkList = TypesTOWorkList.Text;
			var comment = Comment.Text;
			var skladDeteilID = SkladDeteilID.Text;
			var skladKolich = SkladKolich.Text;
			var documentationNameID = DocumentationNameID.Text;

			string selectQuery = @"
        SELECT t.Raspisanie
        FROM [Technical_Service].[dbo].[Devices] d
        INNER JOIN [Technical_Service].[dbo].[Types_TO] t
            ON d.Name_Device = t.Device_Type
        WHERE d.Name_Device = @DeviceName AND t.Name_TO = @TypesTOName";

			string deleteQuery = @"
        DELETE FROM [Technical_Service].[dbo].[Naryad]
        WHERE Device_Name = @DeviceName AND Date_TO = @Date_TO";

			string insertNaryadQuery = @"
        INSERT INTO [Technical_Service].[dbo].[Naryad] 
            ([Device_Name], [Types_TO_Name], [Types_TO_Work_List], 
            [Comment], [Sklad_Deteil_ID], [Sklad_Kolich], [Documentation_Name_ID], [Date_TO]) 
        VALUES 
            (@DeviceName, @TypesTOName, @TypesTOWorkList, 
            @Comment, @SkladDeteilID, @SkladKolich, @DocumentationNameID, @Date_TO)";

			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				connection.Open();

				// Получаем расписание из базы данных
				string schedule;
				using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection))
				{
					selectCommand.Parameters.AddWithValue("@DeviceName", deviceName);
					selectCommand.Parameters.AddWithValue("@TypesTOName", typesTOName);

					schedule = selectCommand.ExecuteScalar()?.ToString();
				}

				if (string.IsNullOrEmpty(schedule))
				{
					MessageBox.Show($"Нет расписания для обработки по {typesTOName} для устройства {deviceName}.");
					return;
				}

				// Парсинг расписания
				var parsedSchedule = ParseSchedule(schedule);

				DateTime? start = DateStart.SelectedDate;
				DateTime? end = DateEnd.SelectedDate;

				if (!start.HasValue || !end.HasValue)
				{
					MessageBox.Show("Выберите начальную и конечную даты.");
					return;
				}

				// Обработка расписания (1 год вперёд)
				DateTime currentDate = start.Value;
				DateTime endDate = end.Value;

				while (currentDate <= endDate)
				{
					if (IsDateMatchingSchedule(currentDate, parsedSchedule))
					{
						// Удаляем строку, если она уже есть
						using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
						{
							deleteCommand.Parameters.AddWithValue("@DeviceName", deviceName);
							deleteCommand.Parameters.AddWithValue("@Date_TO", currentDate);
							deleteCommand.ExecuteNonQuery();
						}

						// Вставляем новый наряд с датой из расписания
						using (SqlCommand insertCommand = new SqlCommand(insertNaryadQuery, connection))
						{
							insertCommand.Parameters.AddWithValue("@DeviceName", deviceName);
							insertCommand.Parameters.AddWithValue("@TypesTOName", typesTOName);
							insertCommand.Parameters.AddWithValue("@TypesTOWorkList", typesTOWorkList);
							insertCommand.Parameters.AddWithValue("@Comment", comment);
							insertCommand.Parameters.AddWithValue("@SkladDeteilID", skladDeteilID);
							insertCommand.Parameters.AddWithValue("@SkladKolich", (object)skladKolich ?? DBNull.Value);
							insertCommand.Parameters.AddWithValue("@DocumentationNameID", documentationNameID);
							insertCommand.Parameters.AddWithValue("@Date_TO", currentDate);

							insertCommand.ExecuteNonQuery();
						}
					}

					// Переход к следующему дню
					currentDate = currentDate.AddDays(1);
				}
			}

			// Обновление данных и закрытие формы
			mainWindow.LoadData_Naryad();
			this.Close();
		}








		private (List<DayOfWeek> DaysOfWeek, List<int> Months, int RepeatWeeks, List<int> SpecificDays, int WeekDayInMonth) ParseSchedule(string schedule)
		{
			var daysOfWeek = new List<DayOfWeek>();
			var months = new List<int>();
			var specificDays = new List<int>();
			int repeatWeeks = 0;
			int weekDayInMonth = 0;

			// Разбиваем расписание на части по ";"
			var parts = schedule.Split(';');
			foreach (var part in parts)
			{
				var cleanedPart = part.Trim();

				if (cleanedPart.StartsWith("Дни недели:"))
				{
					// Извлекаем дни недели
					var days = cleanedPart.Replace("Дни недели:", "").Split(',');
					foreach (var day in days)
					{
						var trimmedDay = day.Trim();
						switch (trimmedDay)
						{
							case "Пн": daysOfWeek.Add(DayOfWeek.Monday); break;
							case "Вт": daysOfWeek.Add(DayOfWeek.Tuesday); break;
							case "Ср": daysOfWeek.Add(DayOfWeek.Wednesday); break;
							case "Чт": daysOfWeek.Add(DayOfWeek.Thursday); break;
							case "Пт": daysOfWeek.Add(DayOfWeek.Friday); break;
							case "Сб": daysOfWeek.Add(DayOfWeek.Saturday); break;
							case "Вс": daysOfWeek.Add(DayOfWeek.Sunday); break;
								//default: throw new ArgumentException($"Неверный формат дня недели: {trimmedDay}");
						}
					}
				}
				else if (cleanedPart.StartsWith("Месяцы:"))
				{
					// Извлекаем месяцы
					var monthNames = cleanedPart.Replace("Месяцы:", "").Split(',');
					foreach (var month in monthNames)
					{
						var trimmedMonth = month.Trim();
						switch (trimmedMonth)
						{
							case "Январь": months.Add(1); break;
							case "Февраль": months.Add(2); break;
							case "Март": months.Add(3); break;
							case "Апрель": months.Add(4); break;
							case "Май": months.Add(5); break;
							case "Июнь": months.Add(6); break;
							case "Июль": months.Add(7); break;
							case "Август": months.Add(8); break;
							case "Сентябрь": months.Add(9); break;
							case "Октябрь": months.Add(10); break;
							case "Ноябрь": months.Add(11); break;
							case "Декабрь": months.Add(12); break;
								//default: throw new ArgumentException($"Неверный формат месяца: {trimmedMonth}");
						}
					}
				}
				else if (cleanedPart.StartsWith("Повторять каждые:"))
				{
					// Извлекаем количество недель для повторения
					var weeks = cleanedPart.Replace("Повторять каждые:", "").Replace("недель", "").Trim();
					if (int.TryParse(weeks, out int parsedWeeks))
					{
						repeatWeeks = parsedWeeks;
					}
					else
					{
						//throw new ArgumentException($"Неверный формат количества недель: {weeks}");
					}
				}
				else if (cleanedPart.StartsWith("День месяца:"))
				{
					// Извлекаем дни месяца
					var days = cleanedPart.Replace("День месяца:", "").Split(',');
					foreach (var day in days)
					{
						var trimmedDay = day.Trim();
						if (int.TryParse(trimmedDay, out int parsedDay) && parsedDay > 0 && parsedDay <= 31)
						{
							specificDays.Add(parsedDay);
						}
						else
						{
							// Игнорируем некорректное значение
							Console.WriteLine($"Предупреждение: День месяца \"{trimmedDay}\" пропущен, так как он некорректен.");
						}
					}
				}
				else if (cleanedPart.StartsWith("Первая неделя месяца:"))
				{
					// Проверяем, указана ли первая неделя месяца
					var firstWeekValue = cleanedPart.Replace("Первая неделя месяца:", "").Trim();
					weekDayInMonth = string.Equals(firstWeekValue, "Да", StringComparison.OrdinalIgnoreCase) ? 1 : 0;
				}
			}

			return (daysOfWeek, months, repeatWeeks, specificDays, weekDayInMonth);
		}





		private bool IsDateMatchingSchedule(DateTime date, (List<DayOfWeek> DaysOfWeek, List<int> Months, int RepeatWeeks, List<int> SpecificDays, int WeekDayInMonth) schedule)
		{
			// Проверяем дни недели
			if (schedule.DaysOfWeek.Count > 0 && !schedule.DaysOfWeek.Contains(date.DayOfWeek))
				return false;

			// Проверяем месяцы
			if (schedule.Months.Count > 0 && !schedule.Months.Contains(date.Month))
				return false;

			// Проверяем частоту повторения (например, каждую неделю)
			if (schedule.RepeatWeeks > 0)
			{
				int weekDifference = (int)((date - DateTime.Now.Date).TotalDays / 7);
				if (weekDifference % schedule.RepeatWeeks != 0)
					return false;
			}

			// Проверяем конкретные дни месяца
			if (schedule.SpecificDays.Count > 0 && !schedule.SpecificDays.Contains(date.Day))
				return false;

			// Проверяем "день недели в месяце", если он задан
			if (schedule.WeekDayInMonth > 0)
			{
				int weekOfMonth = (date.Day - 1) / 7 + 1;
				if (weekOfMonth != schedule.WeekDayInMonth)
					return false;
			}

			return true;
		}




























		//private void LoadDevices()
		//{
		//	string query = "SELECT Name_Device FROM [Technical_Service].[dbo].[Devices]";
		//	try
		//	{
		//		using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//		{
		//			connection.Open();
		//			using (SqlCommand command = new SqlCommand(query, connection))
		//			{
		//				using (SqlDataReader reader = command.ExecuteReader())
		//				{
		//					List<string> deviceNames = new List<string>();
		//					while (reader.Read())
		//					{
		//						deviceNames.Add(reader["Name_Device"].ToString());
		//					}

		//					DeviceName.ItemsSource = deviceNames; // Устанавливаем источник данных для ComboBox
		//				}
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
		//	}
		//}




		private void LoadData_ComboBox()
		{
			LoadDeviceNames();   // Загрузка устройств
								 //LoadTypesTOName();   // Загрузка ТО
								 //LoadUsersFIO();      // Загрузка ФИО сотрудников
								 //LoadStatus();        // Загрузка статусов
			LoadSkladDeteilID(); // Загрузка деталей
			//LoadDocumentationNameID(); // Загрузка документации
		}

		// Загрузка названий устройств
		private void LoadDeviceNames()
		{
			string query = "SELECT Name_Device FROM [Technical_Service].[dbo].[Devices]";
			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> deviceNames = new List<string>();
							while (reader.Read())
							{
								deviceNames.Add(reader["Name_Device"].ToString());
							}
							DeviceName.ItemsSource = deviceNames; // Устанавливаем источник данных для ComboBox
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		// Загрузка типов ТО в ComboBox
		private void LoadTypesTOName(string selectedDeviceName)
		{
			if (string.IsNullOrEmpty(selectedDeviceName))
			{
				MessageBox.Show("Не выбрано устройство.");
				return;
			}

			// Запрос для получения Device_Type и соответствующих Name_TO
			string query = @"
        SELECT t.Name_TO 
        FROM [Technical_Service].[dbo].[Devices] d
        INNER JOIN [Technical_Service].[dbo].[Types_TO] t
        ON d.Name_Device = t.Device_Type
        WHERE d.Name_Device = @DeviceName";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();

					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавляем значение параметра @DeviceName
						command.Parameters.AddWithValue("@DeviceName", selectedDeviceName);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> typesTO = new List<string>();
							while (reader.Read())
							{
								typesTO.Add(reader["Name_TO"].ToString());
							}

							// Устанавливаем источник данных для ComboBox
							TypesTOName.ItemsSource = typesTO;
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		private void DeviceName_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Получаем выбранное устройство
			string selectedDeviceName = (DeviceName.SelectedItem as string);

			// Вызываем метод для загрузки соответствующих Types_TO
			LoadTypesTOName(selectedDeviceName);

			
			if (!string.IsNullOrEmpty(selectedDeviceName))
			{
				LoadDocumentationNameID(selectedDeviceName);
			}
		}



		// Загрузка списка работ в TextBox по выбранному виду ТО
		private void TypesTOName_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (TypesTOName.SelectedItem != null)
			{
				string selectedNameTO = TypesTOName.SelectedItem.ToString();
				LoadWorkList(selectedNameTO);
			}
		}

		// Получение списка работ для выбранного типа ТО
		private void LoadWorkList(string nameTO)
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				connection.Open();
				SqlCommand command = new SqlCommand(
					"SELECT Work_List FROM [Technical_Service].[dbo].[Types_TO] WHERE Name_TO = @NameTO", connection);
				command.Parameters.AddWithValue("@NameTO", nameTO);

				var result = command.ExecuteScalar();
				TypesTOWorkList.Text = result?.ToString() ?? string.Empty;
			}
		}

		// Загрузка ФИО сотрудников в ComboBox
		//private void LoadUsersFIO()
		//{
		//	string query = "SELECT FIO FROM [Technical_Service].[dbo].[Users]";
		//	try
		//	{
		//		using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//		{
		//			connection.Open();
		//			using (SqlCommand command = new SqlCommand(query, connection))
		//			{
		//				using (SqlDataReader reader = command.ExecuteReader())
		//				{
		//					List<string> usersFIO = new List<string>();
		//					while (reader.Read())
		//					{
		//						usersFIO.Add(reader["FIO"].ToString());
		//					}
		//					UsersFIO.ItemsSource = usersFIO; // Устанавливаем источник данных для ComboBox
		//				}
		//			}
		//		}
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
		//	}
		//}

		// Загрузка статусов в ComboBox
		//private void LoadStatus()
		//{
		//	Status.Items.Add("В работе");
		//	Status.Items.Add("Выполнен");
		//	Status.Items.Add("Закрыт");
		//}

		// Загрузка деталей в ComboBox SkladDeteilID
		private void LoadSkladDeteilID()
		{
			string query = "SELECT [ID], [Deteil_Types] FROM [Technical_Service].[dbo].[Sklad]";
			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<ComboBoxItem> detailTypes = new List<ComboBoxItem>();
							while (reader.Read())
							{
								detailTypes.Add(new ComboBoxItem
								{
									Content = reader["Deteil_Types"].ToString(),
									Tag = reader["ID"] // Сохраняем ID в Tag 
								});
							}
							SkladDeteilID.ItemsSource = detailTypes; // Устанавливаем источник данных для ComboBox
						}
					}
				}

				// Подписка на событие выбора элемента
				SkladDeteilID.SelectionChanged += SkladDeteilID_SelectionChanged;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		// Обработка выбора детали в ComboBox
		private void SkladDeteilID_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			if (SkladDeteilID.SelectedItem is ComboBoxItem selectedItem)
			{
				string selectedID = selectedItem.Tag.ToString();
				//LoadSkladKolich(selectedID); // Загрузка количества по выбранной детали
			}
		}

		// Получение количества по ID детали
		//private void LoadSkladKolich(string skladDeteilID)
		//{
		//	using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//	{
		//		connection.Open();
		//		SqlCommand command = new SqlCommand("SELECT [Kolich] FROM [Technical_Service].[dbo].[Sklad] WHERE [ID] = @ID", connection);
		//		command.Parameters.AddWithValue("@ID", skladDeteilID);

		//		var result = command.ExecuteScalar();
		//		SkladKolich.Text = result?.ToString() ?? string.Empty;
		//	}
		//}

		// Загрузка документации в ComboBox DocumentationNameID
		private void LoadDocumentationNameID(string selectedDeviceName)
		{
			string query = "SELECT Name_Doc FROM [Technical_Service].[dbo].[Documentation] WHERE Name_Device = @DeviceName";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавляем параметр для сравнения с выбранным значением
						command.Parameters.AddWithValue("@DeviceName", selectedDeviceName);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> documentationNames = new List<string>();
							while (reader.Read())
							{
								documentationNames.Add(reader["Name_Doc"].ToString());
							}
							DocumentationNameID.ItemsSource = documentationNames; // Устанавливаем источник данных для ComboBox
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

	}
}
