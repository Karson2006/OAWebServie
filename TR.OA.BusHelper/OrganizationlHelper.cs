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

        public string GetTeamMemberList(string xmlString)
        {
            string leaderID = "",colName = "";
            string deptID = "", sql = "", result = "";
            SQLServerHelper runner = new SQLServerHelper();
            DataTable deptDt;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlNode vNode = doc.SelectSingleNode("GetTeamMemberList/LeaderID");

                if (vNode != null && vNode.InnerText.Trim().Length > 0)
                {
                    leaderID = vNode.InnerText.Trim();
                }

                vNode = doc.SelectSingleNode("GetTeamMemberList/DeptID");
                if (vNode != null && vNode.InnerText.Trim().Length > 0)
                {
                    deptID = vNode.InnerText.Trim();
                }
                if(leaderID==""&& deptID=="")
                {
                    throw new Exception("部门ID和部门主管ID不能均为空");
                }
                if(deptID !="")//若部门ID不为空，优先通过DeptID获取其直接下属部门和员工
                {

                }
                else if(leaderID != "-88")//获取腾瑞制药公司根节点
                {
                    deptID = "47a1fd79-7530-4af2-9ed3-95d0fbf9a468";
                }
                else// 由LeaderID获取DeptID
                {
                    sql = @"Select field0002 from v3x.dbo.formmain_8662 Where field0005='"+ leaderID + "' ";
                    runner = new SQLServerHelper();
                    deptDt = runner.ExecuteSql(sql);
                    deptID = "";
                    foreach (DataRow dr in deptDt.Rows)
                    {
                        deptID = deptID == "" ? dr["field0002"].ToString() : deptID + "," + dr["field0002"].ToString();
                    }
                }
                doc.LoadXml(result);
                XmlNode pNode = doc.SelectSingleNode("GetTeamMemberList/DataRows");
                doc.SelectSingleNode("GetTeamMemberList/Result").InnerText = "True";
                //获取汇报人为该Leader或授权人包含leader的直接下属
                sql = @"Select t1.ID As ID ,t1.Name As Name,'{0}' As PID,'1' As Detail,,'{0}' AS LeaderID
                        From v3x.dbo.ORG_MEMBER t1
                        Where t2.FLeaderList like '%{0}%' OR EXT_ATTR_37 ='{0}'";
                sql = string.Format(sql, leaderID);

                DataTable memberDt = runner.ExecuteSql(sql);
                foreach (DataRow memberRow in memberDt.Rows)
                {
                    XmlNode cNode = doc.CreateElement("DataRow");
                    pNode.AppendChild(cNode);
                    vNode = null;

                    foreach (DataColumn col in memberDt.Columns)
                    {
                        colName = col.Caption;
                        vNode = doc.CreateElement(colName);
                        vNode.InnerText = memberRow[colName].ToString();
                        cNode.AppendChild(vNode);
                    }
                }
                //获取相应部门部门
                string[] DeptIDs = deptID.Split(',');
                for(int i=0;i<DeptIDs.Length; i++)
                {
                    sql = @"Select t1.ID ,t1.Name As Name,'{0}' As PID,'1' As Detail,'{1}' AS LeaderID
                            From v3x.dbo.ORG_MEMBER t1
                            Where t1.ORG_DEPARTMENT_ID In ('{0}')
                            Union
                            Select t2.ID As ID ,t2.Name As Name,'{0}' As PID,'0' As Detail,t1.field0005 AS LeaderID
                            From v3x.dbo.formmain_8662 t1
                            Left Join v3x.dbo.ORG_UNIT  t2 On t1.field0002 = t2.ID
                            Where t1.FParentID In ('{0}')";

                    sql = string.Format(sql, DeptIDs[i],leaderID );

                    memberDt = runner.ExecuteSql(sql);
                    foreach (DataRow memberRow in memberDt.Rows)
                    {
                        XmlNode cNode = doc.CreateElement("DataRow");
                        pNode.AppendChild(cNode);
                        vNode = null;
                        colName = "";
                        foreach (DataColumn col in memberDt.Columns)
                        {
                            colName = col.Caption;
                            vNode = doc.CreateElement(colName);
                            vNode.InnerText = memberRow[colName].ToString();
                            cNode.AppendChild(vNode);
                        }
                    }
                }
                result = doc.OuterXml;
            }
            catch (Exception err)
            {
                throw err;
            }
            return result;
        }

        #region GetEmployeeStatus
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
        #endregion
    }

}