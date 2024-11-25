using System;
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




namespace WpfApp4
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{


		public MainWindow()
		{
			InitializeComponent();
			StartDatePicker.SelectedDate = DateTime.Now;
			EndDatePicker.SelectedDate = DateTime.Now.AddDays(7);       // Текущая дата + 7 дней


			LoadData_Monitor_Naryad();

			LoadData_Users();

			LoadData_Devices();

			LoadData_Docs();
			LoadData_Sklad();
			LoadData_TypesTO();
			LoadData_Naryad();
			
			LoadData_Device_Types();





		}





		private void SetVisibility(FrameworkElement visibleElement)
		{
			// Список всех элементов, которые нужно скрыть
			var elements = new List<FrameworkElement>
	{
		MainHelp,
		MainContent,
		Devices_Types,
		Devices,
		NarTO,
		TechObsl,
		//ZapTO,
		Ed_Izmer,
		Kontragents,
		Nomekluatura,
		Users_Nalad,
		Sklad,
		Shtrichcodes,
		Dop_Rekvizits,
		Jurnal_Docs,
		Otch_Kopletuishie,
		Otch_Sklad,
		Monitor_Sklad,
		Kalendar_To,
		Raboti_Devices
	};

			// Устанавливаем видимость для каждого элемента
			foreach (var element in elements)
			{
				element.Visibility = (element == visibleElement) ? Visibility.Visible : Visibility.Hidden;
			}
		}

		// Пример вызова метода
		//SetVisibility(ZapTO);




		public void LoadData_Users()
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				string query = "SELECT [ID], [FIO], [Login], [Password], [Role], [Smena], [Phone] FROM [Users]";
				SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

				DataTable dataTable = new DataTable();

				connection.Open();
				adapter.Fill(dataTable);
				connection.Close();

				dataGridUsers.ItemsSource = dataTable.DefaultView;
			}
		}

		public void LoadData_Docs()
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				string query = "SELECT [ID]\r\n      ,[Name_Doc]\r\n      ,[Doc]\r\n      ,[Opisaniye] FROM [Documentation]";
				SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

				DataTable dataTable = new DataTable();

				connection.Open();
				adapter.Fill(dataTable);
				connection.Close();

				dataGridDocs.ItemsSource = dataTable.DefaultView;
			}
		}

		public void LoadData_Devices()
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				string query = "SELECT [ID], [Name_Device]\r\n      ,[Device_Type]\r\n      ,[Model]\r\n      ,[Ser_Number]\r\n      ,[Year_Create_Device]\r\n      ,[Inventory_Number]\r\n      ,[Location]\r\n      ,[Name_Buh_Uch]\r\n  FROM [Devices]";
				SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

				DataTable dataTable = new DataTable();

				connection.Open();
				adapter.Fill(dataTable);
				connection.Close();

				dataGridDevices.ItemsSource = dataTable.DefaultView;
			}
		}

		public void LoadData_Device_Types()
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				string query = "SELECT [ID]\r\n      ,[Device_Type]\r\n  FROM [Devices_Types]";
				SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

				DataTable dataTable = new DataTable();

				connection.Open();
				adapter.Fill(dataTable);
				connection.Close();

				dataGridDevice_Types.ItemsSource = dataTable.DefaultView;
			}
		}
























		private void SearchBoxDevices_TextChanged(object sender, TextChangedEventArgs e)
		{
			var dataView = dataGridDevices.ItemsSource as DataView;

			if (dataView != null)
			{
				string filterText = SearchBoxDevices.Text;

				if (string.IsNullOrEmpty(filterText))
				{
					dataView.RowFilter = string.Empty; // Показываем все строки
				}
				else
				{
					// Создаем фильтр, приводя числовые данные к строковому типу
					string filter = string.Join(" OR ", dataView.Table.Columns.Cast<DataColumn>()
						.Select(col => col.DataType == typeof(string)
							? $"[{col.ColumnName}] LIKE '%{filterText}%'"
							: $"CONVERT([{col.ColumnName}], 'System.String') LIKE '%{filterText}%'"));

					dataView.RowFilter = filter;
				}
			}
		}

		private void ClearBtnDevices_Click(object sender, RoutedEventArgs e)
		{
			SearchBoxDevices.Clear();
		}

		private void SearchBoxNarTO_TextChanged(object sender, TextChangedEventArgs e)
		{
			var dataView = dataGridNaryad.ItemsSource as DataView;

			if (dataView != null)
			{
				string filterText = SearchBoxNarTO.Text;

				if (string.IsNullOrEmpty(filterText))
				{
					dataView.RowFilter = string.Empty; // Показываем все строки
				}
				else
				{
					// Создаем фильтр, приводя числовые данные к строковому типу
					string filter = string.Join(" OR ", dataView.Table.Columns.Cast<DataColumn>()
						.Select(col => col.DataType == typeof(string)
							? $"[{col.ColumnName}] LIKE '%{filterText}%'"
							: $"CONVERT([{col.ColumnName}], 'System.String') LIKE '%{filterText}%'"));

					dataView.RowFilter = filter;
				}
			}
		}

		private void ClearBtnNarTO_Click(object sender, RoutedEventArgs e)
		{
			SearchBoxNarTO.Clear();
		}




		private void SearchBoxTypesTO_TextChanged(object sender, TextChangedEventArgs e)
		{
			var dataView = dataGridTypesTO.ItemsSource as DataView;

			if (dataView != null)
			{
				string filterText = SearchBoxTypesTO.Text;

				if (string.IsNullOrEmpty(filterText))
				{
					dataView.RowFilter = string.Empty; // Показываем все строки
				}
				else
				{
					// Создаем фильтр, приводя числовые данные к строковому типу
					string filter = string.Join(" OR ", dataView.Table.Columns.Cast<DataColumn>()
						.Select(col => col.DataType == typeof(string)
							? $"[{col.ColumnName}] LIKE '%{filterText}%'"
							: $"CONVERT([{col.ColumnName}], 'System.String') LIKE '%{filterText}%'"));

					dataView.RowFilter = filter;
				}
			}
		}
		private void ClearBtnTypesTO_Click(object sender, RoutedEventArgs e)
		{
			SearchBoxTypesTO.Clear();
		}


		private void SearchBoxUsers_TextChanged(object sender, TextChangedEventArgs e)
		{
			var dataView = dataGridUsers.ItemsSource as DataView;

			if (dataView != null)
			{
				string filterText = SearchBoxUsers.Text;

				if (string.IsNullOrEmpty(filterText))
				{
					dataView.RowFilter = string.Empty; // Показываем все строки
				}
				else
				{
					// Создаем фильтр, приводя числовые данные к строковому типу
					string filter = string.Join(" OR ", dataView.Table.Columns.Cast<DataColumn>()
						.Select(col => col.DataType == typeof(string)
							? $"[{col.ColumnName}] LIKE '%{filterText}%'"
							: $"CONVERT([{col.ColumnName}], 'System.String') LIKE '%{filterText}%'"));

					dataView.RowFilter = filter;
				}
			}
		}

		private void ClearBtnUsers_Click(object sender, RoutedEventArgs e)
		{
			SearchBoxUsers.Clear();
		}


		private void SearchBoxSklad_TextChanged(object sender, TextChangedEventArgs e)
		{
			var dataView = dataGridSklad.ItemsSource as DataView;

			if (dataView != null)
			{
				string filterText = SearchBoxSklad.Text;

				if (string.IsNullOrEmpty(filterText))
				{
					dataView.RowFilter = string.Empty; // Показываем все строки
				}
				else
				{
					// Создаем фильтр, приводя числовые данные к строковому типу
					string filter = string.Join(" OR ", dataView.Table.Columns.Cast<DataColumn>()
						.Select(col => col.DataType == typeof(string)
							? $"[{col.ColumnName}] LIKE '%{filterText}%'"
							: $"CONVERT([{col.ColumnName}], 'System.String') LIKE '%{filterText}%'"));

					dataView.RowFilter = filter;
				}
			}

		}

		private void ClearBtnSklad_Click(object sender, RoutedEventArgs e)
		{
			SearchBoxSklad.Clear();
		}

		private void SearchBoxDocs_TextChanged(object sender, TextChangedEventArgs e)
		{
			var dataView = dataGridDocs.ItemsSource as DataView;

			if (dataView != null)
			{
				string filterText = SearchBoxDocs.Text;

				if (string.IsNullOrEmpty(filterText))
				{
					dataView.RowFilter = string.Empty; // Показываем все строки
				}
				else
				{
					// Создаем фильтр, приводя числовые данные к строковому типу
					string filter = string.Join(" OR ", dataView.Table.Columns.Cast<DataColumn>()
						.Select(col => col.DataType == typeof(string)
							? $"[{col.ColumnName}] LIKE '%{filterText}%'"
							: $"CONVERT([{col.ColumnName}], 'System.String') LIKE '%{filterText}%'"));

					dataView.RowFilter = filter;
				}
			}
		}

		private void ClearBtnDocs_Click(object sender, RoutedEventArgs e)
		{
			SearchBoxDocs.Clear();
		}

		private void SearchBoxDevices_Types_TextChanged(object sender, TextChangedEventArgs e)
		{
			var dataView = dataGridDevice_Types.ItemsSource as DataView;

			if (dataView != null)
			{
				string filterText = SearchBoxDevices_Types.Text;

				if (string.IsNullOrEmpty(filterText))
				{
					dataView.RowFilter = string.Empty; // Показываем все строки
				}
				else
				{
					// Создаем фильтр, приводя числовые данные к строковому типу
					string filter = string.Join(" OR ", dataView.Table.Columns.Cast<DataColumn>()
						.Select(col => col.DataType == typeof(string)
							? $"[{col.ColumnName}] LIKE '%{filterText}%'"
							: $"CONVERT([{col.ColumnName}], 'System.String') LIKE '%{filterText}%'"));

					dataView.RowFilter = filter;
				}
			}
		}

		private void ClearBtnDevices_Types_Click(object sender, RoutedEventArgs e)
		{
			SearchBoxDevices_Types.Clear();
		}





































		public void LoadData_Sklad()
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				string query = "SELECT [ID]\r\n      ,[Deteil_Types]\r\n      ,[Model]\r\n      ,[Proizvod]\r\n      ,[Postav]\r\n      ,[Devices_ID]\r\n ,[Name_Image]     ,[Image]\r\n      ,[Location]\r\n      ,[Kolich]\r\n  FROM [Sklad]";
				SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

				DataTable dataTable = new DataTable();

				connection.Open();
				adapter.Fill(dataTable);
				connection.Close();

				dataGridSklad.ItemsSource = dataTable.DefaultView;
			}
		}

		public void LoadData_TypesTO()
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				string query = "SELECT [ID]\r\n      ,[Device_Type]\r\n      ,[Name_TO]\r\n      ,[Work_List]\r\n      ,[Raspisanie]\r\n  FROM [Types_TO]";
				SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

				DataTable dataTable = new DataTable();

				connection.Open();
				adapter.Fill(dataTable);
				connection.Close();

				dataGridTypesTO.ItemsSource = dataTable.DefaultView;
			}
		}

		public void LoadData_Naryad()
		{
			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				// SQL-запрос с JOIN для получения данных из таблиц Naryad и Types_TO
				string query = @"
            SELECT 
                [ID]
      ,[Device_Name]
      ,[Types_TO_Name]
      ,[Types_TO_Work_List]
      ,[Users_FIO]
      ,[Date_Start]
      ,[Date_End]
      ,[Status]
      ,[Sklad_Deteil_ID]
      ,[Sklad_Kolich]
      ,[Documentation_Name_ID]
      ,[Date_TO]
      ,[Comment]
  FROM [Technical_Service].[dbo].[Naryad]";

				SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
				DataTable dataTable = new DataTable();

				connection.Open();
				adapter.Fill(dataTable);
				connection.Close();

				dataGridNaryad.ItemsSource = dataTable.DefaultView;
			}
		}




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
			if (string.IsNullOrEmpty(status) || status == "В работе")
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












	private void ShowMonitoringTO(object sender, RoutedEventArgs e)
	{
		LoadData_Monitor_Naryad();
		SetVisibility(MainContent);

	}



	private void ShowTipesTO(object sender, RoutedEventArgs e)
	{
		LoadData_Device_Types();
		SetVisibility(Devices_Types);
	}

	private void ShowDevices(object sender, RoutedEventArgs e)
	{
		LoadData_Devices();
		SetVisibility(Devices);
	}
	private void NarTObtn(object sender, RoutedEventArgs e)
	{
		LoadData_Naryad();
		SetVisibility(NarTO);
	}
	private void TechObslbtn(object sender, RoutedEventArgs e)
	{
		LoadData_TypesTO();
		SetVisibility(TechObsl);
	}
	private void ZapolTObtn(object sender, RoutedEventArgs e)
	{
		//SetVisibility(ZapTO);
	}






	private void ShowHelp(object sender, RoutedEventArgs e)
	{
		SetVisibility(MainHelp);
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
		Load_Data_DataGrid();
			LoadData_Monitor_Naryad();




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
		MiniWindows.TypeDeviceWindow typeDeviceWindow = new MiniWindows.TypeDeviceWindow(this);
		typeDeviceWindow.ShowDialog();
	}



	private void DeviceCreateClick(object sender, RoutedEventArgs e)
	{
		MiniWindows.DeviceCreateWindow deviceCreateWindow = new MiniWindows.DeviceCreateWindow(this);
		deviceCreateWindow.ShowDialog();
	}

	private void DeviceGroupsCreateClick(object sender, RoutedEventArgs e)
	{
		//MiniWindows.DeviceGroupsCreateWindow deviceGroupsCreateWindow = new MiniWindows.DeviceGroupsCreateWindow();
		//deviceGroupsCreateWindow.ShowDialog();
	}

	private void NarTOCreateClick(object sender, RoutedEventArgs e)
	{
		MiniWindows.Naryad_Create_Window narTOCreateWindow = new MiniWindows.Naryad_Create_Window(this);
		narTOCreateWindow.ShowDialog();
	}

	private void Types_TO_Create_Click(object sender, RoutedEventArgs e)
	{
		MiniWindows.Types_TO_Create_Window types_TO_Create_Window = new Types_TO_Create_Window(this);
		types_TO_Create_Window.ShowDialog();
	}

	private void Ed_Izmer_Create_Click(object sender, RoutedEventArgs e)
	{
		//MiniWindows.Ed_Izmer_Create_Window ed_Izmer_Create_Window = new MiniWindows.Ed_Izmer_Create_Window();
		//ed_Izmer_Create_Window.ShowDialog();
	}

	private void Show_ED_Izmer(object sender, RoutedEventArgs e)
	{
		SetVisibility(Ed_Izmer);
	}


	private void Kontragents_Create_Click(object sender, RoutedEventArgs e)
	{
		//MiniWindows.Kontragents_Create_Window kontragents_Create_Window = new MiniWindows.Kontragents_Create_Window();
		//kontragents_Create_Window.ShowDialog();
	}

	private void Kontragents_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Kontragents);
	}

	private void Nomekluatura_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Nomekluatura);
	}

	private void Nomekluatura_Create_Click(object sender, RoutedEventArgs e)
	{
		//MiniWindows.Nomekluatura_Create_Window nomekluatura_Create_Window = new MiniWindows.Nomekluatura_Create_Window();
		//nomekluatura_Create_Window.ShowDialog();
	}

	private void Button_Click(object sender, RoutedEventArgs e)
	{

	}

	private void Users_Nalad_Click(object sender, RoutedEventArgs e)
	{
		LoadData_Users();
		SetVisibility(Users_Nalad);
	}

	private void Users_Nalad_Create_Click(object sender, RoutedEventArgs e)
	{
		MiniWindows.Users_Nalad_Create_Window users_Nalad_Create = new MiniWindows.Users_Nalad_Create_Window(this);
		users_Nalad_Create.ShowDialog();
	}

	private void Device_2_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Devices);
	}



	private void MT_Click(object sender, RoutedEventArgs e)
	{
		Load_Data_DataGrid();
	}

	private void Sklad_Click(object sender, RoutedEventArgs e)
	{
		LoadData_Sklad();
		SetVisibility(Sklad);
	}

	private void Sklad_Create_Click(object sender, RoutedEventArgs e)
	{
		MiniWindows.Sklad_Create_Window sklad_Create_Window = new Sklad_Create_Window(this);
		sklad_Create_Window.ShowDialog();
	}

	private void Shtrichcodes_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Shtrichcodes);
	}

	private void Dop_Rekvizits_Create_Click(object sender, RoutedEventArgs e)
	{

	}

	private void Dop_Rekvizits_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Dop_Rekvizits);
	}

	private void Show_Jurnal_Docs(object sender, RoutedEventArgs e)
	{
		LoadData_Docs();
		SetVisibility(Jurnal_Docs);
	}

	private void Otch_Kopletuishie_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Otch_Kopletuishie);
	}

	private void Otch_Sklad_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Otch_Sklad);
	}

	private void Monitor_Sklad_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Monitor_Sklad);
	}

	private void Kalendar_To_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Kalendar_To);
	}

	private void Raboti_Devices_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Raboti_Devices);
	}



	private void Docs_Create_Window_Click(object sender, RoutedEventArgs e)
	{
		//MiniWindows.Docs_Create_Window docs_Create_Window = new MiniWindows.Docs_Create_Window();
		//docs_Create_Window.ShowDialog();
	}

	private void TechObsl_Create_Click(object sender, RoutedEventArgs e)
	{
		//MiniWindows.TechObslCreateWindow techObslCreateWindow = new TechObslCreateWindow();
		//techObslCreateWindow.ShowDialog();
	}

	private void Docs_Click(object sender, RoutedEventArgs e)
	{
		SetVisibility(Jurnal_Docs);
	}

	private void Sklad_Create_Window_Click(object sender, RoutedEventArgs e)
	{
		MiniWindows.Sklad_Create_Window sklad_Create_Window = new Sklad_Create_Window(this);
		sklad_Create_Window.ShowDialog();
	}

	private void Window_Closed(object sender, EventArgs e)
	{
		System.Windows.Application.Current.Shutdown();
	}

	private void dataGridUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		// Проверяем, выбрана ли строка
		if (dataGridUsers.SelectedItem is DataRowView selectedRow)
		{
			// Создаем экземпляр окна редактирования, передавая выбранный DataRowView и ссылку на основное окно
			Users_Nalad_Edit_Window users_Nalad_Edit_Window = new Users_Nalad_Edit_Window(this, selectedRow);

			// Показываем диалоговое окно
			users_Nalad_Edit_Window.ShowDialog();
		}
	}

	private void dataGridDevices_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (dataGridDevices.SelectedItem is DataRowView selectedRow)
		{
			// Создаем экземпляр окна редактирования, передавая выбранный DataRowView и ссылку на основное окно
			DeviceEditWindow users_Nalad_Edit_Window = new DeviceEditWindow(this, selectedRow);

			// Показываем диалоговое окно
			users_Nalad_Edit_Window.ShowDialog();
		}
	}

	private void Docs_Create_Click(object sender, RoutedEventArgs e)
	{
		MiniWindows.Docs_Create_Window docs_Create_Window = new Docs_Create_Window(this);
		docs_Create_Window.ShowDialog();
	}

	private void dataGridDocs_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (dataGridDocs.SelectedItem is DataRowView selectedRow)
		{
			// Создаем экземпляр окна редактирования, передавая выбранный DataRowView и ссылку на основное окно
			Docs_Edit_Window users_Nalad_Edit_Window = new Docs_Edit_Window(this, selectedRow);

			// Показываем диалоговое окно
			users_Nalad_Edit_Window.ShowDialog();
		}
	}

	private void dataGridSklad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (dataGridSklad.SelectedItem is DataRowView selectedRow)
		{
			// Создаем экземпляр окна редактирования, передавая выбранный DataRowView и ссылку на основное окно
			Sklad_Edit_Window sklad_Edit_Window = new Sklad_Edit_Window(this, selectedRow);

			// Показываем диалоговое окно
			sklad_Edit_Window.ShowDialog();
		}
	}

	private void dataGridTypesTO_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (dataGridTypesTO.SelectedItem is DataRowView selectedRow)
		{
			// Создаем экземпляр окна редактирования, передавая выбранный DataRowView и ссылку на основное окно
			Types_TO_Edit_Window sklad_Edit_Window = new Types_TO_Edit_Window(this, selectedRow);

			// Показываем диалоговое окно
			sklad_Edit_Window.ShowDialog();
		}
	}

	private void dataGridNaryad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (dataGridNaryad.SelectedItem is DataRowView selectedRow)
		{
			// Создаем экземпляр окна редактирования, передавая выбранный DataRowView и ссылку на основное окно
			Naryad_Edit_Window naryad_Edit_Window = new Naryad_Edit_Window(this, selectedRow);

			// Показываем диалоговое окно
			naryad_Edit_Window.ShowDialog();
		}
	}

	private void dataGridMonitorNaryad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (dataGridMonitorNaryad.SelectedItem is DataRowView selectedRow)
		{
			// Создаем экземпляр окна редактирования, передавая выбранный DataRowView и ссылку на основное окно
			MonitorNaryad_Edit_Window naryad_Edit_Window = new MonitorNaryad_Edit_Window(this, selectedRow);

			// Показываем диалоговое окно
			naryad_Edit_Window.ShowDialog();
		}
	}
	private void dataGridDevices_Types_MouseDoubleClick(object sender, MouseButtonEventArgs e)
	{
		if (dataGridDevice_Types.SelectedItem is DataRowView selectedRow)
		{
			// Создаем экземпляр окна редактирования, передавая выбранный DataRowView и ссылку на основное окно
			TypeDeviceEditWindow naryad_Edit_Window = new TypeDeviceEditWindow(this, selectedRow);

			// Показываем диалоговое окно
			naryad_Edit_Window.ShowDialog();
		}
	}

	private void dataGridSklad_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
	{
		// Определяем, на какую строку наведен курсор
		var dataGridRow = FindVisualParent<DataGridRow>(e.OriginalSource as DependencyObject);

		if (dataGridRow != null && dataGridRow.Item is DataRowView selectedRow)
		{
			// Извлекаем изображение из строки
			var imageBytes = selectedRow["Image"] as byte[];

			if (imageBytes != null)
			{
				try
				{
					// Преобразуем байтовый массив в BitmapImage
					BitmapImage bitmap = new BitmapImage();
					using (var stream = new MemoryStream(imageBytes))
					{
						bitmap.BeginInit();
						bitmap.StreamSource = stream;
						bitmap.CacheOption = BitmapCacheOption.OnLoad;
						bitmap.EndInit();
					}

					// Устанавливаем изображение в источник Image
					SelectedImage.Source = bitmap;
				}
				catch (Exception ex)
				{
					//MessageBox.Show("Ошибка при отображении изображения: " + ex.Message);
				}
			}
			else
			{
				// Если изображения нет
				SelectedImage.Source = null;
			}
		}
	}

	// Вспомогательный метод для поиска родителя типа DataGridRow
	private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
	{
		while (child != null)
		{
			if (child is T parent)
				return parent;

			child = VisualTreeHelper.GetParent(child);
		}
		return null;

	}






	public class NonEmptyStringConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value is string str && !string.IsNullOrWhiteSpace(str);
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}

		
	}
}

