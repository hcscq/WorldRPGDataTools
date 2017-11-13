using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorldRpgCommon;
using WorldRpgModel;

namespace WorldRpgService
{
	public class ExclusiveServices
	{
		public static Exclusive LoadData()
		{
			string sql;
			DataTable table;
			IList<Exclusive> source;
			sql = "select * from Hero";
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<Exclusive>(table);
			return Enumerable.FirstOrDefault<Exclusive>(source);
		}

		public static IList<Exclusive> LoadDataByHero(string herokey)
		{
			string sql;
			DataTable table;
			sql = string.Concat("select * from Exclusive where HeroKey = '", herokey, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Exclusive>(table);
		}

		public ExclusiveServices()
		{
			
			
		}
	}
}
