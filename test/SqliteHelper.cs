using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
using System.Windows.Forms;

namespace WorldRpgCommon
{
	public class SqliteHelper
	{
		private static string _connectionString_inner;

		public static string ConnectionString
		{
			get
			{
				return SqliteHelper._connectionString_inner;
			}
			set
			{
				SqliteHelper._connectionString_inner = value;
			}
		}

		public static bool ExecuteSql(string string_0)
		{
			bool result;
			SQLiteConnection sQLiteConnection;
			SQLiteTransaction sQLiteTransaction;
			SQLiteCommand sQLiteCommand;
			result = false;
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				sQLiteConnection.Open();
				sQLiteTransaction = sQLiteConnection.BeginTransaction();
				try
				{
					sQLiteCommand = new SQLiteCommand(string_0, sQLiteConnection);
					try
					{
						sQLiteCommand.Transaction = sQLiteTransaction;
						sQLiteCommand.ExecuteNonQuery();
						sQLiteTransaction.Commit();
						result = true;
					}
					finally
					{
						if (sQLiteCommand != null)
						{
							((IDisposable)sQLiteCommand).Dispose();
						}
					}
				}
				finally
				{
					if (sQLiteTransaction != null)
					{
						((IDisposable)sQLiteTransaction).Dispose();
					}
				}
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return result;
		}

		public static bool ExecuteSql(string string_0, List<SQLiteParameter> list)
		{
			bool result;
			SQLiteConnection sQLiteConnection;
			SQLiteTransaction sQLiteTransaction;
			SQLiteCommand sQLiteCommand;
			result = false;
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				sQLiteConnection.Open();
				sQLiteTransaction = sQLiteConnection.BeginTransaction();
				try
				{
					sQLiteCommand = new SQLiteCommand(string_0, sQLiteConnection);
					try
					{
						try
						{
							sQLiteCommand.Parameters.AddRange(list.ToArray());
							sQLiteCommand.Transaction = sQLiteTransaction;
							sQLiteCommand.ExecuteNonQuery();
							sQLiteTransaction.Commit();
							result = true;
						}
						catch (SQLiteException ex)
						{
							sQLiteTransaction.Rollback();
							sQLiteConnection.Close();
							throw ex;
						}
					}
					finally
					{
						if (sQLiteCommand != null)
						{
							((IDisposable)sQLiteCommand).Dispose();
						}
					}
				}
				finally
				{
					if (sQLiteTransaction != null)
					{
						((IDisposable)sQLiteTransaction).Dispose();
					}
				}
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return result;
		}

		public static bool ExecuteSql(string string_0, params SQLiteParameter[] cmdParms)
		{
			bool result;
			SQLiteConnection sQLiteConnection;
			SQLiteCommand sQLiteCommand;
			result = false;
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				sQLiteCommand = new SQLiteCommand();
				try
				{
					SqliteHelper.PrepareCommand(sQLiteCommand, sQLiteConnection, null, CommandType.Text, string_0, cmdParms);
					sQLiteCommand.ExecuteNonQuery();
					sQLiteCommand.Parameters.Clear();
					result = true;
				}
				finally
				{
					if (sQLiteCommand != null)
					{
						((IDisposable)sQLiteCommand).Dispose();
					}
				}
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return result;
		}

		private static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, CommandType cmdType, string cmdText, SQLiteParameter[] cmdParms)
		{
			int i;
			SQLiteParameter sQLiteParameter;
			bool arg_69_0;
			if (conn.State != ConnectionState.Open)
			{
				conn.Open();
			}
			cmd.Connection = conn;
			cmd.CommandText = cmdText;
			if (trans != null)
			{
				cmd.Transaction = trans;
			}
			cmd.CommandType = cmdType;
			if (cmdParms != null)
			{
				i = 0;
				while (i < cmdParms.Length)
				{
					sQLiteParameter = cmdParms[i];
					if (sQLiteParameter.Direction == ParameterDirection.InputOutput)
					{
						goto IL_60;
					}
					if (sQLiteParameter.Direction == ParameterDirection.Input)
					{
						goto IL_60;
					}
					arg_69_0 = false;
					IL_69:
					if (arg_69_0)
					{
						sQLiteParameter.Value = DBNull.Value;
					}
					cmd.Parameters.Add(sQLiteParameter);
					i = i + 1;
					continue;
					IL_60:
					arg_69_0 = (sQLiteParameter.Value == null);
					goto IL_69;
				}
			}
		}