//		public MainWindow()
//		{
//			InitializeComponent();
//			LoadData_Pervoe();
//			LoadData_Vtoroe();
//			LoadData_Tretie();
//		}
//		private SqlDataAdapter dataAdapter;
//		private DataTable dataTable;

//		private void LoadData_Pervoe()
//		{
//			try
//			{
//				// Строка подключения должна быть корректной
//				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
//				{
//					connection.Open();
//					string query = "SELECT * FROM Proyavlen_Photomask_ProvMod ORDER BY Date_Create DESC";
//					dataAdapter = new SqlDataAdapter(query, connection);
//					SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
//					dataTable = new DataTable();
//					dataAdapter.Fill(dataTable);

//					// Устанавливаем источник данных
//					dataGridView1.ItemsSource = dataTable.DefaultView;

//				}
//			}
//			catch (Exception ex)
//			{
//				System.Windows.MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
//			}
//		}

//		private void LoadData_Vtoroe()
//		{
//			try
//			{
//				// Строка подключения должна быть корректной
//				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
//				{
//					connection.Open();
//					string query = "SELECT * FROM Proyavlen_Photomask_ProvMod ORDER BY Date_Create DESC";
//					dataAdapter = new SqlDataAdapter(query, connection);
//					SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
//					dataTable = new DataTable();
//					dataAdapter.Fill(dataTable);

