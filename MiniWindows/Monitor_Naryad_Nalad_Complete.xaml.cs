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
	/// Логика взаимодействия для Monitor_Naryad_Nalad_Complete.xaml
	/// </summary>
	public partial class Monitor_Naryad_Nalad_Complete : Window
	{
		private MainWindowNalad mainWindow;
		private DataRowView _dataRow;
		private int workNumber = 1;
		public Monitor_Naryad_Nalad_Complete(MainWindowNalad mainWindow, DataRowView dataRow)
		{
			InitializeComponent();
			_dataRow = dataRow;
			this.mainWindow = mainWindow;


		}

		

		// Привязка события на изменение DeviceType
		private void DeviceType_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			// Загружаем Work_List в зависимости от выбранного Device_Type

		}


		


		
		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		

		private void StartWork_Click(object sender, RoutedEventArgs e)
		{
			
			var id = _dataRow["ID"];

			// SQL-запрос для обновления данных в базе
			string query = "UPDATE [Technical_Service].[dbo].[Naryad] " +
						   "SET [Date_End] = @Date_End, " +
						   "[Status] = @Status " +
						   "WHERE [ID] = @ID";

			try
			{
				using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
				{
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Date_End", DateTime.Now);
						command.Parameters.AddWithValue("@Status", "Выполнено");
						command.Parameters.AddWithValue("@ID", id);

						connection.Open();
						command.ExecuteNonQuery();

						// После сохранения изменений обновляем данные в главном окне
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
	}
    
}
