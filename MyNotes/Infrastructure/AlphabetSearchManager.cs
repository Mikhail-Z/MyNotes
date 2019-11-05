using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Infrastructure
{
	public class AlphabetSearchManager
	{
		public IDictionary<char, bool> AvailableLetters { private set; get; } = new Dictionary<char, bool>();

		public IDictionary<char, bool> SetAvailableLetters(IEnumerable<string> names)
		{
			foreach (var name in names)
			{
				AvailableLetters[name[0]] = true;
			}

			return AvailableLetters;
		}

		public AlphabetSearchManager(string alphabet)
		{
			foreach (var letter in alphabet) 
			{
				AvailableLetters[letter] = false;
			}
		}
	}
}