//					// Устанавливаем источник данных
//					dataGrid2.ItemsSource = dataTable.DefaultView;

//				}
//			}
//			catch (Exception ex)
//			{
//				System.Windows.MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
//			}
//		}

//		private void LoadData_Tretie()
//		{
//			try
//			{
//				// Строка подключения должна быть корректной
//				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
//				{
//					connection.Open();
//					string query = "SELECT * FROM Proyavlen_Photomask_ProvMod ORDER BY Date_Create DESC";
//					dataAdapter = new SqlDataAdapter(query, connection);
//					SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
//					dataTable = new DataTable();
//					dataAdapter.Fill(dataTable);

//					// Устанавливаем источник данных
//					dataGrid3.ItemsSource = dataTable.DefaultView;

//				}
//			}
//			catch (Exception ex)
//			{
//				System.Windows.MessageBox.Show($"Ошибка при загрузке данных: {ex.Message}");
//			}
//		}
//		private void ShowMessageBox()
//		{

//		}
//		private void Button_Click(object sender, RoutedEventArgs e)
//		{
//			LoadData_Pervoe();
//			////System.Windows.Forms.MessageBox.Show("ffff");
//			////DialogCoordinator.Instance.ShowMessageAsync(this, "Заголовок", "Ваше сообщение");
//			//DialogCoordinator.Instance.ShowMessageAsync(this,
//			//	"Заголовок сообщения",
//			//	"Ваш текст сообщения",
//			//	MessageDialogStyle.AffirmativeAndNegative);






