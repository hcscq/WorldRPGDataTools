using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorldRpgCommon;
using WorldRpgModel;

namespace WorldRpgEquip.Services
{
	public class MaterialService
	{
		public static IList<Material> LoadDataByName(string name)
		{
			string sql;
			DataTable table;
			sql = string.Concat("select * from Material where Name = '", name, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Material>(table);
		}

		public static IList<Material> SearchDataByName(string name)
		{
			string sql;
			DataTable table;
			sql = string.Concat("select * from Material where Name like '%", name, "%'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Material>(table);
		}

		public static Material LoadDataByNameEx(string name)
		{
			string sql;
			DataTable table;
			IList<Material> source;
			sql = string.Concat("select * from Material where Name = '", name, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<Material>(table);
			return Enumerable.FirstOrDefault<Material>(source);
		}

		public static IList<Material> LoadData()
		{
			string sql;
			DataTable table;
			sql = "select * from Material";
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Material>(table);
		}

		public MaterialService()
		{
			
			
		}
	}
}
