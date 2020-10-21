using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TR.OA.BusHelper;
using iTR.Lib;
 

namespace TR.OAWebServie
{
    /// <summary>
    /// OWLAppService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class OWLAppService : System.Web.Services.WebService
    {
        #region GetEntertainmentExpensesList
        [WebMethod]
        public string GetEntertainmentExpensesList(string xmlMessage)
        {
            string result = "<GetList>" +
                          "<Result>False</Result>" +
                          "<Description></Description></GetList>";
            string logID = Guid.NewGuid().ToString();
            try
            {
                FileLogger.WriteLog(logID + "|Start:" + xmlMessage, 1, "OAWebService", "GetEntertainmentExpensesList", "DataService");

                if (Common.CheckAuthCode("GetList", xmlMessage))
                {
                    OWLBusHelper obj = new OWLBusHelper();
                    result = obj.GetEntertainmentExpendList(xmlMessage);
                }
            }
            catch (Exception err)
            {
                result = "<GetList>" +
                         "<Result>False</Result>" +
                         "<Description>" + err.Message + "</Description></GetList>";
            }
            FileLogger.WriteLog(logID + "|End:" + result, 1, "OAWebService", "GetEntertainmentExpensesList", "DataService");
            return result;
        }
        [WebMethod]
        public string GetEntertainmentExpensesListJson(string JsonMessage)
        {
            string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "GetList");
            string result = GetEntertainmentExpensesList(xmlString);
            result = iTR.Lib.Common.XML2Json(result, "GetList");
            return result;
        }

        #endregion

        [WebMethod]
        public string SubmitEntertainmentExpensesForm(string xmlMessage)
        {
            string result = "<UpdateData>" +
                          "<Result>False</Result>" +
                          "<Description></Description></UpdateData>";
            string logID = Guid.NewGuid().ToString();
            try
            {
                FileLogger.WriteLog(logID + "|Start:" + xmlMessage, 1, "OAWebService", "SubmitEntertainmentExpendForm", "DataService");

                if (Common.CheckAuthCode("UpdateData", xmlMessage))
                {
                    OWLBusHelper obj = new OWLBusHelper();
                    result = obj.SubmitEntertainmentExpendForm(xmlMessage);
                }
            }
            catch (Exception err)
            {
                result = "<UpdateData>" +
                         "<Result>False</Result>" +
                         "<Description>" + err.Message + "</Description></UpdateData>";
            }
            FileLogger.WriteLog(logID + "|End:" + result, 1, "OAWebService", "SubmitEntertainmentExpendForm", "DataService");
            return result;
        }
    }
}