//		}
//		//public static class CustomMessageBoxService
//		//{
//		//	/// <summary>
//		//	/// Показывает пользовательское MessageBox с заголовком, сообщением и кнопкой OK.
//		//	/// </summary>
//		//	public static bool Show(string message, string title)
//		//	{
//		//		CustomMessageBox msgBox = new CustomMessageBox(title, message);
//		//		msgBox.Owner = System.Windows.Application.Current.MainWindow;
//		//		bool? result = msgBox.ShowDialog();
//		//		return result == true;
//		//	}

//		//	/// <summary>
//		//	/// Показывает пользовательское MessageBox с заголовком, сообщением и кнопками OK и Cancel.
//		//	/// </summary>
//		//	public static bool? Show(string message, string title, bool showCancel)
//		//	{
//		//		CustomMessageBox msgBox = new CustomMessageBox(title, message, showCancel);
//		//		msgBox.Owner = System.Windows.Application.Current.MainWindow;
//		//		bool? result = msgBox.ShowDialog();
//		//		return result;
//		//	}
//		//}
//		// CustomMessageBoxService.cs





//		private void Window_Loaded(object sender, RoutedEventArgs e)
//		{
//			LoadData_Pervoe();
//			LoadData_Vtoroe();
//			LoadData_Tretie();


