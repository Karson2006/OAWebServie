using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using TR.OA.BusHelper;
using iTR.Lib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        #endregion GetEntertainmentExpensesList

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
            FileLogger.WriteLog("Json：" + JsonMessage, 1, "OAWebService", "SubmitEntertainmentExpendForm", "DataService");
            string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "UpdateData");
            FileLogger.WriteLog("XML：" + xmlString, 1, "OAWebService", "SubmitEntertainmentExpendForm", "DataService");
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

        #endregion GetActivityExpensesList

        #region 提交学术费用单

        [WebMethod]
        public string SubmitActivityExpensesFormJson(string JsonMessage)
        {
            FileLogger.WriteLog("Json：" + JsonMessage, 1, "OAWebService", "SubmitActivityExpensesFormJson", "DataService");
            string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "UpdateData");
            FileLogger.WriteLog("XML：" + xmlString, 1, "OAWebService", "SubmitActivityExpensesFormJson", "DataService");
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
                FileLogger.WriteLog(logID + "|Start:" + xmlMessage, 1, "OAWebService", "SubmitActivityExpensesForm", "DataService");

                if (Common.CheckAuthCode("UpdateData", xmlMessage))
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
            FileLogger.WriteLog(logID + "|End:" + result, 1, "OAWebService", "SubmitActivityExpensesForm", "DataService");
            return result;
        }

        #endregion 提交学术费用单

        #region 上传表单附件

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
                FileLogger.WriteLog(logID + "|Start:" + xmlMessage, 1, "OAWebService", "UploadFile", "DataService");

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
            FileLogger.WriteLog(logID + "|End:" + result, 1, "OAWebService", "UploadFile", "DataService");
            return result;
        }

        [WebMethod]
        public string UploadFileJson(string callType, string JsonMessage)
        {
            FileLogger.WriteLog("Json：" + JsonMessage, 1, "OAWebService", "UploadFileJson", "DataService");
            string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "UploadFile");
            FileLogger.WriteLog("XML：" + xmlString, 1, "OAWebService", "UploadFileJson", "DataService");
            string result = UploadFile(callType, xmlString);
            result = iTR.Lib.Common.XML2Json(result, "UploadFile");
            return result;
        }

        #endregion 上传表单附件

        #region 获取药瑞宝敏感信息

        [WebMethod]
        public string GetAppSystemInfo(string callType, string xmlMessage)
        {
            string result = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
               "<" + callType + ">" +
               "<Result>False</Result>" +
               "<Description></Description></" + callType + ">";
            string logID = Guid.NewGuid().ToString();
            try
            {
                FileLogger.WriteLog(logID + "|Start:" + xmlMessage, 1, "OAWebService", "GetAppSystemInfo", "DataService");
                if (Common.CheckAuthCode(callType, xmlMessage))
                {
                    OWLBusHelper regApp = new OWLBusHelper();
                    result = regApp.GetAppSystemInfo(xmlMessage);
                }
            }
            catch (Exception err)
            {
                result = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                          "<" + callType + ">" +
                          "<Result>False</Result>" +
                          "<Description>" + err.Message + "</Description></" + callType + ">";
            }
            FileLogger.WriteLog(logID + "|End:" + result, 1, "OAWebService", "GetAppSystemInfo", "DataService");
            return result;
        }

        [WebMethod]
        public string GetAppSystemInfoJson(string callType, string JsonMessage)
        {
            FileLogger.WriteLog("Json：" + JsonMessage, 1, "OAWebService", "GetAppSystemInfoJson", "DataService");
            string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "GetAppSystemInfo");
            FileLogger.WriteLog("XML：" + xmlString, 1, "OAWebService", "GetAppSystemInfoJson", "DataService");
            string result = GetAppSystemInfo(callType, xmlString);
            result = iTR.Lib.Common.XML2Json(result, "GetAppSystemInfo");
            return result;
        }

        #endregion 获取药瑞宝敏感信息

        #region 获取销量，支付，流程数据

        //一级个人页面
        [WebMethod]
        public string GetPersonSummaryReport(string JsonMessage)
        {
            string result = "";
            result = GetCompassReport(JsonMessage, "GetPersonSummaryReport");
            return result;
        }

        //流程子页面
        [WebMethod]
        public string GetPersonFlowReport(string JsonMessage)
        {
            string result = "";
            result = GetCompassReport(JsonMessage, "GetPersonFlowReport");
            return result;
        }

        //支付子页面
        [WebMethod]
        public string GetPersonPayReport(string JsonMessage)
        {
            string result = "";
            result = GetCompassReport(JsonMessage, "GetPersonPayReport");
            return result;
        }

        [WebMethod]
        public string NewGetPersonPayReport(string JsonMessage)
        {
            string result = "";
            result = GetCompassReport(JsonMessage, "GetPersonPayReport",true);
            return result;
        }

        //销量子页面
        [WebMethod]
        public string GetPersonSalesReport(string JsonMessage)
        {
            string result = "";
            result = GetCompassReport(JsonMessage, "GetPersonSalesReport");
            return result;
        }

        //支付查询
        [WebMethod]
        public string PayQuery(string JsonMessage)
        {
            string result = "";
            result = GetCompassReport(JsonMessage, "PayQuery");
            return result;
        }
        //支付查询
        [WebMethod]
        public string NewPayQuery(string JsonMessage)
        {
            string result = "";
            result = GetCompassReport(JsonMessage, "NewPayQuery");
            return result;
        }

        //新版本一级个人页面
        [WebMethod]
        public string NewGetPersonSummaryReport(string JsonMessage)
        {
            string result = "";
            result = GetCompassReport(JsonMessage, "SaveAuthData", true);
            return result;
        }

        [WebMethod]
        public string SaveAuthData(string   JsonMessage)
        {
            string result = "200";
            JsonMessage = JsonMessage.Replace("\\", "");
            result = GetCompassReport(JsonMessage, "SaveAuthData", true);
            return result;
        }
        //报表统一入口
        public string GetCompassReport(string JsonMessage, string callType,bool newQuery=false)
        {

            string result, FormatResult = "{{\"{0}\":{{\"Result\":{1},\"Description\":{2},\"DataRows\":{3} }} }}";
            result = string.Format(FormatResult, callType, "\"False\"", "", "");
            string logID = Guid.NewGuid().ToString();

            try
            {
                FileLogger.WriteLog(logID + "Json验证：" + JsonMessage + "FormatResult" + FormatResult + "callType" + callType, 1, "OAWebService", callType, "DataService");
                // FileLogger.WriteLog(logID + "|Start:" + JsonMessage, 1, "", callType);
                OWLBusHelper perRpt = new OWLBusHelper();

                if (Common.CheckAuthCode("GetData", JsonMessage, "json"))
                {
                    FileLogger.WriteLog("验证Json：" + JsonMessage + "FormatResult" + FormatResult + "callType" + callType, 1, "OAWebService", callType, "DataService");
                    //罗盘主页
                    if (callType == "GetPersonSummaryReport")
                    {
                        //OWLBusHelper perRpt = new OWLBusHelper();
                        //没有类型判断，全部获取
                        result = perRpt.GetPersonSummaryReport(JsonMessage, FormatResult, callType, newQuery);
                    }
                    //流程子页面
                    else if (callType == "GetPersonFlowReport")
                    {
                        OWLBusHelper perChildRpt = new OWLBusHelper();
                        result = perChildRpt.GetComPassChildData(JsonMessage, FormatResult, callType, "3");
                    }
                    //支付子页面
                    else if (callType == "GetPersonPayReport")
                    {
                        OWLBusHelper perChildRpt = new OWLBusHelper();
                        result = perChildRpt.GetComPassChildData(JsonMessage, FormatResult, callType, "4",newQuery);
                    }
                    //销量子页面
                    else if (callType == "GetPersonSalesReport")
                    {
                        OWLBusHelper perChildRpt = new OWLBusHelper();
                        result = perChildRpt.GetComPassChildData(JsonMessage, FormatResult, callType, "6");
                    }
                    //支付查询
                    else if (callType == "PayQuery")
                    {
                        OWLBusHelper perChildRpt = new OWLBusHelper();
                        result = perChildRpt.PayQuery(JsonMessage, FormatResult, callType);
                    }
                    //支付查询
                    else if (callType == "NewPayQuery")
                    {
                        OWLBusHelper perChildRpt = new OWLBusHelper();
                        result = perChildRpt.NewPayQuery(JsonMessage, FormatResult, callType);
                    }
                    //保存OA经营授权表
                    else if (callType ==  "SaveAuthData")
                    {
                        AuthHelper authHelper = new AuthHelper();
                        result = authHelper.SaveHospitalAuth(JsonMessage, FormatResult);
                    }
                }
            }
            catch (Exception err)
            {
                result = string.Format(FormatResult, callType, "\"False\"", err.Message, "");
            }
            FileLogger.WriteLog(logID + "Json：" + result, 1, "OAWebService", callType, "DataService");
            return result;
        }

        #endregion 获取销量，支付，流程数据


        #region 获取OA医院列表
        [WebMethod]
        public string GetOAHospitalList(string xmlString)
        {
            FileLogger.WriteLog("XML：" + xmlString, 1, "OAWebService", "GetOAHospitalList", "DataService");
            HospitalHelper h = new HospitalHelper();
            string result = h.GetOAHospitalList(xmlString);
            return result;
        }
        #endregion

        #region 获取OA医院列表
        [WebMethod]
        public string GetOAProductList(string xmlString)
        {
            FileLogger.WriteLog("XML：" + xmlString, 1, "OAWebService", "GetOAHospitalList", "DataService");
            ItemBusHelper obj= new ItemBusHelper();
            string result = obj.GetProductList(xmlString);
            return result;
        }
        #endregion

        #region 获取用户状态
        [WebMethod]
        public string GetRegStatusByMobile(string xmlString)
        {
            FileLogger.WriteLog("XML：" + xmlString, 1, "OAWebService", "GetRegStatusByMobile", "DataService");
            OrganizationlHelper obj = new OrganizationlHelper();
            string result = obj.GetEmployeeStatus(xmlString);
            return result;
        }
        #endregion


        #region 获取指定部门或部门主管的直接下属与部门
        [WebMethod]
        public string GetTeamMemberList(string xmlString)
        {
            FileLogger.WriteLog("XML：" + xmlString, 1, "OAWebService", "GetRegStatusByMobile", "DataService");
            OrganizationlHelper obj = new OrganizationlHelper();
            string result = obj.GetTeamMemberList(xmlString);
            return result;
        }
        #endregion


    }
}