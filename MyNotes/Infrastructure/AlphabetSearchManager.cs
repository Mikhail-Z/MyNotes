using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Infrastructure
{
	public class AlphabetSearchManager
	{
		private string _alphabet;
		public IDictionary<char, bool> AvailableLetters { private set; get; }
			= new Dictionary<char, bool>();


		public IDictionary<char, bool> SetAvailableLetters(IEnumerable<string> names)
		{
			DisableAvailableLetters();
			foreach (var name in names)
			{
				char upperLetter;
				if (Char.IsLetter(name[0]))
				{
					upperLetter = name.ToUpper()[0];
				}
				else
				{
					upperLetter = name[0];
				}
				AvailableLetters[upperLetter] = true;
			}

			return AvailableLetters;
		}

		public AlphabetSearchManager(string alphabet)
		{
			_alphabet = alphabet;
			DisableAvailableLetters();
		}

		private void DisableAvailableLetters()
		{
			foreach (var letter in _alphabet)
			{
				AvailableLetters[letter] = false;
			}
		}
	}
}