//		}

//		private void save_btn_Click(object sender, RoutedEventArgs e)
//		{
//			LoadData_Vtoroe();
//		}



//		private void Add_Row()
//		{
//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
//			{
//				connection.Open();

//				string sqlInsert = "INSERT INTO Proyavlen_Photomask_ProvMod ([Date_Create],[FIO_Lab]) " +
//								   "VALUES (@Date_Create, @FIO_Lab)";

//				using (SqlCommand command = new SqlCommand(sqlInsert, connection))
//				{
//					command.Parameters.AddWithValue("@Date_Create", DateTime.Now);
//					command.Parameters.AddWithValue("@FIO_Lab", WC.fio);
//					command.ExecuteNonQuery();

//				}
//			}
//		}

//		private void Add_btn_Click(object sender, RoutedEventArgs e)
//		{
//			Add_Row();
//			LoadData_Pervoe();
//		}

//		private Dictionary<string, string> ColumnToDatabaseMap = new Dictionary<string, string>
//{
//	{ "Номер", "ID" },
//	{ "Дата создания", "Date_Create" },
//	{ "ФИО Создателя", "FIO_Lab" },
//	{ "CO3 2-", "Pro_Photorez_1_CO32" },
//	{ "HCO3-", "Pro_Photorez_1_CO3" },
//	{ "pH", "Pro_Photorez_1_pH" },
//	{ "A/B", "Pro_Photorez_1_A_B" },
//	{ "Сухой остаток", "Pro_Photorez_1_ostat" },
//	{ "Корректировка Материал", "Pro_Photorez_1_Correction_Mat" },
//	{ "Корректировка Количество", "Pro_Photorez_1_Correction_Score" },
//	{ "Дата создания корректировки", "Date_tech" },
//	{ "ФИО Лаборанта (редакт)", "FIO_Lab_Update" },
//	{ "Дата (редакт)", "Date_Lab_Update" },
//	{ "ФИО Корректировщика", "FIO_Corr" },
//	{ "Время выполнения корректировки", "Date_Corr" },
//	{ "Выполнение", "Сompleted" },
//	{ "Принятие в работу", "Start_corr" },
//	{ "Время принятия корректировки в выполнение", "Date_start_corr" },
//	{ "ФИО Корректировщикa", "FIO_start_corr" },
//	{ "Комментарий", "Сomment" }
//};


