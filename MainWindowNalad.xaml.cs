﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;
using System.Globalization;
using System.Windows.Markup;
using MaterialDesignThemes.Wpf;
using MahApps.Metro.Controls.Dialogs;
using Button = System.Windows.Controls.Button;
using System.Windows.Controls.Primitives;
using DataGridCell = System.Windows.Controls.DataGridCell;
using System.ComponentModel;
using System.Web.UI.WebControls;
using WpfApp4.MiniWindows;
using Style = System.Windows.Style;
using System.IO;
using System.Timers;
using Timer = System.Timers.Timer;
using Path = System.Windows.Shapes.Path;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.Windows.Media.Media3D;
using System.Xml.Linq;
using System.Diagnostics;
using Dapper;
using MessageBox = System.Windows.MessageBox;






namespace WpfApp4
{
	/// <summary>
	/// Логика взаимодействия для MainWindowNalad.xaml
	/// </summary>
	public partial class MainWindowNalad : Window
	{


		public MainWindowNalad()
		{
			InitializeComponent();
			StartDatePicker.SelectedDate = DateTime.Now;
			EndDatePicker.SelectedDate = DateTime.Now.AddDays(7);       // Текущая дата + 7 дней


			LoadData_Monitor_Naryad();
			LoadData_Monitor_Crash();

			
		}





		private void SetVisibility(FrameworkElement visibleElement)
		{
			// Список всех элементов, которые нужно скрыть
			var elements = new List<FrameworkElement>
	{		
		MainContent,
		MonitorCrash
		
	};

			// Устанавливаем видимость для каждого элемента
			foreach (var element in elements)
			{
				element.Visibility = (element == visibleElement) ? Visibility.Visible : Visibility.Hidden;
			}
		}

		// Пример вызова метода
		//SetVisibility(ZapTO);




		

		



		public void LoadData_Monitor_Naryad()
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				DateTime? startDate = StartDatePicker.SelectedDate;
				DateTime? endDate = EndDatePicker.SelectedDate;

				string query = @"
            SELECT [ID], [Device_Name], [Types_TO_Name], [Types_TO_Work_List], [Users_FIO],
                   [Date_Start], [Date_End], [Status], [Sklad_Deteil_ID], [Sklad_Kolich],
                   [Documentation_Name_ID], [Date_TO], [Comment]
            FROM [Technical_Service].[dbo].[Naryad]
            WHERE ([Date_TO] BETWEEN @StartDate AND @EndDate)
              AND ([Status] IS NULL OR [Status] != 'Закрыт')";

				SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
				adapter.SelectCommand.Parameters.AddWithValue("@StartDate", (object)startDate ?? DBNull.Value);
				adapter.SelectCommand.Parameters.AddWithValue("@EndDate", (object)endDate ?? DBNull.Value);

				DataTable dataTable = new DataTable();
				connection.Open();
				adapter.Fill(dataTable);
				connection.Close();

				dataGridMonitorNaryad.ItemsSource = dataTable.DefaultView;

