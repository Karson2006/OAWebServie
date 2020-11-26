using iTR.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;

namespace TR.OA.BusHelper
{
    public class OWLBusHelper
    {
        private SQLServerHelper runner = null;

        public OWLBusHelper()
        {
        }

        #region GetEntertainmentExpendList

        public string GetEntertainmentExpendList(string xmlString)
        {
            string sql = "", result = "", EmployeeeID = "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            XmlNode node = doc.SelectSingleNode("GetList/EmployeeID");
            if (node != null)
                EmployeeeID = node.InnerText;
            else
                throw new Exception("EmployeeID节点不存在");

            sql = @" Select ID,field0001,field0002,field0004,field0009,field0012,field0020,field0018,field0019,field0015,field0013
                          From v3x.dbo.formmain_6185
                          Where field0016 = {0}";
            sql = string.Format(sql, EmployeeeID);
            runner = new SQLServerHelper();
            DataTable dt = runner.ExecuteSql(sql);
            result = iTR.Lib.Common.DataTableToXml(dt, "GetList", "", "List");
            return result;
        }

        #endregion GetEntertainmentExpendList

        #region GeActivityExpendList

        public string GeActivityExpendList(string xmlString)
        {
            string sql = "", result = "", EmployeeeID = "";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            XmlNode node = doc.SelectSingleNode("GetList/EmployeeID");
            if (node != null)
                EmployeeeID = node.InnerText;
            else
                throw new Exception("EmployeeID节点不存在");

            sql = @" Select field0001, field0002,field0004,field0006,field0009,field0011,field0012,field0013,field0015,field0018,field0020,field0021,field0024,field0023
                     from v3x.dbo.formmain_6187 Where field0016 = {0}";
            sql = string.Format(sql, EmployeeeID);
            runner = new SQLServerHelper();
            DataTable dt = runner.ExecuteSql(sql);
            result = iTR.Lib.Common.DataTableToXml(dt, "GetList", "", "List");
            return result;
        }

        #endregion GeActivityExpendList

        #region SubmitEntertainmentExpendForm