//		//private void dataGridView1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
//		//{
//		//	if (e.EditAction == DataGridEditAction.Commit)
//		//	{
//		//		var editedColumn = e.Column.Header.ToString();
//		//		var editedRow = (DataRowView)e.Row.Item;
//		//		var newValue = (e.EditingElement as System.Windows.Controls.TextBox).Text;
//		//		var id = editedRow["ID"];

//		//		if (ColumnToDatabaseMap.TryGetValue(editedColumn, out string databaseColumn))
//		//		{
//		//			UpdateDatabase(id, databaseColumn, newValue);
//		//		}
//		//		else
//		//		{
//		//			System.Windows.MessageBox.Show("Не удалось найти сопоставление для заголовка столбца: " + editedColumn);
//		//		}
//		//	}

//		//}


//		//private void UpdateDatabase(object id, string column, string newValue)
//		//{
//		//	using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
//		//	{
//		//		try
//		//		{
//		//			connection.Open();
//		//			string query = $"UPDATE [Proyavlen_Photomask_ProvMod] SET [{column}] = @newValue WHERE [ID] = @id";
//		//			using (SqlCommand command = new SqlCommand(query, connection))
//		//			{
//		//				command.Parameters.AddWithValue("@newValue", newValue);
//		//				command.Parameters.AddWithValue("@id", id);

