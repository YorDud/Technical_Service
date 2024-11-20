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
		private string _scheduleData;
		public Obrabotka_Raspisanie(string scheduleData)
		{
			InitializeComponent();

			_scheduleData = scheduleData;
			ScheduleTextBox.Text = _scheduleData;

			// Обработка сохранения данных в БД
			SaveScheduleToDatabase();
		}
		private void SaveScheduleToDatabase()
		{
			// Замените на вашу строку подключения

			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				connection.Open();

				DateTime currentDate = DateTime.Now;
				DateTime endDate = currentDate.AddYears(1);

				foreach (DateTime date in GenerateDates(currentDate, endDate))
				{
					string query = "INSERT INTO [Raspisanie] (DateValue, Description) VALUES (@DateValue, @Description)";
					using (SqlCommand command = new SqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@DateValue", date);
						command.Parameters.AddWithValue("@Description", _scheduleData);
						command.ExecuteNonQuery();
					}
				}
			}

			MessageBox.Show("Расписание успешно сохранено в базе данных!");
		}

		private IEnumerable<DateTime> GenerateDates(DateTime startDate, DateTime endDate)
		{
			// Логика генерации дат на основе переданного расписания.
			// Например:
			for (DateTime date = startDate; date < endDate; date = date.AddDays(1))
			{
				// Здесь добавьте логику, чтобы проверить каждый день недели и месяц,
				// а затем добавьте соответствующие даты в результат.
				yield return date; // пример возврата всех дат
			}
		}
	}
}