		public static DataTable ExecuteStoredProcedure(string storedProcedureName, params SQLiteParameter[] p)
		{
			return SqliteHelper.ExecuteSearch(new SQLiteCommand
			{
				CommandText = storedProcedureName,
				CommandType = CommandType.StoredProcedure
			}, p);
		}

		public static object ExecuteScalar(string sql, params SQLiteParameter[] p)
		{
			object result;
			SQLiteCommand sQLiteCommand;
			SQLiteConnection sQLiteConnection;
			int i;
			SQLiteParameter parameter;
			result = null;
			sQLiteCommand = new SQLiteCommand();
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				if (p != null && p.Length != 0)
				{
					i = 0;
					while (i < p.Length)
					{
						parameter = p[i];
						sQLiteCommand.Parameters.Add(parameter);
						i = i + 1;
					}
				}
				sQLiteCommand.Connection = sQLiteConnection;
				sQLiteConnection.Open();
				sQLiteCommand.CommandText = sql;
				result = sQLiteCommand.ExecuteScalar();
				sQLiteConnection.Close();
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return result;
		}

		public static object ExecuteScalarByList(string sql, List<SQLiteParameter> Plist)
		{
			SQLiteParameter[] array;
			object result;
			SQLiteCommand sQLiteCommand;
			SQLiteConnection sQLiteConnection;
			SQLiteParameter[] array2;
			int i;
			SQLiteParameter parameter;
			array = new SQLiteParameter[Plist.Count];
			Plist.CopyTo(array);
			result = null;
			sQLiteCommand = new SQLiteCommand();
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				if (array != null && array.Length != 0)
				{
					array2 = array;
					i = 0;
					while (i < array2.Length)
					{
						parameter = array2[i];
						sQLiteCommand.Parameters.Add(parameter);
						i = i + 1;
					}
				}
				sQLiteCommand.Connection = sQLiteConnection;
				sQLiteConnection.Open();
				sQLiteCommand.CommandText = sql;
				result = sQLiteCommand.ExecuteScalar();
				sQLiteConnection.Close();
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return result;
		}

		private static DataTable ExecuteSearch(SQLiteCommand comm, params SQLiteParameter[] p)
		{
			DataTable dataTable;
			SQLiteConnection sQLiteConnection;
			int i;
			SQLiteParameter sQLiteParameter;
			bool arg_4C_0;
			SQLiteDataAdapter sQLiteDataAdapter;
			dataTable = new DataTable();
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				if (p != null && p.Length != 0)
				{
					i = 0;
					while (i < p.Length)
					{
						sQLiteParameter = p[i];
						if (sQLiteParameter.Direction == ParameterDirection.InputOutput)
						{
							goto IL_42;
						}
						if (sQLiteParameter.Direction == ParameterDirection.Input)
						{
							goto IL_42;
						}
						arg_4C_0 = false;
						IL_4C:
						if (arg_4C_0)
						{
							sQLiteParameter.Value = DBNull.Value;
						}
						comm.Parameters.Add(sQLiteParameter);
						i = i + 1;
						continue;
						IL_42:
						arg_4C_0 = (sQLiteParameter.Value == null);
						goto IL_4C;
					}
				}
				comm.Connection = sQLiteConnection;
				sQLiteDataAdapter = new SQLiteDataAdapter(comm);
				sQLiteDataAdapter.Fill(dataTable);
				comm.Parameters.Clear();
				comm.Dispose();
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return dataTable;
		}

		public static DataTable ExecuteSearch(SQLiteCommand comm)
		{
			DataTable dataTable;
			SQLiteConnection sQLiteConnection;
			SQLiteDataAdapter sQLiteDataAdapter;
			dataTable = new DataTable();
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				comm.Connection = sQLiteConnection;
				sQLiteDataAdapter = new SQLiteDataAdapter(comm);
				sQLiteDataAdapter.Fill(dataTable);
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return dataTable;
		}