//		//				command.ExecuteNonQuery();
//		//			}
//		//		}
//		//		catch (SqlException ex)
//		//		{
//		//			System.Windows.MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
//		//		}
//		//	}
//		//}

//		private void dataGridView1_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
//		{
//			if (e.EditAction == DataGridEditAction.Commit)
//			{
//				var editedColumn = e.Column.Header.ToString();
//				var editedRow = (DataRowView)e.Row.Item;
//				object newValue = null;

//				// Проверяем, что редактируемый элемент это DatePicker
//				if (e.EditingElement is DatePicker datePicker)
//				{
//					if (datePicker.SelectedDate.HasValue)
//					{
//						newValue = datePicker.SelectedDate.Value;
//						// Логирование выбранной даты
//						Console.WriteLine("Выбранная дата: " + newValue.ToString());
//					}
//					else
//					{
//						newValue = DBNull.Value;
//						Console.WriteLine("Дата не выбрана, значение будет NULL");
//					}
//				}
//				else if (e.EditingElement is System.Windows.Controls.TextBox textBox)
//				{
//					newValue = textBox.Text;
//				}

//				var id = editedRow["ID"];
//				if (ColumnToDatabaseMap.TryGetValue(editedColumn, out string databaseColumn))
//				{
//					UpdateDatabase(id, databaseColumn, newValue);
//				}
//				else
//				{
//					System.Windows.MessageBox.Show("Не удалось найти сопоставление для заголовка столбца: " + editedColumn);
//				}
//			}
//		}

//		private void UpdateDatabase(object id, string column, object newValue)
//		{
//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
//			{
//				try
//				{
//					connection.Open();
//					string query = $"UPDATE [Proyavlen_Photomask_ProvMod] SET [{column}] = @newValue WHERE [ID] = @id";
//					using (SqlCommand command = new SqlCommand(query, connection))
//					{
//						if (newValue is DateTime)
//						{
//							command.Parameters.Add("@newValue", SqlDbType.DateTime).Value = newValue;
//						}
//						else if (newValue == DBNull.Value || newValue == null)
//						{
//							command.Parameters.Add("@newValue", SqlDbType.DateTime).Value = DBNull.Value;
//						}
//						else
//						{
//							// Определяем переменную для значения параметра
//							object parameterValue;
//							if (newValue != null)
//							{
//								parameterValue = newValue.ToString();
//							}
//							else
//							{
//								parameterValue = DBNull.Value;
//							}

//							command.Parameters.Add("@newValue", SqlDbType.NVarChar).Value = parameterValue;
//						}

//						command.Parameters.Add("@id", SqlDbType.Int).Value = Convert.ToInt32(id);

//						// Логирование параметров
//						Console.WriteLine("Параметр @newValue: " + (newValue ?? "NULL"));
//						Console.WriteLine("Тип параметра @newValue: " + command.Parameters["@newValue"].SqlDbType.ToString());
//						Console.WriteLine("Параметр @id: " + id.ToString());

//						int rowsAffected = command.ExecuteNonQuery();
//						Console.WriteLine("Изменено строк: " + rowsAffected);
//					}
//				}
//				catch (SqlException ex)
//				{
//					System.Windows.MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
//					Console.WriteLine("Ошибка SQL: " + ex.ToString());
//				}
//				catch (Exception ex)
//				{
//					System.Windows.MessageBox.Show("Непредвиденная ошибка: " + ex.Message);
//					Console.WriteLine("Ошибка: " + ex.ToString());
//				}
//			}
//		}






//		private void dataGrid2_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
//		{

//			if (e.EditAction == DataGridEditAction.Commit)
//			{
//				var editedColumn = e.Column.Header.ToString();
//				var editedRow = (DataRowView)e.Row.Item;
//				var newValue = (e.EditingElement as System.Windows.Controls.TextBox).Text;
//				var id = editedRow["ID"];

//				if (ColumnToDatabaseMap.TryGetValue(editedColumn, out string databaseColumn))
//				{
//					UpdateDatabase2(id, databaseColumn, newValue);
//				}
//				else
//				{
//					System.Windows.MessageBox.Show("Не удалось найти сопоставление для заголовка столбца: " + editedColumn);
//				}
//			}
//		}

//		private void UpdateDatabase2(object id, string column, string newValue)
//		{
//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
//			{
//				try
//				{
//					connection.Open();
//					string query = $"UPDATE [Proyavlen_Photomask_ProvMod] SET [{column}] = @newValue WHERE [ID] = @id";
//					using (SqlCommand command = new SqlCommand(query, connection))
//					{
//						command.Parameters.AddWithValue("@newValue", newValue);
//						command.Parameters.AddWithValue("@id", id);

//						command.ExecuteNonQuery();
//					}
//				}
//				catch (SqlException ex)
//				{
//					System.Windows.MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
//				}
//			}
//		}










//		private void dataGrid3_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
//		{
//			// Указание столбцов, которые можно редактировать
//			var editableColumns = new List<string>
//	{
//		"CO3 2-",
//		"HCO3-",
//		"pH",
//		"A/B",
//		"Сухой остаток"

//	};

