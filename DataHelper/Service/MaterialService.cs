using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using WorldRpgCommon;
using WorldRpgModel;

namespace WorldRpgEquip.Services
{
	public class MaterialService
	{
		public static IList<MaterialShow> LoadDataByName(string name)
		{
			string sql;
			DataTable table;
			sql = string.Concat("select m.*,b.Name as BossName from Material m left join  Boss b on m.Boss=b.key where m.Name = '", name, "'");
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<MaterialShow>(table);
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

		public static IList<MaterialShow> LoadData()
		{
			string sql;
			DataTable table;
			sql = "select m.*,b.Name as BossName from Material m left join  Boss b on m.Boss=b.key";
			table = SqliteHelper.ExecuteSqlToTable(sql);
			return ConvertHelper.ConvertTo<MaterialShow>(table);
		}
        /// <summary>
		/// 增加一条数据
		/// </summary>
		public static void Add(Material model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Material(");
            strSql.Append("Key,Name,Boss");
            strSql.Append(") values (");
            strSql.Append("@Key,@Name,@Boss");
            strSql.Append(") ");

            SQLiteParameter[] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@Name", DbType.String,200) ,
                        new SQLiteParameter("@Boss", DbType.String,36)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Boss;
            SqliteHelper.ExecuteTransSql(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public static bool Update(Material model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Material set ");

            //strSql.Append(" Key = @Key , ");
            strSql.Append(" Name = @Name , ");
            strSql.Append(" Boss = @Boss  ");
            strSql.Append(" where Key=@Key  ");

            SQLiteParameter[] parameters = {
                        new SQLiteParameter("@Key", DbType.String,36) ,
                        new SQLiteParameter("@Name", DbType.String,200) ,
                        new SQLiteParameter("@Boss", DbType.String,36)

            };

            parameters[0].Value = model.Key;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.Boss;
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
            strSql.Append("delete from Material ");
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
        public MaterialService()
		{
			
			
		}
	}
}
