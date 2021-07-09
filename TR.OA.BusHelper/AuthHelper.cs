using iTR.Lib;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TR.OA.BusHelper
{
    public class AuthHelper
    {
        /// <summary>
        /// 保存OA经营授权表
        /// </summary>
        /// <param name="field0001">客户编码</param>
        /// <param name="field0002">客户名称</param>
        /// <param name="field0003">责任人工号</param>
        /// <param name="field0004">责任人</param>
        /// <param name="field0005">授权人</param>
        /// <param name="field0006">客户类型</param>
        /// <returns></returns>
        public bool SaveHospitalAuth(string jsonMessage, string FormatResult = "")
        {
            SQLServerHelper runner = new SQLServerHelper();
            string id = "";
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
                string sql = $"select * from formmain_8739 where field0001='{field0001}' and  field0009 ='1'";
                DataTable dataTable = runner.ExecuteSql(sql);
                if (dataTable.Rows.Count == 0)
                {
                    sql = $@"Insert Into v3x.dbo.formmain_8739(ID,[state],[start_date],start_member_id,approve_member_id,approve_date,finishedflag,ratifyflag,ratify_member_id,modify_member_id,modify_date,sort,ratify_date,field0001,field0002,field0003,field0004,field0005,field0006,field0007,field0009,field0010,field0012)   Values({id}, 1, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 4947084126114450644, 0,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 0, 0, 0, 494708412611445064,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 0,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{field0001}', '{field0002}', '{field0003}', '{field0004}', '{field0005}', '{field0006}', '{field0007}',   '{field0009}', '{field0010}', '{field0012}')";
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
                    
                    //更新失效时间和状态
                    sql = $"UPDATE formmain_8739   SET    field0009='0',field0008='{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}' where ID= '{dataTable.Rows[0]["ID"]}' ";
                    runner.ExecuteSqlNone(sql);
                    #region 插入新的授权记录 新的授权ID 其他表不可查。。。
                    byte[] gb = Guid.NewGuid().ToByteArray();
                    long newid = BitConverter.ToInt64(gb, 0);
                    sql = $@"Insert Into v3x.dbo.formmain_8739(ID,[state],[start_date],start_member_id,approve_member_id,approve_date,finishedflag,ratifyflag,ratify_member_id,modify_member_id,modify_date,sort,ratify_date,field0001,field0002,field0003,field0004,field0005,field0006,field0007,field0009,field0010,field0012)   Values({newid}, 1, '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 4947084126114450644, 0,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 0, 0, 0, 494708412611445064,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 0,  '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', '{field0001}', '{field0002}', '{field0003}', '{field0004}', '{field0005}', '{field0006}', '{field0007}',   '{field0009}', '{field0010}', '{field0012}')";
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
                return true;

            }
            catch (Exception ex)
            {

                return false;
            }
        }
    }
}
