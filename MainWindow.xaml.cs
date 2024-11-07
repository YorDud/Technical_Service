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
using MaterialDesignThemes.MahApps;
using Button = System.Windows.Controls.Button;




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
		}

		private void ShowMonitoringTO(object sender, RoutedEventArgs e)
		{
			MainContent.Visibility = Visibility.Visible;
			MainHelp.Visibility = Visibility.Hidden;
			TO.Visibility = Visibility.Hidden;
			Devices.Visibility = Visibility.Hidden;
			
		}



		private void ShowTipesTO(object sender, RoutedEventArgs e)
		{
			MainHelp.Visibility = Visibility.Hidden;
			MainContent.Visibility = Visibility.Hidden;
			TO.Visibility = Visibility.Visible;
			Devices.Visibility = Visibility.Hidden;
		}

		private void ShowDevices(object sender, RoutedEventArgs e)
		{
			MainHelp.Visibility = Visibility.Hidden;
			MainContent.Visibility = Visibility.Hidden;
			TO.Visibility = Visibility.Hidden;
			Devices.Visibility = Visibility.Visible;
		}


		private void ShowPrivacy(object sender, RoutedEventArgs e)
		{

		}



		private void ShowHelp(object sender, RoutedEventArgs e)
		{
			MainHelp.Visibility = Visibility.Visible;
			MainContent.Visibility = Visibility.Hidden;
			TO.Visibility = Visibility.Hidden;
			Devices.Visibility = Visibility.Hidden;
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





