using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для Naryad_Edit_Window.xaml
	/// </summary>
	public partial class Naryad_Edit_Window: Window
	{
		private MainWindow mainWindow;
		private DataRowView _dataRow;
		public Naryad_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			_dataRow = dataRow;

			LoadData_ComboBox();


			DeviceName.Text = _dataRow["Device_Name"].ToString();
			TypesTOName.Text = _dataRow["Types_TO_Name"].ToString();
			TypesTOWorkList.Text = _dataRow["Types_TO_Work_List"].ToString();
			UsersFIO.Text = _dataRow["Users_FIO"].ToString();
			DateStart.Text = _dataRow["Date_Start"].ToString();
			DateEnd.Text = _dataRow["Date_End"].ToString();
			Status.Text = _dataRow["Status"].ToString();
			Comment.Text = _dataRow["Comment"].ToString();
			SkladDeteilID.Text = _dataRow["Sklad_Deteil_ID"].ToString();
			SkladKolich.Text = _dataRow["Sklad_Kolich"].ToString();
			DocumentationNameID.Text = _dataRow["Documentation_Name_ID"].ToString();
			DateTO.Text = _dataRow["Date_TO"].ToString();
					

		}

		private void Naryad_Update_Click(object sender, RoutedEventArgs e)
		{
			// Предположим, идентификатор записи, которую нужно обновить
			var id = _dataRow["ID"]; // Получите этот идентификатор из контекста, например, из выбранного элемента списка
			var deviceName = DeviceName.Text;
			var typesTOName = TypesTOName.Text;
			var typesTOWorkList = TypesTOWorkList.Text;
			var usersFIO = UsersFIO.Text;
			var dateStart = DateStart.Value;
			var dateEnd = DateEnd.Value;
			var status = Status.Text;
			var comment = Comment.Text;
			var skladDeteilID = SkladDeteilID.Text; // Получение ID запчасти
			var skladKolich = SkladKolich.Text; // Получение количества запчастей
			var documentationNameID = DocumentationNameID.Text; // Получение ID документации
			var dateTO = DateTO.SelectedDate; // Получение ID документации

			string query = "UPDATE [Technical_Service].[dbo].[Naryad] SET " +
						   "[Device_Name] = @DeviceName, " +
						   "[Types_TO_Name] = @TypesTOName, " +
						   "[Types_TO_Work_List] = @TypesTOWorkList, " +
						   "[Users_FIO] = @UsersFIO, " +
						   "[Date_Start] = @DateStart, " +
						   "[Date_End] = @DateEnd, " +
						   "[Status] = @Status, " +
						   "[Comment] = @Comment, " +
						   "[Sklad_Deteil_ID] = @SkladDeteilID, " +
						   "[Sklad_Kolich] = @SkladKolich, " +
						   "[Documentation_Name_ID] = @DocumentationNameID, " +
						   "[Date_TO] = @DateTO " +
						   "WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ID", id); // Параметр для ID наряда
						command.Parameters.AddWithValue("@DeviceName", deviceName);
						command.Parameters.AddWithValue("@TypesTOName", typesTOName);
						command.Parameters.AddWithValue("@TypesTOWorkList", typesTOWorkList);
						command.Parameters.AddWithValue("@UsersFIO", usersFIO);
						command.Parameters.AddWithValue("@DateStart", (object)dateStart ?? DBNull.Value);
						command.Parameters.AddWithValue("@DateEnd", (object)dateEnd ?? DBNull.Value);
						command.Parameters.AddWithValue("@Status", status);
						command.Parameters.AddWithValue("@Comment", comment);
						command.Parameters.AddWithValue("@SkladDeteilID", skladDeteilID);
						command.Parameters.AddWithValue("@SkladKolich", (object)skladKolich ?? DBNull.Value);
						command.Parameters.AddWithValue("@DocumentationNameID", documentationNameID);
						command.Parameters.AddWithValue("@DateTO", (object)dateTO ?? DBNull.Value);

						connection.Open();
						command.ExecuteNonQuery();
						mainWindow.LoadData_Naryad();
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}
		private void Naryad_Delete_Click(object sender, RoutedEventArgs e)
		{
			// Предположим, что у вас есть идентификатор записи, которую нужно удалить
			var id = _dataRow["ID"]; // Получите этот идентификатор из контекста, например, из выбранного элемента списка

			string query = "DELETE FROM [Technical_Service].[dbo].[Naryad] WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ID", id); // Параметр для ID наряда

						connection.Open();
						command.ExecuteNonQuery();
						mainWindow.LoadData_Naryad();
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}





























		private void LoadDevices()
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




		private void LoadData_ComboBox()
		{
			LoadDeviceNames();   // Загрузка устройств
			//LoadTypesTOName(selectedDeviceName);   // Загрузка ТО
			LoadUsersFIO();      // Загрузка ФИО сотрудников
			LoadStatus();        // Загрузка статусов
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

				// Подписка на событие выбора элемента
				TypesTOName.SelectionChanged += TypesTOName_SelectionChanged;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
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
			string selectedDeviceName = (DeviceName.SelectedItem as string);

			// Вызываем метод для загрузки соответствующих Types_TO
			LoadTypesTOName(selectedDeviceName);

			if (!string.IsNullOrEmpty(selectedDeviceName))
			{
				LoadDocumentationNameID(selectedDeviceName);
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
		private void LoadUsersFIO()
		{
			string query = "SELECT FIO FROM [Technical_Service].[dbo].[Users]";
			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							List<string> usersFIO = new List<string>();
							while (reader.Read())
							{
								usersFIO.Add(reader["FIO"].ToString());
							}
							UsersFIO.ItemsSource = usersFIO; // Устанавливаем источник данных для ComboBox
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}

		// Загрузка статусов в ComboBox
		private void LoadStatus()
		{
			Status.Items.Add("В работе");
			Status.Items.Add("Выполнен");
			Status.Items.Add("Закрыт");
		}

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





		private void Naryad_Download_Click(object sender, RoutedEventArgs e)
		{
			if (_dataRow == null)
			{
				MessageBox.Show("Выберите строку данных для создания наряда.");
				return;
			}

			// Извлекаем данные из _dataRow
			int id = Convert.ToInt32(_dataRow["ID"]);
			DateTime dateTo = Convert.ToDateTime(_dataRow["Date_TO"]);
			string deviceName = _dataRow["Device_Name"]?.ToString() ?? " ";
			string typesToName = _dataRow["Types_TO_Name"]?.ToString() ?? " ";
			string typesToWorkList = _dataRow["Types_TO_Work_List"]?.ToString() ?? " ";
			string usersFio = _dataRow["Users_FIO"]?.ToString() ?? " ";

			// Заменяем символы '\n' на перенос строки для Excel
			typesToWorkList = typesToWorkList.Replace("\\n", Environment.NewLine);
			usersFio = usersFio.Replace("\\n", Environment.NewLine);

			// Создание Excel-документа
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("Наряд");

				// Заголовок
				worksheet.Cells["A1:E1"].Merge = true;
				worksheet.Cells["A1"].Value = "ООО \"Резонит Плюс\", Служба ремонта и обслуживания оборудования";
				worksheet.Cells["A1"].Style.Font.Bold = true;
				worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

				worksheet.Cells["A2:E2"].Merge = true;
				worksheet.Cells["A2"].Value = "Наряд на проведение технического обслуживания";
				worksheet.Cells["A2"].Style.Font.Bold = true;
				worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

				worksheet.Cells["A3:E3"].Merge = true;
				worksheet.Cells["A3"].Value = $"Наряд № {id} от {dateTo:dd.MM.yyyy}";
				worksheet.Cells["A3"].Style.Font.Bold = true;
				worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

				// Подписи
				worksheet.Cells["A5:E5"].Merge = true;
				worksheet.Cells["A5"].Value = "Начальник отдела по ремонту и обслуживанию: Гусев И. А.";

				// Таблица
				string[] headers = { "№ п/п", "Наименование оборудования", "Вид работ", "Перечень работ", "Наладчики" };
				for (int i = 0; i < headers.Length; i++)
				{
					worksheet.Cells[8, i + 1].Value = headers[i];
					worksheet.Cells[8, i + 1].Style.Font.Bold = true;
					worksheet.Cells[8, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					worksheet.Cells[8, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
					worksheet.Cells[8, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
					//worksheet.Cells[8, i + 1].Style.WrapText = true; // Включаем перенос текста
				}

				// Данные таблицы
				worksheet.Cells[9, 1].Value = 1; // № п/п
				worksheet.Cells[9, 2].Value = deviceName; // Наименование оборудования
				worksheet.Cells[9, 3].Value = typesToName; // Вид работ
				worksheet.Cells[9, 4].Value = typesToWorkList; // Перечень работ
				worksheet.Cells[9, 5].Value = usersFio; // Наладчики

				for (int col = 1; col <= 5; col++)
				{
					worksheet.Cells[9, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
					worksheet.Cells[9, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
					worksheet.Cells[9, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					worksheet.Cells[9, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
					worksheet.Cells[9, 4].Style.WrapText = true; // Перенос текста
					worksheet.Cells[9, 5].Style.WrapText = true; // Перенос текста
				}


				// Автоматическая ширина для всех колонок
				worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

				// Фиксированная ширина для 4-го и 5-го столбиков
				worksheet.Column(1).Width = 7;
				worksheet.Column(4).Width = 30; // Ширина для "Перечень работ"
				worksheet.Column(5).Width = 25; // Ширина для "Наладчики"


				// После добавления данных таблицы

				// Определяем начальную строку для подписи
				int signatureRow = 13; // 9 (таблица) + 4 строки

				// Добавляем подчеркивание
				worksheet.Cells[signatureRow, 5].Value = ""; // Пустая ячейка
				worksheet.Cells[signatureRow, 5].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

				// Добавляем текст "Подпись" ниже
				worksheet.Cells[signatureRow + 1, 5].Value = "Подпись";
				worksheet.Cells[signatureRow + 1, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				worksheet.Cells[signatureRow + 1, 5].Style.VerticalAlignment = ExcelVerticalAlignment.Top;




				// Сохранение на рабочий стол
				var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				var filePath = System.IO.Path.Combine(desktopPath, $"Наряд_№{id}_от_{dateTo:dd.MM.yyyy}.xlsx");
				package.SaveAs(new FileInfo(filePath));
			}

			MessageBox.Show("Наряд успешно сохранён на рабочем столе.");
			this.Close();
		}


























		//private void Naryad_Download_Click(object sender, RoutedEventArgs e)
		//{
		//	if (_dataRow == null)
		//	{
		//		MessageBox.Show("Выберите строку данных для создания наряда.");
		//		return;
		//	}

		//	// Извлекаем данные из _dataRow
		//	int id = Convert.ToInt32(_dataRow["ID"]);
		//	DateTime dateTo = Convert.ToDateTime(_dataRow["Date_TO"]);
		//	string deviceName = _dataRow["Device_Name"]?.ToString() ?? " ";
		//	string typesToName = _dataRow["Types_TO_Name"]?.ToString() ?? " ";
		//	string typesToWorkList = _dataRow["Types_TO_Work_List"]?.ToString() ?? " ";
		//	string usersFio = _dataRow["Users_FIO"]?.ToString() ?? " ";



		//	// Создание Excel-документа
		//	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

		//	using (var package = new ExcelPackage())
		//	{
		//		var worksheet = package.Workbook.Worksheets.Add("Наряд");

		//		// Заголовок
		//		worksheet.Cells["A1:E1"].Merge = true;
		//		worksheet.Cells["A1"].Value = "ООО \"Резонит Плюс\", Служба ремонта и облуживания оборудования";
		//		worksheet.Cells["A1"].Style.Font.Bold = true;
		//		worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

		//		worksheet.Cells["A2:E2"].Merge = true;
		//		worksheet.Cells["A2"].Value = "Наряд на проведение технического обслуживания";
		//		worksheet.Cells["A2"].Style.Font.Bold = true;
		//		worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

		//		worksheet.Cells["A3:E3"].Merge = true;
		//		worksheet.Cells["A3"].Value = $"Наряд № {id} от {dateTo:dd.MM.yyyy}";
		//		worksheet.Cells["A3"].Style.Font.Bold = true;
		//		worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

		//		// Подписи
		//		worksheet.Cells["A5:E5"].Merge = true;
		//		worksheet.Cells["A5"].Value = "Начальник отдела по ремонту и обслуживанию: Гусев И. А.";

		//		//worksheet.Cells["E6"].Value = "Наладчики";
		//		//worksheet.Cells["E6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

		//		//worksheet.Cells["E7"].Value = usersFio;
		//		//worksheet.Cells["E7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

		//		// Таблица
		//		string[] headers = { "№ п/п", "Наименование оборудования", "Вид работ", "Перечень работ", "Наладчики" };
		//		for (int i = 0; i < headers.Length; i++)
		//		{
		//			worksheet.Cells[8, i + 1].Value = headers[i];
		//			worksheet.Cells[8, i + 1].Style.Font.Bold = true;
		//			worksheet.Cells[8, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//			worksheet.Cells[8, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			worksheet.Cells[8, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//		}

		//		// Данные таблицы
		//		worksheet.Cells[9, 1].Value = 1; // № п/п
		//		worksheet.Cells[9, 2].Value = deviceName; // Наименование оборудования
		//		worksheet.Cells[9, 3].Value = typesToName; // Вид работ
		//		worksheet.Cells[9, 4].Value = typesToWorkList; // Перечень работ
		//		worksheet.Cells[9, 5].Value = usersFio; // Наладчики

		//		for (int col = 1; col <= 5; col++)
		//		{
		//			worksheet.Cells[9, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//			worksheet.Cells[9, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
		//			worksheet.Cells[9, col].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
		//			worksheet.Cells[9, 4].Style.WrapText = true; // Перенос текста
		//			worksheet.Cells[9, 5].Style.WrapText = true; // Перенос текста
		//		}

		//		worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

		//		// Сохранение на рабочий стол
		//		var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//		var filePath = System.IO.Path.Combine(desktopPath, $"Наряд_№{id}_от_{dateTo:dd.MM.yyyy}.xlsx");
		//		package.SaveAs(new FileInfo(filePath));
		//	}

		//	MessageBox.Show("Наряд успешно сохранён на рабочем столе.");
		//	this.Close();
		//}















		//		private void Naryad_Download_Click(object sender, RoutedEventArgs e)
		//		{
		//			string query = @"
		//SELECT n.ID, 
		//       n.Date_TO, 
		//       n.Device_Name, 
		//       n.Types_TO_Name, 
		//       n.Types_TO_Work_List, 
		//       n.Users_FIO
		//FROM Naryad n";

		//			var dbData = new List<(int ID, DateTime Date_TO, string Device_Name, string Types_TO_Name, string Types_TO_Work_List, string Users_FIO)>();

		//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//			{
		//				try
		//				{
		//					connection.Open();
		//					using (SqlCommand command = new SqlCommand(query, connection))
		//					{
		//						using (SqlDataReader reader = command.ExecuteReader())
		//						{
		//							while (reader.Read())
		//							{
		//								var id = reader.GetInt32(0);
		//								var dateTo = reader.GetDateTime(1);
		//								var deviceName = reader.IsDBNull(2) ? " " : reader.GetString(2);
		//								var typesToName = reader.IsDBNull(3) ? " " : reader.GetString(3);
		//								var typesToWorkList = reader.IsDBNull(4) ? " " : reader.GetString(4);
		//								var usersFio = reader.IsDBNull(5) ? " " : reader.GetString(5);

		//								dbData.Add((id, dateTo, deviceName, typesToName, typesToWorkList, usersFio));
		//							}
		//						}
		//					}
		//				}
		//				catch (Exception ex)
		//				{
		//					MessageBox.Show($"Ошибка подключения к БД: {ex.Message}");
		//					return;
		//				}
		//			}

		//			if (dbData.Count == 0)
		//			{
		//				MessageBox.Show("Данные для создания наряда отсутствуют.");
		//				return;
		//			}

		//			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
		//			foreach (var row in dbData)
		//			{
		//				using (var package = new ExcelPackage())
		//				{
		//					var worksheet = package.Workbook.Worksheets.Add("Наряд");

		//					// Заголовок
		//					worksheet.Cells["A1:E1"].Merge = true;
		//					worksheet.Cells["A1"].Value = "ООО \"Микролит\", инженерная служба";
		//					worksheet.Cells["A1"].Style.Font.Bold = true;
		//					worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

		//					worksheet.Cells["A2:E2"].Merge = true;
		//					worksheet.Cells["A2"].Value = "Наряд на проведение технического обслуживания";
		//					worksheet.Cells["A2"].Style.Font.Bold = true;
		//					worksheet.Cells["A2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

		//					worksheet.Cells["A3:E3"].Merge = true;
		//					worksheet.Cells["A3"].Value = $"Наряд № {row.ID} от {row.Date_TO:dd.MM.yyyy}";
		//					worksheet.Cells["A3"].Style.Font.Bold = true;
		//					worksheet.Cells["A3"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

		//					// Подписи
		//					worksheet.Cells["A5:E5"].Merge = true;
		//					worksheet.Cells["A5"].Value = "Начальник отдела по ремонту и обслуживанию Гусев И. А.";

		//					worksheet.Cells["E6"].Value = "Наладчики";
		//					worksheet.Cells["E6"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

		//					worksheet.Cells["E7"].Value = row.Users_FIO;
		//					worksheet.Cells["E7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

		//					// Таблица
		//					string[] headers = { "№ п/п", "Наименование оборудования", "Вид работ", "Перечень работ" };
		//					for (int i = 0; i < headers.Length; i++)
		//					{
		//						worksheet.Cells[8, i + 1].Value = headers[i];
		//						worksheet.Cells[8, i + 1].Style.Font.Bold = true;
		//						worksheet.Cells[8, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//						worksheet.Cells[8, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//						worksheet.Cells[8, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//					}

		//					// Данные таблицы
		//					worksheet.Cells[9, 1].Value = 1; // № п/п
		//					worksheet.Cells[9, 2].Value = row.Device_Name; // Наименование оборудования
		//					worksheet.Cells[9, 3].Value = row.Types_TO_Name; // Вид работ
		//					worksheet.Cells[9, 4].Value = row.Types_TO_Work_List; // Перечень работ

		//					for (int col = 1; col <= 4; col++)
		//					{
		//						worksheet.Cells[9, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//						worksheet.Cells[9, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
		//					}

		//					worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

		//					// Сохранение на рабочий стол
		//					var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//					var filePath = System.IO.Path.Combine(desktopPath, $"Наряд_№{row.ID}_от_{row.Date_TO:dd.MM.yyyy}.xlsx");
		//					package.SaveAs(new FileInfo(filePath));
		//				}
		//			}

		//			MessageBox.Show("Наряды созданы на рабочем столе.");
		//			this.Close();
		//		}
	}
}
