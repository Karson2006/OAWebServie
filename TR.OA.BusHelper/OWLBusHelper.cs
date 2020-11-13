using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using iTR.Lib;
using Newtonsoft.Json;

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
            string sql = "", result = "",EmployeeeID="";
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
            result = iTR.Lib.Common.DataTableToXml(dt, "GetList","","List");
            return result;
        }


        #endregion

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


        #endregion

        #region SubmitEntertainmentExpendForm
        public string SubmitEntertainmentExpendForm(string xmlString)
        {
            string sql = "", EmployeeeID = "", rowDatas = "",loginName= "", EmployeeeName = "";
            string result = @"<UpdateData><Result>False</Result><Description></Description></UpdateData>";

            #region 参数定义
            string formID= ""; //申请日期
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
            #endregion

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
            #endregion

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
                    field0019 = node.InnerText.Trim().Substring(0,10);
                //招待日期
                if (field0031 == "1")
                    field0018 = field0019;
                else
                {
                    node = doc.SelectSingleNode("UpdateData/field0018");
                    if (node != null &&  node.InnerText.Trim().Length >0)
                        field0018 = node.InnerText.Trim().Substring(0,10);
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
                if (node != null && node.InnerText.Trim().Length>0)
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
                    else
                       field0039 = row["field0044"].InnerText.Trim();
                    if (!hospitalList.ContainsKey(field0039))
                        hospitalList.Add(field0039, row["field0040"].InnerText);

                    rowDatas = rowDatas + string.Format(rowData, field0039, row["field0040"].InnerText, row["field0041"].InnerText,
                        row["field0042"].InnerText, row["field0043"].InnerText, row["field0044"].InnerText);

                }
                if (hospitalList.ContainsKey("OA_19888"))
                    throw new Exception("费用不能分摊到【跨区跨院活动】，请选择具体医院");
                
                if(nodes.Count > hospitalList.Count  )
                    throw new Exception("分摊医院不能重复");

                formData = string.Format(formData, EmployeeeID, field0003, field0024, field0006, field0007,field0010, field0011, field0012, field0013, field0017, field0018, field0019,
                    field0020, field0021, field0022, field0023, field0025, field0031, field0035,  -1, field0045, rowDatas, formID);

                //doc.LoadXml(formData)

                sql = @"Insert Into DataService.dbo.OATask([FTemplateCode],[FSenderLoginName],[FEmployeeCode],[FEmployeeName],[FSubject],[FData])
	                     Values('O202001','{0}','{1}','{2}','{3}' , '{4}')";
                sql = string.Format(sql, loginName, field0010, EmployeeeName,"招待费报销-YRB-"+ EmployeeeName+ "-"+ DateTime.Now.ToString(), formData);
                runner.ExecuteSqlNone(sql);
               
                result = "<UpdateData><Result>True</Result><Description></Description></UpdateData>";
            }
            catch (Exception err)
            {
                result = "<UpdateData><Result>False</Result><Description>"+ err.Message+"</Description></UpdateData>";
            }
            return result;
        }


        #endregion

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
                string field0044 = "";  //是否按计划 
                string field0047 = "";//个人报销金额==支付金额
                string field0053 = "";  //可用额
                string field0054 = "";  //在途额
                string field0055 = "";  //财务审批标志
                string field0063 = "";  //费用来源
                string field0067 = "";  //成本中心_归档
                string bankRows = "", hospitalRows = "", attRows = "";
                #endregion

 
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
                    node = doc.SelectSingleNode("UpdateData/field0044");
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
                            throw new Exception("不按计划进行时，学术费日期不能为空");
                    }

                    //实际人数
                    node = doc.SelectSingleNode("UpdateData/field0020");
                    if (node != null)
                        field0020 = node.InnerText.Trim();

                    //事由
                    node = doc.SelectSingleNode("UpdateData/field0021");
                    if (node != null)
                        field0021 = node.InnerText.Trim();
                    //InvoiceMain_ID
                    node = doc.SelectSingleNode("UpdateData/formID");
                    if (node != null && node.InnerText.Trim().Length > 0)
                        formID = node.InnerText.Trim();
                    else
                        throw new Exception("formID不能为空");

                    //申请金额
                    node = doc.SelectSingleNode("UpdateData/field0022");
                    if (node != null && node.InnerText.Trim().Length > 0)
                        field0022 = node.InnerText.Trim();
                    else
                        throw new Exception("申请金额不能为空");

                    //医院
                    node = doc.SelectSingleNode("UpdateData/field0025");
                    if (node != null)
                        field0025 = node.InnerText.Trim();

                    #endregion

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
                    #endregion

                    #region 金额判断


                    //读取学术费明细表可用额
                    sql = @"Select t1.field0006 As field0045,t2.SHOWVALUE AS field0045_Name,Isnull(t1.field0009,0) As field0036,Isnull(t3.field0011,0) As field0017,
                            Isnull(t1.field0007,0) As field0056,t1.field0018 AS field0025,Isnull(t1.field0008,0) AS field0037
                            from v3x.dbo.formmain_6185 t1
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
                        //学术费用明细账已用额
                        // field0067 = dt.Rows[0]["field0056"].ToString();
                        //申请单单号
                        field0024 = dt.Rows[0]["field0024"].ToString();
                        //在途额
                        field0054 = dt.Rows[0]["field0054"].ToString();
                    }
 

                //报销金额
                node = doc.SelectSingleNode("UpdateData/field0023");
                    if (node != null)
                        field0024 = node.InnerText.Trim();
                    else
                        throw new Exception("报销金额不能为空");

                    if (field0024.Trim().Length == 0)
                        throw new Exception("报销金额不能为空");

                    if (decimal.Parse(field0023) < decimal.Parse(field0023))
                        throw new Exception("报销金额不能大于申请金额");

                    if (decimal.Parse(field0017) < decimal.Parse(field0023))
                        throw new Exception("报销金额不能大于月度可用额");
                    #endregion

                    //赋值申请日期
                    field0003 = DateTime.Now.ToString("yyyy-MM-dd");

                    #region 分摊医院

                    string hosRow = @"<row><column name=""编码_分摊_0""><value>{0}</value></column>
                                    <column name=""名称_分摊""><value>{1}</value></column>
                                    <column name=""分摊金额""><value>{2}</value></column>
                                    <column name=""分摊说明""><value>{3}</value></column>
                                    <column name=""编码_分摊_1""><value>{4}</value></column>
                                    <column name=""科室_分摊""><value>{5}</value></column>
                                    <column name=""编码_分摊""><value>{6}</value></column>
                                    </row>";
                    XmlNodeList nodes = doc.SelectNodes("UpdateData/HDataRows/hospit");
                    //分摊医院不能为空
                    if (nodes.Count == 0)
                        throw new Exception("分摊医院不能为空");
                    string field0062 = "";
                    Dictionary<string, string> verifyList = new Dictionary<string, string>();
                    foreach (XmlNode row in nodes)
                    {
                        if (row["field0056"].InnerText.Trim().Length > 0)
                            field0062 = row["field0056"].InnerText.Trim();
                        else
                            field0062 = row["field0060"].InnerText.Trim();
                        if (!verifyList.ContainsKey(field0062))
                            //field0057 名称_分摊	 
                            verifyList.Add(field0062, row["field0057"].InnerText);
                        //编码分摊放到最后一个
                        hosRow = hosRow + string.Format(hosRow, row["field0056"].InnerText, row["field0057"].InnerText,
                        row["field0058"].InnerText, row["field0059"].InnerText, row["field0060"].InnerText, row["field0061"].InnerText, field0062);
                    }

                    if (verifyList.ContainsKey("OA_19888"))
                        throw new Exception("费用不能分摊到【跨区跨院活动】，请选择具体医院");

                    if (nodes.Count > verifyList.Count)
                        throw new Exception("分摊医院不能重复");


                    #endregion

                    #region  开户行
                    verifyList.Clear();
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
                    foreach (XmlNode row in nodes)
                    {
                        bankRows = bankRows + string.Format(bankRow, row["field0048"].InnerText, row["field0049"].InnerText, row["field0050"].InnerText, row["field0051"].InnerText);
                        if (!verifyList.ContainsKey(field0062))
                        //field0057 名称_分摊	 
                        bankDic.Add(field0062, row["field0049"].InnerText);
                    }
                    if (nodes.Count > bankDic.Count)
                        throw new Exception("开户行及账号不能重复");
                    #endregion

                    #region 获取附件
 
 
                  Dictionary<string, string> attDic = new Dictionary<string, string>();

                    nodes = doc.SelectNodes("UpdateData/ADataRows/data");
                    
                    //附件节点不能为空
                    if (nodes.Count == 0)
                        throw new Exception("附件不能为空");
                    string attid = "",attjson="";
                    foreach (XmlNode row in nodes)
                    {
                        attid = "";
                        switch (row["Type"].ToString().Trim())
                        {
                            case "学术材料": attid = "field0032" ; break;
                            case "会议议程": attid = "field0033" ; break;
                            case "会议纪要": attid = "field0034" ; break;
                            case "课酬附件": attid = "field0035" ; break;
                            case "会议照片": attid = "field0036" ; break;
                            case "参会名单": attid = "field0037" ; break;
                        }
                        if (!attDic.ContainsValue(attid))
                        {
                        //FileID做唯一键,不会重复
                        attDic.Add(row["FileID"].ToString().Trim(),attid);
                        }
                    }
                    attjson = JsonConvert.SerializeObject(attDic);

                    #region 附件验证

                    if (!attDic.ContainsValue("field0032"))
                    {
                        throw new Exception("缺少学术材料附件");
                    }
                    if (!attDic.ContainsValue("field0033"))
                    {
                        throw new Exception("缺少会议议程附件");
                    }
                    if (!attDic.ContainsValue("field0034"))
                    {
                        throw new Exception("缺少会议纪要附件");
                    }
                    if (!attDic.ContainsValue("field0036"))
                    {
                        throw new Exception("缺少会议照片附件");
                    }
                    if (!attDic.ContainsValue("field0037"))
                    {
                        throw new Exception("缺少学参会名单附件");
                    }

                    #endregion

                    #endregion


                #region formdata
                string formData = $@"<formExport version=""2.0""><summary id=""-4402170772378466531"" name=""formmain_5925""/><definitions/><values>
                                    <column name=""公司""><value>上海腾瑞制药有限公司</value></column>
                                    <column name=""申请人""><value>{EmployeeeID}</value></column>
                                    <column name=""申请日期""><value>{field0067}</value></column>
                                    <column name=""总金额""><value>{field0023}</value></column>
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
                                    <column name=""医院""><value>{field0025}</value></column>
                                    <column name=""科室""><value>{19}</value></column>
                                    <column name=""学术材料""><value></value></column>
                                    <column name=""会议议程""><value></value></column>
                                    <column name=""会议纪要""><value></value></column>
                                    <column name=""课酬附件""><value></value></column>
                                    <column name=""会议照片""><value></value></column>
                                    <column name=""参会名单""><value></value></column>
                                    <column name=""新医院""><value></value></column>
                                    <column name=""新科室""><value></value></column>
                                    <column name=""新事由""><value></value></column>
                                    <column name=""规模""><value>{field0020}</value></column>
                                    <column name=""是否按计划""><value>{field0044}</value></column>
                                    <column name=""费用类型""><value>学术活动费</value></column>
                                    <column name=""个人报销金额""><value>{field0047}</value></column>
                                    <column name=""财务审批标志""><value>-1</value></column>
                                    <column name=""费用来源""><value>{field0063}</value></column>
                                    <column name=""可用额""><value>{field0053}</value></column>
                                    <column name=""在途额""><value>{field0054}</value></column>
                                    <column name=""活动类型""><value>{39}</value></column>
                                    <column name=""是否有发票""><value>0</value></column>
                                    <column name=""YRB发起""><value>1</value></column>
                                    <column name=""成本中心_归档""><value>{field0067}</value></column>
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

                #endregion

                sql = $@"Insert Into DataService.dbo.OATask([FTemplateCode],[FSenderLoginName],[FEmployeeCode],[FEmployeeName],[FSubject],[FData],[FFormContentAtt]) Values('O202001','{"zhangsh"}','{""}','{EmployeeeName}','{"学术活动费用支付单-YRB-" + EmployeeeName + "-" + DateTime.Now.ToString()}' , '{formData}','{attjson}')";
                runner.ExecuteSqlNone(sql);
                result = "<UpdateData><Result>True</Result><Description></Description></UpdateData>";
            }
            catch (Exception err)
            {
                result = "<UpdateData><Result>False</Result><Description>" + err.Message + "</Description></UpdateData>";
            }
            return result;
        }
        #endregion

        #region UploadFile
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

        #endregion

    }
}
