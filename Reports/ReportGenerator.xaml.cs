using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Text.RegularExpressions;


namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для ReportGenerator.xaml
	/// </summary>
	public partial class ReportGenerator : Window
	{
		

		public ReportGenerator()
		{
			InitializeComponent();
			
			
			
		}
		
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}









		private void CreateReportButton_Click(object sender, RoutedEventArgs e)
		{
			var period = ((ComboBoxItem)PeriodComboBox.SelectedItem)?.Content.ToString();
			var year = ((ComboBoxItem)YearComboBox.SelectedItem)?.Content.ToString();

			if (string.IsNullOrEmpty(period) || string.IsNullOrEmpty(year))
			{
				MessageBox.Show("Пожалуйста, выберите период и год.");
				return;
			}

			DateTime startDate, endDate;
			int selectedYear = int.Parse(year);

			// Установка диапазона дат
			switch (period)
			{
				case "Январь":
					startDate = new DateTime(selectedYear, 1, 1);
					endDate = new DateTime(selectedYear, 1, 31);
					break;
				case "Февраль":
					startDate = new DateTime(selectedYear, 2, 1);
					endDate = new DateTime(selectedYear, 2, DateTime.DaysInMonth(selectedYear, 2));
					break;
				case "Март":
					startDate = new DateTime(selectedYear, 3, 1);
					endDate = new DateTime(selectedYear, 3, 31);
					break;
				case "Апрель":
					startDate = new DateTime(selectedYear, 4, 1);
					endDate = new DateTime(selectedYear, 4, 30);
					break;
				case "Май":
					startDate = new DateTime(selectedYear, 5, 1);
					endDate = new DateTime(selectedYear, 5, 31);
					break;
				case "Июнь":
					startDate = new DateTime(selectedYear, 6, 1);
					endDate = new DateTime(selectedYear, 6, 30);
					break;
				case "Июль":
					startDate = new DateTime(selectedYear, 7, 1);
					endDate = new DateTime(selectedYear, 7, 31);
					break;
				case "Август":
					startDate = new DateTime(selectedYear, 8, 1);
					endDate = new DateTime(selectedYear, 8, 31);
					break;
				case "Сентябрь":
					startDate = new DateTime(selectedYear, 9, 1);
					endDate = new DateTime(selectedYear, 9, 30);
					break;
				case "Октябрь":
					startDate = new DateTime(selectedYear, 10, 1);
					endDate = new DateTime(selectedYear, 10, 31);
					break;
				case "Ноябрь":
					startDate = new DateTime(selectedYear, 11, 1);
					endDate = new DateTime(selectedYear, 11, 30);
					break;
				case "Декабрь":
					startDate = new DateTime(selectedYear, 12, 1);
					endDate = new DateTime(selectedYear, 12, 31);
					break;
				case "1-й квартал":
					startDate = new DateTime(selectedYear, 1, 1);
					endDate = new DateTime(selectedYear, 3, 31);
					break;
				case "2-й квартал":
					startDate = new DateTime(selectedYear, 4, 1);
					endDate = new DateTime(selectedYear, 6, 30);
					break;
				case "3-й квартал":
					startDate = new DateTime(selectedYear, 7, 1);
					endDate = new DateTime(selectedYear, 9, 30);
					break;
				case "4-й квартал":
					startDate = new DateTime(selectedYear, 10, 1);
					endDate = new DateTime(selectedYear, 12, 31);
					break;
				case "Год":
					startDate = new DateTime(selectedYear, 1, 1);
					endDate = new DateTime(selectedYear, 12, 31);
					break;
				default:
					MessageBox.Show("Выберите действительный временной промежуток.");
					return;
			}

			string query = @"
    SELECT n.Device_Name, 
           n.Types_TO_Name, 
           d.Inventory_Number, 
           n.Users_FIO, 
           wl.Norm_Hour,
           n.Types_TO_Work_List,
           d.Device_Type
    FROM Naryad n
    JOIN Devices d ON n.Device_Name = d.Name_Device
    JOIN Work_List wl ON n.Types_TO_Work_List LIKE '%' + wl.Work_List + '%'
                     AND d.Device_Type = wl.Device_Type
    WHERE n.Date_TO >= @startDate 
      AND n.Date_TO <= @endDate";

			var dbData = new List<(string DeviceName, string TOType, string InventoryNumber, string UsersFIO, string NormHour, string TypesToWorkList)>();

			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				try
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@startDate", startDate);
						command.Parameters.AddWithValue("@endDate", endDate);

						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								var deviceName = reader.IsDBNull(0) ? " " : reader.GetString(0);
								var toType = reader.IsDBNull(1) ? " " : reader.GetString(1);
								var inventoryNumber = reader.IsDBNull(2) ? " " : reader.GetString(2);
								var usersFIO = reader.IsDBNull(3) ? " " : reader.GetString(3);
								var normHour = reader.IsDBNull(4) ? "оооооо" : reader.GetString(4);
								var typesToWorkList = reader.IsDBNull(5) ? " " : reader.GetString(5);
								typesToWorkList = CleanTypesToWorkList(typesToWorkList);

								dbData.Add((deviceName, toType, inventoryNumber, usersFIO, normHour, typesToWorkList));
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Ошибка подключения к БД: {ex.Message}");
					return;
				}
			}

			// Получение списка типов работ и их норм часов из базы данных
			var workListNormHours = GetWorkListNormHours();

			// Создание Excel-отчета
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("Отчет");

				worksheet.Cells["A1:G1"].Merge = true;
				worksheet.Cells["A1"].Value = $"ОТЧЕТ ТО и Р\nтехнологического оборудования на {period} {year} г.";
				worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				worksheet.Cells["A1"].Style.WrapText = true;
				worksheet.Row(1).Height = 60;
				worksheet.Cells["A1"].Style.Font.Bold = true;

				string[] headers = { "№ п/п", "Тип, модель оборудования", "Инв. №", "Вид ремонта (ТО, ТР)", "н-часы", "Исполнители", "Начальник отдела" };
				for (int i = 0; i < headers.Length; i++)
				{
					worksheet.Cells[2, i + 1].Value = headers[i];
					worksheet.Cells[2, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
					worksheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
					worksheet.Cells[2, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
					worksheet.Cells[2, i + 1].Style.Font.Bold = true;
				}

				int startRow = 3;
				for (int row = 0; row < dbData.Count; row++)
				{
					// Суммируем значения Norm_Hour для каждой строки
					var normHour = CalculateNormHour(dbData[row].TypesToWorkList, workListNormHours);

					worksheet.Cells[startRow + row, 1].Value = row + 1;
					worksheet.Cells[startRow + row, 2].Value = dbData[row].DeviceName;
					worksheet.Cells[startRow + row, 3].Value = dbData[row].InventoryNumber;
					worksheet.Cells[startRow + row, 4].Value = dbData[row].TOType;
					worksheet.Cells[startRow + row, 5].Value = normHour;
					worksheet.Cells[startRow + row, 6].Value = dbData[row].UsersFIO;

					for (int col = 1; col <= headers.Length; col++)
					{
						worksheet.Cells[startRow + row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
						worksheet.Cells[startRow + row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
						worksheet.Cells[startRow + row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
					}
				}

				worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

				var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				var filePath = System.IO.Path.Combine(desktopPath, $"Отчет_{period}_{year}.xlsx");
				package.SaveAs(new FileInfo(filePath));
				MessageBox.Show($"Отчет создан: {filePath}");
			}
			this.Close();
		}

		// Метод для получения данных о нормированных часах для каждого типа работы
		private Dictionary<string, string> GetWorkListNormHours()
		{
			var workListNormHours = new Dictionary<string, string>();

			string query = "SELECT Work_List, Norm_Hour FROM Work_List";

			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				try
				{
					connection.Open();
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						using (SqlDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								var workList = reader.IsDBNull(0) ? "" : reader.GetString(0);
								var normHour = reader.IsDBNull(1) ? "ооооо" : reader.GetString(1);

								if (!string.IsNullOrEmpty(workList) && !workListNormHours.ContainsKey(workList))
								{
									workListNormHours[workList] = normHour;
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"Ошибка получения данных по типам работ: {ex.Message}");
				}
			}

			return workListNormHours;
		}

		// Метод для подсчета нормированных часов по данным из Work_List
		private string CalculateNormHour(string typesToWorkList, Dictionary<string, string> workListNormHours)
		{
			var items = typesToWorkList.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

			int totalNormHour = 0;
			foreach (var item in items)
			{
				string trimmedItem = item.Trim();

				// Проверяем, существует ли тип работы в словаре и добавляем соответствующие нормированные часы
				if (workListNormHours.ContainsKey(trimmedItem))
				{
					// Конкатенируем строки Norm_Hour
					totalNormHour += int.Parse(workListNormHours[trimmedItem]);
				}
			}

			return totalNormHour.ToString(); // Возвращаем суммированное значение как строку
		}

		// Метод для очистки строки Types_TO_Work_List, оставляя числовые индикаторы
		private string CleanTypesToWorkList(string input)
		{
			// Разделяем строку по числовым индикаторам (например, 1., 2., 3.)
			var regex = new Regex(@"(\d+\. )");
			var matches = regex.Split(input); // Разбиваем строку по числовым индикаторам

			// Убираем пустые строки и возвращаем разделенные элементы с их индикаторами
			return string.Join("\n", matches.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray());
		}




























		//private void CreateReportButton_Click(object sender, RoutedEventArgs e)
		//{
		//	var period = ((ComboBoxItem)PeriodComboBox.SelectedItem)?.Content.ToString();
		//	var year = ((ComboBoxItem)YearComboBox.SelectedItem)?.Content.ToString();

		//	if (string.IsNullOrEmpty(period) || string.IsNullOrEmpty(year))
		//	{
		//		MessageBox.Show("Пожалуйста, выберите период и год.");
		//		return;
		//	}

		//	DateTime startDate, endDate;
		//	int selectedYear = int.Parse(year);

		//	// Установка диапазона дат
		//	switch (period)
		//	{
		//		case "Январь":
		//			startDate = new DateTime(selectedYear, 1, 1);
		//			endDate = new DateTime(selectedYear, 1, 31);
		//			break;
		//		case "Февраль":
		//			startDate = new DateTime(selectedYear, 2, 1);
		//			endDate = new DateTime(selectedYear, 2, DateTime.DaysInMonth(selectedYear, 2));
		//			break;
		//		case "Март":
		//			startDate = new DateTime(selectedYear, 3, 1);
		//			endDate = new DateTime(selectedYear, 3, 31);
		//			break;
		//		case "Апрель":
		//			startDate = new DateTime(selectedYear, 4, 1);
		//			endDate = new DateTime(selectedYear, 4, 30);
		//			break;
		//		case "Май":
		//			startDate = new DateTime(selectedYear, 5, 1);
		//			endDate = new DateTime(selectedYear, 5, 31);
		//			break;
		//		case "Июнь":
		//			startDate = new DateTime(selectedYear, 6, 1);
		//			endDate = new DateTime(selectedYear, 6, 30);
		//			break;
		//		case "Июль":
		//			startDate = new DateTime(selectedYear, 7, 1);
		//			endDate = new DateTime(selectedYear, 7, 31);
		//			break;
		//		case "Август":
		//			startDate = new DateTime(selectedYear, 8, 1);
		//			endDate = new DateTime(selectedYear, 8, 31);
		//			break;
		//		case "Сентябрь":
		//			startDate = new DateTime(selectedYear, 9, 1);
		//			endDate = new DateTime(selectedYear, 9, 30);
		//			break;
		//		case "Октябрь":
		//			startDate = new DateTime(selectedYear, 10, 1);
		//			endDate = new DateTime(selectedYear, 10, 31);
		//			break;
		//		case "Ноябрь":
		//			startDate = new DateTime(selectedYear, 11, 1);
		//			endDate = new DateTime(selectedYear, 11, 30);
		//			break;
		//		case "Декабрь":
		//			startDate = new DateTime(selectedYear, 12, 1);
		//			endDate = new DateTime(selectedYear, 12, 31);
		//			break;
		//		case "1-й квартал":
		//			startDate = new DateTime(selectedYear, 1, 1);
		//			endDate = new DateTime(selectedYear, 3, 31);
		//			break;
		//		case "2-й квартал":
		//			startDate = new DateTime(selectedYear, 4, 1);
		//			endDate = new DateTime(selectedYear, 6, 30);
		//			break;
		//		case "3-й квартал":
		//			startDate = new DateTime(selectedYear, 7, 1);
		//			endDate = new DateTime(selectedYear, 9, 30);
		//			break;
		//		case "4-й квартал":
		//			startDate = new DateTime(selectedYear, 10, 1);
		//			endDate = new DateTime(selectedYear, 12, 31);
		//			break;
		//		case "Год":
		//			startDate = new DateTime(selectedYear, 1, 1);
		//			endDate = new DateTime(selectedYear, 12, 31);
		//			break;
		//		default:
		//			MessageBox.Show("Выберите действительный временной промежуток.");
		//			return;
		//	}

		//	string query = @"
		//      SELECT n.Device_Name, 
		//             n.Types_TO_Name, 
		//             d.Inventory_Number, 
		//             n.Users_FIO, 
		//             wl.Norm_Hour
		//      FROM Naryad n
		//      JOIN Devices d ON n.Device_Name = d.Name_Device
		//      JOIN Work_List wl ON n.Types_TO_Work_List = wl.Work_List
		//                       AND d.Device_Type = wl.Device_Type
		//      WHERE n.Date_TO >= @startDate 
		//        AND n.Date_TO <= @endDate";

		//	var dbData = new List<(string DeviceName, string TOType, string InventoryNumber, string UsersFIO, string NormHour)>();

		//	using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//	{
		//		try
		//		{
		//			connection.Open();
		//			using (SqlCommand command = new SqlCommand(query, connection))
		//			{
		//				command.Parameters.AddWithValue("@startDate", startDate);
		//				command.Parameters.AddWithValue("@endDate", endDate);

		//				using (SqlDataReader reader = command.ExecuteReader())
		//				{
		//					while (reader.Read())
		//					{
		//						var deviceName = reader.IsDBNull(0) ? " " : reader.GetString(0);
		//						var toType = reader.IsDBNull(1) ? " " : reader.GetString(1);
		//						var inventoryNumber = reader.IsDBNull(2) ? " " : reader.GetString(2);
		//						var usersFIO = reader.IsDBNull(3) ? " " : reader.GetString(3);

		//						// Обрабатываем Norm_Hour как строку
		//						var normHour = reader.IsDBNull(4) ? "0" : reader.GetString(4);  // Преобразуем в строку

		//						dbData.Add((deviceName, toType, inventoryNumber, usersFIO, normHour));
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show($"Ошибка подключения к БД: {ex.Message}");
		//			return;
		//		}
		//	}

		//	// Создание Excel-отчета
		//	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
		//	using (var package = new ExcelPackage())
		//	{
		//		var worksheet = package.Workbook.Worksheets.Add("Отчет");

		//		worksheet.Cells["A1:G1"].Merge = true;
		//		worksheet.Cells["A1"].Value = $"ОТЧЕТ ТО и Р\nтехнологического оборудования на {period} {year} г.";
		//		worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//		worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//		worksheet.Cells["A1"].Style.WrapText = true;
		//		worksheet.Row(1).Height = 60;
		//		worksheet.Cells["A1"].Style.Font.Bold = true;

		//		string[] headers = { "№ п/п", "Тип, модель оборудования", "Инв. №", "Вид ремонта (ТО, ТР)", "н-часы", "Исполнители", "Начальник отдела" };
		//		for (int i = 0; i < headers.Length; i++)
		//		{
		//			worksheet.Cells[2, i + 1].Value = headers[i];
		//			worksheet.Cells[2, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//			worksheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.Font.Bold = true;
		//		}

		//		int startRow = 3;
		//		for (int row = 0; row < dbData.Count; row++)
		//		{
		//			worksheet.Cells[startRow + row, 1].Value = row + 1;
		//			worksheet.Cells[startRow + row, 2].Value = dbData[row].DeviceName;
		//			worksheet.Cells[startRow + row, 3].Value = dbData[row].InventoryNumber;
		//			worksheet.Cells[startRow + row, 4].Value = dbData[row].TOType;
		//			worksheet.Cells[startRow + row, 5].Value = dbData[row].NormHour;  // normHour теперь строка
		//			worksheet.Cells[startRow + row, 6].Value = dbData[row].UsersFIO;

		//			for (int col = 1; col <= headers.Length; col++)
		//			{
		//				worksheet.Cells[startRow + row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//				worksheet.Cells[startRow + row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
		//				worksheet.Cells[startRow + row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			}
		//		}

		//		worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

		//		var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//		var filePath = System.IO.Path.Combine(desktopPath, $"Отчет_{period}_{year}.xlsx");
		//		package.SaveAs(new FileInfo(filePath));
		//		MessageBox.Show($"Отчет создан: {filePath}");
		//	}
		//	this.Close();
		//}





































		//private void CreateReportButton_Click(object sender, RoutedEventArgs e)
		//{
		//	var period = ((ComboBoxItem)PeriodComboBox.SelectedItem)?.Content.ToString();
		//	var year = ((ComboBoxItem)YearComboBox.SelectedItem)?.Content.ToString();

		//	if (string.IsNullOrEmpty(period) || string.IsNullOrEmpty(year))
		//	{
		//		MessageBox.Show("Пожалуйста, выберите период и год.");
		//		return;
		//	}

		//	DateTime startDate, endDate;
		//	int selectedYear = int.Parse(year);

		//	// Установка диапазона дат
		//	switch (period)
		//	{
		//		case "Январь":
		//			startDate = new DateTime(selectedYear, 1, 1);
		//			endDate = new DateTime(selectedYear, 1, 31);
		//			break;
		//		case "Февраль":
		//			startDate = new DateTime(selectedYear, 2, 1);
		//			endDate = new DateTime(selectedYear, 2, DateTime.DaysInMonth(selectedYear, 2));
		//			break;
		//		case "Март":
		//			startDate = new DateTime(selectedYear, 3, 1);
		//			endDate = new DateTime(selectedYear, 3, 31);
		//			break;
		//		case "Апрель":
		//			startDate = new DateTime(selectedYear, 4, 1);
		//			endDate = new DateTime(selectedYear, 4, 30);
		//			break;
		//		case "Май":
		//			startDate = new DateTime(selectedYear, 5, 1);
		//			endDate = new DateTime(selectedYear, 5, 31);
		//			break;
		//		case "Июнь":
		//			startDate = new DateTime(selectedYear, 6, 1);
		//			endDate = new DateTime(selectedYear, 6, 30);
		//			break;
		//		case "Июль":
		//			startDate = new DateTime(selectedYear, 7, 1);
		//			endDate = new DateTime(selectedYear, 7, 31);
		//			break;
		//		case "Август":
		//			startDate = new DateTime(selectedYear, 8, 1);
		//			endDate = new DateTime(selectedYear, 8, 31);
		//			break;
		//		case "Сентябрь":
		//			startDate = new DateTime(selectedYear, 9, 1);
		//			endDate = new DateTime(selectedYear, 9, 30);
		//			break;
		//		case "Октябрь":
		//			startDate = new DateTime(selectedYear, 10, 1);
		//			endDate = new DateTime(selectedYear, 10, 31);
		//			break;
		//		case "Ноябрь":
		//			startDate = new DateTime(selectedYear, 11, 1);
		//			endDate = new DateTime(selectedYear, 11, 30);
		//			break;
		//		case "Декабрь":
		//			startDate = new DateTime(selectedYear, 12, 1);
		//			endDate = new DateTime(selectedYear, 12, 31);
		//			break;
		//		case "1-й квартал":
		//			startDate = new DateTime(selectedYear, 1, 1);
		//			endDate = new DateTime(selectedYear, 3, 31);
		//			break;
		//		case "2-й квартал":
		//			startDate = new DateTime(selectedYear, 4, 1);
		//			endDate = new DateTime(selectedYear, 6, 30);
		//			break;
		//		case "3-й квартал":
		//			startDate = new DateTime(selectedYear, 7, 1);
		//			endDate = new DateTime(selectedYear, 9, 30);
		//			break;
		//		case "4-й квартал":
		//			startDate = new DateTime(selectedYear, 10, 1);
		//			endDate = new DateTime(selectedYear, 12, 31);
		//			break;
		//		case "Год":
		//			startDate = new DateTime(selectedYear, 1, 1);
		//			endDate = new DateTime(selectedYear, 12, 31);
		//			break;
		//		default:
		//			MessageBox.Show("Выберите действительный временной промежуток.");
		//			return;
		//	}

		//	string query = @"SELECT n.Device_Name, n.Types_TO_Name, d.Inventory_Number, n.Users_FIO
		//                   FROM Naryad n
		//                   JOIN Devices d ON n.Device_Name = d.Name_Device
		//                   WHERE n.Date_TO >= @startDate AND n.Date_TO <= @endDate";

		//	var dbData = new List<(string DeviceName, string TOType, string InventoryNumber, string UsersFIO)>();

		//	using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//	{
		//		try
		//		{
		//			connection.Open();
		//			using (SqlCommand command = new SqlCommand(query, connection))
		//			{
		//				command.Parameters.AddWithValue("@startDate", startDate);
		//				command.Parameters.AddWithValue("@endDate", endDate);

		//				using (SqlDataReader reader = command.ExecuteReader())
		//				{
		//					while (reader.Read())
		//					{
		//						var deviceName = reader.IsDBNull(0) ? " " : reader.GetString(0);
		//						var toType = reader.IsDBNull(1) ? " " : reader.GetString(1);
		//						var inventoryNumber = reader.IsDBNull(2) ? " " : reader.GetString(2);
		//						var usersFIO = reader.IsDBNull(3) ? " " : reader.GetString(3);
		//						dbData.Add((deviceName, toType, inventoryNumber, usersFIO));
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show($"Ошибка подключения к БД: {ex.Message}");
		//			return;
		//		}
		//	}

		//	// Создание Excel-отчета
		//	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
		//	using (var package = new ExcelPackage())
		//	{
		//		var worksheet = package.Workbook.Worksheets.Add("Отчет");

		//		worksheet.Cells["A1:G1"].Merge = true;
		//		worksheet.Cells["A1"].Value = $"ОТЧЕТ ТО и Р\nтехнологического оборудования на {period} {year} г.";
		//		worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//		worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//		worksheet.Cells["A1"].Style.WrapText = true;
		//		worksheet.Row(1).Height = 60;
		//		worksheet.Cells["A1"].Style.Font.Bold = true;

		//		string[] headers = { "№ п/п", "Тип, модель оборудования", "Инв. №", "Вид ремонта (ТО, ТР)", "н-часы", "Исполнители", "Начальник отдела" };
		//		for (int i = 0; i < headers.Length; i++)
		//		{
		//			worksheet.Cells[2, i + 1].Value = headers[i];
		//			worksheet.Cells[2, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//			worksheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.Font.Bold = true;
		//		}

		//		int startRow = 3;
		//		for (int row = 0; row < dbData.Count; row++)
		//		{
		//			worksheet.Cells[startRow + row, 1].Value = row + 1;
		//			worksheet.Cells[startRow + row, 2].Value = dbData[row].DeviceName;
		//			worksheet.Cells[startRow + row, 3].Value = dbData[row].InventoryNumber;
		//			worksheet.Cells[startRow + row, 4].Value = dbData[row].TOType;
		//			worksheet.Cells[startRow + row, 6].Value = dbData[row].UsersFIO;

		//			for (int col = 1; col <= headers.Length; col++)
		//			{
		//				worksheet.Cells[startRow + row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//				worksheet.Cells[startRow + row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
		//				worksheet.Cells[startRow + row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			}
		//		}

		//		worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

		//		var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//		var filePath = System.IO.Path.Combine(desktopPath, $"Отчет_{period}_{year}.xlsx");
		//		package.SaveAs(new FileInfo(filePath));
		//		MessageBox.Show($"Отчет создан: {filePath}");
		//	}
		//	this.Close();
		//}







































		//private void CreateReportButton_Click(object sender, RoutedEventArgs e)
		//{
		//	var period = ((ComboBoxItem)PeriodComboBox.SelectedItem)?.Content.ToString();
		//	var year = ((ComboBoxItem)YearComboBox.SelectedItem)?.Content.ToString();

		//	if (string.IsNullOrEmpty(period) || string.IsNullOrEmpty(year))
		//	{
		//		MessageBox.Show("Пожалуйста, выберите период и год.");
		//		return;
		//	}

		//	// Переменные для дат начала и окончания
		//	DateTime startDate, endDate;

		//	// Установка начала и конца диапазона на основе выбранного периода
		//	int selectedYear = int.Parse(year);
		//	switch (period)
		//	{
		//		case "Январь":
		//			startDate = new DateTime(selectedYear, 1, 1);
		//			endDate = new DateTime(selectedYear, 1, 31);
		//			break;
		//		case "Февраль":
		//			startDate = new DateTime(selectedYear, 2, 1);
		//			endDate = new DateTime(selectedYear, 2, DateTime.DaysInMonth(selectedYear, 2));
		//			break;
		//		case "Март":
		//			startDate = new DateTime(selectedYear, 3, 1);
		//			endDate = new DateTime(selectedYear, 3, 31);
		//			break;
		//		case "Апрель":
		//			startDate = new DateTime(selectedYear, 4, 1);
		//			endDate = new DateTime(selectedYear, 4, 30);
		//			break;
		//		case "Май":
		//			startDate = new DateTime(selectedYear, 5, 1);
		//			endDate = new DateTime(selectedYear, 5, 31);
		//			break;
		//		case "Июнь":
		//			startDate = new DateTime(selectedYear, 6, 1);
		//			endDate = new DateTime(selectedYear, 6, 30);
		//			break;
		//		case "Июль":
		//			startDate = new DateTime(selectedYear, 7, 1);
		//			endDate = new DateTime(selectedYear, 7, 31);
		//			break;
		//		case "Август":
		//			startDate = new DateTime(selectedYear, 8, 1);
		//			endDate = new DateTime(selectedYear, 8, 31);
		//			break;
		//		case "Сентябрь":
		//			startDate = new DateTime(selectedYear, 9, 1);
		//			endDate = new DateTime(selectedYear, 9, 30);
		//			break;
		//		case "Октябрь":
		//			startDate = new DateTime(selectedYear, 10, 1);
		//			endDate = new DateTime(selectedYear, 10, 31);
		//			break;
		//		case "Ноябрь":
		//			startDate = new DateTime(selectedYear, 11, 1);
		//			endDate = new DateTime(selectedYear, 11, 30);
		//			break;
		//		case "Декабрь":
		//			startDate = new DateTime(selectedYear, 12, 1);
		//			endDate = new DateTime(selectedYear, 12, 31);
		//			break;
		//		case "1-й квартал":
		//			startDate = new DateTime(selectedYear, 1, 1);
		//			endDate = new DateTime(selectedYear, 3, 31);
		//			break;
		//		case "2-й квартал":
		//			startDate = new DateTime(selectedYear, 4, 1);
		//			endDate = new DateTime(selectedYear, 6, 30);
		//			break;
		//		case "3-й квартал":
		//			startDate = new DateTime(selectedYear, 7, 1);
		//			endDate = new DateTime(selectedYear, 9, 30);
		//			break;
		//		case "4-й квартал":
		//			startDate = new DateTime(selectedYear, 10, 1);
		//			endDate = new DateTime(selectedYear, 12, 31);
		//			break;
		//		case "Год":
		//			startDate = new DateTime(selectedYear, 1, 1);
		//			endDate = new DateTime(selectedYear, 12, 31);
		//			break;
		//		default:
		//			MessageBox.Show("Выберите действительный временной промежуток.");
		//			return;
		//	}

		//	// Запрос к базе данных с учетом диапазона дат
		//	string query = @"SELECT n.Device_Name, n.Types_TO_Name, d.Inventory_Number
		//                   FROM Naryad n
		//                   JOIN Devices d ON n.Device_Name = d.Name_Device
		//                   WHERE n.Date_TO >= @startDate AND n.Date_TO <= @endDate";

		//	var dbData = new List<(string DeviceName, string TOType, string InventoryNumber)>();

		//	using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//	{
		//		try
		//		{
		//			connection.Open();
		//			using (SqlCommand command = new SqlCommand(query, connection))
		//			{
		//				command.Parameters.AddWithValue("@startDate", startDate);
		//				command.Parameters.AddWithValue("@endDate", endDate);

		//				using (SqlDataReader reader = command.ExecuteReader())
		//				{
		//					while (reader.Read())
		//					{
		//						var deviceName = reader.IsDBNull(0) ? " " : reader.GetString(0);
		//						var toType = reader.IsDBNull(1) ? " " : reader.GetString(1);
		//						var inventoryNumber = reader.IsDBNull(2) ? " " : reader.GetString(2);
		//						dbData.Add((deviceName, toType, inventoryNumber));
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show($"Ошибка подключения к БД: {ex.Message}");
		//			return;
		//		}
		//	}

		//	// Создание отчета Excel
		//	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
		//	using (var package = new ExcelPackage())
		//	{
		//		var worksheet = package.Workbook.Worksheets.Add("Отчет");

		//		worksheet.Cells["A1:G1"].Merge = true;
		//		worksheet.Cells["A1"].Value = $"ОТЧЕТ ТО и Р\nтехнологического оборудования производства печатных плат\nна {period} {year} г.";
		//		worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//		worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//		worksheet.Cells["A1"].Style.WrapText = true;
		//		worksheet.Row(1).Height = 60;
		//		worksheet.Cells["A1"].Style.Font.Bold = true;

		//		string[] headers = { "№ п/п", "Тип, модель оборудования", "Инв. №", "Вид ремонта (ТО, ТР)", "н-часы", "Исполнители", "Начальник отдела" };
		//		for (int i = 0; i < headers.Length; i++)
		//		{
		//			worksheet.Cells[2, i + 1].Value = headers[i];
		//			worksheet.Cells[2, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//			worksheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.Font.Bold = true;
		//		}

		//		int startRow = 3;
		//		for (int row = 0; row < dbData.Count; row++)
		//		{
		//			worksheet.Cells[startRow + row, 1].Value = row + 1;
		//			worksheet.Cells[startRow + row, 2].Value = dbData[row].DeviceName;
		//			worksheet.Cells[startRow + row, 3].Value = dbData[row].InventoryNumber;
		//			worksheet.Cells[startRow + row, 4].Value = dbData[row].TOType;

		//			for (int col = 1; col <= headers.Length; col++)
		//			{
		//				worksheet.Cells[startRow + row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//				worksheet.Cells[startRow + row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
		//				worksheet.Cells[startRow + row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			}
		//		}

		//		worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

		//		var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//		var filePath = System.IO.Path.Combine(desktopPath, $"Отчет_{period}_{year}.xlsx");
		//		package.SaveAs(new FileInfo(filePath));
		//		MessageBox.Show($"Отчет создан: {filePath}");
		//	}
		//	this.Close();
		//}






































		//private void CreateReportButton_Click(object sender, RoutedEventArgs e)
		//{
		//	var period = ((ComboBoxItem)PeriodComboBox.SelectedItem)?.Content.ToString();
		//	var year = ((ComboBoxItem)YearComboBox.SelectedItem)?.Content.ToString();

		//	if (string.IsNullOrEmpty(period) || string.IsNullOrEmpty(year))
		//	{
		//		MessageBox.Show("Пожалуйста, выберите период и год.");
		//		return;
		//	}



		//	// Обновленный запрос к базе данных для получения Device_Name, Types_TO_Name и Inventory_Number
		//	string query = @"
		//      SELECT n.Device_Name, n.Types_TO_Name, d.Inventory_Number
		//      FROM Naryad n
		//      JOIN Devices d ON n.Device_Name = d.Name_Device";

		//	var dbData = new List<(string DeviceName, string TOType, string InventoryNumber)>();

		//	using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//	{
		//		try
		//		{
		//			connection.Open();
		//			using (SqlCommand command = new SqlCommand(query, connection))
		//			{
		//				using (SqlDataReader reader = command.ExecuteReader())
		//				{
		//					while (reader.Read())
		//					{
		//						var deviceName = reader.IsDBNull(0) ? " " : reader.GetString(0);
		//						var toType = reader.IsDBNull(1) ? " " : reader.GetString(1);
		//						var inventoryNumber = reader.IsDBNull(2) ? " " : reader.GetString(2);
		//						dbData.Add((deviceName, toType, inventoryNumber));
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show($"Ошибка подключения к БД: {ex.Message}");
		//			return;
		//		}
		//	}

		//	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

		//	using (var package = new ExcelPackage())
		//	{
		//		var worksheet = package.Workbook.Worksheets.Add("Отчет");
		//		worksheet.Cells["A1:G1"].Merge = true;
		//		worksheet.Cells["A1"].Value = $"ОТЧЕТ ТО и Р\nтехнологического оборудования производства печатных плат\nна {period} {year} г.";
		//		worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//		worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//		worksheet.Cells["A1"].Style.WrapText = true;
		//		worksheet.Row(1).Height = 60;
		//		worksheet.Cells["A1"].Style.Font.Bold = true;

		//		string[] headers = { "№ п/п", "Тип, модель оборудования", "Инв. №", "Вид ремонта (ТО, ТР)", "н-часы", "Исполнители", "Начальник отдела" };
		//		for (int i = 0; i < headers.Length; i++)
		//		{
		//			worksheet.Cells[2, i + 1].Value = headers[i];
		//			worksheet.Cells[2, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//			worksheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.Font.Bold = true;
		//		}

		//		int startRow = 3;
		//		for (int row = 0; row < dbData.Count; row++)
		//		{
		//			worksheet.Cells[startRow + row, 1].Value = row + 1;
		//			worksheet.Cells[startRow + row, 2].Value = dbData[row].DeviceName;
		//			worksheet.Cells[startRow + row, 3].Value = dbData[row].InventoryNumber;
		//			worksheet.Cells[startRow + row, 4].Value = dbData[row].TOType;

		//			for (int col = 1; col <= headers.Length; col++)
		//			{
		//				worksheet.Cells[startRow + row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//				worksheet.Cells[startRow + row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

		//				worksheet.Cells[startRow + row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			}
		//		}

		//		worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

		//		var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//		var filePath = System.IO.Path.Combine(desktopPath, $"Отчет_{period}_{year}.xlsx");
		//		package.SaveAs(new FileInfo(filePath));

		//		MessageBox.Show($"Отчет создан: {filePath}");
		//	}

		//	this.Close();
		//}


















































		//private void CreateReportButton_Click(object sender, RoutedEventArgs e)
		//{
		//	var period = ((ComboBoxItem)PeriodComboBox.SelectedItem)?.Content.ToString();
		//	var year = ((ComboBoxItem)YearComboBox.SelectedItem)?.Content.ToString();

		//	if (string.IsNullOrEmpty(period) || string.IsNullOrEmpty(year))
		//	{
		//		MessageBox.Show("Пожалуйста, выберите период и год.");
		//		return;
		//	}



		//	// Обновленный запрос к базе данных для получения Device_Name, Types_TO_Name и Inventory_Number
		//	string query = @"
		//      SELECT n.Device_Name, n.Types_TO_Name, d.Inventory_Number
		//      FROM Naryad n
		//      JOIN Devices d ON n.Device_Name = d.Name_Device";

		//	var dbData = new List<(string DeviceName, string TOType, string InventoryNumber)>();

		//	using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//	{
		//		try
		//		{
		//			connection.Open();
		//			using (SqlCommand command = new SqlCommand(query, connection))
		//			{
		//				using (SqlDataReader reader = command.ExecuteReader())
		//				{
		//					while (reader.Read())
		//					{
		//						var deviceName = reader.GetString(0);
		//						var toType = reader.GetString(1);
		//						var inventoryNumber = reader.GetString(2);
		//						dbData.Add((deviceName, toType, inventoryNumber));
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show($"Ошибка подключения к БД: {ex.Message}");
		//			return;
		//		}
		//	}

		//	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

		//	using (var package = new ExcelPackage())
		//	{
		//		var worksheet = package.Workbook.Worksheets.Add("Отчет");
		//		worksheet.Cells["A1:H1"].Merge = true;
		//		worksheet.Cells["A1"].Value = $"ОТЧЕТ ТО и Р\nтехнологического оборудования производства печатных плат\nна {period} {year} г.";
		//		worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//		worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//		worksheet.Cells["A1"].Style.WrapText = true;
		//		worksheet.Row(1).Height = 60;
		//		worksheet.Cells["A1"].Style.Font.Bold = true;

		//		string[] headers = { "№ п/п", "Тип, модель оборудования", "Инв. №", "Вид ремонта (ТО, ТР)", "План (н-часы)", "Отчет (н-часы)", "Исполнители", "Начальник отдела" };
		//		for (int i = 0; i < headers.Length; i++)
		//		{
		//			worksheet.Cells[2, i + 1].Value = headers[i];
		//			worksheet.Cells[2, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//			worksheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.Font.Bold = true;
		//		}

		//		int startRow = 3;
		//		for (int row = 0; row < dbData.Count; row++)
		//		{
		//			worksheet.Cells[startRow + row, 1].Value = row + 1;
		//			worksheet.Cells[startRow + row, 2].Value = dbData[row].DeviceName;
		//			worksheet.Cells[startRow + row, 3].Value = dbData[row].InventoryNumber;
		//			worksheet.Cells[startRow + row, 4].Value = dbData[row].TOType;

		//			for (int col = 1; col <= headers.Length; col++)
		//			{
		//				worksheet.Cells[startRow + row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//				worksheet.Cells[startRow + row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

		//				worksheet.Cells[startRow + row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			}
		//		}

		//		worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

		//		var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//		var filePath = System.IO.Path.Combine(desktopPath, $"Отчет_{period}_{year}.xlsx");
		//		package.SaveAs(new FileInfo(filePath));

		//		MessageBox.Show($"Отчет создан: {filePath}");
		//	}

		//	this.Close();
		//}
















































		//private void CreateReportButton_Click(object sender, RoutedEventArgs e)
		//{
		//	// Получение выбранного периода и года
		//	var period = ((ComboBoxItem)PeriodComboBox.SelectedItem)?.Content.ToString();
		//	var year = ((ComboBoxItem)YearComboBox.SelectedItem)?.Content.ToString();

		//	// Проверка на пустые значения
		//	if (string.IsNullOrEmpty(period) || string.IsNullOrEmpty(year))
		//	{
		//		MessageBox.Show("Пожалуйста, выберите период и год.");
		//		return;
		//	}


		//	// Запрос к базе данных
		//	string query = "SELECT Device_Name, Types_TO_Name FROM Naryad";

		//	// Список для хранения данных
		//	var dbData = new List<(string DeviceName, string TOType)>();

		//	// Подключение к базе данных
		//	using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//	{
		//		try
		//		{
		//			connection.Open();
		//			using (SqlCommand command = new SqlCommand(query, connection))
		//			{
		//				using (SqlDataReader reader = command.ExecuteReader())
		//				{
		//					while (reader.Read())
		//					{
		//						// Получаем данные из столбцов Device_Name и Types_TO_Name
		//						var deviceName = reader.GetString(0);  // Индекс 0 для Device_Name
		//						var toType = reader.GetString(1);      // Индекс 1 для Types_TO_Name
		//						dbData.Add((deviceName, toType));
		//					}
		//				}
		//			}
		//		}
		//		catch (Exception ex)
		//		{
		//			MessageBox.Show($"Ошибка подключения к БД: {ex.Message}");
		//			return;
		//		}
		//	}

		//	// Установите контекст лицензии для ExcelPackage
		//	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

		//	// Создание Excel файла
		//	using (var package = new ExcelPackage())
		//	{
		//		var worksheet = package.Workbook.Worksheets.Add("Отчет");

		//		// Заголовок отчета
		//		worksheet.Cells["A1:H1"].Merge = true;
		//		worksheet.Cells["A1"].Value = $"ОТЧЕТ ТО и Р\nтехнологического оборудования производства печатных плат\nна {period} {year} г.";
		//		worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//		worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//		worksheet.Cells["A1"].Style.WrapText = true;
		//		worksheet.Row(1).Height = 60; // Высота строки заголовка
		//		worksheet.Cells["A1"].Style.Font.Bold = true;

		//		// Заголовки колонок
		//		string[] headers = { "№ п/п", "Тип, модель оборудования", "Инв. №", "Вид ремонта (ТО, ТР)", "План (н-часы)", "Отчет (н-часы)", "Исполнители", "Начальник отдела" };
		//		for (int i = 0; i < headers.Length; i++)
		//		{
		//			worksheet.Cells[2, i + 1].Value = headers[i];
		//			worksheet.Cells[2, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//			worksheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			worksheet.Cells[2, i + 1].Style.Font.Bold = true;
		//		}

		//		// Заполнение данных из базы данных
		//		int startRow = 3; // Данные начинаются с третьей строки
		//		for (int row = 0; row < dbData.Count; row++)
		//		{
		//			worksheet.Cells[startRow + row, 1].Value = row + 1;                                    // Порядковый номер

		//			worksheet.Cells[startRow + row, 2].Value = dbData[row].DeviceName;                    // Тип, модель оборудования
		//			worksheet.Cells[startRow + row, 4].Value = dbData[row].TOType;                        // Вид ремонта (ТО, ТР)

		//			// Форматирование ячеек
		//			for (int col = 1; col <= headers.Length; col++)
		//			{
		//				worksheet.Cells[startRow + row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//				worksheet.Cells[startRow + row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
		//				worksheet.Cells[startRow + row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			}
		//		}

		//		// Автоматическая ширина колонок
		//		worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

		//		// Сохранение файла на рабочий стол
		//		var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//		var filePath = System.IO.Path.Combine(desktopPath, $"Отчет_{period}_{year}.xlsx");
		//		package.SaveAs(new FileInfo(filePath));

		//		// Сообщение о завершении
		//		MessageBox.Show($"Отчет создан: {filePath}");
		//	}

		//	// Закрытие окна после создания отчета
		//	this.Close();
		//}





















		//private void CreateReportButton_Click(object sender, RoutedEventArgs e)
		//{
		//	var period = ((ComboBoxItem)PeriodComboBox.SelectedItem)?.Content.ToString();
		//	var year = ((ComboBoxItem)YearComboBox.SelectedItem)?.Content.ToString();

		//	if (string.IsNullOrEmpty(period) || string.IsNullOrEmpty(year))
		//	{
		//		MessageBox.Show("Пожалуйста, выберите период и год.");
		//		return;
		//	}

		//	// Установите контекст лицензии
		//	ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

		//	// Создание Excel файла
		//	using (var package = new ExcelPackage())
		//	{
		//		var worksheet = package.Workbook.Worksheets.Add("Отчет");

		//		// Заголовок отчета
		//		worksheet.Cells["A1:H1"].Merge = true; // Объединение ячеек
		//		worksheet.Cells["A1"].Value = $"ОТЧЕТ ТО и Р\nтехнологического оборудования производства печатных плат\nна {period} {year} г.";
		//		worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
		//		worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//		worksheet.Cells["A1"].Style.WrapText = true; // Текст переносится автоматически
		//		worksheet.Row(1).Height = 60; // Высота строки заголовка
		//		worksheet.Cells["A1"].Style.Font.Bold = true;

		//		// Заголовки колонок
		//		string[] headers = { "№ п/п", "Тип, модель оборудования", "Инв. №", "Вид ремонта (ТО, ТР)", "План (н-часы)", "Отчет (н-часы)", "Исполнители", "Начальник отдела" };
		//		for (int i = 0; i < headers.Length; i++)
		//		{
		//			worksheet.Cells[2, i + 1].Value = headers[i]; // Установка значения ячейки
		//			worksheet.Cells[2, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin); // Рамки по всему периметру ячейки
		//			worksheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Горизонтальное центрирование
		//			worksheet.Cells[2, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // Вертикальное центрирование
		//			//worksheet.Cells[2, i + 1].Style.WrapText = true; // Текст переносится автоматически
		//			worksheet.Cells[2, i + 1].Style.Font.Bold = true; // Жирный шрифт
		//		}

		//		// Автоматическая ширина колонок
		//		worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

		//		// Пример данных
		//		var data = new object[,]
		//		{
		//			{ 1, "Линия конвейерного оснащения, 1.LUDY", 1430292, "ТО1", 4.3, 4.3, "", "" },
		//			{ 2, "Перегрузчик, SIMATEC VS90", 1400749, "ТО1", 1.1, 1.1, "", "" },
		//			{ 3, "Линия запайки, ANTECH", 1440766, "ТО2", 46.7, 46.7, "", "" },
		//			{ 4, "Линия формирования фоторезиста...", 2020110, "ТО1", 3.2, 3.2, "", "" },
		//			{ 5, "Комбинация линии очистки ПМ...", 2020217, "ТО1", 5, 5, "", "" }
		//                  // Дополните данными по аналогии
		//              };

		//		// Заполнение данных
		//		int startRow = 3; // Данные начинаются с третьей строки
		//		for (int row = 0; row < data.GetLength(0); row++)
		//		{
		//			for (int col = 0; col < data.GetLength(1); col++)
		//			{
		//				worksheet.Cells[startRow + row, col + 1].Value = data[row, col];
		//				worksheet.Cells[startRow + row, col + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
		//				worksheet.Cells[startRow + row, col + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
		//				worksheet.Cells[startRow + row, col + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
		//			}
		//		}

		//		// Сохранение файла на рабочий стол
		//		var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		//		var filePath = System.IO.Path.Combine(desktopPath, $"Отчет_{period}_{year}.xlsx");
		//		package.SaveAs(new FileInfo(filePath));

		//		MessageBox.Show($"Отчет создан: {filePath}");
		//	}

		//		this.Close();
		//}
	}
} 
