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
		private ContactDto _chosenContact = null;
		private bool _IsContactSelected = false;
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
			SearchByFirstLetterCommand = new DelegateCommand(() =>
			{
				Contacts = new ObservableCollection<ContactDto>(
					_foundContacts.Where(x => x.Name.StartsWith(SelectedLetter)));
			});
			UpdateContactList();
		}


		#region ui
		public string Title { get; set; } = "MyNotes";

		public char SelectedLetter { 
		   get; 
		   set; 
		}

		public bool BirthdaySoonIsChecked
		{
			get
			{
				return _birthdaySoonIsChecked;
			}
			set
			{
				_birthdaySoonIsChecked = value;
				RaisePropertyChanged("BirthdaySoonIsChecked");
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
				return _IsContactSelected;
			}
			set
			{
				_IsContactSelected = value;
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
		public DelegateCommand SearchByFirstLetterCommand
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
				ServiceProviderFactory.ServiceProvider.GetRequiredService<ILogger<ContactVM>>(), true);
			WindowManager.OpenNewDialogWindow(viewModel, ModelAction.Add);
			Search();
		}

		void Remove()
		{
			var viewModel = new ContactVM(SelectedContact, _contactManagerService, 
				ServiceProviderFactory.ServiceProvider.GetRequiredService<ILogger<ContactVM>>(), false);
			WindowManager.OpenNewDialogWindow(viewModel, ModelAction.Remove);
			Search();
		}

		void Edit()
		{
			var viewModel = new ContactVM(SelectedContact, _contactManagerService, 
				ServiceProviderFactory.ServiceProvider.GetRequiredService<ILogger<ContactVM>>(), true);
			WindowManager.OpenNewDialogWindow(viewModel, ModelAction.Change);
			Search();
		}

		void Show()
		{
			var viewModel = new ContactVM(SelectedContact, _contactManagerService, 
				ServiceProviderFactory.ServiceProvider.GetRequiredService<ILogger<ContactVM>>(), false);
			WindowManager.OpenNewDialogWindow(viewModel, ModelAction.Show);
		}

		void UpdateContactList()
		{
			var contacts = _contactManagerService.GetAllContacts(false).GetAwaiter().GetResult();
			Contacts = new ObservableCollection<ContactDto>(contacts);
			_foundContacts = Contacts;

			var availableLetters = _alphabetSearchManager.SetAvailableLetters(
				contacts.Select(x => x.Name));
			SearchAlphabet = new ObservableCollection<AlphabetItem>(
				availableLetters.Select(x => new AlphabetItem { Letter = x.Key, IsEnabled = x.Value }));
		}

		void Search()
		{
			IsContactSelected = false;

			IEnumerable<ContactDto> contacts;

			if (String.IsNullOrEmpty(SearchQuery))
			{
				Contacts = new ObservableCollection<ContactDto>(
					_contactManagerService.GetAllContacts(BirthdaySoonIsChecked)
						.GetAwaiter().GetResult());
			}
			else
			{
				Contacts = new ObservableCollection<ContactDto>(
					_contactManagerService.GetContactsByQuery(SearchQuery, BirthdaySoonIsChecked)
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
