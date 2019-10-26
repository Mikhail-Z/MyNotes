using MyNotes.Domain.Entities;
using MyNotes.Infrastructure;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Data;

namespace MyNotes.ViewModels
{
	class ContactInfoVM : BindableBase
	{
		private ContactInfoAdapter _contactInfoAdapter;
		public event PropertyChangedEventHandler PropertyChanged;

		private bool CanBeCreated()
		{
			return String.IsNullOrWhiteSpace(_contactInfoAdapter.Name) == false;
		}

		public ContactInfoVM(Contact contactInfo)
		{
			_contactInfoAdapter = new ContactInfoAdapter(contactInfo);
		}

		public string Name
		{
			get
			{
				return _contactInfoAdapter.Name;
			}
			set
			{
				_contactInfoAdapter.Name = value;
				OnPropertyChanged();
			}
		}

		public string HomePhones
		{
			get
			{
				return _contactInfoAdapter.HomePhones;
			}
			set
			{
				_contactInfoAdapter.HomePhones = value;
				OnPropertyChanged();
			}
		}

		public string WorkPhones
		{
			get
			{
				return _contactInfoAdapter.WorkPhones;
			}
			set
			{
				_contactInfoAdapter.WorkPhones = value;
				OnPropertyChanged();
			}
		}

		public string Emails
		{
			get
			{
				return _contactInfoAdapter.Emails;
			}
			set
			{
				_contactInfoAdapter.Emails = value;
				OnPropertyChanged();
			}
		}

		public string SkypeAccounts
		{
			get
			{
				return _contactInfoAdapter.SkypeAccounts;
			}
			set
			{
				_contactInfoAdapter.SkypeAccounts = value;
				OnPropertyChanged();
			}
		}

		public DateTime? BirthDay
		{
			get
			{
				return _contactInfoAdapter.BirthDay;
			}
			set
			{
				_contactInfoAdapter.BirthDay = value;
				OnPropertyChanged();
			}
		}

		public string Comment
		{
			get
			{
				return _contactInfoAdapter.Comment;
			}
			set
			{
				_contactInfoAdapter.Comment = value;
				OnPropertyChanged();
			}
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
