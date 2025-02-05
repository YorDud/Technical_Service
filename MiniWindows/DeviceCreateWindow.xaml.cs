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
using ControlzEx.Standard;

namespace WpfApp4.MiniWindows
{
	/// <summary>
	/// Логика взаимодействия для DeviceCreateWindow.xaml
	/// </summary>
	public partial class DeviceCreateWindow : Window
	{
		//private DataRowView _dataRow;
		private MainWindow mainWindow;

		public DeviceCreateWindow(MainWindow mainWindow/*DataRowView dataRow)*/)
		{
			InitializeComponent();

			this.mainWindow = mainWindow; // сохраняем ссылку на главное окно

			//_dataRow = dataRow;
			//FIO.Text = _dataRow["ID"].ToString();
			//Log.Text = _dataRow["FIO"].ToString();
			//Pass.Text = _dataRow["Pass"].ToString();
			//Role.Text = _dataRow["Role"].ToString();
			//Smena.Text = _dataRow["Smena"].ToString();
			//Phone.Text = _dataRow["Phone"].ToString();

			//LoadDevice_Type();
			LoadLocation();
		}

		private void Device_Dobav_Click(object sender, RoutedEventArgs e)
		{
			var Name_Device1 = Name_Device.Text;
			//var Device_Type1 = Device_Type.Text;
			var Model1 = Model.Text;
			var Ser_Number1 = Ser_Number.Text;
			var Year_Create_Device1 = Year_Create_Device.Text;
			var Inventory_Number1 = Inventory_Number.Text;
			var Location1 = Location.Text;
			var Name_Buh_Uch1 = Name_Buh_Uch.Text;



			// SQL-запрос на добавление
			string query = "INSERT INTO [Technical_Service].[dbo].[Devices] " +
			   "([Name_Device], [Model], [Ser_Number], [Year_Create_Device], [Inventory_Number], [Location], [Name_Buh_Uch], [Firm] ) " +
			   "VALUES (@Name_Device, @Model, @Ser_Number, @Year_Create_Device, @Inventory_Number, @Location, @Name_Buh_Uch, @Firm)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Добавление значений для параметров
						command.Parameters.AddWithValue("@Name_Device", Name_Device1);
						//command.Parameters.AddWithValue("@Device_Type", Device_Type1);
						command.Parameters.AddWithValue("@Model", Model1);
						command.Parameters.AddWithValue("@Ser_Number", Ser_Number1);
						command.Parameters.AddWithValue("@Year_Create_Device", Year_Create_Device1);
						command.Parameters.AddWithValue("@Inventory_Number", Inventory_Number1);
						command.Parameters.AddWithValue("@Location", Location1);
						command.Parameters.AddWithValue("@Name_Buh_Uch", Name_Buh_Uch.Text);
						command.Parameters.AddWithValue("@Firm", Firm.Text);


						// Открытие соединения и выполнение команды
						connection.Open();
						int rowsAffected = command.ExecuteNonQuery();
						//CustomMessageBoxService.Show($"Пользователь, {FIOv} добавлен!", "");

						mainWindow.LoadData_Devices();
						this.Close();

					}
				}


			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}


		//private void LoadDevice_Type()
		//{
		//	string query = "SELECT Device_Type FROM [Technical_Service].[dbo].[Devices_Types]";
		//	try
		//	{
		//		using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
		//		{
		//			connection.Open();
		//			using (SqlCommand command = new SqlCommand(query, connection))
		//			{
		//				using (SqlDataReader reader = command.ExecuteReader())
		//				{
		//					List<string> typesTO = new List<string>();
		//					while (reader.Read())
		//					{
		//						typesTO.Add(reader["Device_Type"].ToString());
		//					}
		//					Device_Type.ItemsSource = typesTO; // Устанавливаем источник данных для ComboBox
		//				}
		//			}
		//		}

		//		// Подписка на событие выбора элемента
		//		//Device_Type.SelectionChanged += Device_Type_SelectionChanged;
		//	}
		//	catch (Exception ex)
		//	{
		//		MessageBox.Show("Ошибка при загрузке данных: " + ex.Message);
		//	}
		//}
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

		private void Device_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//if (Device_Type.SelectedItem != null)
			//{
			//	string selectedNameTO = TypesTOName.SelectedItem.ToString();
			//	LoadWorkList(selectedNameTO);
			//}
		}
	}
}
