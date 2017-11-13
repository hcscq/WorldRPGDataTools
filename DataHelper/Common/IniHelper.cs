using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WorldRpgCommon
{
	public class IniHelper
	{
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		[DllImport("kernel32")]
		private static extern int GetPrivateProfileInt(string lpApplicationName, string lpKeyName, int nDefault, string lpFileName);

		[DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
		private static extern int GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer, int nSize, string filePath);

		[DllImport("KERNEL32.DLL ", CharSet = CharSet.Ansi)]
		private static extern int GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, int nSize, string filePath);

		public static void Write(string Section, string Key, string Value, string path)
		{
			IniHelper.WritePrivateProfileString(Section, Key, Value, path);
		}

		public static string Read(string Section, string Key, string path)
		{
			StringBuilder stringBuilder;
			stringBuilder = new StringBuilder(255);
			IniHelper.GetPrivateProfileString(Section, Key, "", stringBuilder, 255, path);
			return stringBuilder.ToString();
		}

		public static int GetAllSectionNames(out string[] sections, string path)
		{
			IntPtr intPtr;
			int privateProfileSectionNames;
			int result;
			string text;
			intPtr = Marshal.AllocCoTaskMem(32767);
			privateProfileSectionNames = IniHelper.GetPrivateProfileSectionNames(intPtr, 32767, path);
			if (privateProfileSectionNames == 0)
			{
				sections = null;
				result = -1;
			}
			else
			{
				text = Marshal.PtrToStringAnsi(intPtr, privateProfileSectionNames).ToString();
				Marshal.FreeCoTaskMem(intPtr);
				sections = text.Substring(0, text.Length - 1).Split(new char[1]);
				result = 0;
			}
			return result;
		}

		public static List<string> GetAllSectionNames(string path)
		{
			List<string> list;
			IntPtr intPtr;
			int privateProfileSectionNames;
			string text;
			list = new List<string>();
			intPtr = Marshal.AllocCoTaskMem(32767);
			privateProfileSectionNames = IniHelper.GetPrivateProfileSectionNames(intPtr, 32767, path);
			if (privateProfileSectionNames != 0)
			{
				text = Marshal.PtrToStringAnsi(intPtr, privateProfileSectionNames).ToString();
				Marshal.FreeCoTaskMem(intPtr);
				list.AddRange(text.Substring(0, text.Length - 1).Split(new char[1]));
			}
			return list;
		}

		public static int GetAllKeyValues(string section, out string[] keys, out string[] values, string path)
		{
			byte[] array;
			string @string;
			string[] array2;
			List<string> list;
			string[] array3;
			int i;
			string text;
			int j;
			string[] array4;
			array = new byte[65535];
			IniHelper.GetPrivateProfileSection(section, array, array.Length, path);
			@string = Encoding.Default.GetString(array);
			array2 = @string.Split(new char[1]);
			list = new List<string>();
			array3 = array2;
			i = 0;
			while (i < array3.Length)
			{
				text = array3[i];
				if (string.Equals(text, string.Empty))
				{
					list.Add(text);
				}
				i = i + 1;
			}
			keys = new string[list.Count];
			values = new string[list.Count];
			j = 0;
			while (j < list.Count)
			{
				array4 = list[j].Split(new char[]
				{
					'='
				});
				if (array4.Length > 2)
				{
					keys[j] = array4[0].Trim();
					values[j] = list[j].Substring(keys[j].Length + 1);
				}
				if (array4.Length == 2)
				{
					keys[j] = array4[0].Trim();
					values[j] = array4[1].Trim();
				}
				else
				{
					if (array4.Length == 1)
					{
						keys[j] = array4[0].Trim();
						values[j] = "";
					}
					else
					{
						if (array4.Length == 0)
						{
							keys[j] = "";
							values[j] = "";
						}
					}
				}
				j = j + 1;
			}
			return 0;
		}

		public static int GetAllKeys(string section, out string[] keys, string path)
		{
			byte[] array;
			string @string;
			string[] array2;
			ArrayList arrayList;
			string[] array3;
			int i;
			string text;
			int j;
			string[] array4;
			array = new byte[65535];
			IniHelper.GetPrivateProfileSection(section, array, array.Length, path);
			@string = Encoding.Default.GetString(array);
			array2 = @string.Split(new char[1]);
			arrayList = new ArrayList();
			array3 = array2;
			i = 0;
			while (i < array3.Length)
			{
				text = array3[i];
				if (string.Equals(text, string.Empty))
				{
					arrayList.Add(text);
				}
				i = i + 1;
			}
			keys = new string[arrayList.Count];
			j = 0;
			while (j < arrayList.Count)
			{
				array4 = arrayList[j].ToString().Split(new char[]
				{
					'='
				});
				if (array4.Length == 2)
				{
					keys[j] = array4[0].Trim();
				}
				else
				{
					if (array4.Length == 1)
					{
						keys[j] = array4[0].Trim();
					}
					else
					{
						if (array4.Length == 0)
						{
							keys[j] = "";
						}
					}
				}
				j = j + 1;
			}
			return 0;
		}

		public static List<string> GetAllKeys(string section, string path)
		{
			List<string> list;
			byte[] array;
			string @string;
			string[] array2;
			List<string> list2;
			string[] array3;
			int i;
			string text;
			int j;
			string[] array4;
			list = new List<string>();
			array = new byte[65535];
			IniHelper.GetPrivateProfileSection(section, array, array.Length, path);
			@string = Encoding.Default.GetString(array);
			array2 = @string.Split(new char[1]);
			list2 = new List<string>();
			array3 = array2;
			i = 0;
			while (i < array3.Length)
			{
				text = array3[i];
				if (string.Equals(text, string.Empty))
				{
					list2.Add(text);
				}
				i = i + 1;
			}
			j = 0;
			while (j < list2.Count)
			{
				array4 = list2[j].Split(new char[]
				{
					'='
				});
				if (array4.Length == 2 || array4.Length == 1)
				{
					list.Add(array4[0].Trim());
				}
				else
				{
					if (array4.Length == 0)
					{
						list.Add(string.Empty);
					}
				}
				j = j + 1;
			}
			return list;
		}

		public static List<string> GetAllValues(string section, string path)
		{
			List<string> list;
			byte[] array;
			string @string;
			string[] array2;
			List<string> list2;
			string[] array3;
			int i;
			string text;
			int j;
			string[] array4;
			list = new List<string>();
			array = new byte[65535];
			IniHelper.GetPrivateProfileSection(section, array, array.Length, path);
			@string = Encoding.Default.GetString(array);
			array2 = @string.Split(new char[1]);
			list2 = new List<string>();
			array3 = array2;
			i = 0;
			while (i < array3.Length)
			{
				text = array3[i];
				if (string.Equals(text, string.Empty))
				{
					list2.Add(text);
				}
				i = i + 1;
			}
			j = 0;
			while (j < list2.Count)
			{
				array4 = list2[j].Split(new char[]
				{
					'='
				});
				if (array4.Length == 2 || array4.Length == 1)
				{
					list.Add(array4[1].Trim());
				}
				else
				{
					if (array4.Length == 0)
					{
						list.Add(string.Empty);
					}
				}
				j = j + 1;
			}
			return list;
		}

		public static string GetFirstKeyByValue(string section, string path, string value)
		{
			List<string>.Enumerator enumerator;
			string current;
			string result;
			enumerator = IniHelper.GetAllKeys(section, path).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					current = enumerator.Current;
					if (string.Equals(IniHelper.ReadString(section, current, "", path), value))
					{
						result = current;
						return result;
					}
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			result = string.Empty;
			return result;
		}

		public static List<string> GetKeysByValue(string section, string path, string value)
		{
			List<string> list;
			List<string>.Enumerator enumerator;
			string current;
			list = new List<string>();
			enumerator = IniHelper.GetAllKeys(section, path).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					current = enumerator.Current;
					if (string.Equals(IniHelper.ReadString(section, current, "", path), value))
					{
						list.Add(current);
					}
				}
			}
			finally
			{
				((IDisposable)enumerator).Dispose();
			}
			return list;
		}

		public static string ReadString(string sectionName, string keyName, string defaultValue, string path)
		{
			StringBuilder stringBuilder;
			stringBuilder = new StringBuilder(255);
			IniHelper.GetPrivateProfileString(sectionName, keyName, defaultValue, stringBuilder, 255, path);
			return stringBuilder.ToString();
		}

		public static void WriteString(string sectionName, string keyName, string value, string path)
		{
			IniHelper.WritePrivateProfileString(sectionName, keyName, value, path);
		}

		public static int ReadInteger(string sectionName, string keyName, int defaultValue, string path)
		{
			return IniHelper.GetPrivateProfileInt(sectionName, keyName, defaultValue, path);
		}

		public static void WriteInteger(string sectionName, string keyName, int value, string path)
		{
			IniHelper.WritePrivateProfileString(sectionName, keyName, value.ToString(), path);
		}

		public static bool ReadBoolean(string sectionName, string keyName, bool defaultValue, string path)
		{
			int nDefault;
			nDefault = (defaultValue ? 1 : 0);
			return IniHelper.GetPrivateProfileInt(sectionName, keyName, nDefault, path) != 0;
		}

		public static void WriteBoolean(string sectionName, string keyName, bool value, string path)
		{
			string val;
			val = (value ? "1 " : "0 ");
			IniHelper.WritePrivateProfileString(sectionName, keyName, val, path);
		}

		public static void DeleteKey(string sectionName, string keyName, string path)
		{
			IniHelper.WritePrivateProfileString(sectionName, keyName, null, path);
		}

		public static void EraseSection(string sectionName, string path)
		{
			IniHelper.WritePrivateProfileString(sectionName, null, null, path);
		}

		public static bool ExistSection(string section, string fileName)
		{
			string[] array;
			string[] array2;
			int i;
			string a;
			bool result;
			array = null;
			IniHelper.GetAllSectionNames(out array, fileName);
			if (array != null)
			{
				array2 = array;
				i = 0;
				while (i < array2.Length)
				{
					a = array2[i];
					if (string.Equals(a, section))
					{
						result = true;
						return result;
					}
					i = i + 1;
				}
			}
			result = false;
			return result;
		}

		public static bool ExistKey(string section, string key, string fileName)
		{
			string[] array;
			string[] array2;
			int i;
			string a;
			bool result;
			array = null;
			IniHelper.GetAllKeys(section, out array, fileName);
			if (array != null)
			{
				array2 = array;
				i = 0;
				while (i < array2.Length)
				{
					a = array2[i];
					if (string.Equals(a, key))
					{
						result = true;
						return result;
					}
					i = i + 1;
				}
			}
			result = false;
			return result;
		}

		public static bool AddSectionWithKeyValues(string section, List<string> keyList, List<string> valueList, string path)
		{
			bool result;
			int i;
			result = true;
			i = 0;
			while (i < keyList.Count)
			{
				IniHelper.WriteString(section, keyList[i], valueList[i], path);
				i = i + 1;
			}
			return result;
		}

		public IniHelper()
		{
			
			
		}
	}
}
