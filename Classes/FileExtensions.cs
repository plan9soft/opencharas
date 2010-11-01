using System;
using Microsoft.Win32;
using System.Collections.Generic;

namespace Paril.Windows.Registry
{
	/// <summary>
	/// A container class that holds actions and whatnot
	/// </summary>
	public class FileExtensionActions
	{
		List<string> _openedPrograms = new List<string>();

		public FileExtensionActions(string keyLocation, string extension)
		{
			using (RegistryKey mainKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(keyLocation + extension))
			{
				if (mainKey == null)
					return;

				// Check for an OpenWithProgids list
				using (RegistryKey openWithProdidsListKey = mainKey.OpenSubKey("OpenWithProgids"))
				{
					if (openWithProdidsListKey != null)
					{
						foreach (var v in openWithProdidsListKey.GetValueNames())
							_openedPrograms.Add(v);
					}
				}
			}
		}
	}

	/// <summary>
	/// Main file extension class
	/// </summary>
	public class FileExtension
	{
		string _extension;

		// CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\
		Dictionary<char, string> _recommendedPrograms = new Dictionary<char, string>();
		List<string> _openProgIDs = new List<string>();
		string _userChoice;
		string _recommendedList;

		public FileExtension(string extension)
		{
			_extension = extension;

			using (RegistryKey mainKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\FileExts"))
			{
				using (RegistryKey extKey = mainKey.OpenSubKey(extension))
				{
					if (extKey != null)
					{
						// Check for an OpenWithList
						using (RegistryKey openWithListKey = extKey.OpenSubKey("OpenWithList"))
						{
							if (openWithListKey != null)
							{
								foreach (var v in openWithListKey.GetValueNames())
								{
									if (v.ToLower() == "mrulist")
										_recommendedList = (string)openWithListKey.GetValue(v);
									else
										_recommendedPrograms.Add(v[0], (string)openWithListKey.GetValue(v));
								}
							}
						}
		
						// Check for an OpenWithProgids list
						using (RegistryKey openWithProdidsListKey = extKey.OpenSubKey("OpenWithProgids"))
						{
							if (openWithProdidsListKey != null)
							{
								foreach (var v in openWithProdidsListKey.GetValueNames())
									_openProgIDs.Add(v);
							}
						}

						// see if we have a main user choice
						using (RegistryKey userChoice = extKey.OpenSubKey("UserChoice"))
						{
							if (userChoice != null)
								_userChoice = (string)userChoice.GetValue(userChoice.GetValueNames()[0]);
						}
					}
				}
			}
		}
	}
}
