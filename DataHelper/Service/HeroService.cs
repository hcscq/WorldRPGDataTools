using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public static void Add(Hero model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Hero(");
            strSql.Append("Key,Name,Zl,Hj,Vocation,Type,Hp,Mp,AttMin,AttMax,Ll,Mj");
            strSql.Append(") values (");
            strSql.Append("@Key,@Name,@Zl,@Hj,@Vocation,@Type,@Hp,@Mp,@AttMin,@AttMax,@Ll,@Mj");
            strSql.Append(") ");

            SQLiteParameter[] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@Name", DbType.String,30) ,
                        new SQLiteParameter("@Zl", DbType.Int32,8) ,
                        new SQLiteParameter("@Hj", DbType.Int32,8) ,
                        new SQLiteParameter("@Vocation", DbType.String,30) ,
                        new SQLiteParameter("@Type", DbType.Int32,4) ,
                        new SQLiteParameter("@Hp", DbType.Int32,8) ,
                        new SQLiteParameter("@Mp", DbType.Int32,8) ,
                        new SQLiteParameter("@AttMin", DbType.Int32,8) ,
                        new SQLiteParameter("@AttMax", DbType.Int32,8) ,
                        new SQLiteParameter("@Ll", DbType.Int32,8) ,
                        new SQLiteParameter("@Mj", DbType.Int32,8)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Zl;
            parameters[3].Value = model.Hj;
            parameters[4].Value = model.Vocation;
            parameters[5].Value = model.Type;
            parameters[6].Value = model.Hp;
            parameters[7].Value = model.Mp;
            parameters[8].Value = model.AttMin;
            parameters[9].Value = model.AttMax;
            parameters[10].Value = model.Ll;
            parameters[11].Value = model.Mj;
            SqliteHelper.ExecuteTransSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Hero model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Hero set ");

            //strSql.Append(" Key = @Key , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Zl = @Zl , ");
            strSql.Append(" Hj = @Hj , ");
            strSql.Append(" Vocation = @Vocation , ");
            strSql.Append(" Type = @Type , ");
            strSql.Append(" Hp = @Hp , ");
            strSql.Append(" Mp = @Mp , ");
            strSql.Append(" AttMin = @AttMin , ");
            strSql.Append(" AttMax = @AttMax , ");
            strSql.Append(" Ll = @Ll , ");
            strSql.Append(" Mj = @Mj  ");
            strSql.Append(" where Key=@Key  ");

            SQLiteParameter[] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@Name", DbType.String,30) ,
                        new SQLiteParameter("@Zl", DbType.Int32,8) ,
                        new SQLiteParameter("@Hj", DbType.Int32,8) ,
                        new SQLiteParameter("@Vocation", DbType.String,30) ,
                        new SQLiteParameter("@Type", DbType.Int32,4) ,
                        new SQLiteParameter("@Hp", DbType.Int32,8) ,
                        new SQLiteParameter("@Mp", DbType.Int32,8) ,
                        new SQLiteParameter("@AttMin", DbType.Int32,8) ,
                        new SQLiteParameter("@AttMax", DbType.Int32,8) ,
                        new SQLiteParameter("@Ll", DbType.Int32,8) ,
                        new SQLiteParameter("@Mj", DbType.Int32,8)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Zl;
            parameters[3].Value = model.Hj;
            parameters[4].Value = model.Vocation;
            parameters[5].Value = model.Type;
            parameters[6].Value = model.Hp;
            parameters[7].Value = model.Mp;
            parameters[8].Value = model.AttMin;
            parameters[9].Value = model.AttMax;
            parameters[10].Value = model.Ll;
            parameters[11].Value = model.Mj;
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
            strSql.Append("delete from Hero ");
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
        public HeroService()
		{
			
			
		}
	}
}
