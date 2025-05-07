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
	/// Логика взаимодействия для Obr_Rasp.xaml
	/// </summary>
	public partial class Obr_Rasp : Window
	{
		public Obr_Rasp()
		{
			InitializeComponent();

			LoadIds();
		}

		private void LoadIds()
		{
			string query = "SELECT Id FROM Types_TO";

			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				SqlCommand command = new SqlCommand(query, connection);
				connection.Open();
				using (SqlDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						IdComboBox.Items.Add(reader["Id"].ToString());
					}
				}
			}

			if (IdComboBox.Items.Count > 0)
				IdComboBox.SelectedIndex = 0; // Выбираем первый элемент по умолчанию
		}

		// Метод обработки расписания
		private void ProcessScheduleButton_Click(object sender, RoutedEventArgs e)
		{
			if (IdComboBox.SelectedItem == null)
			{
				ResultTextBox.Text = "Пожалуйста, выберите ID.";
				return;
			}

			string selectedId = IdComboBox.SelectedItem.ToString();

			string selectQuery = "SELECT Raspisanie FROM Types_TO WHERE Id = @Id";
			string insertQuery = "INSERT INTO Naryad (Date_TO) VALUES (@Date_TO)";

			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				SqlCommand selectCommand = new SqlCommand(selectQuery, connection);
				selectCommand.Parameters.AddWithValue("@Id", selectedId);
				connection.Open();

				// Получаем расписание
				string schedule = selectCommand.ExecuteScalar()?.ToString();
				if (string.IsNullOrEmpty(schedule))
				{
					ResultTextBox.Text = $"Нет расписания для обработки по ID {selectedId}.";
					return;
				}

				// Парсинг расписания
				var parsedSchedule = ParseSchedule(schedule);

				// Определяем текущую дату и конечную дату (1 год вперед)
				DateTime currentDate = DateTime.Now;
				DateTime endDate = currentDate.AddYears(1);

				while (currentDate <= endDate)
				{
					// Проверяем, соответствует ли текущая дата расписанию
					if (IsDateMatchingSchedule(currentDate, parsedSchedule))
					{
						// Записываем дату в таблицу Naryad
						using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
						{
							insertCommand.Parameters.AddWithValue("@Date_TO", currentDate);
							insertCommand.ExecuteNonQuery();
						}
					}

					// Переходим к следующему дню
					currentDate = currentDate.AddDays(1);
				}
			}

			ResultTextBox.Text = $"Расписание по ID {selectedId} обработано и записано.";
		}

		// Обработка события изменения выбранного элемента в ComboBox
		private void IdComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			ResultTextBox.Text = string.Empty; // Очищаем текстовое поле при выборе нового ID
		}




		private (List<DayOfWeek> DaysOfWeek, List<int> Months, int RepeatWeeks, List<int> SpecificDays, int WeekDayInMonth) ParseSchedule(string schedule)
		{
			var daysOfWeek = new List<DayOfWeek>();
			var months = new List<int>();
			var specificDays = new List<int>();
			int repeatWeeks = 0, weekDayInMonth = 0;

			// Разделяем расписание на части
			var parts = schedule.Split(';');
			foreach (var part in parts)
			{
				var cleanedPart = part.Trim();

				if (cleanedPart.StartsWith("Дни недели:"))
				{
					var days = cleanedPart.Replace("Дни недели:", "").Split(',').Select(d => d.Trim());
					foreach (var day in days)
					{
						switch (day)
						{
							case "Пн": daysOfWeek.Add(DayOfWeek.Monday); break;
							case "Вт": daysOfWeek.Add(DayOfWeek.Tuesday); break;
							case "Ср": daysOfWeek.Add(DayOfWeek.Wednesday); break;
							case "Чт": daysOfWeek.Add(DayOfWeek.Thursday); break;
							case "Пт": daysOfWeek.Add(DayOfWeek.Friday); break;
							case "Сб": daysOfWeek.Add(DayOfWeek.Saturday); break;
							case "Вс": daysOfWeek.Add(DayOfWeek.Sunday); break;
						}
					}
				}
				else if (cleanedPart.StartsWith("Месяцы:"))
				{
					var monthsStr = cleanedPart.Replace("Месяцы:", "").Split(',').Select(m => m.Trim());
					foreach (var month in monthsStr)
					{
						switch (month)
						{
							case "Январь": months.Add(1); break;
							case "Февраль": months.Add(2); break;
							case "Март": months.Add(3); break;
							case "Апрель": months.Add(4); break;
							case "Май": months.Add(5); break;
							case "Июнь": months.Add(6); break;
							case "Июль": months.Add(7); break;
							case "Август": months.Add(8); break;
							case "Сентябрь": months.Add(9); break;
							case "Октябрь": months.Add(10); break;
							case "Ноябрь": months.Add(11); break;
							case "Декабрь": months.Add(12); break;
						}
					}
				}
				else if (cleanedPart.StartsWith("Повторять каждые:"))
				{
					int.TryParse(cleanedPart.Replace("Повторять каждые:", "").Replace("недель", "").Trim(), out repeatWeeks);
				}
				else if (cleanedPart.StartsWith("День месяца:"))
				{
					var days = cleanedPart.Replace("День месяца:", "").Split(',').Select(d => d.Trim());
					foreach (var day in days)
					{
						if (int.TryParse(day, out int specificDay) && specificDay != 0)
							specificDays.Add(specificDay);
					}
				}
				else if (cleanedPart.StartsWith("День недели в месяце:"))
				{
					int.TryParse(cleanedPart.Replace("День недели в месяце:", "").Trim(), out weekDayInMonth);
				}
			}

			return (daysOfWeek, months, repeatWeeks, specificDays, weekDayInMonth);
		}


		private bool IsDateMatchingSchedule(DateTime date, (List<DayOfWeek> DaysOfWeek, List<int> Months, int RepeatWeeks, List<int> SpecificDays, int WeekDayInMonth) schedule)
		{
			// Проверяем дни недели
			if (schedule.DaysOfWeek.Count > 0 && !schedule.DaysOfWeek.Contains(date.DayOfWeek))
				return false;

			// Проверяем месяцы
			if (schedule.Months.Count > 0 && !schedule.Months.Contains(date.Month))
				return false;

			// Проверяем частоту повторения (например, каждую неделю)
			if (schedule.RepeatWeeks > 0)
			{
				int weekDifference = (int)((date - DateTime.Now.Date).TotalDays / 7);
				if (weekDifference % schedule.RepeatWeeks != 0)
					return false;
			}

			// Проверяем конкретные дни месяца
			if (schedule.SpecificDays.Count > 0 && !schedule.SpecificDays.Contains(date.Day))
				return false;

			// Проверяем "день недели в месяце", если он задан
			if (schedule.WeekDayInMonth > 0)
			{
				int weekOfMonth = (date.Day - 1) / 7 + 1;
				if (weekOfMonth != schedule.WeekDayInMonth)
					return false;
			}

			return true;
		}









	}
}