//			if (editableColumns.Contains(e.Column.Header.ToString()))
//			{
//				if (e.EditAction == DataGridEditAction.Commit)
//				{
//					var editedColumn = e.Column.Header.ToString();
//					var editedRow = (DataRowView)e.Row.Item;
//					var newValue = (e.EditingElement as System.Windows.Controls.TextBox).Text;
//					var id = editedRow["ID"];
//					if (ColumnToDatabaseMap.TryGetValue(editedColumn, out string databaseColumn))
//					{
//						UpdateDatabase3(id, databaseColumn, newValue);
//					}
//					else
//					{
//						System.Windows.MessageBox.Show("Не удалось найти сопоставление для заголовка столбца: " + editedColumn);
//					}
//				}
//			}
//			else
//			{
//				System.Windows.MessageBox.Show("Редактирование данного столбца запрещено.");
//			}
//		}





//		private void UpdateDatabase3(object id, string column, string newValue)
//		{
//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
//			{
//				try
//				{
//					connection.Open();
//					string query = $"UPDATE [Proyavlen_Photomask_ProvMod] SET [{column}] = @newValue WHERE [ID] = @id";
//					using (SqlCommand command = new SqlCommand(query, connection))
//					{
//						command.Parameters.AddWithValue("@newValue", newValue);
//						command.Parameters.AddWithValue("@id", id);

//						command.ExecuteNonQuery();
//					}
//				}
//				catch (SqlException ex)
//				{
//					System.Windows.MessageBox.Show("Ошибка при обновлении данных: " + ex.Message);
//				}
//			}
//		}

//		private void save_btn3_Click(object sender, RoutedEventArgs e)
//		{
//			LoadData_Tretie();
//		}

//		private void btnclose(object sender, RoutedEventArgs e)
//		{
//			bool result = CustomMessageBoxService.Show("Операция завершена успешно.", "Информация");
//			if (result)
//			{
//				// Пользователь нажал OK
//			}

//			bool? confirm = CustomMessageBoxService.Show("Вы уверены, что хотите выйти?", "Подтверждение", true);
//			if (confirm == true)
//			{
//				//System.Windows.Application.Current.Shutdown();
//			}
//		}
//	}

//}

//		//		private void UpdateRow()
//		//		{
//		//			// Убедитесь, что редактируемая ячейка находится в первой строке
//		//			int rowIndex = 0;

//		//			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
//		//			{
//		//				var selectedData = dataGridView1.SelectedItem as DataRowView;
//		//				// Получаем значение первичного ключа


//		//				var id = selectedData["ID"];

//		//				// Получаем значения обновляемых столбцов
//		//				var fioLab = WC.fio; // Убедитесь, что WC.fio и WC.ConnectionString доступны
//		//				var Date_Create = DateTime.Now;
//		//				var Pro_Photorez_1_CO32 = selectedData["Pro_Photorez_1_CO32"];
//		//				var Pro_Photorez_1_CO3 = selectedData["Pro_Photorez_1_CO3"];
//		//				var Pro_Photorez_1_pH = selectedData["Pro_Photorez_1_pH"];
//		//				var Pro_Photorez_1_A_B = selectedData["Pro_Photorez_1_A_B"];
//		//				var Pro_Photorez_1_ostat = selectedData["Pro_Photorez_1_ostat"];


//		//				// Создаем команду обновления с несколькими столбцами
//		//				string updateQuery = @"
//		//            UPDATE Proyavlen_Photomask_ProvMod
//		//   SET [Pro_Photorez_1_CO32] = @Pro_Photorez_1_CO32,
//		//[Pro_Photorez_1_CO3] = @Pro_Photorez_1_CO3, 
//		//[Pro_Photorez_1_pH] = @Pro_Photorez_1_pH, 
//		//[Pro_Photorez_1_A_B] = @Pro_Photorez_1_A_B,
//		//[Pro_Photorez_1_ostat] = @Pro_Photorez_1_ostat
//		//      ,[Date_Create] = @Date_Create,
//		//                [FIO_Lab] = @FIO_Lab
//		//	WHERE ID = @ID";

//		//				using (SqlCommand command = new SqlCommand(updateQuery, connection))
//		//				{
//		//					// Добавляем параметры для каждого столбца
//		//					command.Parameters.AddWithValue("@Date_Create", Date_Create);
//		//					command.Parameters.AddWithValue("@FIO_Lab", fioLab);
//		//					//
//		//					command.Parameters.AddWithValue("@Pro_Photorez_1_CO32", Pro_Photorez_1_CO32);
//		//					command.Parameters.AddWithValue("@Pro_Photorez_1_CO3", Pro_Photorez_1_CO3);
//		//					command.Parameters.AddWithValue("@Pro_Photorez_1_pH", Pro_Photorez_1_pH);
//		//					command.Parameters.AddWithValue("@Pro_Photorez_1_A_B", Pro_Photorez_1_A_B);
//		//					command.Parameters.AddWithValue("@Pro_Photorez_1_ostat", Pro_Photorez_1_ostat);

//		//					//
//		//					command.Parameters.AddWithValue("@ID", id); // ID - это первичный ключ

//		//					connection.Open();
//		//					command.ExecuteNonQuery();
//		//					connection.Close();
//		//				}

//		//				DialogHost.Show("Данные успешно Добавлены!");
//		//				//LoadData();
//		//			}
//		//		}





