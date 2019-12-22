using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MyNotes.Infrastructure;
using MyNotes.Infrastructure.Configuration;
using MyNotes.Services.Interfaces;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MyNotes.ViewModels
{
	public class MainViewVM : ViewModelBase
	{
		private IContactManagerService _contactManagerService;
		private ILogger<MainViewVM> _logger;
		private ContactDto _chosenContact;
		private bool _isContactSelected = false;
		private AlphabetSearchManager _alphabetSearchManager;
		private ObservableCollection<AlphabetItem> _searchAlphabet;
		private ObservableCollection<ContactDto> _contacts;
		private string _searchQuery;
		private DelegateCommand _searchContacts;
		private bool _birthdaySoonIsChecked;
		private IEnumerable<ContactDto> _foundContacts;

		public MainViewVM(
			IContactManagerService contactManagerService,
			ILogger<MainViewVM> logger,
			AlphabetSearchManager alphabetSearchManager)
		{
			_contactManagerService = contactManagerService;
			_logger = logger;
			AddContactCommand = new DelegateCommand(Add);
			RemoveContactCommand = new DelegateCommand(Remove);
			EditContactCommand = new DelegateCommand(Edit);
			ShowContactCommand = new DelegateCommand(Show);
			SearchContactsCommand = new DelegateCommand(Search);
			_alphabetSearchManager = alphabetSearchManager;

			EnableMenuButtons = new DelegateCommand(
				() => { IsContactSelected = true; });
			SearchByFirstLetterCommand = new DelegateCommand<object>(SelectButton);

			ShowAllContacts();
		}

		public void SelectButton(object c)
		{
			char selectedLetter = ((AlphabetItem)c).Letter;
			Contacts = new ObservableCollection<ContactDto>(
					_foundContacts.Where(x => x.Name.ToUpper().StartsWith(selectedLetter)));
		}

		#region ui
		public string Title { get; set; } = "MyNotes";

		public bool BirthDaySoonIsChecked
		{
			get
			{
				return _birthdaySoonIsChecked;
			}
			set
			{
				_birthdaySoonIsChecked = value;
				Search();
			}
		}

		public string SearchQuery
		{
			get
			{
				return _searchQuery;
			}
			set
			{
				_searchQuery = value;
				RaisePropertyChanged("SearchQuery");
			}
		}

		public ObservableCollection<AlphabetItem> SearchAlphabet
		{
			get
			{
				return _searchAlphabet;
			}
			set
			{
				_searchAlphabet = value;
				RaisePropertyChanged("SearchAlphabet");
			}
		}

		public ObservableCollection<ContactDto> Contacts
		{
			get
			{
				return _contacts;
			}
			set
			{
				_contacts = value;
				RaisePropertyChanged("Contacts");
			}
		}

		public bool IsContactSelected
		{
			get
			{
				return _isContactSelected;
			}
			set
			{
				_isContactSelected = value;
				RaisePropertyChanged("IsContactSelected");
			}
		}

		public ContactDto SelectedContact
		{
			get
			{
				return _chosenContact;
			}
			set
			{
				_chosenContact = value;
				RaisePropertyChanged("SelectedContact");
			}
		}

		#endregion


		#region commands
		public DelegateCommand<object> SearchByFirstLetterCommand
		{
			get;
		}

		public DelegateCommand EnableMenuButtons { get; }

		public DelegateCommand SearchContactsCommand
		{
			get
			{
				return _searchContacts;
			}
			set
			{
				_searchContacts = value;
				RaisePropertyChanged(nameof(SearchContactsCommand));
			}
		}
		public DelegateCommand RemoveContactCommand { get; set; }
		public DelegateCommand EditContactCommand { get; set; }
		public DelegateCommand ShowContactCommand { get; set; }
		public DelegateCommand AddContactCommand { get; set; }

		#endregion


		#region methods

		void Add()
		{
			var viewModel = new ContactVM(new ContactDto(), _contactManagerService,
				ServiceProviderSingleton.ServiceProvider.GetRequiredService<ILogger<ContactVM>>(), true);
			WindowManager.OpenNewDialogWindow(viewModel, ModelAction.Add);
			Search();
			IsContactSelected = false;
		}

		void Remove()
		{
			var viewModel = new ContactVM(SelectedContact, _contactManagerService, 
				ServiceProviderSingleton.ServiceProvider.GetRequiredService<ILogger<ContactVM>>(), false);
			WindowManager.OpenNewDialogWindow(viewModel, ModelAction.Remove);
			Search();
			IsContactSelected = false;
		}

		void Edit()
		{
			var viewModel = new ContactVM(SelectedContact, _contactManagerService, 
				ServiceProviderSingleton.ServiceProvider.GetRequiredService<ILogger<ContactVM>>(), true);
			WindowManager.OpenNewDialogWindow(viewModel, ModelAction.Change);
			Search();
			IsContactSelected = false;
		}

		void Show()
		{
			var viewModel = new ContactVM(SelectedContact, _contactManagerService, 
				ServiceProviderSingleton.ServiceProvider.GetRequiredService<ILogger<ContactVM>>(), false);
			WindowManager.OpenNewDialogWindow(viewModel, ModelAction.Show);
			IsContactSelected = true;
		}

		void ShowAllContacts()
		{
			var contacts = _contactManagerService.GetContactsByQuery(null, false)
				.GetAwaiter().GetResult();
			Contacts = new ObservableCollection<ContactDto>(contacts);
			_foundContacts = Contacts;

			var availableLetters = _alphabetSearchManager.SetAvailableLetters(
				contacts.Select(x => x.Name));
			SearchAlphabet = new ObservableCollection<AlphabetItem>(
				availableLetters.Select(x => new AlphabetItem { Letter = x.Key, IsEnabled = x.Value }));
		}

		void Search()
		{
			IEnumerable<ContactDto> contacts;

			if (String.IsNullOrEmpty(SearchQuery))
			{
				Contacts = new ObservableCollection<ContactDto>(
					_contactManagerService.GetContactsByQuery(null, BirthDaySoonIsChecked)
						.GetAwaiter().GetResult());
			}
			else
			{
				Contacts = new ObservableCollection<ContactDto>(
					_contactManagerService.GetContactsByQuery(SearchQuery, BirthDaySoonIsChecked)
						.GetAwaiter().GetResult());
			}

			_foundContacts = Contacts.Select(x => x);
			_alphabetSearchManager.SetAvailableLetters(Contacts.Select(x => x.Name));
			foreach ((AlphabetItem oldAlphabetItem, AlphabetItem newAlphabetItem) in SearchAlphabet.Zip(
				_alphabetSearchManager.AvailableLetters
					.Select(x => new AlphabetItem
					{
						Letter = x.Key,
						IsEnabled = x.Value
					})))
			{
				if (oldAlphabetItem.IsEnabled != newAlphabetItem.IsEnabled)
				{
					oldAlphabetItem.IsEnabled = newAlphabetItem.IsEnabled;
				}
			}
		}

		#endregion
	}
}
