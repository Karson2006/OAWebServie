using iTR.Lib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TR.OA.BusHelper
{
    public class OrganizationlHelper
    {
        public OrganizationlHelper()
        {

        }
        /// <summary>
        /// 获取OA 员工数据库的状态信息
        /// </summary>
        /// State：1，在职 2，离职
        /// <returns></returns>
        public string GetEmployeeStatus(string xmlString)
        {

            string result = "", sql = "", status = "", employeeID = "";
            string queryStatus = "False";

            try
            {
                SQLServerHelper runner = new SQLServerHelper();
                DataTable dt;
                XmlDocument doc = new XmlDocument();
                XmlNode vNode = doc.SelectSingleNode("GetRegStatusByMobile/Mobile");
                if (vNode == null || vNode.InnerText.Trim().Length == 0)
                {
                    throw new Exception("Mobile不能为空");
                }
                else
                {
                    sql = @"Select [ID] FEmployeeID, [State],Isnull([RegisterStatus],0) RegisterStatus from v3x.dbo.ORG_MEMBER 
                            Where EXT_ATTR_1  = '" + vNode.InnerText + "'";
                    dt = runner.ExecuteSql(sql);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RegisterStatus"].ToString() == "2")//已离职
                        {
                            status = "4";
                        }
                        else if (!bool.Parse(dt.Rows[0]["RegisterStatus"].ToString()))// RegisterStatus 为Null，第一次使用App
                            status = "1";
                        else
                            status = "2";

                        employeeID = dt.Rows[0]["FEmployeeID"].ToString();
                        queryStatus = "True";


                    }
                    else
                    {
                        status = "0";
                    }

                }
                result = "<GetRegStatusByMobile>" +
                       "<Status>" + status + "</Status>" +
                       "<Result>" + queryStatus + "</Result>" +
                       "<ID>" + employeeID + "</ID>" +
                       "<EmployeeID>" + employeeID + "</EmployeeID>" +
                       "</GetRegStatusByMobile>";
            }
            catch (Exception err)
            {
                throw err;
            }
            return result;
        }
    }

}