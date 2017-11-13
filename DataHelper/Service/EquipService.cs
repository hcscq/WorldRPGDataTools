using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
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
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public static void Add(Equip model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Equip(");
            strSql.Append("Key,Name,Quality,Attribute,Level,Origin,Exclusive,Type");
            strSql.Append(") values (");
            strSql.Append("@Key,@Name,@Quality,@Attribute,@Level,@Origin,@Exclusive,@Type");
            strSql.Append(") ");

            SQLiteParameter[] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@Name", DbType.String,200) ,
                        new SQLiteParameter("@Quality", DbType.String,100) ,
                        new SQLiteParameter("@Attribute", DbType.String,5000) ,
                        new SQLiteParameter("@Level", DbType.String,10) ,
                        new SQLiteParameter("@Origin", DbType.String,2000) ,
                        new SQLiteParameter("@Exclusive", DbType.String,200) ,
                        new SQLiteParameter("@Type", DbType.String,10)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Quality;
            parameters[3].Value = model.Attribute;
            parameters[4].Value = model.Level;
            parameters[5].Value = model.Origin;
            parameters[6].Value = model.Exclusive;
            parameters[7].Value = model.Type;
            SqliteHelper.ExecuteTransSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Equip model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Equip set ");

            //strSql.Append(" Key = @Key , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Quality = @Quality , ");
            strSql.Append(" Attribute = @Attribute , ");
            strSql.Append(" Level = @Level , ");
            strSql.Append(" Origin = @Origin , ");
            strSql.Append(" Exclusive = @Exclusive , ");
            strSql.Append(" Type = @Type  ");
            strSql.Append(" where Key=@Key  ");

            SQLiteParameter[] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@Name", DbType.String,200) ,
                        new SQLiteParameter("@Quality", DbType.String,100) ,
                        new SQLiteParameter("@Attribute", DbType.String,5000) ,
                        new SQLiteParameter("@Level", DbType.String,10) ,
                        new SQLiteParameter("@Origin", DbType.String,2000) ,
                        new SQLiteParameter("@Exclusive", DbType.String,200) ,
                        new SQLiteParameter("@Type", DbType.String,10)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Quality;
            parameters[3].Value = model.Attribute;
            parameters[4].Value = model.Level;
            parameters[5].Value = model.Origin;
            parameters[6].Value = model.Exclusive;
            parameters[7].Value = model.Type;
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
            strSql.Append("delete from Equip ");
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
        public EquipService()
		{
			
			
		}
	}
}
