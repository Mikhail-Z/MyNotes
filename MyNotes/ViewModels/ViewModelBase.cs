using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.ViewModels
{
	public abstract class ViewModelBase : BindableBase
	{
		protected virtual void RegisterCommands() { }
	}
}
