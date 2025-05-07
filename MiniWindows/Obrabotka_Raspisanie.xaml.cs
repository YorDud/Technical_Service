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
	/// Логика взаимодействия для Obrabotka_Raspisanie.xaml
	/// </summary>
	public partial class Obrabotka_Raspisanie : Window
	{
		
		public Obrabotka_Raspisanie()
		{
			InitializeComponent();

			
		}
		private void ProcessScheduleButton_Click(object sender, RoutedEventArgs e)
		{
			
			string selectQuery = "SELECT Raspisanie FROM Types_TO";
			string insertQuery = "INSERT INTO Naryad (Date_TO) VALUES (@Date_TO)";

			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
				connection.Open();

				string schedule = selectCommand.ExecuteScalar()?.ToString();
				if (schedule == null)
				{
					ResultTextBox.Text = "Нет расписания для обработки.";
					return;
				}

				// Пример обработки расписания
				DateTime currentDate = DateTime.Now;
				DateTime endDate = currentDate.AddYears(1);

				while (currentDate <= endDate)
				{
					// Ваша логика генерации дат (например, каждая неделя)
					currentDate = currentDate.AddDays(7);

					SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
					insertCommand.Parameters.AddWithValue("@Date_TO", currentDate);
					insertCommand.ExecuteNonQuery();
				}
			}

			ResultTextBox.Text = "Расписание обработано и записано.";
		}
	}
}
