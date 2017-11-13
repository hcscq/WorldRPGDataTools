using System.Collections.Generic;
using System.Data;
using System.Linq;
using WorldRpgCommon;
using WorldRpgModel;

namespace WorldRpgEquip.Services
{
	public class EquipService
	{
		public static IList<Equip> LoadData()
		{
			string sql;
			DataTable table;
			sql = "select * from Equip";
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Equip>(table);
		}

		public static Equip LoadDataByName(string name)
		{
			string sql;
			DataTable table;
			IList<Equip> source;
			sql = string.Concat("select * from Equip where Name = '", name, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<Equip>(table);
			return Enumerable.FirstOrDefault<Equip>(source);
		}

		public static IList<Equip> LoadDataByNameEx(string name)
		{
			string sql;
			DataTable table;
			sql = string.Concat("select * from Equip where Name like '%", name, "%'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Equip>(table);
		}

		public static IList<Equip> LoadDataByOrigin(string name)
		{
			string sql;
			DataTable table;
			sql = string.Concat(new string[]
			{
				"select * from Equip where Origin like '%,",
				name,
				",%' or Origin like '%或者",
				name,
				",%' or Origin like '%,",
				name,
				"或者%'"
			});
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Equip>(table);
		}

		public static Equip LoadDataByKey(string key)
		{
			string sql;
			DataTable table;
			IList<Equip> source;
			sql = string.Concat("select * from Equip where Key = '", key, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<Equip>(table);
			return Enumerable.FirstOrDefault<Equip>(source);
		}

		public EquipService()
		{
			
			
		}
	}
}
