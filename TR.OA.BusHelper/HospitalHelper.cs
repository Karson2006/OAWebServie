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
    public class HospitalHelper
    {
        public HospitalHelper()
        {

        }
        /// <summary>
        /// 获取OA数据库中的医院列表
        /// </summary>
        /// <returns></returns>
        public string GetOAHospitalList( string xmlString)
        {

            string result = "", sql = "", filter = " t1.FIsDeleted=0 ", val = "";
            SQLServerHelper runner;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlNode vNode = doc.SelectSingleNode("GetOAHospitalList/ID");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.ID='" + val + "'" : "t1.ID='" + val + "'";
                }
                //医院名称
                vNode = doc.SelectSingleNode("GetHospitalList/Name");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.field0002 like '%" + val + "%'" : "t1.field0002 like '%" + val + "%'";
                }
                //医院代码
                vNode = doc.SelectSingleNode("GetHospitalList/Number");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.field0001 like '%" + val + "%'" : "t2.field0001 like '%" + val + "%'";
                }
               
                //所在省市
                vNode = doc.SelectSingleNode("GetHospitalList/ProvinceName");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.field0005 like '%" + val + "%'" : " t1.field0005= '%" + val + "%'";
                }
                //所在地市
                vNode = doc.SelectSingleNode("GetHospitalList/CityName");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.field0010 like '%" + val + "%'" : " t1.field0010= '%" + val + "%'";
                }


                //社会信用代码Cod
                vNode = doc.SelectSingleNode("GetHospitalList/CreditCode");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.field0023 like '%" + val + "%'" : " t1.field0023 like '%" + val + "%'";
                }

                //经济性质
                vNode = doc.SelectSingleNode("GetHospitalList/EconomicType");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.field0030= '" + val + "'" : " t1.field0030= '" + val + "'";
                }

                sql = @"Select t1.ID FID, t1.field0001 FName,t1.field0002 FNumber,t1.field0004 FType,t1.field0005 FProvinceName,t1.field0010 FCityName,t1.field0011 FCountryName,
                        (Case t1.field0008 When - 4875734478274671070 Then '是' Else '否' End) FStatus, t1.field0016 FRegisterName, t1.field0017 FAddress, t1.field0019 ,t1.field0020,t1.field0021,
                        t1.field0022,t1.field0023,t1.field0024,t1.field0025,t1.field0026,t1.field0027,t1.field0028,t1.field0030
                        From v3x.dbo.formmain_8044 t1";
                if (filter.Length > 0)
                    sql = sql + " Where " + filter;
                sql = sql + " order by t1.field0002 Asc";

                runner = new SQLServerHelper();
                DataTable dt = runner.ExecuteSql(sql);
                result = Common.DataTableToXml(dt, "GetHospitalList", "", "List");
               
            }
            catch (Exception err)
            {
                throw err;
            }
            return result;
        }
    }
        /// <summary>
        /// 保存OA经营授权表
        /// </summary>
        /// <param name="field0001">客户编码</param>
        /// <param name="field0002">客户名称</param>
        /// <param name="field0003">责任人工号</param>
        /// <param name="field0004">责任人</param>
        /// <param name="field0005">授权人</param>
        /// <param name="field0006">客户类型</param>
        /// <returns>传回ID供药瑞宝回调删除</returns>
        public string SaveHospitalAuth(string jsonMessage, string FormatResult = "")
        {
            FileLogger.WriteLog("授权Json：" + jsonMessage, 1, "OAWebService", "SaveHospitalAuth", "DataService");
            SQLServerHelper runner = new SQLServerHelper();
            string id = "";

            //做查入formmain_8739失败的删除记录的操作，不做执行存储过程执行失败的异常操作
            try
            {
                JObject pairs = JObject.Parse(jsonMessage);

                id = pairs["ID"]?.ToString();
                string field0001 = pairs["field0001"]?.ToString();
                string field0002 = pairs["field0002"]?.ToString();
                string field0003 = pairs["field0003"]?.ToString();
                string field0004 = pairs["field0004"]?.ToString();
                string field0005 = pairs["field0005"]?.ToString();
                string field0006 = pairs["field0006"]?.ToString();
                string field0007 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                string field0009 = "1";
                string field0010 = "0";
                string field0012 = pairs["field0012"]?.ToString();

                //string CONTENT_TEMPLATE_ID = "1385111134175381816";
                //string MODULE_TEMPLATE_ID = "621248607670977085";
                string Title = "经营授权表_维护";
                //先查询是否已经有授权记录
                string sql = $"select * from formmain_8739 where field0001='{field0001}' and   field0009 ='1'";
                DataTable dataTable = runner.ExecuteSql(sql);
                //没有医院记录
                if (dataTable.Rows.Count == 0)
                {
                    sql = $@"Insert Into v3x.dbo.formmain_8739(ID,[state],[start_date],start_member_id,approve_member_id,approve_date,finishedflag,ratifyflag,ratify_member_id,modify_member_id,modify_date,sort,ratify_date,field0001,field0002,field0003,field0004,field0005,field0006,field0007,field0009,field0010,field0012)   Values({id}, 1, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 4947084126114450644, 0,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 0, 0, 0, 494708412611445064,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 0,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{field0001}', '{field0002}', '{field0003}', '{field0004}', '{field0005??""}', '{field0006}', '{field0007}',   '{field0009}', '{field0010}', '{field0012}')";
                    runner.ExecuteSqlNone(sql);
                    try
                    { 
                        sql = $"EXEC DataService.dbo.Insert_CTP_CONTENT_ALL_Data {id},1385111134175381816,621248607670977085,'经营授权表_维护',42";
                        runner.ExecuteSqlNone(sql);
                    }
                    catch (Exception)
                    {
                        sql = $"DELETE FROM  formmain_8739 WHERE ID ='{id}'";
                        runner.ExecuteSqlNone(sql);
                        throw;
                    }
                }                
                else
                {
                    //有责任人的授权记录
                    sql = $"select * from formmain_8739 where field0001='{field0001}' and  field0003='{field0003}' and  field0009 ='1'";
                    dataTable = runner.ExecuteSql(sql);

                    //只更新授权人
                    if (dataTable.Rows.Count>0)
                    {
                        for (int i = 0; i < field0005.Split(',').Length; i++)
                        {
                            //沒有授权人
                            if (string.IsNullOrEmpty(dataTable.Rows[0]["field0005"].ToString()??""))
                            {
                                sql = $"UPDATE formmain_8739   SET   field0008='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' ,field0005 = '{dataTable.Rows[0]["field0005"] + "," + field0005.Split(',')[i]}'  where ID= '{dataTable.Rows[0]["ID"]}' ";
                                runner.ExecuteSqlNone(sql);
                            }
                            else
                            {
                                //添加新的授权人
                                if (!dataTable.Rows[0]["field0005"].ToString().Contains(field0005.Split(',')[i]))
                                {
                                    if (!string.IsNullOrEmpty(field0005.Split(',')[i]))
                                    {
                                        sql = $"UPDATE formmain_8739   SET   field0008='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' ,field0005 = '{dataTable.Rows[0]["field0005"] + "," + field0005.Split(',')[i]}'  where ID= '{dataTable.Rows[0]["ID"]}' ";
                                        runner.ExecuteSqlNone(sql);
                                    }

                                }
                            }

                        }

                    }
                    else
                    {
                        //更新失效时间和状态 还有授权人
                        sql = $"select * from formmain_8739 where field0001='{field0001}' and   field0009 ='1'";
                        dataTable = runner.ExecuteSql(sql);
                        sql = $"UPDATE formmain_8739   SET    field0009='0',field0008='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' where ID= '{dataTable.Rows[0]["ID"]}' ";
                        runner.ExecuteSqlNone(sql);

                        #region 插入新的授权记录 新的授权ID 其他表不可查。。。
                        byte[] gb = Guid.NewGuid().ToByteArray();
                        long newid = BitConverter.ToInt64(gb, 0);
                        id = newid.ToString();
                        sql = $@"Insert Into v3x.dbo.formmain_8739(ID,[state],[start_date],start_member_id,approve_member_id,approve_date,finishedflag,ratifyflag,ratify_member_id,modify_member_id,modify_date,sort,ratify_date,field0001,field0002,field0003,field0004,field0005,field0006,field0007,field0009,field0010,field0012)   Values({newid}, 1, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 4947084126114450644, 0,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 0, 0, 0, 494708412611445064,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 0,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{field0001}', '{field0002}', '{field0003}', '{field0004}', '{field0005??""}', '{field0006}', '{field0007}',   '{field0009}', '{field0010}', '{field0012}')";
                        runner.ExecuteSqlNone(sql);
                        try
                        {
                            sql = $"EXEC DataService.dbo.Insert_CTP_CONTENT_ALL_Data {newid},1385111134175381816,621248607670977085,'经营授权表_维护',42";
                            runner.ExecuteSqlNone(sql);
                        }
                        catch (Exception)
                        {
                            sql = $"DELETE FROM  formmain_8739 WHERE ID ='{newid}'";
                            runner.ExecuteSqlNone(sql);
                            throw;
                        }
                        #endregion
                    }

                }
                return id;
            }
            catch (Exception ex)
            {
                FileLogger.WriteLog("授权异常信息：" + ex.Message+ ","+ex.StackTrace??""+ ex.InnerException, 1, "OAWebService", "SaveHospitalAuth", "DataService");
                return "500";
            }
        }
    }
}
