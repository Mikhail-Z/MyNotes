using Microsoft.Extensions.Logging;
using MyNotes.Infrastructure;
using MyNotes.Services.Interfaces;
using MyNotes.Utils;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace MyNotes.ViewModels
{
	public class ContactVM : BindableBase, IDataErrorInfo
	{
		private ContactDto _contact;
		private IContactManagerService _contactManagerService;
		private ILogger<ContactVM> _logger;
		private string _error;
		private bool _isActionEnabled;

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
				_contact.Name = StringUtils.InsignificantStringToNull(value);
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
				_contact.HomePhones = StringUtils.InsignificantStringToNull(value);
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
				_contact.WorkPhones = StringUtils.InsignificantStringToNull(value);
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
				_contact.Emails = StringUtils.InsignificantStringToNull(value);
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
				_contact.Skypes = StringUtils.InsignificantStringToNull(value);
				RaisePropertyChanged(nameof(Skypes));
			}
		}

		public bool IsActionEnabled
		{
			get
			{
				return _isActionEnabled;
			}
			private set
			{
				_isActionEnabled = value;
				RaisePropertyChanged(nameof(IsActionEnabled));
			}
		}

		public string BirthDay
		{
			get
			{
				return _contact.BirthDay;
			}
			set
			{
				_contact.BirthDay = value;
				RaisePropertyChanged(nameof(BirthDay));
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

		public IDictionary<string, string> ValidationErrorMessages { get; private set; } 
			= new Dictionary<string, string>();

		#endregion

		#region validation

		public string Error
		{
			get
			{
				return _error;
			}
			set
			{
				_error = value;
				IsActionEnabled = _error != null && _error == "";
			}
		}

		public string this[string columnName]
		{
			get
			{
				string errorMessage = String.Empty;

				if (columnName == nameof(Name))
				{
					if (StringUtils.IsNullOrEmptyOrWhiteSpace(Name))
					{
						errorMessage = "Имя должно быть непустой строкой и содержать только символы a-z, A-Z, _ и пробелы";
					}
				}
				else if (columnName == nameof(BirthDay))
				{
					if (StringUtils.IsNullOrEmptyOrWhiteSpace(BirthDay) == false)
					{
						bool isDateTime = DateTime.TryParseExact(
							BirthDay,
							"dd.MM.yyyy",
							CultureInfo.InvariantCulture,
							DateTimeStyles.AssumeLocal,
							out var birthDayDateTime);

						if (isDateTime == false)
						{
							errorMessage = "День Рождения должен иметь формат дд.мм.гггг";
						}
						else if (birthDayDateTime < new DateTime(1900, 1, 1) || birthDayDateTime > DateTime.Now)
						{
							errorMessage = "День рождения может быть после 1900г. и до сегодняшнего дня";
						}
					}
				}

				if (errorMessage == "")
				{
					ValidationErrorMessages.Remove(columnName);
				}
				else if (errorMessage != null)
				{
					ValidationErrorMessages[columnName] = errorMessage;
				}

				IsActionEnabled = ValidationErrorMessages.Count == 0;
				return errorMessage;
			}
		}

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

		#region regex

		private Regex _valuesArrayRegex = new Regex(@"(.+;\s*)*.+\s");
		private Regex _nameRegex = new Regex(@"[\w ]+");

		#endregion
	}
}