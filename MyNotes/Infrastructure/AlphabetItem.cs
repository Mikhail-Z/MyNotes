using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Prism.Mvvm;

namespace MyNotes.Infrastructure
{
	public class AlphabetItem : BindableBase
	{
		private char _letter;
		private bool _isEnabled;

		public char Letter
		{
			get
			{
				return _letter;
			}
			set
			{
				_letter = value;
				RaisePropertyChanged("Letter");
			}
		}

		public bool IsEnabled
		{
			get
			{
				return _isEnabled;
			}
			set
			{
				_isEnabled = value;
				RaisePropertyChanged("IsEnabled");
			}
		}
	}
}
