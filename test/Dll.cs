using System;
using System.Reflection;
using System.Windows.Forms;

namespace WorldRpgCommon
{
	public class Dll
	{
		public static object Invoke(string lpFileName, string Namespace, string ClassName, string lpProcName, object[] ObjArray_Parameter)
		{
			Assembly assembly;
			Type[] types;
			Type[] array;
			int i;
			Type type;
			MethodInfo method;
			object obj;
			object result;
			try
			{
				assembly = Assembly.LoadFrom(lpFileName);
				types = assembly.GetTypes();
				array = types;
				i = 0;
				while (i < array.Length)
				{
					type = array[i];
					if (string.Equals(type.Namespace, Namespace) && string.Equals(type.Name, ClassName))
					{
						method = type.GetMethod(lpProcName);
						if (MethodInfo.Equals(method, null))
						{
							obj = Activator.CreateInstance(type);
							result = method.Invoke(obj, ObjArray_Parameter);
							return result;
						}
					}
					i = i + 1;
				}
			}
			catch (NullReferenceException ex)
			{
				MessageBox.Show(ex.Message);
			}
			result = 0;
			return result;
		}

		public Dll()
		{
			
			
		}
	}
}
