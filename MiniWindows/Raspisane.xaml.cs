using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp4.MiniWindows
{
	public partial class Raspisane : Window
	{
		public Raspisane()
		{
			InitializeComponent();
		}

		private void DayOfWeekChecked(object sender, RoutedEventArgs e)
		{
			// Когда выбран день недели, нужно отключить дни месяца
			SpecificDaysTextBox.IsEnabled = false;
		}

		private void DayOfWeekUnchecked(object sender, RoutedEventArgs e)
		{
			// Когда день недели снят, включаем дни месяца
			if (!AreAnyDayOfWeekChecked())
			{
				SpecificDaysTextBox.IsEnabled = true;
			}
		}

		private bool AreAnyDayOfWeekChecked()
		{
			return MondayCheckBox.IsChecked == true ||
				   TuesdayCheckBox.IsChecked == true ||
				   WednesdayCheckBox.IsChecked == true ||
				   ThursdayCheckBox.IsChecked == true ||
				   FridayCheckBox.IsChecked == true ||
				   SaturdayCheckBox.IsChecked == true ||
				   SundayCheckBox.IsChecked == true;
		}

		private void CreateScheduleButton_Click(object sender, RoutedEventArgs e)
		{
			// Сбор данных из UI
			var selectedDays = new[] {
				MondayCheckBox.IsChecked == true ? "Пн" : null,
				TuesdayCheckBox.IsChecked == true ? "Вт" : null,
				WednesdayCheckBox.IsChecked == true ? "Ср" : null,
				ThursdayCheckBox.IsChecked == true ? "Чт" : null,
				FridayCheckBox.IsChecked == true ? "Пт" : null,
				SaturdayCheckBox.IsChecked == true ? "Сб" : null,
				SundayCheckBox.IsChecked == true ? "Вс" : null
			}.Where(day => day != null).ToList();

			var selectedMonths = new[] {
				JanuaryCheckBox.IsChecked == true ? "Январь" : null,
				FebruaryCheckBox.IsChecked == true ? "Февраль" : null,
				MarchCheckBox.IsChecked == true ? "Март" : null,
                // остальные месяцы
            }.Where(month => month != null).ToList();

			int.TryParse(RepeatWeeksTextBox.Text, out int repeatWeeks);
			int.TryParse(SpecificDaysTextBox.Text, out int specificDay);
			//int.TryParse(WeekDaysInMonthTextBox.Text, out int weekDayInMonth);

			// Формирование строки расписания
			string schedule = $"Дни недели: {string.Join(", ", selectedDays)}; " +
							  $"Месяцы: {string.Join(", ", selectedMonths)}; " +
							  $"Повторять каждые: {repeatWeeks} недель; " +
							  $"День месяца: {specificDay}; ";
							  //$"День недели в месяце: {weekDayInMonth}";

			// Запись расписания в БД
			string query = "INSERT INTO Types_TO (Raspisanie) VALUES (@Schedule)";
			using (var connection = new SqlConnection(WC.ConnectionString))
			{
				connection.Open();
				using (var command = new SqlCommand(query, connection))
				{
					command.Parameters.AddWithValue("@Schedule", schedule);
					command.ExecuteNonQuery();
				}
			}

			MessageBox.Show("Расписание успешно сохранено!");
		}
	}
}