		public static DataTable ExecuteSqlToTable(string sql, params SQLiteParameter[] p)
		{
			return SqliteHelper.ExecuteSearch(new SQLiteCommand
			{
				CommandText = sql
			}, p);
		}

		public static DataTable ExecuteSqlToTable(string sql)
		{
			return SqliteHelper.ExecuteSearch(new SQLiteCommand
			{
				CommandText = sql
			}, null);
		}

		public static DataSet ExecuteSqlToDataSet(string sql, params SQLiteParameter[] p)
		{
			SQLiteCommand sQLiteCommand;
			sQLiteCommand = new SQLiteCommand();
			sQLiteCommand.CommandText = sql;
			return new DataSet
			{
				Tables = 
				{
					SqliteHelper.ExecuteSearch(sQLiteCommand, p)
				}
			};
		}

		public static DataSet ExecuteSqlToDataSetByList(string sql, List<SQLiteParameter> Plist)
		{
			SQLiteCommand sQLiteCommand;
			DataSet dataSet;
			SQLiteParameter[] array;
			sQLiteCommand = new SQLiteCommand();
			sQLiteCommand.CommandText = sql;
			dataSet = new DataSet();
			array = new SQLiteParameter[Plist.Count];
			Plist.CopyTo(array);
			dataSet.Tables.Add(SqliteHelper.ExecuteSearch(sQLiteCommand, array));
			return dataSet;
		}

		private static int ExecuteTrans(SQLiteCommand comm, params SQLiteParameter[] p)
		{
			SQLiteConnection sQLiteConnection;
			int i;
			SQLiteParameter sQLiteParameter;
			bool arg_53_0;
			int num;
			int result;		
			new DataTable();
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				comm.Parameters.Clear();
				if (p != null && p.Length != 0)
				{
					i = 0;
					while (i < p.Length)
					{
						sQLiteParameter = p[i];
						if (sQLiteParameter.Direction == ParameterDirection.InputOutput)
						{
							goto IL_4A;
						}
						if (sQLiteParameter.Direction == ParameterDirection.Input)
						{
							goto IL_4A;
						}
						arg_53_0 = false;
						IL_53:
						if (arg_53_0)
						{
							sQLiteParameter.Value = DBNull.Value;
						}
						comm.Parameters.Add(sQLiteParameter);
						i = i + 1;
						continue;
						IL_4A:
						arg_53_0 = (sQLiteParameter.Value == null);
						goto IL_53;
					}
				}
				comm.Connection = sQLiteConnection;
				sQLiteConnection.Open();
				try
				{
					try
					{
						comm.Transaction = sQLiteConnection.BeginTransaction();
						num = comm.ExecuteNonQuery();
						comm.Transaction.Commit();
						result = num;
					}
					catch (Exception ex)
					{
						if (comm.Transaction != null)
						{
							comm.Transaction.Rollback();
						}
						throw ex;
					}
				}
				finally
				{
					if (sQLiteConnection != null)
					{
						sQLiteConnection.Close();
					}
				}
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return result;
		}

		public static int ExecuteTransStoredProcedure(string storedProcedureName, params SQLiteParameter[] p)
		{
			return SqliteHelper.ExecuteTrans(new SQLiteCommand
			{
				CommandText = storedProcedureName,
				CommandType = CommandType.StoredProcedure
			}, p);
		}

		public static int ExecuteTransSql(string sql, params SQLiteParameter[] p)
		{
			return SqliteHelper.ExecuteTrans(new SQLiteCommand
			{
				CommandText = sql
			}, p);
		}

