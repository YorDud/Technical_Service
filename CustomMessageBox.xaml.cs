using System;
using System.Collections.Generic;
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

namespace WpfApp4
{
	/// <summary>
	/// Логика взаимодействия для CustomMessageBox.xaml
	/// </summary>
	public partial class CustomMessageBox : Window
	{
		public string TitleTextContent { get; set; }
		public string MessageTextContent { get; set; }
		public bool IsCancelVisible => ButtonCancel.Visibility == Visibility.Visible;

		/// <summary>
		/// Конструктор для MessageBox с одной кнопкой "OK".
		/// </summary>
		public CustomMessageBox(string title, string message)
			: this(title, message, false)
		{
		}

		/// <summary>
		/// Конструктор для MessageBox с кнопками "OK" и "Cancel".
		/// </summary>
		public CustomMessageBox(string title, string message, bool showCancel)
		{
			InitializeComponent();
			TitleTextContent = title;
			MessageTextContent = message;
			ButtonCancel.Visibility = showCancel ? Visibility.Visible : Visibility.Collapsed;
			DataContext = this;
		}

		private void OK_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
			this.Close();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			this.Close();
		}
	}
}

