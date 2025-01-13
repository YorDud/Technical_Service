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
	/// Логика взаимодействия для Crash_Create_Wndow_Nalad.xaml
	/// </summary>
	public partial class Crash_Create_Wndow_Nalad : Window
	{
		private MainWindowNalad mainWindow;

		public Crash_Create_Wndow_Nalad(MainWindowNalad mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			LoadLocation();
			LoadDeviceNames();
		}
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
							Device.ItemsSource = deviceNames; // Устанавливаем источник данных для ComboBox
						}
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
			}
		}
		private void LoadLocation()
		{
			string query = "SELECT Location FROM [Technical_Service].[dbo].[Location]";
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
								typesTO.Add(reader["Location"].ToString());
							}
							Location.ItemsSource = typesTO; // Устанавливаем источник данных для ComboBox
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
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Create_Click(object sender, RoutedEventArgs e)
		{
			// Проверяем, что все необходимые поля заполнены
			if (Location.SelectedItem == null || Device.SelectedItem == null)
			{
				MessageBox.Show("Пожалуйста, укажите наименование оборудования и помещение.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			// Проверка значения WC.FIO

			WC.fio = "Технолог";
			// Собираем данные для вставки
			string userFIO = WC.fio; // Имя сотрудника (переменная WC.FIO)
			string selectedLocation = Location.SelectedItem.ToString(); // Выбранное помещение
			string selectedDevice = Device.SelectedItem.ToString(); // Выбранное оборудование
			string comment = Comment.Text; // Описание
			string status = "Поломка"; // Фиксированный статус
			DateTime currentDate = DateTime.Now; // Текущая дата и время

			// SQL-запрос для вставки строки в таблицу
			string query = @"
        INSERT INTO [Technical_Service].[dbo].[Crash] 
        ([Date_Crash], [FIO_Tech], [Location], [Device], [Comment], [Status]) 
        VALUES (@Date_Crash, @FIO_Tech, @Location, @Device, @Comment, @Status)";

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
						command.Parameters.AddWithValue("@Date_Crash", currentDate);
						command.Parameters.AddWithValue("@FIO_Tech", userFIO);
						command.Parameters.AddWithValue("@Location", selectedLocation);
						command.Parameters.AddWithValue("@Device", selectedDevice);
						command.Parameters.AddWithValue("@Comment", comment);
						command.Parameters.AddWithValue("@Status", status);

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
