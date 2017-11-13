using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorldRpgCommon;
using WorldRpgModel;

namespace WorldRpgEquip.Services
{
	public class BossService
	{
		public static Boss LoadDataByName(string name)
		{
			string sql;
			DataTable table;
			IList<Boss> source;
			sql = string.Concat("select * from Boss where Name = '", name, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<Boss>(table);
			return Enumerable.FirstOrDefault<Boss>(source);
		}

		public static IList<Boss> SearchDataByName(string name)
		{
			string sql;
			DataTable table;
			sql = string.Concat("select * from Boss where Name like '%", name, "%'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Boss>(table);
		}

		public static Boss LoadDataByKey(string key)
		{
			string sql;
			DataTable table;
			IList<Boss> source;
			sql = string.Concat("select * from Boss where Key = '", key, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<Boss>(table);
			return Enumerable.FirstOrDefault<Boss>(source);
		}

		public static Boss LoadDataByNameEx(string name)
		{
			string sql;
			DataTable table;
			IList<Boss> source;
			sql = string.Concat("select * from Boss where Name like '%", name, "%'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<Boss>(table);
			return Enumerable.FirstOrDefault<Boss>(source);
		}

		public static IList<Boss> LoadData()
		{
			string sql;
			DataTable table;
			sql = "select * from Boss";
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Boss>(table);
		}

		public static IList<Boss> LoadDataByDropOut(string name)
		{
			string sql;
			DataTable table;
			sql = string.Concat("select * from Boss where DropOut like '%", name, "%'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Boss>(table);
		}

		public BossService()
		{
			
			
		}
	}
}
