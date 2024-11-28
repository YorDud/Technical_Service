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
	/// Логика взаимодействия для Crash_Edit_Wndow.xaml
	/// </summary>
	public partial class Crash_Edit_Wndow : Window
	{
		private MainWindow mainWindow;
		private DataRowView _dataRow;
		public Crash_Edit_Wndow(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			_dataRow = dataRow;

			Status.Text = _dataRow["Status"].ToString();
		}
		
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Create_Click(object sender, RoutedEventArgs e)
		{
			// Проверяем, что все необходимые поля заполнены
			string status = Status.Text;
			string query = "UPDATE [Crash] " +
						   "SET [Status] = @Status " +
						   "WHERE [ID] = @ID";

			try
			{
				// Создание подключения к базе данных (замените строку подключения на актуальную)
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString)) // WC.ConnectionString — строка подключения
				{
					connection.Open();

					// Создаём SQL-команду для выполнения
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавляем параметры в запрос
						command.Parameters.AddWithValue("@Status", status);
						command.Parameters.AddWithValue("@ID", _dataRow["ID"]);

						// Выполняем команду
						int result = command.ExecuteNonQuery();

						// Проверяем успешность добавления
						mainWindow.LoadData_Monitor_Crash();
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				// Обработка ошибок и вывод сообщений
				MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}


	}
}