		public static bool ExecuteTransForListWithSQL(string[] sql)
		{
			bool result;
			SQLiteConnection sQLiteConnection;
			SQLiteTransaction sQLiteTransaction;
			int num;
			int i;
			result = false;
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				sQLiteTransaction = null;
				try
				{
					try
					{
						sQLiteConnection.Open();
						sQLiteTransaction = sQLiteConnection.BeginTransaction();
						num = 0;
						if (sql != null && sql.Length != 0)
						{
							i = 0;
							while (i < sql.Length)
							{
								if (sql[i] != null && string.Equals(sql[i], ""))
								{
									num = num + new SQLiteCommand(sql[i])
									{
										Connection = sQLiteConnection,
										Transaction = sQLiteTransaction
									}.ExecuteNonQuery();
								}
								i = i + 1;
							}
						}
						sQLiteTransaction.Commit();
						result = true;
					}
					catch (Exception ex)
					{
						if (sQLiteTransaction != null)
						{
							sQLiteTransaction.Rollback();
						}
						throw ex;
					}
				}
				finally
				{
					if (sQLiteConnection != null)
					{
						sQLiteConnection.Close();
					}
				}
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return result;
		}

		public static DataSet ExecuteForListWithSQL(string[] sql)
		{
			DataSet dataSet;
			int i;
			dataSet = new DataSet();
			if (sql != null && sql.Length != 0)
			{
				i = 0;
				while (i < sql.Length)
				{
					if (sql[i] != null && string.Equals(sql[i], ""))
					{
						dataSet.Tables.Add(SqliteHelper.ExecuteSqlToTable(sql[i]));
					}
					i = i + 1;
				}
			}
			return dataSet;
		}

		public static object GetSingle(string string_0, params SQLiteParameter[] cmdParms)
		{
			SQLiteConnection sQLiteConnection;
			SQLiteCommand sQLiteCommand;
			object obj;
			object result;
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				sQLiteCommand = new SQLiteCommand();
				try
				{
					try
					{
						SqliteHelper.PrepareCommand(sQLiteCommand, sQLiteConnection, null, CommandType.Text, string_0, cmdParms);
						obj = sQLiteCommand.ExecuteScalar();
						sQLiteCommand.Parameters.Clear();
						if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
						{
							result = null;
						}
						else
						{
							result = obj;
						}
					}
					catch (SQLiteException ex)
					{
						throw ex;
					}
				}
				finally
				{
					if (sQLiteCommand != null)
					{
						((IDisposable)sQLiteCommand).Dispose();
					}
				}
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return result;
		}

		public static bool Exists(string strSql, params SQLiteParameter[] cmdParms)
		{
			object single;
			int num;
			single = SqliteHelper.GetSingle(strSql, cmdParms);
			if (object.Equals(single, null) || object.Equals(single, DBNull.Value))
			{
				num = 0;
			}
			else
			{
				num = int.Parse(single.ToString());
			}
			return num != 0;
		}

		public static DataTable CreateSqlByPageExcuteSql(string Sql, int PageIndex, int PageSize, string OrderBy, SQLiteParameter[] Paras, ref int TotalCount)
		{
			StringBuilder stringBuilder;
			int num;
			int num2;
			SQLiteParameter[] array;
			int i;
			DataTable result;
			stringBuilder = new StringBuilder();
			if (PageIndex == 1)
			{
				stringBuilder.Append(string.Concat("SELECT TOP ", PageSize, " *  FROM"));
			}
			else
			{
				stringBuilder.Append("SELECT * FROM ");
			}
			stringBuilder.Append(string.Concat(" ( SELECT ROW_NUMBER() OVER (ORDER BY ", OrderBy, ") as RowNumber,tempTable.*"));
			stringBuilder.Append(string.Concat(" FROM ( ", Sql, " ) AS  tempTable ) AS tmp "));
			if (PageIndex != 1)
			{
				stringBuilder.Append("WHERE RowNumber BETWEEN CONVERT(varchar,(@PageIndex-1)*@PageSize+1) AND CONVERT(varchar,(@PageIndex-1)*@PageSize+@PageSize) ");
			}
			stringBuilder.Append(string.Concat(" SELECT @TotalRecord=count(*) from (", Sql, ") tempTable"));
			num = 0;
			num2 = 0;
			if (Paras != null && Paras.Length != 0)
			{
				num2 = Paras.Length;
				array = new SQLiteParameter[num2 + 3];
				i = 0;
				while (i < Paras.Length)
				{
					array[i] = Paras[i];
					num = num + 1;
					i = i + 1;
				}
			}
			else
			{
				array = new SQLiteParameter[num2 + 3];
			}
			array[num] = new SQLiteParameter("@PageIndex", SqlDbType.Int);
			array[num].Value = PageIndex;
			num = num + 1;
			array[num] = new SQLiteParameter("@PageSize", SqlDbType.Int);
			array[num].Value = PageSize;
			num = num + 1;
			array[num] = new SQLiteParameter("@TotalRecord", SqlDbType.Int);
			array[num].Direction = ParameterDirection.Output;
			result = SqliteHelper.ExecuteSqlToTable(stringBuilder.ToString(), array);
			TotalCount = (int)array[num].Value;
			return result;
		}

