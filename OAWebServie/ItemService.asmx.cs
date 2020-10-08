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
    public class ItemService : System.Web.Services.WebService
    {
        #region GetItemList
        [WebMethod]
        public string GetItemList(string xmlMessage)
        {
                string result =  "<GetList>" +
                              "<Result>False</Result>" +
                              "<Description></Description></GetList>";
                string logID = Guid.NewGuid().ToString();
                try
                {
                    FileLogger.WriteLog( logID + "|Start:" + xmlMessage, 1, "OAWebService", "GetItemList", "DataService" );

                    if (Common.CheckAuthCode("GetList", xmlMessage))
                    {
                        ItemBusHelper obj = new ItemBusHelper();
                        result = obj.GetItemList(xmlMessage);
                    }
                }
                catch (Exception err)
                {
                     result ="<GetList>" +
                              "<Result>False</Result>" +
                              "<Description>"+err.Message+"</Description></GetList>";
                }
                FileLogger.WriteLog(logID + "|End:" + result, 1, "OAWebService", "GetItemList", "DataService");
                return result;
        }
        [WebMethod]
        public string GetItemListJson( string JsonMessage)
        {
            string xmlString = iTR.Lib.Common.Json2XML(JsonMessage, "GetList");
            string result = GetItemList( xmlString);
            result = iTR.Lib.Common.XML2Json(result, "GetList");
            return result;
        }

        #endregion

    }
}
