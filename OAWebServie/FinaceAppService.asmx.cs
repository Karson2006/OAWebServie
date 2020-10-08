using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using iTR.OP.Invoice;
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
    public class FinaceAppService : System.Web.Services.WebService
    {
        #region CheckInvoiceData
        [WebMethod]
        public string CheckInvoiceData(string xmlMessage,string formID,string formType)
        {
                string result =  "<UpdateData>" +
                              "<Result>False</Result>" +
                              "<Description></Description></UpdateData>";
                string logID = Guid.NewGuid().ToString();
                try
                {
                    FileLogger.WriteLog( logID + "|Start:" + xmlMessage+";" + formID +";" + formType, 1, "FinaceAppService", "CheckInvoiceData", "DataService" );

                 
                        OAInvoiceHelper obj = new OAInvoiceHelper();
                        result = obj.UpdateInvoiceDB(xmlMessage, formID, formType);
                   
                }
                catch (Exception err)
                {
                     result = "<UpdateData>" +
                              "<Result>False</Result>" +
                              "<Description>"+err.Message+ "</Description></UpdateData>";
                }
                FileLogger.WriteLog(logID + "|End:" + result, 1, "FinaceAppService", "CheckInvoiceData", "DataService");
                return result;
        }
        //[WebMethod]
        //public string CheckInvoiceDataJson( string JsonMessage)
        //{
        //    string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "GetList");
        //    string result = CheckInvoiceData( xmlString);
        //    result = iTR.Lib.Common.XML2Json(result, "GetList");
        //    return result;
        //}

        #endregion

    }
}
