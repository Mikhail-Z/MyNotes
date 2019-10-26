using MyNotes.Domain.Entities;
using MyNotes.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyNotes
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow(MainViewVM mainViewVM)
		{
			InitializeComponent();
			DataContext = mainViewVM;


			var alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			var i = 0;
			var curColumn = 0;
			var curRow = 0;
			foreach (var letter in alphabet)
			{
				Button newBtn = new Button();
				newBtn.Content = letter;
				if (i % 2 == 0)
				{
					newBtn.IsEnabled = false;
				}
				if (i == 13)
				{
					curColumn = 1;
					curRow = 0;
				}
				Grid.SetRow(newBtn, curRow);
				Grid.SetColumn(newBtn, curColumn);
				newBtn.Style = this.FindResource("LetterBtnStyle") as Style;
				AlphabetButtonsGrid.Children.Add(newBtn);
				i++;
				curRow++;
			}
		}
	}
}
