using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTR.Lib;
using System.Xml;
using System.Data;

namespace TR.OA.BusHelper
{
    public class ItemBusHelper
    {
        private SQLServerHelper runner = null;
        public ItemBusHelper()
        { 

        }

        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <param name="xmlString"></param>
        /// <returns>XML String</returns>
        /// 王天池
        public string GetProductList(string xmlString)
        {
            string result = "", sql = "", filter = " t1.field0008=-4875734478274671070 ", val = "";
            SQLServerHelper runner;

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);

                XmlNode vNode = doc.SelectSingleNode("GetProductList/ID");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.ID='" + val + "'" : "t1.ID='" + val + "'";
                }

                vNode = doc.SelectSingleNode("GetProductList/Name");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.field0002 like '%" + val + "%'" : "t1.field0002 like '%" + val + "%'";
                }
                vNode = doc.SelectSingleNode("GetProductList/Number");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.field0001 like '%" + val + "%'" : "t1.field0001 like '%" + val + "%'";
                }
                vNode = doc.SelectSingleNode("GetProductList/TypeID");
                if (vNode != null)
                {
                    val = vNode.InnerText.Trim();
                    if (val.Length > 0)
                        filter = filter.Length > 0 ? filter = filter + " And t1.field0003 = '" + val + "'" : " t1.field0003= '" + val + "'";
                }


                sql = @"Select t1.field0001 FNumber,t1.field0002 FName,t1.field0003 FTypeName ,t1.field0004 ,t1.field0005,t1.field0006,t1.field0007,
                        t1.field0009,t1.field0011,t1.field0012,t1.field0013,t1.field0014,t1.field0015,t1.field0017,t1.field0019,t1.field0021,
                        t1.field0023
                        from  v3x.dbo.formmain_8673 t1";

                if (filter.Length > 0)
                    sql = sql + " Where " + filter;
                sql = sql + " order by t1.field0001 Asc";

                runner = new SQLServerHelper();
                DataTable dt = runner.ExecuteSql(sql);
                result = iTR.Lib.Common.DataTableToXml(dt, "GetProductList", "", "List");
                return result;
            }
            catch(Exception Err)
            {
                throw Err;
            }

         }
            

        public string GetItemList(string xmlString)
        {
            string sql = "", result = "",ClassID="";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            XmlNode node = doc.SelectSingleNode("GetList/ClassID");
            if (node != null)
                ClassID = node.InnerText;

            if (ClassID.Trim().Length < 19)
            {
                sql = @"Select  field0001 As FNumber, field0008 AS FName
                    From v3x.dbo.formmain_2894
                    Where field0002 ='-4875734478274671070'  and field0005='{0}'
                    Order by field0001 ASC";
            }
            else
            {
                sql = @" Select ID AS FNumber, SHOWVALUE AS FName
                        From v3x.dbo.CTP_ENUM_ITEM
                        Where REF_ENUMID = '{0}'
                        Order by SORTNUMBER ASc";
            }

            sql = string.Format(sql, ClassID);
            runner = new SQLServerHelper();
            DataTable dt = runner.ExecuteSql(sql);
            result = iTR.Lib.Common.DataTableToXml(dt, "GetList","","List");
            return result;
        }

    }
}
