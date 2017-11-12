using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebApiDemo.Common
{
    public class JSONHelphers
    {
        /// <summary>
        /// JSON反序列化成DataTable
        /// </summary>
        /// <param name="json"></param>
        /// <returns>DataTable</returns>
        public static DataTable JsonToDatableWithJsonNet(string json)
        {
            DataTable data = JsonConvert.DeserializeObject<DataTable>(json);
            return data;
        }

        /// <summary>
        /// 用这个方法把datatable转换成json快点。
        /// datatable分页用的。
        /// 下载个json.net.web.dll
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string DataTableToJsonWithJsonNet(DataTable table)
        {
            string total = string.Empty;
            if (table.Rows.Count > 1)
            {
                total = table.Rows[table.Rows.Count - 1]["cnt"].ToString();//取最后一行，最后一行是条数汇总。
            }
            else
            {
                return "";
            }

            table.Rows.RemoveAt(table.Rows.Count - 1);//移除最后的汇总行。

            string JsonString = string.Empty;
            JsonString = JsonConvert.SerializeObject(table);
            JsonString = JsonString + "￥" + total;
            return JsonString;
        }
    }
}