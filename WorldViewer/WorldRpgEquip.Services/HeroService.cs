using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorldRpgCommon;
using WorldRpgModel;

namespace WorldRpgEquip.Services
{
	public class HeroService
	{
		public static IList<Hero> LoadData()
		{
			string sql;
			DataTable table;
			sql = "select * from Hero";
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Hero>(table);
		}

		public static Hero LoadDataByName(string key)
		{
			string sql;
			DataTable table;
			IList<Hero> source;
			sql = string.Concat("select * from Hero where Key = '", key, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<Hero>(table);
			return Enumerable.FirstOrDefault<Hero>(source);
		}

		public HeroService()
		{
			
			
		}
	}
}
