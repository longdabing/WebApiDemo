using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    //[EnableCors(origins: "http://localhost:8080/", headers: "*", methods: "GET,POST,PUT,DELETE")]
    public class WebApiTestController : ApiController
    {
        [HttpGet]
        public string GetString()
        {
            return "webapitest2222";
        }

        [HttpGet]
        public IHttpActionResult GetStudentInfor()
        {
            List<StudentModel> stlists = new List<StudentModel>();
            stlists.Add(new StudentModel { hno = "1001", hname = "龙大炳", hobject = "WebApi", hscore = "90" });
            stlists.Add(new StudentModel { hno = "1002", hname = "龙大炳", hobject = "Ajax", hscore = "80" });
            stlists.Add(new StudentModel { hno = "1003", hname = "龙大炳", hobject = "SignalR", hscore = "88" });
            return Json<List<StudentModel>>(stlists);//, Newtonsoft.Json.JsonSerializer.CreateDefault.stlists);
        }

        /// <summary>
        /// post，一个参数。用[FromBody]去http的请求体里面去取参数。
        /// Client请求成功
        /// </summary>
        /// <param name="hname"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostStudentInforOnePara([FromBody]string hname)
        {
            List<StudentModel> stlists = new List<StudentModel>();
            stlists.Add(new StudentModel { hno = "1001", hname = "龙", hobject = "WebApi", hscore = "90" });
            stlists.Add(new StudentModel { hno = "1002", hname = "龙大", hobject = "Ajax", hscore = "80" });
            stlists.Add(new StudentModel { hno = "1003", hname = "longdb", hobject = "SignalR", hscore = "88" });
            StudentModel retstu = stlists.FirstOrDefault(stu => stu.hname == hname);

            return Json<StudentModel>(retstu);//, Newtonsoft.Json.JsonSerializer.CreateDefault.stlists);
        }
    }
}
