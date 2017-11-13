using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorldRpgCommon;
using WorldRpgModel;

namespace WorldRpgService
{
	public class SaveDataHistoryService
	{
		public static IList<SaveDataHistory> LoadData()
		{
			string sql;
			DataTable table;
			sql = "select * from SaveDataHistory";
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<SaveDataHistory>(table);
		}

		public static SaveDataHistory LoadDataByKey(string key)
		{
			string sql;
			DataTable table;
			IList<SaveDataHistory> source;
			sql = string.Concat("select * from SaveDataHistory where Key = '", key, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<SaveDataHistory>(table);
			return Enumerable.FirstOrDefault<SaveDataHistory>(source);
		}

		public static SaveDataHistory LoadDataBySaveTime(string saveTime)
		{
			string sql;
			DataTable table;
			IList<SaveDataHistory> source;
			sql = string.Concat("select * from SaveDataHistory where SaveTime = '", saveTime, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<SaveDataHistory>(table);
			return Enumerable.FirstOrDefault<SaveDataHistory>(source);
		}

		public static void Insert(SaveDataHistory model)
		{
			SaveDataHistory saveDataHistory;
			string string_;
			try
			{
				saveDataHistory = SaveDataHistoryService.LoadDataBySaveTime(model.SaveTime);
				if (saveDataHistory == null)
				{
					string_ = string.Concat(new string[]
					{
						"insert into SaveDataHistory values('",
						model.Key,
						"','",
						model.SaveTime,
						"','",
						model.Data,
						"')"
					});
					SqliteHelper.ExecuteSql(string_);
				}
			}
			catch (Exception)
			{
			}
		}

		public SaveDataHistoryService()
		{
			
			
		}
	}
}
