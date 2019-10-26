using Microsoft.Extensions.Logging;
using MyNotes.Commands;
using MyNotes.Domain.Entities;
using MyNotes.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;
using System.Windows.Input;

namespace MyNotes.ViewModels
{
	public class MainViewVM : ViewModelBase
	{
		private IContactManagerService _contactManagerService;
		private ILogger<MainViewVM> _logger;
		private string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private string _title = "MyNotes";
		private bool _isContactChosen = false;

		

		public bool IsContactChosen
		{
			get
			{
				return _isContactChosen;
			}
			set
			{
				_isContactChosen = value;
				RaisePropertyChanged("IsContactChosen");
			}

		}

		public DelegateCommand ChooseContact { get; set; }
		public DelegateCommand AddContact { get; set; }
		public DelegateCommand RemoveContact { get; set; }
		public DelegateCommand EditContact { get; set; }
		public DelegateCommand ShowContact { get; set; }

		public Contact ChosenContactInfo { get; private set; }

		public MainViewVM()
		{
			ChooseContact = new DelegateCommand(Choose);
			AddContact = new DelegateCommand(Add);
			RemoveContact = new DelegateCommand(Remove);
			EditContact = new DelegateCommand(Edit);
			ShowContact = new DelegateCommand(Show);
		}	

		public ObservableCollection<Contact> Contacts { get; private set; }


		public event PropertyChangedEventHandler PropertyChanged;

		public virtual void OnPropertyChanged

		public MainViewVM(IContactManagerService contactManagerService, ILogger<MainViewVM> logger)
		{
			_contactManagerService = contactManagerService;
			_logger = logger;

			Contacts = new ObservableCollection<Contact>(_contactManagerService.GetAllContacts());
		}

		void Add()
		{
			WindowManager.OpenNewDialogWindow(ChosenContactInfo, ModelAction.Add);
		}

		void Remove()
		{
			WindowManager.OpenNewDialogWindow(ChosenContactInfo, ModelAction.Remove);
		}

		void Edit()
		{
			WindowManager.OpenNewDialogWindow(ChosenContactInfo, ModelAction.Change);
		}

		void Show()
		{

		}

		void Choose()
		{
			WindowManager.OpenNewDialogWindow(ChosenContactInfo, ModelAction.Choose);
		}
	}
}
