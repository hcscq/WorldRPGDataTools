using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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
        public static bool insertBatch(IList<BossDropOut> list,int batchSize=50)
        {
            string strInsert = "insert into bossdropout(key,bosskey,droptype,chance,equipkey) values('{0}','{1}',{2},{3},'{4}');";
            //
            StringBuilder sb = new StringBuilder();
            List<string> sqlStr = new List<string>();
            foreach (var it in list)
            {
                SqliteHelper.ExecuteSql(string.Format(strInsert, it.Key, it.BossKey, it.DropType, it.Chance, it.EquipKey));
                //sqlStr.Add(string.Format(strInsert,it.Key,it.BossKey,it.DropType,it.Chance,it.EquipKey));
                //if (sqlStr.Count > batchSize)
                //{
                //    SqliteHelper.ExecuteTransForListWithSQL(sqlStr.ToArray());
                //    sqlStr.Clear();
                //}
            }
            return true;
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public static void Add(BossDropOut model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into BossDropOut(");
            strSql.Append("Key,BossKey,DropType,Chance,EquipKey");
            strSql.Append(") values (");
            strSql.Append("@Key,@BossKey,@DropType,@Chance,@EquipKey");
            strSql.Append(") ");

            SQLiteParameter [] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@BossKey", DbType.String,36) ,
                        new SQLiteParameter("@DropType", DbType.Int32,4) ,
                        new SQLiteParameter("@Chance", DbType.Decimal,8) ,
                        new SQLiteParameter("@EquipKey", DbType.String,36)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.BossKey;
            parameters[2].Value = model.DropType;
            parameters[3].Value = model.Chance;
            parameters[4].Value = model.EquipKey;
            SqliteHelper.ExecuteSql(strSql.ToString(), parameters);

        }
        /// <summary>
		/// 更新一条数据
		/// </summary>
		public static bool Update(BossDropOut model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update BossDropOut set ");

            //strSql.Append(" Key = @Key , ");
            strSql.Append(" BossKey = @BossKey , ");
            strSql.Append(" DropType = @DropType , ");
            strSql.Append(" Chance = @Chance , ");
            strSql.Append(" EquipKey = @EquipKey  ");
            strSql.Append(" where Key=@Key  ");

            SQLiteParameter[] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@BossKey", DbType.String,36) ,
                        new SQLiteParameter("@DropType", DbType.Int32,4) ,
                        new SQLiteParameter("@Chance", DbType.Decimal,8) ,
                        new SQLiteParameter("@EquipKey", DbType.String,36)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.BossKey;
            parameters[2].Value = model.DropType;
            parameters[3].Value = model.Chance;
            parameters[4].Value = model.EquipKey;
            int rows = SqliteHelper.ExecuteTransSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Key)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from BossDropOut ");
            strSql.Append(" where Key=@Key ");
            SQLiteParameter[] parameters = {
                    new SQLiteParameter("@Key", DbType.String,36)           };
            parameters[0].Value = Key;


            int rows = SqliteHelper.ExecuteTransSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public BossDropOutService()
		{
			
			
		}

        internal static IList<BossDropOutShow> LoadData()
        {
            DataTable table;
            table = SqliteHelper.ExecuteSqlToTable(sqlStr);
            return ConvertHelper.ConvertTo<BossDropOutShow>(table);
        }
    }
}