				// Применяем цветовые стили строк
				dataGridMonitorNaryad.UpdateLayout();
				foreach (DataRowView row in dataGridMonitorNaryad.ItemsSource)
				{
					string status = row["Status"] == DBNull.Value ? null : row["Status"].ToString().Trim();
					DataGridRow dataGridRow = dataGridMonitorNaryad.ItemContainerGenerator.ContainerFromItem(row) as DataGridRow;

					if (dataGridRow != null)
					{
						if (string.IsNullOrEmpty(status))
						{
							dataGridRow.Background = new SolidColorBrush(Colors.LightCoral);
						}
						else if (string.Equals(status, "В работе", StringComparison.OrdinalIgnoreCase))
						{
							dataGridRow.Background = new SolidColorBrush(Color.FromRgb(255, 255, 102));

						}
						else if (string.Equals(status, "Выполнено", StringComparison.OrdinalIgnoreCase))
						{
							dataGridRow.Background = new SolidColorBrush(Colors.LightGreen);
						}

					}
					dataGridMonitorNaryad.UpdateLayout();
				}
			}
		}
		


		private void StartDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			LoadData_Monitor_Naryad();
		}

		private void EndDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			LoadData_Monitor_Naryad();
		}
		private void dataGridMonitorNaryad_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			// Получаем текущую строку
			var row = e.Row.Item as DataRowView;
			if (row != null)
			{
				string status = row["Status"]?.ToString();

				// Если статус пустой или "В работе", устанавливаем светло-красный цвет
				if (string.IsNullOrEmpty(status))
				{
					e.Row.Background = new SolidColorBrush(Colors.LightCoral);
				}
				// Если статус "Выполнено", устанавливаем светло-зеленый цвет
				else if (status == "Выполнен")
				{
					e.Row.Background = new SolidColorBrush(Colors.LightGreen);
				}
				else if (status == "В работе")
				{
					e.Row.Background = new SolidColorBrush(Color.FromRgb(255, 255, 102));

				}

			}
		}



		public void LoadData_Monitor_Crash()
		{
			// Устанавливаем соединение с базой данных
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				string query = @"
            SELECT 
                [ID],
                [Date_Crash],
                [FIO_Tech],
                [Location],
                [Device],
                [Comment],
                [FIO_Nalad],
                [Date_Complete],
                [Status]
            FROM 
                [Technical_Service].[dbo].[Crash] 
            ORDER BY [Date_Crash] DESC";

				SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

				DataTable dataTable = new DataTable();

				try
				{
					connection.Open();
					adapter.Fill(dataTable);
				}
				finally
				{
					connection.Close();
				}

				// Привязываем данные к DataGrid
				dataGridMonitorCrash.ItemsSource = dataTable.DefaultView;
			}
		}

		// Окрашивание строк в зависимости от значения статуса
		private void dataGridMonitorCrash_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			// Получаем текущую строку
			var rowView = e.Row.Item as DataRowView;
			if (rowView != null)
			{
				// Получаем статус
				string status = rowView["Status"] == DBNull.Value ? null : rowView["Status"].ToString().Trim();

				// Устанавливаем цвет фона строки в зависимости от статуса
				if (string.Equals(status, "Поломка", StringComparison.OrdinalIgnoreCase))
				{
					e.Row.Background = new SolidColorBrush(Colors.LightCoral); // Красный
				}
				else if (string.Equals(status, "В работе", StringComparison.OrdinalIgnoreCase))
				{
					e.Row.Background = new SolidColorBrush(Color.FromRgb(255, 255, 102)); // Желтый
				}
				else if (string.Equals(status, "Выполнено", StringComparison.OrdinalIgnoreCase))
				{
					e.Row.Background = new SolidColorBrush(Colors.LightGreen); // Зеленый
				}
			}
		}








		private void ShowMonitoringCrash(object sender, RoutedEventArgs e)
		{
			LoadData_Monitor_Crash();
			SetVisibility(MonitorCrash);
		}

		

		private void ShowMonitoringTO(object sender, RoutedEventArgs e)
		{
			LoadData_Monitor_Naryad();
			SetVisibility(MainContent);

		}



		

		private void Load_Data_DataGrid()
		{
			WpfApp4.Lab_RezDataSet lab_RezDataSet = ((WpfApp4.Lab_RezDataSet)(this.FindResource("lab_RezDataSet")));
			// Загрузить данные в таблицу test_table. Можно изменить этот код как требуется.
			WpfApp4.Lab_RezDataSetTableAdapters.test_tableTableAdapter lab_RezDataSettest_tableTableAdapter = new WpfApp4.Lab_RezDataSetTableAdapters.test_tableTableAdapter();
			lab_RezDataSettest_tableTableAdapter.Fill(lab_RezDataSet.test_table);
			System.Windows.Data.CollectionViewSource test_tableViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("test_tableViewSource")));
			test_tableViewSource.View.MoveCurrentToFirst();
		}


		private void Window_Loaded(object sender, RoutedEventArgs e)
		{			
			LoadData_Monitor_Naryad();
			LoadData_Monitor_Crash();
					////test_tableDataGrid_Loaded(sender, e);
		}

		//private void LoadData()                                                                                                 //   LoadData   !!!!!!!!!!!!!!!!!!!!!!!!!!!
		//{
		//	string connectionString = WC.ConnectionString;
		//	string query = "SELECT [id], [ffff], [rrrr] FROM [test_table]";

		//	using (SqlConnection connection = new SqlConnection(connectionString))
		//	{
		//		SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
		//		DataTable dataTable = new DataTable();
		//		adapter.Fill(dataTable);
		//		test_tableDataGrid.ItemsSource = dataTable.DefaultView;
		//	}
		//}




		private void TypeDeviceCreatebtn_Click(object sender, RoutedEventArgs e)
		{
			
		}



		private void Deteil_TypesCreatebtn_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void CrashCreateClick(object sender, RoutedEventArgs e)
		{
			
		}





















		



		

		private void Window_Closed(object sender, EventArgs e)
		{
			System.Windows.Application.Current.Shutdown();
		}
		private void dataGridMonitorCrash_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (dataGridMonitorCrash.SelectedItem is DataRowView selectedRow)
			{
				// Создаем экземпляр окна редактирования, передавая выбранный DataRowView и ссылку на основное окно
				
			}
		}
		private void dataGridUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			// Проверяем, выбрана ли строка
			
		}

		private void dataGridDevices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			
		}

		private void Docs_Create_Click(object sender, RoutedEventArgs e)
		{
			
		}

		private void dataGridDocs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			
		}

		private void dataGridSklad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			
		}

		private void dataGridTypesTO_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			
		}

		private void dataGridNaryad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			
		}

		private void dataGridMonitorNaryad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (dataGridMonitorNaryad.SelectedItem is DataRowView selectedRow)
			{
				// Создаем экземпляр окна редактирования, передавая выбранный DataRowView и ссылку на основное окно
				Monitor_Naryad_Nalad users_Nalad_Edit_Window = new Monitor_Naryad_Nalad(this, selectedRow);

				// Показываем диалоговое окно
				users_Nalad_Edit_Window.ShowDialog();
			}
		}





		private void dataGridDevices_Types_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			
		}

		







		/// <summary>
		/// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    ВИЗУАЛ ПОЛОМОК И ТО НА СХЕМЕ ЗДАНИЯ
		/// </summary>


		
		//private void UpdateRectangleStatus(Rectangle point, TextBlock label, string deviceName, DataTable crashData, DataTable naryadData)
		//{
		//	// Очистка возможных заметок
		//	label.Text = deviceName;

		//	// Проверяем данные в таблице Crash
		//	DataRow[] crashRows = crashData.Select($"Device = '{deviceName}'");

		//	foreach (var row in crashRows) // Ищем указанное оборудование по всем строкам
		//	{
		//		string status = row["Status"].ToString();
		//		if (status == "Поломка")
		//		{
		//			point.Fill = Brushes.Red;
		//			label.Text += " (Поломка)";
		//			return;
		//		}
		//		else if (status == "В работе")
		//		{
		//			point.Fill = Brushes.Yellow;
		//			label.Text += " (Поломка)";
		//			return;
		//		}

		//	}

		//	// Если в Crash не найдены актуальные статусы, продолжить проверку в таблице Naryad
		//	string todayDate = DateTime.Now.ToString("yyyy-MM-dd"); // Текущая дата
		//	DataRow[] naryadRows = naryadData.Select($"Device_Name = '{deviceName}' AND Date_TO = '{todayDate}'");

		//	foreach (var row in naryadRows) // Перебираем все строки Naryad
		//	{
		//		string naryadStatus = row["Status"].ToString();
		//		if (string.IsNullOrEmpty(naryadStatus) || naryadStatus == "null")
		//		{
		//			point.Fill = Brushes.Red;
		//			label.Text += " (ТО)";
		//			return;
		//		}
		//		else if (naryadStatus == "В работе")
		//		{
		//			point.Fill = Brushes.Yellow;
		//			label.Text += " (ТО)";
		//			return;
		//		}
		//		else if (naryadStatus == "Выполнен")
		//		{
		//			point.Fill = Brushes.Green;
		//			label.Text += " (ТО)";
		//			return;
		//		}

		//	}

		//	// Если не найдено ни одного совпадения со статусами, оставить стандартный цвет
		//	point.Fill = Brushes.LightGray;
		//}


	}
}







