using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace WorldRpgCommon
{
	public class ConvertHelper
	{
		public static IList<T> ConvertTo<T>(IList<DataRow> rows)
		{
			IList<T> list;
			IEnumerator<DataRow> enumerator;
			DataRow current;
			T item;
			list = null;
			if (rows != null)
			{
				list = new List<T>();
				enumerator = rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						current = enumerator.Current;
						item = ConvertHelper.CreateItem<T>(current);
						list.Add(item);
					}
				}
				finally
				{
					if (enumerator != null)
					{
						enumerator.Dispose();
					}
				}
			}
			return list;
		}

		public static DataTable ConvertTo<T>(IList<T> list)
		{
			DataTable dataTable;
			Type typeFromHandle;
			PropertyDescriptorCollection properties;
			IEnumerator<T> enumerator;
			T current;
			DataRow dataRow;
			IEnumerator enumerator2;
			PropertyDescriptor propertyDescriptor;
			IDisposable disposable;
			dataTable = ConvertHelper.CreateTable<T>();
			typeFromHandle = Type.GetTypeFromHandle(typeof(T).TypeHandle);
			properties = TypeDescriptor.GetProperties(typeFromHandle);
			enumerator = list.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					current = enumerator.Current;
					dataRow = dataTable.NewRow();
					enumerator2 = properties.GetEnumerator();
					try
					{
						while (enumerator2.MoveNext())
						{
							propertyDescriptor = (PropertyDescriptor)enumerator2.Current;
							dataRow[propertyDescriptor.Name] = propertyDescriptor.GetValue(current);
						}
					}
					finally
					{
						disposable = (enumerator2 as IDisposable);
						if (disposable != null)
						{
							disposable.Dispose();
						}
					}
					dataTable.Rows.Add(dataRow);
				}
			}
			finally
			{
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}
			return dataTable;
		}

		public static IList<T> ConvertTo<T>(DataTable table)
		{
			IList<T> result;
			List<DataRow> list;
			IEnumerator enumerator;
			DataRow item;
			IDisposable disposable;
			if (table == null)
			{
				result = null;
			}
			else
			{
				list = new List<DataRow>();
				enumerator = table.Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						item = (DataRow)enumerator.Current;
						list.Add(item);
					}
				}
				finally
				{
					disposable = (enumerator as IDisposable);
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
				result = ConvertHelper.ConvertTo<T>(list);
			}
			return result;
		}

		public static T CreateItem<T>(DataRow row)
		{
			T t;
			IEnumerator enumerator;
			DataColumn dataColumn;
			PropertyInfo property;
			object obj;
			IDisposable disposable;
			t = default(T);
			if (row != null)
			{
				t = Activator.CreateInstance<T>();
                int len = t.GetType().GetProperties().Length;
				enumerator = row.Table.Columns.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						dataColumn = (DataColumn)enumerator.Current;
						property = t.GetType().GetProperty(dataColumn.ColumnName);
						try
						{
							obj = row[dataColumn.ColumnName];
							if (obj is DBNull)
							{
								obj = "";
							}
							property.SetValue(t, obj, null);
						}
						catch(Exception e1)
						{
							throw e1;
						}
					}
				}
				finally
				{
					disposable = (enumerator as IDisposable);
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			return t;
		}

		public static DataTable CreateTable<T>()
		{
			Type typeFromHandle;
			DataTable dataTable;
			PropertyDescriptorCollection properties;
			IEnumerator enumerator;
			PropertyDescriptor propertyDescriptor;
			IDisposable disposable;
			typeFromHandle = Type.GetTypeFromHandle(typeof(T).TypeHandle);
			dataTable = new DataTable(typeFromHandle.Name);
			properties = TypeDescriptor.GetProperties(typeFromHandle);
			enumerator = properties.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					propertyDescriptor = (PropertyDescriptor)enumerator.Current;
					dataTable.Columns.Add(propertyDescriptor.Name, propertyDescriptor.PropertyType);
				}
			}
			finally
			{
				disposable = (enumerator as IDisposable);
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
			return dataTable;
		}

		public ConvertHelper()
		{
			
			
		}
	}
}
