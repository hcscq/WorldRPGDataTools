using System.Collections.Generic;
using System.Data;
using WorldRpgCommon;
using WorldRpgModel;

namespace WorldRpgEquip.Services
{
	public class BossDropOutService
	{
        public static string sqlStr = @"select * from(select d.*,e.Name EquipName,b.Name as BossName from BossDropOut d left join Boss b on d.BossKey=b.Key  join Equip e on d.EquipKey=e.Key 
                                        union
                                        select d.*,m.Name EquipName,b.Name as BossName from BossDropOut d left join Boss b on d.BossKey=b.Key  join material m on d.EquipKey=m.Key)k 
                                        where 1=1 ";
        public static IList<BossDropOutShow> LoadDataByBossKey(string key)
        {
            string sql;
            DataTable table;
            sql = string.Concat(sqlStr, " and k.BossKey = '", key, "'");
            table = SqliteHelper.ExecuteSqlToTable(sql);
            return ConvertHelper.ConvertTo<BossDropOutShow>(table);
        }
        public static IList<BossDropOutShow> LoadDataByEquipKey(string key)
        {
            string sql;
            DataTable table;
            sql = string.Concat(sqlStr, "and k.EquipKey = '", key, "'");
            table = SqliteHelper.ExecuteSqlToTable(sql);
            return ConvertHelper.ConvertTo<BossDropOutShow>(table);
        }
        public static IList<BossDropOutShow> LoadDataByBossName(string name)
		{
			string sql;
			DataTable table;
			sql = string.Concat(sqlStr, "and k.BossName = '", name, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<BossDropOutShow>(table);
		}

		public static IList<BossDropOutShow> SearchDataByBossName(string name)
		{
			string sql;
			DataTable table;
			sql = string.Concat(sqlStr, "and k.BossName like '%", name, "%'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<BossDropOutShow>(table);
		}
        public static IList<BossDropOutShow> SearchDataByEquipName(string name)
        {
            string sql;
            DataTable table;
            sql = string.Concat(sqlStr, "and k.EquipName like '%", name, "%'");
            table = SqliteHelper.ExecuteSqlToTable(sql);
            return ConvertHelper.ConvertTo<BossDropOutShow>(table);
        }

		public BossDropOutService()
		{
			
			
		}
	}
}
