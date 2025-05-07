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
	/// Логика взаимодействия для MonitorNaryad_Edit_Window.xaml
	/// </summary>
	public partial class MonitorNaryad_Edit_Window: Window
	{
		private MainWindow mainWindow;
		private DataRowView _dataRow;
		public MonitorNaryad_Edit_Window(MainWindow mainWindow, DataRowView dataRow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			_dataRow = dataRow;


			LoadStatus();

			Status.Text = _dataRow["Status"].ToString();
			
		}

		private void Naryad_Update_Click(object sender, RoutedEventArgs e)
		{
			// Предположим, идентификатор записи, которую нужно обновить
			var id = _dataRow["ID"]; // Получите этот идентификатор из контекста, например, из выбранного элемента списка
			
			var status = Status.Text;
			

			string query = "UPDATE [Technical_Service].[dbo].[Naryad] SET " +
						   "[Status] = @Status " +
						   "WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						
						command.Parameters.AddWithValue("@Status", status);
						command.Parameters.AddWithValue("@ID", id);
						

						connection.Open();
						command.ExecuteNonQuery();
						mainWindow.LoadData_Monitor_Naryad();
						this.Close();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка: " + ex.Message);
			}
		}
		

		
		private void LoadStatus()
		{
			Status.Items.Add("В работе");
			Status.Items.Add("Выполнено");
			Status.Items.Add("Закрыт");
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }
    }
}
