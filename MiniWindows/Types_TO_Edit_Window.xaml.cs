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
	/// Логика взаимодействия для Types_TO_Edit_Window.xaml
	/// </summary>
	public partial class Types_TO_Edit_Window : Window
	{
		private DataRowView _dataRow;
		private MainWindow mainWindow;

		public Types_TO_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();

			this.mainWindow = mainWindow;
			_dataRow = dataRow;

			// Заполнение полей данными из выбранной записи
			DeviceType.Text = _dataRow["Device_Type"].ToString();
			NameTO.Text = _dataRow["Name_TO"].ToString();
			WorkList.Text = _dataRow["Work_List"].ToString();
			Raspisanie.Text = _dataRow["Raspisanie"].ToString();
		}

		private void SaveChanges_Click(object sender, RoutedEventArgs e)
		{
			var deviceType = DeviceType.Text;
			var nameTO = NameTO.Text;
			var workList = WorkList.Text;
			var raspisanie = Raspisanie.Text;

			var id = _dataRow["ID"];

			// SQL-запрос для обновления данных
			string query = "UPDATE [Technical_Service].[dbo].[Types_TO] " +
						   "SET [Device_Type] = @DeviceType, [Name_TO] = @NameTO, " +
						   "[Work_List] = @WorkList, [Raspisanie] = @Raspisanie " +
						   "WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DeviceType", deviceType);
						command.Parameters.AddWithValue("@NameTO", nameTO);
						command.Parameters.AddWithValue("@WorkList", workList);
						command.Parameters.AddWithValue("@Raspisanie", raspisanie);
						command.Parameters.AddWithValue("@ID", id);

						connection.Open();
						command.ExecuteNonQuery();

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

		private void DeleteTypeTO_Click(object sender, RoutedEventArgs e)
		{
			var id = _dataRow["ID"];

			// SQL-запрос для удаления записи
			string query = "DELETE FROM [Technical_Service].[dbo].[Types_TO] WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@ID", id);

						connection.Open();
						command.ExecuteNonQuery();

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
