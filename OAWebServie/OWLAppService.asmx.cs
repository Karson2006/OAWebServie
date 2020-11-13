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
        [WebMethod]
        public string SubmitEntertainmentExpensesFormJson(string JsonMessage)
        {
            FileLogger.WriteLog("Json："+JsonMessage, 1, "OAWebService", "SubmitEntertainmentExpendForm", "DataService");
            string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "UpdateData");
            FileLogger.WriteLog("XML："+ xmlString, 1, "OAWebService", "SubmitEntertainmentExpendForm", "DataService");
            string result = SubmitEntertainmentExpensesForm(xmlString);
            result = iTR.Lib.Common.XML2Json(result, "UpdateData");
            return result;
        }

        #region GetActivityExpensesList
        [WebMethod]
        public string GetActivityExpensesList(string xmlMessage)
        {
            string result = "<GetList>" +
                          "<Result>False</Result>" +
                          "<Description></Description></GetList>";
            string logID = Guid.NewGuid().ToString();
            try
            {
                FileLogger.WriteLog(logID + "|Start:" + xmlMessage, 1, "OAWebService", "GetActivityExpensesList", "DataService");

                if (Common.CheckAuthCode("GetList", xmlMessage))
                {
                    OWLBusHelper obj = new OWLBusHelper();
                    result = obj.GeActivityExpendList(xmlMessage);
                }
            }
            catch (Exception err)
            {
                result = "<GetList>" +
                         "<Result>False</Result>" +
                         "<Description>" + err.Message + "</Description></GetList>";
            }
            FileLogger.WriteLog(logID + "|End:" + result, 1, "OAWebService", "GetActivityExpensesList", "DataService");
            return result;
        }
        [WebMethod]
        public string GetActivityExpensesListJson(string JsonMessage)
        {
            string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "GetList");
            string result = GetActivityExpensesList(xmlString);
            result = iTR.Lib.Common.XML2Json(result, "GetList");
            return result;
        }

        #endregion



        #region 提交学术费用单

        [WebMethod]
        public string SubmitActivityExpensesFormJson(string JsonMessage)
        {
            FileLogger.WriteLog("Json：" + JsonMessage, 1, "OAWebService", "SubmitAcademicExpensesFormJson", "DataService");
            string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "UpdateData");
            FileLogger.WriteLog("XML：" + xmlString, 1, "OAWebService", "SubmitAcademicExpensesFormJson", "DataService");
            string result = SubmitActivityExpensesForm(xmlString);
            result = iTR.Lib.Common.XML2Json(result, "UpdateData");
            return result;
        }
        /// <summary>
        /// 提交学术费用单
        /// </summary>
        /// <param name="xmlMessage"></param>
        /// <returns></returns>
        [WebMethod]
        public string SubmitActivityExpensesForm(string xmlMessage)
        {
            string result = "<UpdateData>" +
                          "<Result>False</Result>" +
                          "<Description></Description></UpdateData>";
            string logID = Guid.NewGuid().ToString();
            try
            {
                FileLogger.WriteLog(logID + "|Start:" + xmlMessage, 1, "OAWebService", "SubmitAcademicExpensesForm", "DataService");

                if (Common.CheckAuthCode("SubmitPaymentForm", xmlMessage))
                {
                    OWLBusHelper obj = new OWLBusHelper();
                    result = obj.SubmitActivityExpensesForm(xmlMessage);
                }
            }
            catch (Exception err)
            {
                result = "<UpdateData>" +
                         "<Result>False</Result>" +
                         "<Description>" + err.Message + "</Description></UpdateData>";
            }
            FileLogger.WriteLog(logID + "|End:" + result, 1, "OAWebService", "SubmitAcademicExpensesForm", "DataService");
            return result;
        }

        #endregion


        #region  上传表单附件
        [WebMethod]
        public string UploadFile(string callType, string xmlMessage)
        {
            string result = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                           "<" + callType + ">" +
                           "<Result>False</Result>" +
                           "<Description></Description></" + callType + ">";
            string logID = Guid.NewGuid().ToString();
            try
            {
                //     FileLogger.WriteLog(logID + "|Start:" + xmlMessage, 1, "", callType);
                if (Common.CheckAuthCode(callType, xmlMessage))
                {
                    OWLBusHelper regApp = new OWLBusHelper();
                    result = regApp.UploadFile(xmlMessage);
                }
            }
            catch (Exception err)
            {
                result = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                          "<" + callType + ">" +
                          "<Result>False</Result>" +
                          "<Description>" + err.Message + "</Description></" + callType + ">";
            }
            //  FileLogger.WriteLog(logID + "|End:" + result, 1, "", callType);
            return result;
        }

        [WebMethod]
        public string UploadFileJson(string callType, string JsonMessage)
        {
            string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "UploadFile");
            string result = UploadFile(callType, xmlString);
            result = iTR.Lib.Common.XML2Json(result, "UploadFile");
            return result;
        }
        #endregion
    }
}
