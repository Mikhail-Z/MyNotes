using Microsoft.Extensions.Logging;
using MyNotes.Infrastructure;
using MyNotes.Services.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace MyNotes.ViewModels
{
	public class ContactVM : BindableBase
	{
		private ContactDto _contact;
		private IContactManagerService _contactManagerService;
		private ILogger<ContactVM> _logger;

		public ContactVM(
			ContactDto contact,
			IContactManagerService contactManagerService,
			ILogger<ContactVM> logger,
			bool isEditable)
		{
			_contact = contact;
			_contactManagerService = contactManagerService;
			_logger = logger;
			IsEditable = isEditable;
			RemoveCommand = new DelegateCommand(Remove);
			AddCommand = new DelegateCommand(Add);
			EditCommand = new DelegateCommand(Edit);
		}

		#region properties

		public string Name
		{
			get
			{
				return _contact.Name;
			}
			set
			{
				_contact.Name = value;
				RaisePropertyChanged(nameof(Name));
			}
		}

		public string HomePhones
		{
			get
			{
				return _contact.HomePhones;
			}
			set
			{
				_contact.Name = value;
				RaisePropertyChanged(nameof(HomePhones));
			}
		}

		public string WorkPhones
		{
			get
			{
				return _contact.WorkPhones;
			}
			set
			{
				_contact.Name = value;
				RaisePropertyChanged(nameof(WorkPhones));
			}
		}

		public string Emails
		{
			get
			{
				return _contact.Emails;
			}
			set
			{
				_contact.Emails = value;
				RaisePropertyChanged(nameof(Emails));
			}
		}

		public string Skypes
		{
			get
			{
				return _contact.Skypes;
			}
			set
			{
				_contact.Skypes = value;
				RaisePropertyChanged(nameof(Skypes));
			}
		}

		public DateTime? Birthday
		{
			get
			{
				return _contact.BirthDay;
			}
			set
			{
				_contact.BirthDay = value;
				RaisePropertyChanged(nameof(Birthday));
			}
		}

		public string Comment
		{
			get
			{
				return _contact.Comment;
			}
			set
			{
				_contact.Comment = value;
				RaisePropertyChanged(nameof(Comment));
			}
		}

		public bool IsEditable { private set; get; }

		#endregion


		#region commands

		public DelegateCommand RemoveCommand
		{
			get; set;
		}

		public DelegateCommand AddCommand
		{
			get; set;
		}

		public DelegateCommand EditCommand
		{
			get; set;
		}

		public DelegateCommand CancelCommand
		{
			get; set;
		}

		#endregion

		#region methods

		private void Add()
		{
			_contactManagerService.AddContact(_contact);
			if (CancelCommand.CanExecute())
			{
				CancelCommand.Execute();
			}
		}

		private void Remove()
		{
			_contactManagerService.RemoveContact(_contact);
			if (CancelCommand.CanExecute())
			{
				CancelCommand.Execute();
			}
		}

		private void Edit()
		{
			_contactManagerService.UpdateContact(_contact);
			if (CancelCommand.CanExecute())
			{
				CancelCommand.Execute();
			}
		}

		#endregion
	}
}