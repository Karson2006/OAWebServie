using iTR.Lib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TR.OA.BusHelper
{
    public class AuthHelper
    {
        /// <summary>
        /// 保存OA经营授权表
        /// </summary>
        /// <param name="field0001">客户编码</param>
        /// <param name="field0002">客户名称</param>
        /// <param name="field0003">责任人工号</param>
        /// <param name="field0004">责任人</param>
        /// <param name="field0005">授权人</param>
        /// <param name="field0006">客户类型</param>
        /// <returns></returns>
        public bool SaveHospitalAuth(string jsonMessage,string FormatResult="")
        {
            JObject pairs = JObject.Parse(jsonMessage);
            SQLServerHelper runner = new SQLServerHelper();
            runner.ExecuteSqlNone($"insert into formmain_8739(field0001,field0002,field0003,field0004,field0005,field0006) values('{pairs["field0001"]?.ToString()}','{pairs["field0001"]?.ToString()}','{pairs["field0001"]?.ToString()}','{pairs["field0001"]?.ToString()}','{pairs["field0001"]?.ToString()}','{pairs["field0001"]?.ToString()}')");
            //string sql = "";
            //runner.ExecuteSql(sql);

            return true;
        }
    }
}
