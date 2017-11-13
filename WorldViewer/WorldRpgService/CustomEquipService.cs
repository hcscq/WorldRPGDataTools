using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using WorldRpgCommon;
using WorldRpgModel;

namespace WorldRpgService
{
	public class CustomEquipService
	{
		public static IList<CustomEquip> LoadData()
		{
			string sql;
			DataTable table;
			sql = "select * from CustomEquip";
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<CustomEquip>(table);
		}

		public static CustomEquip LoadDataByName(string name)
		{
			string sql;
			DataTable table;
			IList<CustomEquip> source;
			sql = string.Concat("select * from CustomEquip where Name = '", name, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			source = ConvertHelper.ConvertTo<CustomEquip>(table);
			return Enumerable.FirstOrDefault<CustomEquip>(source);
		}

		public static bool DeleteByName(string name)
		{
			string string_;
			string_ = string.Concat("delete from CustomEquip where Name = '", name, "'");
			return SqliteHelper.ExecuteSql(string_);
		}

		public static bool Insert(CustomEquip custom)
		{
			string string_;
			SQLiteParameter[] cmdParms;
			string_ = "insert into CustomEquip(Name,Equip1,Equip2,Equip3,Equip4,Equip5,Equip6,Equip7,Equip8,Equip9,Equip10) values(@Name,@Equip1,@Equip2,@Equip3,@Equip4,@Equip5,@Equip6,@Equip7,@Equip8,@Equip9,@Equip10)";
			cmdParms = new SQLiteParameter[]
			{
				new SQLiteParameter("@Name", custom.Name),
				new SQLiteParameter("@Equip1", custom.Name),
				new SQLiteParameter("@Equip2", custom.Name),
				new SQLiteParameter("@Equip3", custom.Name),
				new SQLiteParameter("@Equip4", custom.Name),
				new SQLiteParameter("@Equip5", custom.Name),
				new SQLiteParameter("@Equip6", custom.Name),
				new SQLiteParameter("@Equip7", custom.Name),
				new SQLiteParameter("@Equip8", custom.Name),
				new SQLiteParameter("@Equip9", custom.Name),
				new SQLiteParameter("@Equip10", custom.Name)
			};
			return SqliteHelper.ExecuteSql(string_, cmdParms);
		}

		public CustomEquipService()
		{
			
			
		}
	}
}
