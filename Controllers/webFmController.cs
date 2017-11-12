using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDemo.Common;

namespace WebApiDemo.Controllers
{
    public class webFmController : ApiController
    {
        [HttpGet]
        public string GetData()
        {
           string sql = @"SELECT TOP 10 0 cnt, '1' ck,no,name,object,score,id FROM dbo.infor 
                WHERE id  IN(SELECT TOP 10 id FROM dbo.infor ORDER BY id)
                UNION ALL
                SELECT COUNT(id) cnt,'1' ck,'' no, '' name,'' object,'' score,'' id FROM dbo.infor";

            DataTable dt = DBHelphers.DBExecuteByAdapter(sql);
            string json = JSONHelphers.DataTableToJsonWithJsonNet(dt);
            return json;
        }
    }
}
