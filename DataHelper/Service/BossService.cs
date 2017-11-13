using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Boss model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Boss set ");

            //strSql.Append(" Key = @Key , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Beckon = @Beckon , ");
            strSql.Append(" DropOut = @DropOut  ");
            strSql.Append(" where Key=@Key  ");

            SQLiteParameter[] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@Name", DbType.String,50) ,
                        new SQLiteParameter("@Beckon", DbType.String,2000) ,
                        new SQLiteParameter("@DropOut", DbType.String,2000)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Beckon;
            parameters[3].Value = model.DropOut;
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
        /// 增加一条数据
        /// </summary>
        public static void Add(Boss model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Boss(");
            strSql.Append("Key,Name,Beckon,DropOut");
            strSql.Append(") values (");
            strSql.Append("@Key,@Name,@Beckon,@DropOut");
            strSql.Append(") ");

            SQLiteParameter [] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@Name", DbType.String,50) ,
                        new SQLiteParameter("@Beckon", DbType.String,2000) ,
                        new SQLiteParameter("@DropOut", DbType.String,2000)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Beckon;
            parameters[3].Value = model.DropOut;
            SqliteHelper.ExecuteSql(strSql.ToString(), parameters);

        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string Key)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Boss ");
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
        public BossService()
		{
			
			
		}
	}
}
