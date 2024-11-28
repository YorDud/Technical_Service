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
	/// Логика взаимодействия для Crash_Create_Wndow.xaml
	/// </summary>
	public partial class Crash_Create_Wndow : Window
	{
		private MainWindow mainWindow;

		public Crash_Create_Wndow(MainWindow mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;

		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Create_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
