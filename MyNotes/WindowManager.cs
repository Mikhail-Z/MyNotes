﻿using MyNotes.ViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MyNotes
{
	public class WindowManager
	{
		private static IDictionary<Tuple<Type, ModelAction>, Type> _windows = 
			new Dictionary<Tuple<Type, ModelAction>, Type>();

		public static void Register(Type modelType, ModelAction action, Type viewType)
		{
			_windows[new Tuple<Type, ModelAction>(modelType, action)] = viewType;
		}

		public static void OpenNewDialogWindow(object viewModel, ModelAction action)
		{
			var viewType = _windows[new Tuple<Type, ModelAction>(viewModel.GetType(), action)];
			Window newWindow;
			newWindow = Activator.CreateInstance(viewType) as Window;
			if (viewModel.GetType() == typeof(ContactVM))
			{
				((ContactVM)viewModel).CancelCommand = new DelegateCommand(newWindow.Close);
			}
			newWindow.DataContext = viewModel;

			newWindow.ShowDialog();
		}
	}
}