		public static DataTable CreateSqlByPageExcuteSqlArr(string Sql, int PageIndex, int PageSize, string OrderBy, ArrayList paramList, ref int TotalCount)
		{
			StringBuilder stringBuilder;
			int num;
			int num2;
			SQLiteParameter[] array;
			int i;
			DataTable result;
			stringBuilder = new StringBuilder();
			if (PageIndex == 1)
			{
				stringBuilder.Append(string.Concat("SELECT TOP ", PageSize, " *  FROM"));
			}
			else
			{
				stringBuilder.Append("SELECT * FROM ");
			}
			stringBuilder.Append(string.Concat(" ( SELECT ROW_NUMBER() OVER (ORDER BY ", OrderBy, ") as RowNumber,tempTable.*"));
			stringBuilder.Append(string.Concat(" FROM ( ", Sql, " ) AS  tempTable ) AS tmp "));
			if (PageIndex != 1)
			{
				stringBuilder.Append("WHERE RowNumber BETWEEN CONVERT(varchar,(@PageIndex-1)*@PageSize+1) AND CONVERT(varchar,(@PageIndex-1)*@PageSize+@PageSize) ");
			}
			stringBuilder.Append(string.Concat(" SELECT @TotalRecord=count(*) from (", Sql, ") tempTable"));
			num = 0;
			num2 = 0;
			if (paramList != null && paramList.Count > 0)
			{
				array = new SQLiteParameter[paramList.Count + 3];
				i = 0;
				while (i < paramList.Count)
				{
					array[i] = (SQLiteParameter)paramList[i];
					num = num + 1;
					i = i + 1;
				}
			}
			else
			{
				array = new SQLiteParameter[num2 + 3];
			}
			array[num] = new SQLiteParameter("@PageIndex", SqlDbType.Int);
			array[num].Value = PageIndex;
			num = num + 1;
			array[num] = new SQLiteParameter("@PageSize", SqlDbType.Int);
			array[num].Value = PageSize;
			num = num + 1;
			array[num] = new SQLiteParameter("@TotalRecord", SqlDbType.Int);
			array[num].Direction = ParameterDirection.Output;
			result = SqliteHelper.ExecuteSqlToTable(stringBuilder.ToString(), array);
			TotalCount = (int)array[num].Value;
			return result;
		}

		public static DataTable PagerWithCommand(SQLiteCommand cmd, int PageIndex, int PageSize, string OrderBy, ref int TotalCount)
		{
			StringBuilder stringBuilder;
			SQLiteParameter sQLiteParameter;
			DataTable result;
			stringBuilder = new StringBuilder();
			if (PageIndex == 1)
			{
				stringBuilder.Append(string.Concat("SELECT TOP ", PageSize, " *  FROM"));
			}
			else
			{
				stringBuilder.Append("SELECT * FROM ");
			}
			stringBuilder.Append(string.Concat(" ( SELECT ROW_NUMBER() OVER (ORDER BY ", OrderBy, ") as RowNumber,tempTable.*"));
			stringBuilder.Append(string.Concat(" FROM ( ", cmd.CommandText, " ) AS  tempTable ) AS tmp "));
			if (PageIndex != 1)
			{
				stringBuilder.Append("WHERE RowNumber BETWEEN CONVERT(varchar,(@PageIndex-1)*@PageSize+1) AND CONVERT(varchar,(@PageIndex-1)*@PageSize+@PageSize) ");
			}
			stringBuilder.Append(string.Concat("; SELECT @TotalRecord = count(*) FROM (", cmd.CommandText, ") tempTable"));
			cmd.CommandText = stringBuilder.ToString();
			sQLiteParameter = new SQLiteParameter("@PageIndex", SqlDbType.Int);
			sQLiteParameter.Value = PageIndex;
			cmd.Parameters.Add(sQLiteParameter);
			sQLiteParameter = new SQLiteParameter("@PageSize", SqlDbType.Int);
			sQLiteParameter.Value = PageSize;
			cmd.Parameters.Add(sQLiteParameter);
			sQLiteParameter = new SQLiteParameter("@TotalRecord", SqlDbType.Int);
			sQLiteParameter.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(sQLiteParameter);
			result = SqliteHelper.ExecuteSearch(cmd);
			TotalCount = (int)cmd.Parameters["@TotalRecord"].Value;
			return result;
		}

