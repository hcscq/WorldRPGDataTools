using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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
        public static  IList<Exclusive> LoadAllData()
        {
            string sql;
            DataTable table;
            sql = string.Concat("select * from Exclusive ");
            table = SqliteHelper.ExecuteSqlToTable(sql);
            return ConvertHelper.ConvertTo<Exclusive>(table);
        }

        public static IList<Exclusive> LoadDataByHero(string herokey)
		{
			string sql;
			DataTable table;
			sql = string.Concat("select * from Exclusive where HeroKey = '", herokey, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<Exclusive>(table);
		}
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public static void Add(Exclusive model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Exclusive(");
            strSql.Append("Key,HeroKey,EquipKey,Name,Effect");
            strSql.Append(") values (");
            strSql.Append("@Key,@HeroKey,@EquipKey,@Name,@Effect");
            strSql.Append(") ");

            SQLiteParameter[] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@HeroKey", DbType.String,36) ,
                        new SQLiteParameter("@EquipKey", DbType.String,36) ,
                        new SQLiteParameter("@Name", DbType.String,50) ,
                        new SQLiteParameter("@Effect", DbType.String)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.HeroKey;
            parameters[2].Value = model.EquipKey;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.Effect;
            SqliteHelper.ExecuteTransSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Exclusive model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Exclusive set ");

            //strSql.Append(" Key = @Key , ");
            strSql.Append(" HeroKey = @HeroKey , ");
            strSql.Append(" EquipKey = @EquipKey , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Effect = @Effect  ");
            strSql.Append(" where Key=@Key  ");

            SQLiteParameter[] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@HeroKey", DbType.String,36) ,
                        new SQLiteParameter("@EquipKey", DbType.String,36) ,
                        new SQLiteParameter("@Name", DbType.String,50) ,
                        new SQLiteParameter("@Effect", DbType.String)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.HeroKey;
            parameters[2].Value = model.EquipKey;
            parameters[3].Value = model.Name;
            parameters[4].Value = model.Effect;
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
            strSql.Append("delete from Exclusive ");
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
        public ExclusiveServices()
		{
			
			
		}
	}
}
