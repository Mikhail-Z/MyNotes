using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace MyNotes
{
	public class WindowManager
	{
		private static IDictionary<Tuple<Type, ModelAction>, Type> _windows;

		public static void Register(Type modelType, ModelAction action, Type viewType)
		{
			_windows[new Tuple<Type, ModelAction>(modelType, action)] = viewType;
		}

		public static void OpenNewDialogWindow(object modelType, ModelAction action)
		{
			var viewType = _windows[new Tuple<Type, ModelAction>(modelType.GetType(), action)];
			var window = Activator.CreateInstance(viewType) as Window;
			window.ShowDialog();
		}
	}
}