		public static bool ExecuteTrans(List<string> sqlList, List<SQLiteParameter[]> paraList)
		{
			bool result;
			SQLiteConnection sQLiteConnection;
			SQLiteCommand sQLiteCommand;
			SQLiteTransaction sQLiteTransaction;
			int i;
			
			result = false;
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				sQLiteCommand = new SQLiteCommand();
				sQLiteTransaction = null;
				sQLiteCommand.Connection = sQLiteConnection;
				try
				{
					sQLiteConnection.Open();
					sQLiteTransaction = sQLiteConnection.BeginTransaction();
					sQLiteCommand.Transaction = sQLiteTransaction;
					i = 0;
					while (i < sqlList.Count)
					{
						sQLiteCommand.CommandText = sqlList[i];
						if (paraList != null && paraList[i] != null)
						{
							sQLiteCommand.Parameters.Clear();
							sQLiteCommand.Parameters.AddRange(paraList[i]);
						}
						sQLiteCommand.ExecuteNonQuery();
						i = i + 1;
					}
					sQLiteTransaction.Commit();
					result = true;
				}
				catch (Exception ex)
				{
					try
					{
						sQLiteTransaction.Rollback();
					}
					catch
					{
					}
					throw ex;
				}
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return result;
		}

		public static bool ExecuteTrans(List<string> sqlList, List<List<SQLiteParameter>> paraList)
		{
			bool result;
			SQLiteConnection sQLiteConnection;
			SQLiteCommand sQLiteCommand;
			SQLiteTransaction sQLiteTransaction;
			int i;
			List<SQLiteParameter> list;
			int j;
			
			result = false;
			sQLiteConnection = new SQLiteConnection(SqliteHelper.ConnectionString);
			try
			{
				sQLiteCommand = new SQLiteCommand();
				sQLiteTransaction = null;
				sQLiteCommand.Connection = sQLiteConnection;
				try
				{
					sQLiteConnection.Open();
					sQLiteTransaction = sQLiteConnection.BeginTransaction();
					sQLiteCommand.Transaction = sQLiteTransaction;
					i = 0;
					while (i < sqlList.Count)
					{
						sQLiteCommand.CommandText = sqlList[i];
						if (paraList != null && paraList[i] != null)
						{
							list = paraList[i];
							sQLiteCommand.Parameters.Clear();
							j = 0;
							while (j < list.Count)
							{
								sQLiteCommand.Parameters.Add(list[j]);
								j = j + 1;
							}
						}
						sQLiteCommand.ExecuteNonQuery();
						i = i + 1;
					}
					sQLiteTransaction.Commit();
					result = true;
				}
				catch (Exception ex)
				{
					try
					{
						sQLiteTransaction.Rollback();
					}
					catch
					{
					}
					throw ex;
				}
			}
			finally
			{
				if (sQLiteConnection != null)
				{
					((IDisposable)sQLiteConnection).Dispose();
				}
			}
			return result;
		}

		public SqliteHelper()
		{
			
			
		}

		static SqliteHelper()
		{
			// Note: this type is marked as 'beforefieldinit'.
			
			SqliteHelper._connectionString_inner = string.Concat("Data Source=", Application.StartupPath, "\\WxxWorldRPG.db;Version=3;Password=123698745");
		}
	}
}
