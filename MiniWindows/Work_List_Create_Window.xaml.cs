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
	/// Логика взаимодействия для Work_List_Create_Window.xaml
	/// </summary>
	public partial class Work_List_Create_Window : Window
	{
		private MainWindow mainWindow;
		public Work_List_Create_Window(MainWindow mainWindow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow;
			LoadDevice_Type();

		}

		private void Work_List_Dobav_Click(object sender, RoutedEventArgs e)
		{
			var Work_ListV = Work_List.Text;
			var Device_TypeV = DeviceType.Text;
			var Norm_HourV = NormHour.Text;


			// SQL-запрос на добавление
			string query = "INSERT INTO [Work_List] " +
			   "([Work_List], [Device_Type], [Norm_Hour]) " +
			   "VALUES (@Work_List, @Device_Type, @Norm_Hour)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значений для параметров
						command.Parameters.AddWithValue("@Work_List", Work_ListV);
						command.Parameters.AddWithValue("@Device_Type", Device_TypeV);
						command.Parameters.AddWithValue("@Norm_Hour", Norm_HourV);
						
						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						//CustomMessageBoxService.Show($"Пользователь, {FIOv} добавлен!", "");

						mainWindow.LoadData_Work_List();
						this.Close();

					}
				}


			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
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

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		
	}
    
}
