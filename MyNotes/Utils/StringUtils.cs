using System;
using System.Collections.Generic;
using System.Text;

namespace MyNotes.Utils
{
	public static class StringUtils
	{
		public static bool IsNullOrEmptyOrWhiteSpace(string s)
		{
			return String.IsNullOrWhiteSpace(s) || s.Length == 0;
		}
		
		public static string InsignificantStringToNull(string s)
		{
			return IsNullOrEmptyOrWhiteSpace(s) ? null : s;
		}
	}
}
