using System;
using System.Collections.Generic;
using System.Data;
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
	/// Логика взаимодействия для Types_TO_Edit_Window.xaml
	/// </summary>
	/// 
	public partial class Types_TO_Edit_Window : Window
	{
		private DataRowView _dataRow;
		private MainWindow mainWindow;
		private int workNumber = 1;


		public Types_TO_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow;
			_dataRow = dataRow;

			LoadDevice_Type();
			LoadWork_List();

			// Заполнение полей данными из выбранной записи
			DeviceType.Text = _dataRow["Device_Type"].ToString();
			NameTO.Text = _dataRow["Name_TO"].ToString();
			LoadWorkList();

			// Установка расписания
			SetScheduleFromDatabase(_dataRow["Raspisanie"].ToString());
		}

		// Метод для установки расписания на основе строки из базы данных
		private void SetScheduleFromDatabase(string schedule)
		{
			// Пример формата строки: "Дни недели: Пн, Ср; Месяцы: Январь, Февраль; Повторять каждые: 2 недель; День месяца: 15; Первая неделя месяца: Да"
			var daysOfWeek = ExtractFromSchedule(schedule, "Дни недели: ", ";");
			var months = ExtractFromSchedule(schedule, "Месяцы: ", ";");
			var repeatWeeks = ExtractFromSchedule(schedule, "Повторять каждые: ", " недель;");
			var specificDay = ExtractFromSchedule(schedule, "День месяца: ", ";");
			var firstWeek = ExtractFromSchedule(schedule, "Первая неделя месяца: ", ";");

			// Отмечаем чекбоксы для дней недели
			MondayCheckBox.IsChecked = daysOfWeek.Contains("Пн");
			TuesdayCheckBox.IsChecked = daysOfWeek.Contains("Вт");
			WednesdayCheckBox.IsChecked = daysOfWeek.Contains("Ср");
			ThursdayCheckBox.IsChecked = daysOfWeek.Contains("Чт");
			FridayCheckBox.IsChecked = daysOfWeek.Contains("Пт");
			SaturdayCheckBox.IsChecked = daysOfWeek.Contains("Сб");
			SundayCheckBox.IsChecked = daysOfWeek.Contains("Вс");

			// Отмечаем чекбоксы для месяцев
			JanuaryCheckBox.IsChecked = months.Contains("Январь");
			FebruaryCheckBox.IsChecked = months.Contains("Февраль");
			MarchCheckBox.IsChecked = months.Contains("Март");
			AprilCheckBox.IsChecked = months.Contains("Апрель");
			MayCheckBox.IsChecked = months.Contains("Май");
			JuneCheckBox.IsChecked = months.Contains("Июнь");
			JulyCheckBox.IsChecked = months.Contains("Июль");
			AugustCheckBox.IsChecked = months.Contains("Август");
			SeptemberCheckBox.IsChecked = months.Contains("Сентябрь");
			OctoberCheckBox.IsChecked = months.Contains("Октябрь");
			NovemberCheckBox.IsChecked = months.Contains("Ноябрь");
			DecemberCheckBox.IsChecked = months.Contains("Декабрь");

			// Устанавливаем значения для повторений и конкретного дня
			if (int.TryParse(repeatWeeks, out int weeks))
			{
				RepeatWeeksTextBox.Text = weeks.ToString();
			}

			if (int.TryParse(specificDay, out int day))
			{
				SpecificDaysTextBox.Text = day.ToString();
			}

			// Отмечаем первую неделю месяца
			FirstWeekMonthCheckBox.IsChecked = firstWeek == "Да";
		}

		// Вспомогательный метод для извлечения данных из строки расписания
		private string ExtractFromSchedule(string schedule, string startDelimiter, string endDelimiter)
		{
			int startIndex = schedule.IndexOf(startDelimiter, StringComparison.OrdinalIgnoreCase);
			if (startIndex == -1) return string.Empty;

			startIndex += startDelimiter.Length;
			int endIndex = schedule.IndexOf(endDelimiter, startIndex, StringComparison.OrdinalIgnoreCase);
			if (endIndex == -1) return schedule.Substring(startIndex).Trim();

			return schedule.Substring(startIndex, endIndex - startIndex).Trim();
		}

		private void LoadWork_List()
		{
			if (DeviceType.SelectedItem == null)
			{
				//MessageBox.Show("Пожалуйста, выберите тип устройства.");
				return;
			}

			// Получаем выбранное значение из DeviceType ComboBox
			string selectedDevice = DeviceType.SelectedItem.ToString();

			// SQL-запрос с фильтром по Device_Type
			string query = @"
		       SELECT [Work_List] 
		       FROM [Technical_Service].[dbo].[Work_List] 
		       WHERE [Device_Type] = @Device_Type";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Передаём значение выбранного устройства в качестве параметра
						command.Parameters.AddWithValue("@Device_Type", selectedDevice);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> workListItems = new List<string>();
							while (reader.Read())
							{
								// Добавляем элементы Work_List в список
								workListItems.Add(reader["Work_List"].ToString());
							}

							// Устанавливаем источник данных для ComboBox WorkList
							WorkList.ItemsSource = workListItems;
						}
					}
				}
			}
			catch (Exception ex)
			{
				// Обработка ошибок
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		// Привязка события на изменение DeviceType
		private void DeviceType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			// Загружаем Work_List в зависимости от выбранного Device_Type
			LoadWork_List();
		}


		private void LoadDevice_Type()
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
							List<string> typesTO = new List<string>();
							while (reader.Read())
							{
								typesTO.Add(reader["Name_Device"].ToString());
							}
							DeviceType.ItemsSource = typesTO; // Устанавливаем источник данных для ComboBox
						}
					}
				}

				// Подписка на событие выбора элемента
				//Device_Type.SelectionChanged += Device_Type_SelectionChanged;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}


		private void LoadWorkList()
		{
			// Очищаем существующий список работ
			WorkListBox.Items.Clear();

			// Извлекаем список работ для выбранного типа ТО из базы данных
			string query = "SELECT Work_List FROM Types_TO WHERE ID = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ID", _dataRow["ID"]);
						connection.Open();

						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								string workList = reader["Work_List"].ToString();

								// Разбиваем список работ по строкам и добавляем каждую в ListBox
								var workLines = workList.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
								foreach (var work in workLines)
								{
									WorkListBox.Items.Add(work);
								}

								// Устанавливаем начальный номер для следующей работы
								workNumber = WorkListBox.Items.Count + 1;
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке работ: " + ex.Message);
			}
		}

		// Метод для добавления новой работы в ListBox
		private void AddWork_Click(object sender, RoutedEventArgs e)
		{
			string workName = WorkList.Text;

			if (!string.IsNullOrEmpty(workName))
			{
				// Создаем строку с номером и текстом работы
				string workLine = $"{workNumber}. {workName}";

				// Добавляем работу в ListBox
				WorkListBox.Items.Add(workLine);

				// Увеличиваем номер для следующей работы
				workNumber++;
			}

			// Очищаем текстовое поле после добавления работы
			WorkList.Text = string.Empty;
		}

		// Метод для удаления выбранной работы из ListBox
		private void RemoveWork_Click(object sender, RoutedEventArgs e)
		{
			// Получаем выбранную работу
			var selectedWork = WorkListBox.SelectedItem as string;

			if (selectedWork != null)
			{
				// Удаляем выбранную работу из ListBox
				WorkListBox.Items.Remove(selectedWork);
				// После удаления номера нужно пересчитать их, если нужно, чтобы номера были последовательными
				RecalculateWorkNumbers();
			}
		}

		// Метод для редактирования выбранной работы
		private void EditWork_Click(object sender, RoutedEventArgs e)
		{
			// Получаем выбранную работу
			var selectedWork = WorkListBox.SelectedItem as string;

			if (selectedWork != null)
			{
				// Открываем текстовое поле для редактирования
				WorkList.Text = selectedWork.Split(new[] { ". " }, StringSplitOptions.None)[1]; // Извлекаем только текст работы

				// Удаляем выбранную работу из ListBox
				WorkListBox.Items.Remove(selectedWork);
			}
		}

		// Метод для сохранения изменений в базу данных
		private void SaveChanges_Click(object sender, RoutedEventArgs e)
		{
			var deviceType = DeviceType.Text;
			var nameTO = NameTO.Text;

			var workList = string.Join("\n", WorkListBox.Items.Cast<string>()); // Перечень работ как строка

			var id = _dataRow["ID"];


			// Формирование расписания
			var selectedDays = new[] {
			MondayCheckBox.IsChecked == true ? "Пн" : null,
			TuesdayCheckBox.IsChecked == true ? "Вт" : null,
			WednesdayCheckBox.IsChecked == true ? "Ср" : null,
			ThursdayCheckBox.IsChecked == true ? "Чт" : null,
			FridayCheckBox.IsChecked == true ? "Пт" : null,
			SaturdayCheckBox.IsChecked == true ? "Сб" : null,
			SundayCheckBox.IsChecked == true ? "Вс" : null
		}.Where(day => day != null).ToList();

			var selectedMonths = new[] {
			JanuaryCheckBox.IsChecked == true ? "Январь" : null,
			FebruaryCheckBox.IsChecked == true ? "Февраль" : null,
			MarchCheckBox.IsChecked == true ? "Март" : null,
			AprilCheckBox.IsChecked == true ? "Апрель" : null,
			MayCheckBox.IsChecked == true ? "Май" : null,
			JuneCheckBox.IsChecked == true ? "Июнь" : null,
			JulyCheckBox.IsChecked == true ? "Июль" : null,
			AugustCheckBox.IsChecked == true ? "Август" : null,
			SeptemberCheckBox.IsChecked == true ? "Сентябрь" : null,
			OctoberCheckBox.IsChecked == true ? "Октябрь" : null,
			NovemberCheckBox.IsChecked == true ? "Ноябрь" : null,
			DecemberCheckBox.IsChecked == true ? "Декабрь" : null
		}.Where(month => month != null).ToList();

			int.TryParse(RepeatWeeksTextBox.Text, out int repeatWeeks);
			int.TryParse(SpecificDaysTextBox.Text, out int specificDay);

			string firstWeek = FirstWeekMonthCheckBox.IsChecked == true ? "Да" : "Нет";

			string schedule = $"Дни недели: {string.Join(", ", selectedDays)}; " +
							  $"Месяцы: {string.Join(", ", selectedMonths)}; " +
							  $"Повторять каждые: {repeatWeeks} недель; " +
							  $"День месяца: {specificDay}; " +
							  $"Первая неделя месяца: {firstWeek}";



			// SQL-запрос для обновления данных в базе
			string query = "UPDATE [Technical_Service].[dbo].[Types_TO] " +
						   "SET [Device_Type] = @DeviceType, [Name_TO] = @NameTO, " +
						   "[Work_List] = @WorkList, [Raspisanie] = @Schedule " +
						   "WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DeviceType", deviceType);
						command.Parameters.AddWithValue("@NameTO", nameTO);
						command.Parameters.AddWithValue("@WorkList", workList); // Обновляем перечень работ
						command.Parameters.AddWithValue("@Schedule", schedule);
						command.Parameters.AddWithValue("@ID", id);

						connection.Open();
						command.ExecuteNonQuery();

						// После сохранения изменений обновляем данные в главном окне
						mainWindow.LoadData_TypesTO();
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}

		// Метод для пересчета номеров работ после их изменения
		private void RecalculateWorkNumbers()
		{
			for (int i = 0; i < WorkListBox.Items.Count; i++)
			{
				string workLine = WorkListBox.Items[i] as string;
				if (workLine != null)
				{
					string newWorkLine = $"{i + 1}. {workLine.Split(new[] { ". " }, StringSplitOptions.None)[1]}";
					WorkListBox.Items[i] = newWorkLine;
				}
			}

			// Обновляем переменную workNumber на следующий номер после последней работы
			workNumber = WorkListBox.Items.Count + 1;
		}




		private void FirstWeekMonthCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			SpecificDaysTextBox.IsEnabled = false;
			SpecificDaysTextBox.Clear();
			RepeatWeeksTextBox.IsEnabled = false;
			RepeatWeeksTextBox.Clear();
		}

		private void FirstWeekMonthCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			SpecificDaysTextBox.IsEnabled = true;
			RepeatWeeksTextBox.IsEnabled = true;
		}
		private void DayOfWeekChecked(object sender, RoutedEventArgs e)
		{
			// Когда выбран день недели, нужно отключить дни месяца
			SpecificDaysTextBox.IsEnabled = false;
		}

		private void DayOfWeekUnchecked(object sender, RoutedEventArgs e)
		{
			// Когда день недели снят, включаем дни месяца
			if (!AreAnyDayOfWeekChecked())
			{
				SpecificDaysTextBox.IsEnabled = true;
			}
		}

		private bool AreAnyDayOfWeekChecked()
		{
			return MondayCheckBox.IsChecked == true ||
				   TuesdayCheckBox.IsChecked == true ||
				   WednesdayCheckBox.IsChecked == true ||
				   ThursdayCheckBox.IsChecked == true ||
				   FridayCheckBox.IsChecked == true ||
				   SaturdayCheckBox.IsChecked == true ||
				   SundayCheckBox.IsChecked == true;
		}









		private void DeleteTypeTO_Click(object sender, RoutedEventArgs e)
		{
			var id = _dataRow["ID"];

			// SQL-запрос для удаления записи
			string query = "DELETE FROM [Technical_Service].[dbo].[Types_TO] WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ID", id);

						connection.Open();
						command.ExecuteNonQuery();

						mainWindow.LoadData_TypesTO();
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}















































		//public partial class Types_TO_Edit_Window : Window
		//{
		//	private DataRowView _dataRow;
		//	private MainWindow mainWindow;
		//	private int workNumber = 1;
		//	public Types_TO_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		//	{
		//		InitializeComponent();

		//		this.mainWindow = mainWindow;
		//		_dataRow = dataRow;

		//		LoadDevice_Type();
		//		LoadWork_List();

		//		// Заполнение полей данными из выбранной записи
		//		DeviceType.Text = _dataRow["Device_Type"].ToString();
		//		NameTO.Text = _dataRow["Name_TO"].ToString();
		//		//WorkList.Text = _dataRow["Work_List"].ToString();
		//		LoadWorkList();


		//		Raspisanie.Text = _dataRow["Raspisanie"].ToString();
		//	}

		//	private void LoadWork_List()
		//	{
		//		if (DeviceType.SelectedItem == null)
		//		{
		//			//MessageBox.Show("Пожалуйста, выберите тип устройства.");
		//			return;
		//		}

		//		// Получаем выбранное значение из DeviceType ComboBox
		//		string selectedDevice = DeviceType.SelectedItem.ToString();

		//		// SQL-запрос с фильтром по Device_Type
		//		string query = @"
		//       SELECT [Work_List] 
		//       FROM [Technical_Service].[dbo].[Work_List] 
		//       WHERE [Device_Type] = @Device_Type";

		//		try
		//		{
		//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//			{
		//				connection.Open();
		//				using (SqlCommand command = new SqlCommand(query, connection))
		//				{
		//					// Передаём значение выбранного устройства в качестве параметра
		//					command.Parameters.AddWithValue("@Device_Type", selectedDevice);

		//					using (SqlDataReader reader = command.ExecuteReader())
		//					{
		//						List<string> workListItems = new List<string>();
		//						while (reader.Read())
		//						{
		//							// Добавляем элементы Work_List в список
		//							workListItems.Add(reader["Work_List"].ToString());
		//						}

		//						// Устанавливаем источник данных для ComboBox WorkList
		//						WorkList.ItemsSource = workListItems;
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			// Обработка ошибок
		//			MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
		//		}
		//	}

		//	// Привязка события на изменение DeviceType
		//	private void DeviceType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		//	{
		//		// Загружаем Work_List в зависимости от выбранного Device_Type
		//		LoadWork_List();
		//	}


		//	private void LoadDevice_Type()
		//	{
		//		string query = "SELECT Device_Type FROM [Technical_Service].[dbo].[Devices_Types]";
		//		try
		//		{
		//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//			{
		//				connection.Open();
		//				using (SqlCommand command = new SqlCommand(query, connection))
		//				{
		//					using (SqlDataReader reader = command.ExecuteReader())
		//					{
		//						List<string> typesTO = new List<string>();
		//						while (reader.Read())
		//						{
		//							typesTO.Add(reader["Device_Type"].ToString());
		//						}
		//						DeviceType.ItemsSource = typesTO; // Устанавливаем источник данных для ComboBox
		//					}
		//				}
		//			}

		//			// Подписка на событие выбора элемента
		//			//Device_Type.SelectionChanged += Device_Type_SelectionChanged;
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
		//		}
		//	}


		//	private void LoadWorkList()
		//	{
		//		// Очищаем существующий список работ
		//		WorkListBox.Items.Clear();

		//		// Извлекаем список работ для выбранного типа ТО из базы данных
		//		string query = "SELECT Work_List FROM Types_TO WHERE ID = @ID";

		//		try
		//		{
		//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//			{
		//				using (SqlCommand command = new SqlCommand(query, connection))
		//				{
		//					command.Parameters.AddWithValue("@ID", _dataRow["ID"]);
		//					connection.Open();

		//					using (SqlDataReader reader = command.ExecuteReader())
		//					{
		//						while (reader.Read())
		//						{
		//							string workList = reader["Work_List"].ToString();

		//							// Разбиваем список работ по строкам и добавляем каждую в ListBox
		//							var workLines = workList.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		//							foreach (var work in workLines)
		//							{
		//								WorkListBox.Items.Add(work);
		//							}

		//							// Устанавливаем начальный номер для следующей работы
		//							workNumber = WorkListBox.Items.Count + 1;
		//						}
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show("Ошибка при загрузке работ: " + ex.Message);
		//		}
		//	}

		//	// Метод для добавления новой работы в ListBox
		//	private void AddWork_Click(object sender, RoutedEventArgs e)
		//	{
		//		string workName = WorkList.Text;

		//		if (!string.IsNullOrEmpty(workName))
		//		{
		//			// Создаем строку с номером и текстом работы
		//			string workLine = $"{workNumber}. {workName}";

		//			// Добавляем работу в ListBox
		//			WorkListBox.Items.Add(workLine);

		//			// Увеличиваем номер для следующей работы
		//			workNumber++;
		//		}

		//		// Очищаем текстовое поле после добавления работы
		//		WorkList.Text = string.Empty;
		//	}

		//	// Метод для удаления выбранной работы из ListBox
		//	private void RemoveWork_Click(object sender, RoutedEventArgs e)
		//	{
		//		// Получаем выбранную работу
		//		var selectedWork = WorkListBox.SelectedItem as string;

		//		if (selectedWork != null)
		//		{
		//			// Удаляем выбранную работу из ListBox
		//			WorkListBox.Items.Remove(selectedWork);
		//			// После удаления номера нужно пересчитать их, если нужно, чтобы номера были последовательными
		//			RecalculateWorkNumbers();
		//		}
		//	}

		//	// Метод для редактирования выбранной работы
		//	private void EditWork_Click(object sender, RoutedEventArgs e)
		//	{
		//		// Получаем выбранную работу
		//		var selectedWork = WorkListBox.SelectedItem as string;

		//		if (selectedWork != null)
		//		{
		//			// Открываем текстовое поле для редактирования
		//			WorkList.Text = selectedWork.Split(new[] { ". " }, StringSplitOptions.None)[1]; // Извлекаем только текст работы

		//			// Удаляем выбранную работу из ListBox
		//			WorkListBox.Items.Remove(selectedWork);
		//		}
		//	}

		//	// Метод для сохранения изменений в базу данных
		//	private void SaveChanges_Click(object sender, RoutedEventArgs e)
		//	{
		//		var deviceType = DeviceType.Text;
		//		var nameTO = NameTO.Text;
		//		var raspisanie = Raspisanie.Text;
		//		var workList = string.Join("\n", WorkListBox.Items.Cast<string>()); // Перечень работ как строка

		//		var id = _dataRow["ID"];

		//		// SQL-запрос для обновления данных в базе
		//		string query = "UPDATE [Technical_Service].[dbo].[Types_TO] " +
		//					   "SET [Device_Type] = @DeviceType, [Name_TO] = @NameTO, " +
		//					   "[Work_List] = @WorkList, [Raspisanie] = @Raspisanie " +
		//					   "WHERE [ID] = @ID";

		//		try
		//		{
		//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//			{
		//				using (SqlCommand command = new SqlCommand(query, connection))
		//				{
		//					command.Parameters.AddWithValue("@DeviceType", deviceType);
		//					command.Parameters.AddWithValue("@NameTO", nameTO);
		//					command.Parameters.AddWithValue("@WorkList", workList); // Обновляем перечень работ
		//					command.Parameters.AddWithValue("@Raspisanie", raspisanie);
		//					command.Parameters.AddWithValue("@ID", id);

		//					connection.Open();
		//					command.ExecuteNonQuery();

		//					// После сохранения изменений обновляем данные в главном окне
		//					mainWindow.LoadData_TypesTO();
		//					this.Close();
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show("Ошибка: " + ex.Message);
		//		}
		//	}

		//	// Метод для пересчета номеров работ после их изменения
		//	private void RecalculateWorkNumbers()
		//	{
		//		for (int i = 0; i < WorkListBox.Items.Count; i++)
		//		{
		//			string workLine = WorkListBox.Items[i] as string;
		//			if (workLine != null)
		//			{
		//				string newWorkLine = $"{i + 1}. {workLine.Split(new[] { ". " }, StringSplitOptions.None)[1]}";
		//				WorkListBox.Items[i] = newWorkLine;
		//			}
		//		}

		//		// Обновляем переменную workNumber на следующий номер после последней работы
		//		workNumber = WorkListBox.Items.Count + 1;
		//	}














		//	private void DeleteTypeTO_Click(object sender, RoutedEventArgs e)
		//	{
		//		var id = _dataRow["ID"];

		//		// SQL-запрос для удаления записи
		//		string query = "DELETE FROM [Technical_Service].[dbo].[Types_TO] WHERE [ID] = @ID";

		//		try
		//		{
		//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//			{
		//				using (SqlCommand command = new SqlCommand(query, connection))
		//				{
		//					command.Parameters.AddWithValue("@ID", id);

		//					connection.Open();
		//					command.ExecuteNonQuery();

		//					mainWindow.LoadData_TypesTO();
		//					this.Close();
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show("Ошибка: " + ex.Message);
		//		}
		//	}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }

        //private void DeleteTypeTO_Click(object sender, RoutedEventArgs e)
        //{
        //	var id = _dataRow["ID"];

        //	// SQL-запрос для удаления записи
        //	string query = "DELETE FROM [Technical_Service].[dbo].[Types_TO] WHERE [ID] = @ID";

        //	try
        //	{
        //		using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
        //		{
        //			using (SqlCommand command = new SqlCommand(query, connection))
        //			{
        //				command.Parameters.AddWithValue("@ID", id);

        //				connection.Open();
        //				command.ExecuteNonQuery();

        //				mainWindow.LoadData_TypesTO();
        //				this.Close();
        //			}
        //		}
        //	}
        //	catch (Exception ex)
        //	{
        //		MessageBox.Show("Ошибка: " + ex.Message);
        //	}
        //}
    }


}
