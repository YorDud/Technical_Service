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
	/// Логика взаимодействия для Crash_Work_Window.xaml
	/// </summary>
	public partial class Crash_Work_Window : Window
	{
		private string _recordId;
		private string _connectionString = WC.ConnectionString;

		public Crash_Work_Window(string recordId, string dateCrash, string device, string status)
		{
			InitializeComponent();

			_recordId = recordId;
			DateCrashLabel.Content = dateCrash;
			DeviceLabel.Content = device;
			StatusLabel.Content = status;
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void StartWorkButton_Click(object sender, RoutedEventArgs e)
		{
			UpdateStatus("В работе");
		}

		private void UpdateStatus(string newStatus)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(_connectionString))
				{
					connection.Open();
					string query = "UPDATE [Technical_Service].[dbo].[Crash] SET Status = @Status WHERE ID = @ID";
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@Status", newStatus);
						command.Parameters.AddWithValue("@ID", _recordId);

						int rowsAffected = command.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							MessageBox.Show($"Статус обновлен на '{newStatus}'", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
							StatusLabel.Content = newStatus;
						}
						else
						{
							MessageBox.Show("Не удалось обновить статус.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
						}
					}
				}
			}
			catch (SqlException ex)
			{
				MessageBox.Show($"Ошибка работы с базой данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
