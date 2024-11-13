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
	/// Логика взаимодействия для EditWindow_Monitor_TO.xaml
	/// </summary>
	public partial class EditWindow_Monitor_TO : Window
	{
		private DataRowView _dataRow;
		public EditWindow_Monitor_TO(DataRowView dataRow)
		{
			InitializeComponent();
			_dataRow = dataRow;
			idTextBox.Text = _dataRow["id"].ToString();
			ffffTextBox.Text = _dataRow["ffff"].ToString();
			rrrrTextBox.Text = _dataRow["rrrr"].ToString();
			
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			var newFfffValue = ffffTextBox.Text;
			var newRrrrValue = rrrrTextBox.Text;
			int idValue = Convert.ToInt32(idTextBox.Text); // предполагаем, что id — это int

			// Обновляем данные в базе данных
			// Укажите строку подключения к вашей базе данных

			using (SqlConnection connection = new SqlConnection(WC.ConnectionString))
			{
				connection.Open();
				string updateQuery = "UPDATE [test_table] SET [ffff] = @ffff, [rrrr] = @rrrr WHERE [id] = @id";

				using (SqlCommand command = new SqlCommand(updateQuery, connection))
				{
					command.Parameters.AddWithValue("@ffff", newFfffValue);
					command.Parameters.AddWithValue("@rrrr", newRrrrValue);
					command.Parameters.AddWithValue("@id", idValue);

					try
					{
						int rowsAffected = command.ExecuteNonQuery();
						if (rowsAffected > 0)
						{
							MessageBox.Show("Изменения успешно сохранены!");
						}
						else
						{
							MessageBox.Show("Не удалось сохранить изменения.");
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Произошла ошибка: " + ex.Message);
					}
				}
			}

			
			


			this.Close();
		}

	}
}
