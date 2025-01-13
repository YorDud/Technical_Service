using System;
using System.Collections.Generic;
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
using OfficeOpenXml;
using OfficeOpenXml.Style;


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

			// Установите контекст лицензии
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

			// Создание Excel файла
			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("Отчет");

				// Заголовок отчета
				worksheet.Cells["A1:H1"].Merge = true; // Объединение ячеек
				worksheet.Cells["A1"].Value = $"ОТЧЕТ ТО и Р\nтехнологического оборудования производства печатных плат\nна {period} {year} г.";
				worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
				worksheet.Cells["A1"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
				worksheet.Cells["A1"].Style.WrapText = true; // Текст переносится автоматически
				worksheet.Row(1).Height = 60; // Высота строки заголовка
				worksheet.Cells["A1"].Style.Font.Bold = true;

				// Заголовки колонок
				string[] headers = { "№ п/п", "Тип, модель оборудования", "Инв. №", "Вид ремонта (ТО, ТР)", "План (н-часы)", "Отчет (н-часы)", "Исполнители", "Начальник отдела" };
				for (int i = 0; i < headers.Length; i++)
				{
					worksheet.Cells[2, i + 1].Value = headers[i]; // Установка значения ячейки
					worksheet.Cells[2, i + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin); // Рамки по всему периметру ячейки
					worksheet.Cells[2, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center; // Горизонтальное центрирование
					worksheet.Cells[2, i + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center; // Вертикальное центрирование
					//worksheet.Cells[2, i + 1].Style.WrapText = true; // Текст переносится автоматически
					worksheet.Cells[2, i + 1].Style.Font.Bold = true; // Жирный шрифт
				}

				// Автоматическая ширина колонок
				worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

				// Пример данных
				var data = new object[,]
				{
					{ 1, "Линия конвейерного оснащения, 1.LUDY", 1430292, "ТО1", 4.3, 4.3, "", "" },
					{ 2, "Перегрузчик, SIMATEC VS90", 1400749, "ТО1", 1.1, 1.1, "", "" },
					{ 3, "Линия запайки, ANTECH", 1440766, "ТО2", 46.7, 46.7, "", "" },
					{ 4, "Линия формирования фоторезиста...", 2020110, "ТО1", 3.2, 3.2, "", "" },
					{ 5, "Комбинация линии очистки ПМ...", 2020217, "ТО1", 5, 5, "", "" }
                    // Дополните данными по аналогии
                };

				// Заполнение данных
				int startRow = 3; // Данные начинаются с третьей строки
				for (int row = 0; row < data.GetLength(0); row++)
				{
					for (int col = 0; col < data.GetLength(1); col++)
					{
						worksheet.Cells[startRow + row, col + 1].Value = data[row, col];
						worksheet.Cells[startRow + row, col + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
						worksheet.Cells[startRow + row, col + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
						worksheet.Cells[startRow + row, col + 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
					}
				}

				// Сохранение файла на рабочий стол
				var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
				var filePath = System.IO.Path.Combine(desktopPath, $"Отчет_{period}_{year}.xlsx");
				package.SaveAs(new FileInfo(filePath));

				MessageBox.Show($"Отчет создан: {filePath}");
			}

				this.Close();
		}
	}
}