        public string SubmitEntertainmentExpendForm(string xmlString)
        {
            string sql = "", EmployeeeID = "", rowDatas = "", loginName = "", EmployeeeName = "";
            string result = @"<UpdateData><Result>False</Result><Description></Description></UpdateData>";

            #region 参数定义 参考

            string formID = ""; //申请日期
            string field0003 = ""; //申请日期
            string field0006 = "";  //单号
            string field0007 = "";  //成本中心
            string field0010 = "";  //工号
            string field0011 = "";  //所在部门
            string field0012 = "";  //收款开户行
            string field0013 = "";  //收款银行账号
            string field0017 = "";  //月度申请总额
            string field0018 = "";  //招待日期
            string field0019 = "";  //计划日期
            string field0020 = "";  //预计人数
            string field0021 = "";  //实际人数
            string field0022 = "";  //事由
            string field0023 = "";  //申请金额
            string field0024 = "";  //报销金额
            string field0025 = "";  //申请单单号
            string field0031 = "";  //是否按计划
            string field0035 = "";  //医院
            string field0036 = "";  //可用额
            string field0037 = "";  //在途额
            string field0045 = "";  //费用来源
            string field0050 = "";  //成本中心_归档
            string field0056 = "";  //招待费明细账已用额

            #endregion 参数定义 参考

            #region FormData

            string formData = @"<formExport version=""2.0""><summary id=""7810778149231805299"" name=""formmain_5935""/>
                                            <definitions/>
                                            <values>
                                            <column name=""公司""><value>上海腾瑞制药有限公司</value></column>
                                            <column name=""申请人""><value>{0}</value></column>
                                            <column name=""申请日期""><value>{1}</value></column>
                                            <column name=""总金额""><value>{2}</value></column>
                                            <column name=""单号""><value>{3}</value></column>
                                            <column name=""成本中心""><value>{4}</value></column>
                                            <column name=""申请单""><value>{3}</value></column>
                                            <column name=""工号""><value>{5}</value></column>
                                            <column name=""所在部门""><value>{6}</value></column>
                                            <column name=""收款开户行""><value>{7}</value></column>
                                            <column name=""收款银行账号""><value>{8}</value></column>
                                            <column name=""表单类型""><value>招待费报销单</value></column>
                                            <column name=""查看查验结果""><value>通过</value></column>
                                            <column name=""月度申请总额""><value>{9}</value></column>
                                            <column name=""招待日期""><value>{10}</value></column>
                                            <column name=""计划日期""><value>{11}</value></column>
                                            <column name=""预计人数""><value>{12}</value></column>
                                            <column name=""实际人数""><value>{13}</value></column>
                                            <column name=""事由""><value>{14}</value></column>
                                            <column name=""申请金额""><value>{15}</value></column>
                                            <column name=""报销金额""><value>{2}</value></column>
                                            <column name=""申请单单号""><value>{16}</value></column>
                                            <column name=""是否按计划""><value>{17}</value></column>
                                            <column name=""费用类型""><value>业务招待费</value></column>
                                            <column name=""医院""><value>{18}</value></column>
                                            <column name=""财务审批标志""><value>{19}</value></column>
                                            <column name=""费用来源""><value>{20}</value></column>
                                            <column name=""是否有发票""><value>0</value></column>
                                            <column name=""成本中心_归档""><value>{4}</value></column>
                                            <column name=""YRB发起""><value>1</value></column>
                                            <column name=""InvoiceMain_ID""><value>{22}</value></column>
                                            </values>
                                            <subForms>
                                            <subForm>
                                            <definitions>
                                            <column id=""field0039"" type=""0"" name=""编码_分摊"" length=""255""/>
                                            <column id=""field0040"" type=""0"" name=""医院_分摊"" length=""255""/>
                                            <column id=""field0041"" type=""4"" name=""分摊金额"" length=""20""/>
                                            <column id=""field0042"" type=""0"" name=""分摊说明"" length=""255""/>
                                            <column id=""field0043"" type=""0"" name=""编码_分摊_0"" length=""255""/>
                                            <column id=""field0044"" type=""0"" name=""编码_分摊_1"" length=""255""/>
                                            </definitions>
                                            <values>{21}</values>
                                            </subForm>
                                            </subForms>
                                            </formExport>";

            string rowData = @" <row>
                                                <column name=""编码_分摊""><value>{0}</value></column>
                                                <column name=""医院_分摊""><value>{1}</value></column>
                                                <column name=""分摊金额""><value>{2}</value></column>
                                                <column name=""分摊说明""><value>{3}</value></column>
                                                <column name=""编码_分摊_0""><value>{4}</value>
                                                </column><column name=""编码_分摊_1""><value>{5}</value></column>
                                            </row>";

            #endregion FormData

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlNode node = doc.SelectSingleNode("UpdateData/field0002");
                if (node != null && node.InnerText.Trim().Length > 0)
                    EmployeeeID = node.InnerText;
                else
                    throw new Exception("申请人field0002节点不存在或为空");

                sql = @"Select t1.ID As FEmployeeID, t1.Code As FEmployeeCode,t1.ORG_DEPARTMENT_ID AS FDeptID, Isnull(t2.field0008,'') As FCostCenter,
                            Isnull(t3.field0003,'') As FBank,Isnull(t3.field0005,'') As FAccount,Isnull(t3.field0004,'') As FCommpany,t4.LOGIN_NAME,t1.Name AS FEmployeeName
                            From  v3x.dbo.ORG_MEMBER t1
                            Left Join v3x.dbo.formmain_5499 t2 On t1.ORG_DEPARTMENT_ID= t2.field0002
                            Left Join v3x.dbo.formmain_8130 t3 On t3.field0002= t1.Code
                            Left Join v3x.dbo.ORG_PRINCIPAL t4 On t1.ID= t4.MEMBER_ID
                            Where  t1.ID='{0}' ";
                sql = string.Format(sql, EmployeeeID);
                runner = new SQLServerHelper();
                DataTable dt = runner.ExecuteSql(sql);
                if (dt.Rows.Count > 0)
                {
                    field0010 = dt.Rows[0]["FEmployeeCode"].ToString();
                    field0011 = dt.Rows[0]["FDeptID"].ToString();
                    field0007 = dt.Rows[0]["FCostCenter"].ToString();
                    field0050 = dt.Rows[0]["FCostCenter"].ToString();
                    field0012 = dt.Rows[0]["FBank"].ToString();
                    field0013 = dt.Rows[0]["FAccount"].ToString();
                    loginName = dt.Rows[0]["LOGIN_NAME"].ToString();
                    EmployeeeName = dt.Rows[0]["FEmployeeName"].ToString();
                }

                field0003 = DateTime.Now.ToString("yyyy-MM-dd");
                node = doc.SelectSingleNode("UpdateData/field0006");
                if (node != null && node.InnerText.Trim().Length > 0)
                    field0006 = node.InnerText.Trim();
                else
                    throw new Exception("单号不能为空");
                //读取招待费明细表可用额
                sql = @"Select t1.field0006 As field0045,t2.SHOWVALUE AS field0045_Name,Isnull(t1.field0009,0) As field0036,Isnull(t3.field0011,0) As field0017,
                            Isnull(t1.field0007,0) As field0056,t1.field0018 AS field0025,Isnull(t1.field0008,0) AS field0037
                            from v3x.dbo.formmain_6185 t1
                            Left Join v3x.dbo.CTP_ENUM_ITEM  t2 On t1.field0006= t2.ID
                            Left Join v3x.dbo.formmain_6180  t3 On t1.field0018= t3.field0001
                            Where t1.field0001='" + field0006 + "'";
                dt = runner.ExecuteSql(sql);
                if (dt.Rows.Count > 0)
                {
                    field0045 = dt.Rows[0]["field0045"].ToString();
                    field0036 = dt.Rows[0]["field0036"].ToString();
                    field0017 = dt.Rows[0]["field0017"].ToString();
                    field0056 = dt.Rows[0]["field0056"].ToString();
                    field0025 = dt.Rows[0]["field0025"].ToString();
                    field0037 = dt.Rows[0]["field0037"].ToString();
                }
                //不能分次核销,在途额 +  可用额
                if (decimal.Parse(field0037) + decimal.Parse(field0056) > 0)
                    throw new Exception("招待费不能分次核销");

                //是否按计划
                node = doc.SelectSingleNode("UpdateData/field0031");
                if (node != null)
                    field0031 = node.InnerText.Trim().Length > 0 ? node.InnerText : "1";
                //计划日期
                node = doc.SelectSingleNode("UpdateData/field0019");
                if (node != null)
                    field0019 = node.InnerText.Trim().Substring(0, 10);
                //招待日期
                if (field0031 == "1")
                    field0018 = field0019;
                else
                {
                    node = doc.SelectSingleNode("UpdateData/field0018");
                    if (node != null && node.InnerText.Trim().Length > 0)
                        field0018 = node.InnerText.Trim().Substring(0, 10);
                    else
                        throw new Exception("不按计划进行时，招待费日期不能为空");
                }
                //预计人数
                node = doc.SelectSingleNode("UpdateData/field0020");
                if (node != null)
                    field0020 = node.InnerText.Trim();
                //实际人数
                node = doc.SelectSingleNode("UpdateData/field0021");
                if (node != null)
                    field0021 = node.InnerText.Trim();

                //事由
                node = doc.SelectSingleNode("UpdateData/field0022");
                if (node != null)
                    field0022 = node.InnerText.Trim();
                //InvoiceMain_ID
                node = doc.SelectSingleNode("UpdateData/formID");
                if (node != null && node.InnerText.Trim().Length > 0)
                    formID = node.InnerText.Trim();
                else
                    throw new Exception("formID不能为空");

                //申请金额
                node = doc.SelectSingleNode("UpdateData/field0023");
                if (node != null && node.InnerText.Trim().Length > 0)
                    field0023 = node.InnerText.Trim();
                else
                    throw new Exception("申请金额不能为空");

                //报销金额
                node = doc.SelectSingleNode("UpdateData/field0024");
                if (node != null)
                    field0024 = node.InnerText.Trim();
                else
                    throw new Exception("报销金额不能为空");

                if (field0024.Trim().Length == 0)
                    throw new Exception("报销金额不能为空");

                if (decimal.Parse(field0023) < decimal.Parse(field0024))
                    throw new Exception("报销金额不能大于申请金额");

                if (decimal.Parse(field0017) < decimal.Parse(field0024))
                    throw new Exception("报销金额不能大于月度可用额");

                ////费用申请单号
                //node = doc.SelectSingleNode("UpdateData/field0025");
                //if (node != null)
                //    field0025 = node.InnerText.Trim();

                //医院
                node = doc.SelectSingleNode("UpdateData/field0035");
                if (node != null)
                    field0035 = node.InnerText.Trim();

                XmlNodeList nodes = doc.SelectNodes("UpdateData/DataRows/DataRow");
                //分摊医院不能为空
                if (nodes.Count == 0)
                    throw new Exception("分摊医院节点不能为空");

                Dictionary<string, string> hospitalList = new Dictionary<string, string>();
                string field0039 = "";

                foreach (XmlNode row in nodes)
                {
                    if (row["field0043"].InnerText.Trim().Length > 0)
                        field0039 = row["field0043"].InnerText.Trim();
                    if (!hospitalList.ContainsKey(field0039))
                        hospitalList.Add(field0039, row["field0040"].InnerText);
                    rowDatas = rowDatas + string.Format(rowData, field0039, row["field0040"].InnerText, row["field0041"].InnerText,
                        row["field0042"].InnerText, row["field0043"].InnerText, row["field0044"].InnerText);
                }
                if (hospitalList.ContainsKey("OA_19888"))
                    throw new Exception("费用不能分摊到【跨区跨院活动】，请选择具体医院");

                if (nodes.Count > hospitalList.Count)
                    throw new Exception("分摊医院不能重复");

                formData = string.Format(formData, EmployeeeID, field0003, field0024, field0006, field0007, field0010, field0011, field0012, field0013, field0017, field0018, field0019,
                    field0020, field0021, field0022, field0023, field0025, field0031, field0035, -1, field0045, rowDatas, formID);

                //doc.LoadXml(formData)

                sql = @"Insert Into DataService.dbo.OATask([FTemplateCode],[FSenderLoginName],[FEmployeeCode],[FEmployeeName],[FSubject],[FData])
	                     Values('O202001','{0}','{1}','{2}','{3}' , '{4}')";
                sql = string.Format(sql, loginName, field0010, EmployeeeName, "招待费报销-YRB-" + EmployeeeName + "-" + DateTime.Now.ToString(), formData);
                runner.ExecuteSqlNone(sql);

                result = "<UpdateData><Result>True</Result><Description></Description></UpdateData>";
            }
            catch (Exception err)
            {
                result = "<UpdateData><Result>False</Result><Description>" + err.Message + "</Description></UpdateData>";
            }
            return result;
        }

        #endregion SubmitEntertainmentExpendForm

        #region 提交学术活动费用支付单3.0

