using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApiDemo.Common
{
    public class DBHelphers
    {
        private static SqlConnection conn = new SqlConnection("server=longdabing;database=longtest;uid=sa;pwd=sasa");
        public static void OpenSql()
        {
            conn.Open();
        }
        /// <summary>
        /// 插入,删除，更新。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static bool DBExecute(string sql)
        {
            try
            {
                OpenSql();
                SqlCommand cmd = new SqlCommand(sql, conn);
                //cmd.ExecuteNonQuery();
                cmd.ExecuteNonQuery();
                cmd.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>返回一张表</returns>
        public static DataTable DBExecuteByAdapter(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                OpenSql();
                SqlDataAdapter sdd = new SqlDataAdapter(sql, conn);
                sdd.Fill(dt);
            }
            catch (Exception)
            {
                //return dt;
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }

        /// <summary>
        /// 批量插入数据。
        /// </summary>
        /// <param name="dt"></param>
        public static void sqlBulkCopyData(DataTable dt)
        {
            try
            {
                SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
                bulkCopy.DestinationTableName = "infor";
                bulkCopy.BatchSize = dt.Rows.Count;

                conn.Open();

                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    bulkCopy.ColumnMappings.Add(i, i);  //映射定义数据源中的列和目标表中的列之间的关系
                //}

                if (dt != null && dt.Rows.Count > 0)
                {
                    bulkCopy.WriteToServer(dt);
                }
            }
            catch (Exception EX)
            {

            }
            finally
            {
                conn.Close();
            }

        }


        public static List<string> DBExecuteList(string sql)
        {
            List<string> list = new List<string>();
            try
            {
                OpenSql();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {
                    //将结果集信息添加到返回向量中  
                    list.Add(reader[0].ToString());
                    list.Add(reader[1].ToString());
                    list.Add(reader[2].ToString());
                    list.Add(reader[3].ToString());
                }

                reader.Close();
                cmd.Dispose();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static bool MultiUpdateData(DataTable dt, string Columns, string tableName)
        {
            string sql = string.Format("select {0} from {1}", Columns, tableName);

            try
            {
                conn.Open();

                SqlDataAdapter myDataAdapter = new SqlDataAdapter();
                myDataAdapter.SelectCommand = new SqlCommand(sql, conn);
                SqlCommandBuilder custCB = new SqlCommandBuilder(myDataAdapter);
                custCB.ConflictOption = ConflictOption.OverwriteChanges;
                custCB.SetAllValues = true;

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr.RowState == DataRowState.Unchanged)
                        dr.SetModified();
                }

                myDataAdapter.Update(dt);
                dt.AcceptChanges();
                myDataAdapter.Dispose();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return true;
        }
    }
}