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
	/// Логика взаимодействия для Types_TO_Create_Window.xaml
	/// </summary>
	public partial class Types_TO_Create_Window : Window
	{
		private MainWindow mainWindow;

		public Types_TO_Create_Window(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
		}

		private void AddTypeTO_Click(object sender, RoutedEventArgs e)
		{
			var deviceType = DeviceType.Text;
			var nameTO = NameTO.Text;
			var workList = WorkList.Text;
			var raspisanie = Raspisanie.Text;

			// SQL-запрос для добавления записи в таблицу Types_TO
			string query = "INSERT INTO [Technical_Service].[dbo].[Types_TO] " +
						   "([Device_Type], [Name_TO], [Work_List], [Raspisanie]) " +
						   "VALUES (@DeviceType, @NameTO, @WorkList, @Raspisanie)";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						// Установка параметров для запроса
						command.Parameters.AddWithValue("@DeviceType", deviceType);
						command.Parameters.AddWithValue("@NameTO", nameTO);
						command.Parameters.AddWithValue("@WorkList", workList);
						command.Parameters.AddWithValue("@Raspisanie", raspisanie);

						connection.Open();
						command.ExecuteNonQuery();

						// Обновление данных в главном окне
						mainWindow.LoadData_TypesTO();
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}
	}

}