        public string SubmitActivityExpensesForm(string xmlString)
        {
            string sql = "", EmployeeeID = "", rowDatas = "", loginName = "", EmployeeeName = "";
            string result = @"<UpdateData><Result>False</Result><Description></Description></UpdateData>";
            try
            {
                #region 参数定义

                string formID = ""; //
                string field0003 = ""; //申请日期
                string field0006 = "";  //单号
                string field0007 = "";  //成本中心
                string field0010 = "";  //工号
                string field0011 = "";  //所在部门
                string field0012 = "";  //收款开户行
                string field0013 = "";  //收款银行账号
                string field0017 = "";  //月度申请总额
                string field0018 = "";  //举办日期
                string field0019 = "";  //计划日期
                string field0020 = "";  //实际人数
                string field0021 = "";  //事由
                string field0022 = "";  //申请金额
                string field0023 = "";  //报销金额
                string field0024 = "";  //申请单单号
                string field0025 = "";  //医院
                string field0032 = "";  //学术材料":
                string field0033 = "";  //会议议程":
                string field0034 = "";  //会议纪要":
                string field0035 = "";  //课酬附件":
                string field0036 = "";  //会议照片":
                string field0037 = "";  //参会名单":
                string field0043 = "";  //规模
                string field0044 = "";  //是否按计划
                string field0047 = "";  //个人报销金额
                string field0053 = "";  //可用额
                string field0054 = "";  //在途额
                string field0063 = "";  //费用来源

                string field0065 = "";   //活动类型
                string field0067 = "";  //成本中心_归档
                string field1000 = "";  //已用额
                string field0062 = "";  //付款方式

                string field0064 = ""; //付款阶段
                string bankRows = "", hospitalRows = "";

                #endregion 参数定义

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);

                #region xml取值

                XmlNode node = doc.SelectSingleNode("UpdateData/field0002");
                if (node != null && node.InnerText.Trim().Length > 0)
                    EmployeeeID = node.InnerText;
                else
                    throw new Exception("申请人field0002节点不存在或为空");
                node = doc.SelectSingleNode("UpdateData/field0006");
                if (node != null && node.InnerText.Trim().Length > 0)
                    field0006 = node.InnerText.Trim();
                else
                    throw new Exception("单号不能为空");
                //是否按计划
                //node = doc.SelectSingleNode("UpdateData/field0044");
                node = doc.SelectSingleNode("UpdateData/field0031");
                if (node != null)
                    field0044 = node.InnerText.Trim().Length > 0 ? node.InnerText : "1";

                //计划日期
                node = doc.SelectSingleNode("UpdateData/field0019");
                if (node != null)
                    field0019 = node.InnerText.Trim().Substring(0, 10);

                //招待日期
                if (field0044 == "1")
                    field0018 = field0019;
                else
                {
                    node = doc.SelectSingleNode("UpdateData/field0018");
                    if (node != null && node.InnerText.Trim().Length > 0)
                        field0018 = node.InnerText.Trim().Substring(0, 10);
                    else
                        throw new Exception("不按计划进行时，举办日期不能为空");
                }

                //实际人数
                //node = doc.SelectSingleNode("UpdateData/field0020");
                node = doc.SelectSingleNode("UpdateData/field0021");
                if (node != null)
                    field0020 = node.InnerText.Trim();

                //事由
                //node = doc.SelectSingleNode("UpdateData/field0021");
                node = doc.SelectSingleNode("UpdateData/field0022");
                if (node != null)
                    field0021 = node.InnerText.Trim();

                //InvoiceMain_ID
                node = doc.SelectSingleNode("UpdateData/formID");
                if (node != null && node.InnerText.Trim().Length > 0)
                    formID = node.InnerText.Trim();
                else
                    throw new Exception("formID不能为空");

                //禁止插入相同formID
                sql = $@"select * from formmain_6185 where ID = {formID}";
                runner = new SQLServerHelper();
                DataTable dtForm = runner.ExecuteSql(sql);
                if (dtForm.Rows.Count > 1)
                {
                    throw new Exception("formID已存在");
                }
                //申请金额
                //node = doc.SelectSingleNode("UpdateData/field0022");
                node = doc.SelectSingleNode("UpdateData/field0009");
                if (node != null && node.InnerText.Trim().Length > 0)
                    field0022 = node.InnerText.Trim();
                else
                    throw new Exception("申请金额不能为空");

                //医院
                node = doc.SelectSingleNode("UpdateData/field0025");
                if (node != null)
                    field0025 = node.InnerText.Trim();

                #endregion xml取值

                #region 获取个人资料

                sql = @"Select t1.ID As FEmployeeID, t1.Code As FEmployeeCode,t1.ORG_DEPARTMENT_ID AS FDeptID, Isnull(t2.field0008,'') As FCostCenter,
                            Isnull(t3.field0003,'') As FBank,Isnull(t3.field0005,'') As FAccount,Isnull(t3.field0004,'') As FCommpany,t4.LOGIN_NAME,t1.Name AS FEmployeeName
                            From  v3x.dbo.ORG_MEMBER t1
                            Left Join v3x.dbo.formmain_5499 t2 On t1.ORG_DEPARTMENT_ID= t2.field0002
                            Left Join v3x.dbo.formmain_8130 t3 On t3.field0002= t1.Code
                            Left Join v3x.dbo.ORG_PRINCIPAL t4 On t1.ID= t4.MEMBER_ID
                            Where  t1.ID='{0}' ";
                sql = string.Format(sql, EmployeeeID);
                runner = new SQLServerHelper();
                DataTable dt = runner.ExecuteSql(sql);

                if (dt.Rows.Count > 0)
                {
                    field0010 = dt.Rows[0]["FEmployeeCode"].ToString();
                    field0011 = dt.Rows[0]["FDeptID"].ToString();
                    field0007 = dt.Rows[0]["FCostCenter"].ToString();
                    field0067 = dt.Rows[0]["FCostCenter"].ToString();
                    field0012 = dt.Rows[0]["FBank"].ToString();
                    field0013 = dt.Rows[0]["FAccount"].ToString();
                    loginName = dt.Rows[0]["LOGIN_NAME"].ToString();
                    EmployeeeName = dt.Rows[0]["FEmployeeName"].ToString();
                }

                #endregion 获取个人资料

                #region 金额判断

                //读取学术费明细表可用额
                sql = @"Select t1.field0006 As field0063,t2.SHOWVALUE AS field0045_Name,Isnull(t1.field0009,0) As field0053,Isnull(t3.field0011,0) As field0017,
                            Isnull(t1.field0007,0) As field1000,t1.field0018 AS field0024,Isnull(t1.field0008,0) AS field0054
                            from v3x.dbo.formmain_6187 t1
                            Left Join v3x.dbo.CTP_ENUM_ITEM  t2 On t1.field0006= t2.ID
                            Left Join v3x.dbo.formmain_6180  t3 On t1.field0018= t3.field0001
                            Where t1.field0001='" + field0006 + "'";
                dt = runner.ExecuteSql(sql);
                if (dt.Rows.Count > 0)
                {
                    //费用来源
                    field0063 = dt.Rows[0]["field0063"].ToString();
                    //可用额
                    field0053 = dt.Rows[0]["field0053"].ToString();
                    //月度申请总额
                    field0017 = dt.Rows[0]["field0017"].ToString();
                    //学术费用明细账 已用额
                    field1000 = dt.Rows[0]["field1000"].ToString();
                    //申请单单号
                    field0024 = dt.Rows[0]["field0024"].ToString();
                    //在途额
                    field0054 = dt.Rows[0]["field0054"].ToString();
                }

                //报销金额
                //node = doc.SelectSingleNode("UpdateData/field0023");
                node = doc.SelectSingleNode("UpdateData/field0035");
                if (node != null)
                    field0023 = node.InnerText.Trim();
                else
                    throw new Exception("报销金额不能为空");

                if (field0023.Trim().Length == 0)
                    throw new Exception("报销金额不能为空");

                if (decimal.Parse(field0022) < decimal.Parse(field0023))
                    throw new Exception("报销金额不能大于申请金额");

                if (decimal.Parse(field0017) < decimal.Parse(field0053))
                    throw new Exception("报销金额不能大于月度可用额");

                #endregion 金额判断

                //赋值申请日期
                field0003 = DateTime.Now.ToString("yyyy-MM-dd");

                #region 分摊医院

                string hosRow = @"<row><column name=""编码_分摊_0""><value>{0}</value></column>
                                    <column name=""名称_分摊""><value>{1}</value></column>
                                    <column name=""分摊金额""><value>{2}</value></column>
                                    <column name=""分摊说明""><value>{3}</value></column>
                                    <column name=""编码_分摊_1""><value>{0}</value></column>
                                    <column name=""科室_分摊""><value>{4}</value></column>
                                    <column name=""编码_分摊""><value>{5}</value></column>
                                    </row>";
                XmlNodeList nodes = doc.SelectNodes("UpdateData/HDataRows/hospit");

                //分摊医院不能为空
                if (nodes.Count == 0)
                    throw new Exception("分摊医院节点不能为空");
                string hosstr = "", field24 = "", field12 = "", field58 = "", field42 = "", field61 = "";
                hosstr = doc.SelectSingleNode("UpdateData/HDataRows/hospit/field0024").InnerText.Trim();
                Dictionary<string, string> verifyList = new Dictionary<string, string>();
                //获取分摊医院信息
                for (int i = 0; i < hosstr.Split('|').Length; i++)
                {
                    field24 = doc.SelectSingleNode("UpdateData/HDataRows/hospit/field0024").InnerText.Trim().Split('|')[i];
                    field12 = doc.SelectSingleNode("UpdateData/HDataRows/hospit/field0012").InnerText.Trim().Split('|')[i];
                    field58 = doc.SelectSingleNode("UpdateData/HDataRows/hospit/field0058").InnerText.Trim().Split('|')[i];
                    field42 = doc.SelectSingleNode("UpdateData/HDataRows/hospit/field0042").InnerText.Trim().Split('|')[i];
                    field61 = doc.SelectSingleNode("UpdateData/HDataRows/hospit/field0061").InnerText.Trim().Split('|')[i];
                    if (!verifyList.ContainsKey(field24))
                        //field0057 名称_分摊
                        verifyList.Add(field24, field12);
                    //编码分摊放到最后一个
                    hospitalRows = hospitalRows + string.Format(hosRow, field24, field12,
                    field58, field42, field61, field24);
                }
                //foreach (XmlNode row in nodes)
                //{
                //    //if (row["field0056"].InnerText.Trim().Length > 0)
                //    //    field0062 = row["field0056"].InnerText.Trim();
                //    //else
                //    //    field0062 = row["field0062"].InnerText.Trim();
                //    //if (!verifyList.ContainsKey(field0062))
                //    //    //field0057 名称_分摊
                //    //    verifyList.Add(field0062, row["field0057"].InnerText);
                //    ////编码分摊放到最后一个
                //    //hosRow = hosRow + string.Format(hosRow, row["field0056"].InnerText, row["field0057"].InnerText,
                //    //row["field0058"].InnerText, row["field0059"].InnerText,  row["field0061"].InnerText, field0062);

                //    if (row["field0024"].InnerText.Trim().Length > 0)
                //        tempcode = row["field0024"].InnerText.Trim();
                //    if (!verifyList.ContainsKey(tempcode))
                //        //field0057 名称_分摊
                //        verifyList.Add(tempcode, row["field0012"].InnerText);
                //    //编码分摊放到最后一个
                //    hospitalRows = hospitalRows + string.Format(hosRow, row["field0024"].InnerText, row["field0012"].InnerText,
                //    row["field0058"].InnerText, row["field0042"].InnerText, row["field0061"].InnerText, tempcode);
                //}

                if (verifyList.ContainsKey("OA_19888"))
                    throw new Exception("费用不能分摊到【跨区跨院活动】，请选择具体医院");

                if (nodes.Count > verifyList.Count)
                    throw new Exception("分摊医院不能重复");

                #endregion 分摊医院

                #region 开户行

                //个人报销金额
                //node = doc.SelectSingleNode("UpdateData/field0047");
                node = doc.SelectSingleNode("UpdateData/field0023");
                if (node != null)
                    field0047 = node.InnerText.Trim();

                //if ((int.Parse(field0047) + int.Parse(field0023)) <= int.Parse(field0017))
                //{
                //    throw new Exception("报销金额不能大于月度申请总额");
                //}
                //if ((int.Parse() + int.Parse()) = int.Parse())
                //{
                //    throw new Exception("报销金额不能与总金额");
                //}
                //个人报销金额小于报销金额有第三方
                if (int.Parse(field0047) < int.Parse(field0023))
                {
                    int thirdpay = 0;
                    Dictionary<string, string> bankDic = new Dictionary<string, string>();

                    string bankRow = @"<row><column name=""收款方""><value>{0}</value></column>
                                    <column name=""开户行及账号""><value>{1}</value></column>
                                    <column name=""收款金额""><value>{2}</value></column>
                                    <column name=""付款事由""><value>{3}</value></column>
                                    </row>";
                    nodes = doc.SelectNodes("UpdateData/PDataRows/payee");
                    //收款方节点不能为空
                    if (nodes.Count == 0)
                        throw new Exception("收款方不能为空");
                    string field0048 = "", field0049 = "", field0050 = "", field0051 = "";
                    string[] paystr = null;

                    paystr = doc.SelectSingleNode("UpdateData/PDataRows/payee/field0048").InnerText.Trim().Split('|');
                    if (doc.SelectSingleNode("UpdateData/PDataRows/payee/field0048") == null)
                    {
                        throw new Exception("收款方不能为空");
                    }
                    for (int i = 0; i < paystr.Length; i++)
                    {
                        //过滤空字符
                        if (paystr[i].Trim().Length == 0)
                        {
                            continue;
                        }
                        field0048 = doc.SelectSingleNode("UpdateData/PDataRows/payee/field0048").InnerText.Trim();
                        field0049 = doc.SelectSingleNode("UpdateData/PDataRows/payee/field0049").InnerText.Trim();
                        field0050 = doc.SelectSingleNode("UpdateData/PDataRows/payee/field0050").InnerText.Trim() == "" ? "0" : doc.SelectSingleNode("UpdateData/PDataRows/payee/field0050").InnerText.Trim();
                        thirdpay += int.Parse(field0050);
                        field0051 = doc.SelectSingleNode("UpdateData/PDataRows/payee/field0051").InnerText.Trim();
                        bankRows = bankRows + string.Format(bankRow, field0048, field0049, field0050, field0051);
                    }
                    //总金额和个人报销金额 + 第三方付款不相等

                    if (int.Parse(field0023) < (int.Parse(field0047) + thirdpay))
                    {
                        throw new Exception("个人报销金额加第三方付款金额不能大于总金额");
                    }
                    //foreach (XmlNode row in nodes)
                    //{
                    //    bankRows = bankRows + string.Format(bankRow, row["field0048"].InnerText, row["field0049"].InnerText, row["field0050"].InnerText, row["field0051"].InnerText);
                    //}
                }
                else
                {
                    throw new Exception("个人报销金额不能大于总金额");
                }

                #endregion 开户行

                #region 获取附件

                Dictionary<string, string> attDic = new Dictionary<string, string>();

                XmlNode typenode = doc.SelectSingleNode("UpdateData/ADataRows/data/Type");
                XmlNode fileIDnode = doc.SelectSingleNode("UpdateData/ADataRows/data/FileID");
                string attid = "";
                string[] attTypesName = null;
                string[] attTypeIDs = null;
                //附件节点不能为空
                if (typenode != null && typenode.InnerText.Trim().Length > 0)
                {
                    if (typenode.InnerText.ToString().Trim().Length == 0)
                    {
                        throw new Exception("附件名称为空");
                    }
                    attTypesName = typenode.InnerText.ToString().Trim().Split('|');
                }
                else
                    throw new Exception("附件名称为空");
                if (fileIDnode != null && fileIDnode.InnerText.Trim().Length > 0)
                {
                    if (fileIDnode.InnerText.ToString().Trim().Length == 0)
                    {
                        throw new Exception("缺少附件ID");
                    }
                    attTypeIDs = fileIDnode.InnerText.ToString().Trim().Split('|');
                }
                else
                    throw new Exception("缺少附件ID");

                for (int i = 0; i < attTypesName.Length; i++)
                {
                    //过滤空字符 肯定有五个| 不一定有值
                    if (attTypesName[i].Trim().Length == 0)
                    {
                        continue;
                    }
                    attid = "";
                    if (attTypesName[i].Contains("学术材料"))
                    {
                        attid = "field0032";
                    }
                    else if (attTypesName[i].Contains("会议议程"))
                    {
                        attid = "field0033";
                    }
                    else if (attTypesName[i].Contains("会议纪要"))
                    {
                        attid = "field0034";
                    }
                    else if (attTypesName[i].Contains("课酬附件"))
                    {
                        attid = "field0035";
                    }
                    else if (attTypesName[i].Contains("会议照片"))
                    {
                        attid = "field0036";
                    }
                    else if (attTypesName[i].Contains("参会名单"))
                    {
                        attid = "field0037";
                    }
                    //switch (attTypesName[i])
                    //{
                    //    case "学术材料": attid = "field0032"; break;
                    //    case "会议议程": attid = "field0033"; break;
                    //    case "会议纪要": attid = "field0034"; break;
                    //    case "课酬附件": attid = "field0035"; break;
                    //    case "会议照片": attid = "field0036"; break;
                    //    case "参会名单": attid = "field0037"; break;
                    //}
                    if (!attDic.ContainsKey(attid))
                    {
                        ////FileID做唯一键,不会重复
                        //attDic.Add(attTypeIDs[i], attid + "-" + subid);

                        //FileID做唯一键,不会重复
                        attDic.Add(attid, attTypeIDs[i]);
                    }
                }

                #region 附件验证

                //获取付款方式
                node = doc.SelectSingleNode("UpdateData/field0062");
                if (node != null)
                    field0062 = node.InnerText.Trim();

                if (!attDic.ContainsKey("field0032"))
                {
                    throw new Exception("缺少学术材料附件");
                }

                if (!field0062.Contains("预付款"))
                {
                    if (!attDic.ContainsKey("field0033"))
                    {
                        throw new Exception("缺少会议议程附件");
                    }
                    if (!attDic.ContainsKey("field0034"))
                    {
                        throw new Exception("缺少会议纪要附件");
                    }
                    if (!attDic.ContainsKey("field0036"))
                    {
                        throw new Exception("缺少会议照片附件");
                    }
                    if (!attDic.ContainsKey("field0037"))
                    {
                        throw new Exception("缺少参会名单附件");
                    }
                }
                //更新付款阶段
                field0064 = field0062.Split('-')[1];
                //获取原始fileid数组
                field0032 = attDic.FirstOrDefault(x => x.Key == "field0032").Value ?? "";
                field0062 = attDic.FirstOrDefault(x => x.Key == "field0062").Value ?? "";
                field0034 = attDic.FirstOrDefault(x => x.Key == "field0034").Value ?? "";
                field0035 = attDic.FirstOrDefault(x => x.Key == "field0035").Value ?? "";
                field0036 = attDic.FirstOrDefault(x => x.Key == "field0036").Value ?? "";
                field0037 = attDic.FirstOrDefault(x => x.Key == "field0037").Value ?? "";

                #endregion 附件验证

                #endregion 获取附件

                #region formdata

                string formData = $@"<formExport version=""2.0""><summary id=""-4402170772378466531"" name=""formmain_5925""/><definitions/><values>
                                    <column name=""公司""><value>上海腾瑞制药有限公司</value></column>
                                    <column name=""申请人""><value>{EmployeeeID}</value></column>
                                    <column name=""申请日期""><value>{field0067}</value></column>
                                    <column name=""单号""><value>{field0006}</value></column>
                                    <column name=""成本中心""><value>{field0007}</value></column>
                                    <column name=""申请单""><value>{field0006}</value></column>
                                    <column name=""工号""><value>{field0010}</value></column>
                                    <column name=""所在部门""><value>{field0011}</value></column>
                                    <column name=""收款开户行""><value>{field0012}</value></column>
                                    <column name=""收款银行账号""><value>{field0013}</value></column>
                                    <column name=""表单类型""><value>学术活动费用支付单</value></column>
                                    <column name=""查看查验结果""><value>通过</value></column>
                                    <column name=""月度申请总额""><value>{field0017}</value></column>
                                    <column name=""举办日期""><value>{field0018}</value></column>
                                    <column name=""计划日期""><value>{field0019}</value></column>
                                    <column name=""实际规模""><value>{field0020}</value></column>
                                    <column name=""事由""><value>{field0021}</value></column>
                                    <column name=""申请金额""><value>{field0022}</value></column>
                                    <column name=""报销金额""><value>{field0023}</value></column>
                                    <column name=""申请单单号""><value>{field0024}</value></column>
                                    <column name=""学术材料""><value>{field0032}</value></column>
                                    <column name=""会议议程""><value>{field0033}</value></column>
                                    <column name=""会议纪要""><value>{field0034}</value></column>
                                    <column name=""课酬附件""><value>{field0035}</value></column>
                                    <column name=""会议照片""><value>{field0036}</value></column>
                                    <column name=""参会名单""><value>{field0037}</value></column>
                                    <column name=""规模""><value>{field0043}</value></column>
                                    <column name=""是否按计划""><value>{field0044}</value></column>
                                    <column name=""费用类型""><value>学术活动费</value></column>
                                    <column name=""个人报销金额""><value>{field0047}</value></column>
                                    <column name=""收款方式""><value>0</value></column>
                                    <column name=""费用来源""><value>{field0063}</value></column>
                                    <column name=""付款阶段""><value>{field0064}</value></column>
                                    <column name=""可用额""><value>{field0053}</value></column>
                                    <column name=""在途额""><value>{field0054}</value></column>
                                    <column name=""活动类型""><value>{field0065}</value></column>
                                    <column name=""是否有发票""><value>0</value></column>
                                    <column name=""成本中心_归档""><value>{field0067}</value></column>
                                    <column name=""YRB发起""><value>1</value></column>
                                    <column name=""InvoiceMain_ID""><value>{formID}</value></column>
                                    </values>
                                    <subForms>
                                    <subForm><definitions>
                                    <column id=""field0048"" type=""0"" name=""收款方"" length=""255""/>
                                    <column id=""field0049"" type=""0"" name=""开户行及账号"" length=""255""/>
                                    <column id=""field0050"" type=""4"" name=""收款金额"" length=""20""/>
                                    <column id=""field0051"" type=""0"" name=""付款事由"" length=""255""/>
                                    </definitions><values>{bankRows}</values>
                                    </subForm><subForm><definitions>
                                    <column id=""field0056"" type=""0"" name=""编码_分摊_0"" length=""255""/>
                                    <column id=""field0057"" type=""0"" name=""名称_分摊"" length=""255""/>
                                    <column id=""field0058"" type=""4"" name=""分摊金额"" length=""20""/>
                                    <column id=""field0059"" type=""0"" name=""分摊说明"" length=""255""/>
                                    <column id=""field0060"" type=""0"" name=""编码_分摊_1"" length=""255""/>
                                    <column id=""field0061"" type=""0"" name=""科室_分摊"" length=""255""/>
                                    <column id=""field0062"" type=""0"" name=""编码_分摊"" length=""255""/>
                                    </definitions><values>{hospitalRows}</values></subForm></subForms></formExport>";

                #endregion formdata

                string oaid = Guid.NewGuid().ToString();
                //插入流程数据
                sql = $@"Insert Into DataService.dbo.OATask([FTemplateCode],[FSenderLoginName],[FEmployeeCode],[FEmployeeName],[FSubject],[FData]) Values('O202004','{loginName}','{field0010}','{EmployeeeName}','{"学术活动费用支付单-YRB-" + EmployeeeName + "-" + DateTime.Now.ToString()}' , '{formData}')";
                runner.ExecuteSqlNone(sql);
                result = "<UpdateData><Result>True</Result><Description></Description></UpdateData>";
            }
            catch (Exception err)
            {
                result = "<UpdateData><Result>False</Result><Description>" + err.Message + "</Description></UpdateData>";
            }
            return result;
        }

        #endregion 提交学术活动费用支付单3.0

        #region 上传文件

        public string UploadFile(string xmlString)
        {
            string callType = "UploadFile";
            string result = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                          "<" + callType + ">" +
                          "<Result>False</Result>" +
                          "<Description></Description></" + callType + ">";
            try
            {
                string base64String = "", fileName = "", EmployeeID = "", fileSize = "", mimeType = "";

                string path = System.Configuration.ConfigurationManager.AppSettings["Path"];
                path = path + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlNode vNode = doc.SelectSingleNode(callType + "/Base64String");
                if (vNode == null || vNode.InnerText.Trim().Length == 0)
                    throw new Exception("Base64String不能为空");
                else
                    base64String = vNode.InnerText.Trim();

                vNode = doc.SelectSingleNode(callType + "/FileName");
                if (vNode == null || vNode.InnerText.Trim().Length == 0)
                    throw new Exception("FileName不能为空");
                else
                    fileName = vNode.InnerText.Trim();

                vNode = doc.SelectSingleNode(callType + "/FileSize");
                if (vNode == null || vNode.InnerText.Trim().Length == 0)
                    throw new Exception("FileSize不能为空");
                else
                    fileSize = vNode.InnerText.Trim();

                vNode = doc.SelectSingleNode(callType + "/MimeType");

                if (vNode == null || vNode.InnerText.Trim().Length == 0)
                    throw new Exception("MimeType不能为空");
                else
                    mimeType = vNode.InnerText.Trim();

                vNode = doc.SelectSingleNode(callType + "/EmployeeID");
                if (vNode == null || vNode.InnerText.Trim().Length == 0)
                    throw new Exception("EmployeeID不能为空");
                else
                    EmployeeID = vNode.InnerText.Trim();
                //检查文件类型
                FileHelper.CheckFileType(fileName);

                byte[] gb = Guid.NewGuid().ToByteArray();
                long fileID = BitConverter.ToInt64(gb, 0);
                //long fileID,string name,string fileSize,string mimeType,string EmployeeID
                string sql = $"insert into [v3x].[dbo].[CTP_FILE](ID,FILENAME,MIME_TYPE,CREATE_DATE,CREATE_MEMBER,FILE_SIZE) values('{fileID}','{fileName}','{mimeType}','{DateTime.Now.ToString()}','{EmployeeID}','{fileSize}')";

                if (FileHelper.UploadFile(base64String, path, fileID, sql))
                {
                    result = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                          "<" + callType + ">" +
                          "<Result>True</Result>" +
                          "<FileID>" + fileID + "</FileID>" +
                          "<Description></Description></" + callType + ">";
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return result;
        }

        #endregion 上传文件

        #region 获取药瑞宝敏感信息

        public string GetAppSystemInfo(string xmlMessage)
        {
            string xmlkey = "";
            string result;
            try
            {
                runner = new SQLServerHelper();
                string sql = "select FKey,FValue from [DataService].[dbo].[SysInfos] where FSystem='yrb'";

                DataTable dataTable = runner.ExecuteSql(sql);

                foreach (DataRow row in dataTable.Rows)
                {
                    xmlkey += $"<{row["FKey"]}>{row["FValue"]}</{row["FKey"]}>";
                }
                FileLogger.WriteLog("SaveSql|End:" + xmlkey, 1, "OAWebService", "GetAppSystemInfo", "DataService");
                result = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
          "<GetAppSystemInfo>" +
          "<Result>True</Result>" +
          xmlkey +
          "<Description></Description></GetAppSystemInfo>";
            }
            catch (Exception err)
            {
                result = "<?xml version=\"1.0\" encoding=\"utf-8\"?><GetAppSystemInfo><Result>False</Result><Description>提示" + err.Message + "</Description></GetAppSystemInfo>";
            }
            return result;
        }

        #endregion 获取药瑞宝敏感信息

        #region 获取支付销量流程数据

        #region 罗盘主页数据

        public string GetPersonSummaryReport(string dataString, string FormatResult, string callType)
        {
            FormatResult = "{{\"{0}\":{{\"Result\":{1},\"Description\":{2},\"DataRows\":{3} }} }}";
            string result = string.Format(FormatResult, callType, "\"False\"", "", "");
            //加,连接起前面的json字符串
            string rdataRow, datarows, yearweek = "", panelRow = "", weekindex = ",\"FWeekIndex\":";
            List<string> dataRowList = new List<string>();
            //初始化状态
            //   result = string.Format(FormatResult, callType, "\"False\"", "", "");
            //每个DataRow格式
            string rowcontent = "{{\"dataSets\":[{{\"values\":[{{ \"value\": {0}, \"label\": \"\"}},{{ \"value\": {1}, \"label\":\"\"}}],\"label\":\"\",\"config\": {2}}}],\"name\": \"{3}\",\"Index\":\"{4}\",\"value\":\"{5}\",\"Count\":\"{6}\",\"startTime\":\"{7}\",\"endTime\":\"{8}\"}}";
            //dataString = "{\"FWeekIndex\":\"10\",\"AuthCode\":\"1d340262-52e0-413f-b0e7-fc6efadc2ee5\",\"EmployeeID\":\"4255873149499886263\",\"BeginDate\":\"2020-08-05\",\"EndDate\":\"2020-08-31\"}";
            try
            {
                //查询实体
                RouteEntity routeEntity = JsonConvert.DeserializeObject<RouteEntity>(dataString);
                weekindex += routeEntity.FWeekIndex;
                weekindex += (",\"Year\":" + DateTime.Now.Year + "");
                DateTime startTime, endTime;
                //获取第一天和最后一天
                Tuple<DateTime, DateTime> pertime = Common.GetPerTime(routeEntity.FWeekIndex);
                //开始时间
                startTime = pertime.Item1;
                //结束时间
                endTime = pertime.Item2;
                //5-8使用
                if (routeEntity.FWeekIndex != "-1000")
                {
                    yearweek = Common.GetYearWithWeeks(routeEntity.FWeekIndex);
                }
                else
                {
                    yearweek = routeEntity.FWeekIndex;
                }
                //自定义》1,签到，2,拜访，3,流程，4,支付,5,艾夫吉夫 6,销量 7,****，8，奖金
                //目前有些数据没有，暂时跳过
                for (int i = 1; i < 9; i++)
                {
                    //主要区分返回格式 和时间查询问题
                    if (i == 3 || i == 4)
                    {
                        rdataRow = GetDataRow(i, rowcontent, routeEntity.EmployeeId, startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd"), "");
                        dataRowList.Add(rdataRow);
                    }
                    else if (i == 6)
                    {
                        panelRow += GetDataRow(i, rowcontent, routeEntity.EmployeeId, startTime.ToString("yyyy-MM-dd"), endTime.ToString("yyyy-MM-dd"), "");
                    }
                    else
                    {
                        continue;
                    }
                }
                //加，拼接下面的json
                datarows = string.Join(",", dataRowList.ToArray()) + ",";
                //最后结果
                result = string.Format(FormatResult, callType, "\"True\"", "\"\"", "{\"DataRow\":[" + datarows + "{" + panelRow + weekindex + "}" + "]}");
            }
            catch (Exception err)
            {
                result = string.Format(FormatResult, callType, "\"False\"", err.Message, "");
            }
            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="EmployeeId">团队的EmployeeId是'0001','0002'格式</param>
        /// <param name="rowContent"></param>
        /// <param name="type">1,签到，2,拜访，3,流程，4,待定,5,艾夫吉夫 6,丙戊酸钠 7,待支付金额，8，奖金</param>
        /// <param name="YearOrWeek">按年查或者按周查，按年查比较特殊，sql语句会有变化</param>
        /// <returns></returns>
        public string GetDataRow(int viewType, string rowContent, string EmployeeId, string startTime, string endTime, string yearweek)
        {
            string sql = "", viewName = "", tempresult = "", routeconfig, p1, p2, partsql;
            int total, okcount, per;
            try
            {
                //产品多的时候 获取前两个产品 select t1.FProductID,t2.FProductName from ( select top 2 FProductID, COUNT(FProductID) as ProductCount from [yaodaibao].[dbo].[HospitalStock] group by FProductID order by ProductCount desc)  t1 left join (Select FID AS  FProductID, FName As FProductName From t_Items Where FClassID = '36d33d13-4f8b-4ee4-84c5-49ff7c8691c4' and FIsDeleted=0 )  t2 on t1.FProductID = t2.FProductID

                //SQLServerHelper runner = new SQLServerHelper();
                //sql = "select top 2 FProductID as  ProductID from [yaodaibao].[dbo].[HospitalStock]";
                //DataTable dtproduct = runner.ExecuteSql(sql);
                //p1 = dtproduct.Rows[0]["ProductID"] ==DBNull.Value? "" :dtproduct.Rows[0]["ProductID"].ToString();
                //p2 = dtproduct.Rows[1]["ProductID"] == DBNull.Value ? "" : dtproduct.Rows[1]["ProductID"].ToString();

                switch (viewType)
                {
                    //3,流程
                    case 3:
                        viewName = "流程";
                        sql = $"SELECT COUNT(*) Total, sum(case t1.STATE when '3' then 1 Else 0 End) As OKCount FROM COL_SUMMARY t1 LEFT JOIN ORG_MEMBER t2 ON t1.START_MEMBER_ID = t2.ID LEFT JOIN ORG_MEMBER t3 ON t1.current_nodes_info = CAST(t3.ID AS nvarchar(20)) WHERE t1.State IN( 0, 3 ) AND START_DATE >= '20200801' AND '{ startTime }' <= t1.[START_DATE] and t1.[START_DATE] <= '{ endTime }' and t1.START_MEMBER_ID in ({EmployeeId})";
                        //sql = $"SELECT t1.ID, t1.[SUBJECT], t1.START_MEMBER_ID, t1.STATE ISTATE, t1.[START_DATE], DATEDIFF(HOUR, t1.[START_DATE], Isnull(t1.Finish_Date, getdate())) AS Hours, t1.Finish_Date, (CASE t1.STATE WHEN 3 THEN '完成' WHEN 0 THEN '流转中' END ) AS STATE, t2.[name] AS START_MEMBER_Name, Isnull( t3.[Name], '' ) AS Current_Member_Name, t1.current_nodes_info Current_Member_ID FROM COL_SUMMARY t1 LEFT JOIN ORG_MEMBER t2 ON t1.START_MEMBER_ID = t2.ID  LEFT JOIN ORG_MEMBER t3 ON t1.current_nodes_info = CAST(t3.ID AS nvarchar(20)) WHERE  t1.State IN( 0, 3 ) AND START_DATE >= '20200801' AND '{ endTime }' <= t1.[START_DATE]  and t1.[START_DATE] <= '{ endTime }' and t1.START_MEMBER_ID in ({EmployeeId}) AND t1.STATE in (0)";
                        //sql = $"Select  count(*) As Total , sum(case FState when '完成' then 1 Else 0 End) As OKCount From [DataService].[dbo].[OAProcessStatus] where '{startTime}' <= [FStart_Date]  and  [FStart_Date] <= '{ endTime }' and FStart_Member_ID in ({EmployeeId})";
                        break;

                    //4,支付
                    case 4:
                        viewName = "支付";
                        sql = $"SELECT SUM(isnull(field0008, 0 )) Total,SUM(field0008 - field0034) OKCount FROM [v3x].[dbo].[formmain_3460] t1 LEFT JOIN v3x.dbo.ORG_MEMBER t2 ON t1.field0006 = t2.ID where'{startTime}' <= t1.start_date and t1.start_date <= '{endTime}' and t1.field0006 in ('{EmployeeId}')";
                        //sql = $"SELECT  isnull( [field0002], '' ) field0002, isnull( [field0004], '' ) field0004, isnull( [field0005], '' ) field0005, isnull(field0006, '') field0006, t2.NAME AS FApplyName, isnull( [field0007], '' ) field0007, isnull( [field0008], 0 ) field0008, isnull( [field0009], 0 ) field0009, isnull( [field0013], '' ) field0013, isnull( [field0014], '' ) field0014, isnull( [field0031], '' ) field0031, isnull( [field0032], '' ) field0032, isnull( [field0033], '' ) field0033, isnull( [field0034], 0 ) field0034, isnull( [field0035], '' ) field0035, t1.ID AS ID, isnull(t1.start_date, '') AS Start_Date FROM [v3x].[dbo].[formmain_3460] t1 LEFT JOIN v3x.dbo.ORG_MEMBER t2 ON t1.field0006 = t2.ID  where  '{startTime}' <= t1.start_date  and t1.start_date <= '{startTime}' and t1.field0006 in ('{EmployeeId}')";
                        //sql = $"select  sum(field0008) Total,sum(field0008-field0034) OKCount   FROM [DataService].[dbo].[formmain_3460]  where '{startTime}' <= [FStart_Date]  and  [FStart_Date] <= '{ endTime }' and field0006 in ('{EmployeeId}')";
                        break;
                    // 6,销量
                    case 6:
                        viewName = "数量";
                        sql = $"SELECT SUM(isnull(field0008, 0)) as Total, SUM(field0008) OKCount FROM [v3x].[dbo].[formmain_6786] where field0011 in ('人员') and'{ startTime }' <= start_date and start_date <= '{ endTime }' and field0014 in ('{EmployeeId}')";
                        //sql = $"select SUM(field0008) Total,SUM(field0008) OKCount  from [DataService].[dbo].[formmain_6786]  where field0011 in ('人员') and  '{startTime}' <= [FStart_Date]  and  [FStart_Date] <= '{ endTime }' and field0014 in ('{EmployeeId}')";
                        break;
                    // 7,待支付金额
                    case 7:
                        viewName = "待支付金额";
                        sql = "";
                        break;
                    //8，奖金
                    case 8:
                        viewName = "奖金";
                        sql = "";
                        break;
                }
                SQLServerHelper runner = new SQLServerHelper();
                DataTable dt = runner.ExecuteSql(sql);
                //百分比
                total = (int)double.Parse((dt.Rows[0]["Total"] == DBNull.Value) ? "0" : dt.Rows[0]["Total"].ToString());
                okcount = (int)double.Parse(dt.Rows[0]["OKCount"] == DBNull.Value ? "0" : dt.Rows[0]["OKCount"].ToString());
                if (viewType < 5)
                {
                    if (total == 0)
                    {
                        per = 0;
                    }
                    else
                    {
                        per = okcount * 100 / total;
                    }
                    // 药瑞宝已经获取 获取配置文件
                    routeconfig = Common.GetCompassConfigFromXml("Route").Replace("Quot", "\"");
                    //DataRow数据
                    tempresult = string.Format(rowContent, per, (100 - per), routeconfig, viewName, viewType, per + "%", total.ToString(), startTime, endTime);
                }
                else
                {
                    if (viewType == 6)
                    {
                        tempresult = $"\"SalesName\":\"数量\",\"SalesCount\":{okcount}";
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return tempresult;
        }

        #endregion 罗盘主页数据

        #region 罗盘子页数据

        //自定义》1,签到，2,拜访，3,流程，4,待定,5,艾夫吉夫 6,丙戊酸钠 7,待支付金额，8，奖金
        public string GetComPassChildData(string dataString, string FormatResult, string callType, string childtype)
        {
            int childType = int.Parse(childtype);
            string sql = "", result = "", yearweek, weekindex = ",\"FWeekIndex\":";
            //dataString = "{\"FWeekIndex\":\"-11\",\"AuthCode\":\"1d340262-52e0-413f-b0e7-fc6efadc2ee5\",\"EmployeeID\":\"4255873149499886263\",\"BeginDate\":\"2020-08-05\",\"EndDate\":\"2020-08-31\"}";
            string rowcontent, dataRow = "";
            List<string> rowList = new List<string>();
            try
            {
                //查询实体
                RouteEntity routeEntity = JsonConvert.DeserializeObject<RouteEntity>(dataString);
                weekindex += routeEntity.FWeekIndex;
                DateTime startTime, endTime;
                Tuple<DateTime, DateTime> pertime = Common.GetPerTime(routeEntity.FWeekIndex);
                //开始时间
                startTime = pertime.Item1;
                //结束时间
                endTime = pertime.Item2;
                //5-8使用
                yearweek = Common.GetYearWithWeeks(routeEntity.FWeekIndex);
                SQLServerHelper runner = new SQLServerHelper();
                DataTable dt = new DataTable();
                switch (childType)
                {
                    case 1: break;
                    case 2: break;
                    //流程
                    case 3:
                        sql = $"SELECT t1.ID, t1.[SUBJECT], t1.START_MEMBER_ID, t1.STATE ISTATE, t1.[START_DATE], DATEDIFF(HOUR, t1.[START_DATE], Isnull(t1.Finish_Date, getdate())) AS Hours, t1.Finish_Date, (CASE t1.STATE WHEN 3 THEN '完成' WHEN 0 THEN '流转中' END ) AS STATE, t2.[name] AS START_MEMBER_Name, Isnull( t3.[Name], '' ) AS Current_Member_Name, t1.current_nodes_info Current_Member_ID FROM COL_SUMMARY t1 LEFT JOIN ORG_MEMBER t2 ON t1.START_MEMBER_ID = t2.ID  LEFT JOIN ORG_MEMBER t3 ON t1.current_nodes_info = CAST(t3.ID AS nvarchar(20)) WHERE  t1.State IN( 0, 3 ) AND START_DATE >= '20200801' AND '{ startTime }' <= t1.[START_DATE]  and t1.[START_DATE] <= '{ endTime }' and t1.START_MEMBER_ID in ({routeEntity.EmployeeIds}) AND t1.STATE not in (3)";
                        //sql = $"SELECT [FStart_Member_Name] as FStart_Member_Name, [FSubject] as FSubject ,[FStart_Date] as StartDate,[FCurrent_Member_Name] as CurrentMemberName FROM [DataService].[dbo].[OAProcessStatus]  where   '{startTime}' <= [FStart_Date]  and [FStart_Date] <= '{endTime}' and FState in ('流转中') and FStart_Member_ID in ({routeEntity.EmployeeIds}) order by FStart_Date desc";
                        dt = runner.ExecuteSql(sql);
                        foreach (DataRow item in dt.Rows)
                        {
                            rowcontent = "{\"Time\":\"" + DateTime.Parse(item["StartDate"].ToString()).ToString("yyyy年MM月yy日") + "\",\"Subject\":\"" + item["FSubject"] + "\",\"Code\":\"" + item["FSubject"] + "\",\"CurrentName\":\"" + item["CurrentMemberName"] + "\",\"Name\":\"" + item["FStart_Member_Name"] + "\",\"startTime\":\"" + startTime.ToString("yyyyMMdd") + "\",\"endTime\":\"" + endTime.ToString("yyyyMMdd") + "\",\"FWeekIndex\":\"" + routeEntity.FWeekIndex + "\"}";
                            rowList.Add(rowcontent);
                        }
                        break;
                    //支付
                    case 4:
                        //Ffield0006 可为空
                        sql = $"select field0005 as PayType,field0007 as PayCode ,field0008 as Amount ,FApplyName as ApplyName,(field0008-field0034) as  Paid,field0034 as Balance from [DataService].[dbo].[formmain_3460]  where   '{startTime}' <= [FStart_Date]  and [FStart_Date] <= '{endTime}' and field0006 in ('{routeEntity.EmployeeIds}') order by FStart_Date desc";
                        dt = runner.ExecuteSql(sql);
                        foreach (DataRow item in dt.Rows)
                        {
                            rowcontent = "{\"Year\":\"" + DateTime.Now.ToString("yyyy") + "\",\"PayType\":\"" + item["PayType"] + "\",\"PayCode\":\"" + item["PayCode"] + "\",\"ApplyName\":\"" + item["ApplyName"] + "\",\"Paid\":\"" + item["Paid"] + "\",\"Amount\":\"" + item["Amount"] + "\",\"Balance\":\"" + item["Balance"] + "\",\"startTime\":\"" + startTime.ToString("yyyyMMdd") + "\",\"endTime\":\"" + endTime.ToString("yyyyMMdd") + "\"}";
                            rowList.Add(rowcontent);
                        }
                        break;

                    case 5: break;
                    //销量
                    case 6:
                        //Ffield0014 可为空
                        sql = $"select   [field0002] as Hospital,[field0008] as  Total FROM [DataService].[dbo].[formmain_6786]  where field0011 in ('招商') and  '{startTime}' <= [FStart_Date]  and [FStart_Date] <= '{endTime}' and field0014 in ('{routeEntity.EmployeeIds}') order by FStart_Date desc";
                        dt = runner.ExecuteSql(sql);
                        string nextpage = "false";
                        //没有招商
                        if (dt.Rows.Count < 0)
                        {
                            sql = $"select   [field0002] as Hospital,[field0008] as  Total FROM [DataService].[dbo].[formmain_6786]  where  '{startTime}' <= [FStart_Date]  and [FStart_Date] <= '{endTime}' and field0014 in ('{routeEntity.EmployeeIds}') order by FStart_Date desc";
                            dt = runner.ExecuteSql(sql);
                        }
                        //有招商
                        else
                        {
                            nextpage = "true";
                        }
                        foreach (DataRow item in dt.Rows)
                        {
                            rowcontent = "{\"nextpage\":\"" + nextpage + "\",\"Year\":\"" + DateTime.Now.ToString("yyyy") + "\",\"Hospital\":\"" + item["Hospital"] + "\",\"Total\":\"" + item["Total"] + "\",\"startTime\":\"" + startTime.ToString("yyyyMMdd") + "\",\"endTime\":\"" + endTime.ToString("yyyyMMdd") + "\",\"FWeekIndex\":\"" + routeEntity.FWeekIndex + "\"}";
                            rowList.Add(rowcontent);
                        }
                        break;

                    case 7: break;
                    case 8: break;
                    default:
                        break;
                }
                dataRow = string.Join(",", rowList.ToArray());
                //最后结果
                result = string.Format(FormatResult, callType, "\"True\"", "\"\"", "{\"DataRow\":[" + dataRow + "]}");
            }
            catch (Exception err)
            {
                result = string.Format(FormatResult, callType, "\"False\"", err.Message, "");
            }

            return result;
        }

        #endregion 罗盘子页数据

        #region 支付查询

        public string PayQuery(string dataString, string FormatResult, string callType)
        {
            string rowcontent, dataRow = "", sql = "", result = "", partsql = "";
            List<string> rowList = new List<string>();
            SQLServerHelper runner = new SQLServerHelper();
            DataTable dt = new DataTable();
            try
            {
                RouteEntity routeEntity = JsonConvert.DeserializeObject<RouteEntity>(dataString);
                //未支付 只看应付余额是否为0
                if (routeEntity.FilterType == "-1")
                {
                    partsql = "  and Ffield0034 > 0  ";
                }
                //已支付
                else if (routeEntity.FilterType == "1")
                {
                    partsql = "  and Ffield0034 = 0  ";
                }
                //另外情况全查
                //else
                //Ffield0006 可为空 Ffield0009//已付
                sql = $"select Ffield0005 as PayType,Ffield0007 as PayCode ,Ffield0008 as Amount ,FApplyName as ApplyName,(Ffield0008-Ffield0034)  as  Paid,Ffield0034 as Balance from [v3x].[dbo].[formmain_3460]  where   '{routeEntity.StartTime}' <= [FStart_Date]  and [FStart_Date] <= '{routeEntity.EndTime}' and Ffield0006 in ('{routeEntity.EmployeeIds}')  {partsql} order by FStart_Date desc";
                dt = runner.ExecuteSql(sql);
                foreach (DataRow item in dt.Rows)
                {
                    rowcontent = "{\"Year\":\"" + DateTime.Now.ToString("yyyy") + "\",\"PayType\":\"" + item["PayType"] + "\",\"PayCode\":\"" + item["PayCode"] + "\",\"ApplyName\":\"" + item["ApplyName"] + "\",\"Paid\":\"" + item["Paid"] + "\",\"Amount\":\"" + item["Amount"] + "\",\"Balance\":\"" + item["Balance"] + "\",\"startTime\":\"" + routeEntity.StartTime + "\",\"endTime\":\"" + routeEntity.EndTime + "\"}";
                    rowList.Add(rowcontent);
                }
                dataRow = string.Join(",", rowList.ToArray());
                //最后结果
                result = string.Format(FormatResult, callType, "\"True\"", "\"\"", "{\"DataRow\":[" + dataRow + "]}");
            }
            catch (Exception err)
            {
                result = string.Format(FormatResult, callType, "\"False\"", err.Message, "");
            }

            return result;
        }

        #endregion 支付查询

        #endregion 获取支付销量流程数据
    }

    #region 罗盘实体类

    //json转实体不区分大小写
    public class RouteEntity
    {
        public string AuthCode { get; set; }
        public string EndTime { get; set; }
        public string StartTime { get; set; }

        //主页用到
        public string EmployeeId { get; set; }

        //子页用到

        public string EmployeeIds { get; set; }
        public string FWeekIndex { get; set; }

        //罗盘类型ID 1,签到，2,拜访，3,流程，4,支付,5,艾夫吉夫 6,数量 7,，8，奖金
        public int ChildType { get; set; }

        //有没有下一页
        public string NextPage { get; set; }

        //查询需要
        public string FilterType { get; set; }
    }

    #endregion 罗盘实体类
}