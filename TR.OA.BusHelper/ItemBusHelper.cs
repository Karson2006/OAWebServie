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

        public string GetItemList(string xmlString)
        {
            string sql = "", result = "",ClassID="";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            XmlNode node = doc.SelectSingleNode("GetList/ClassID");
            if (node != null)
                ClassID = node.InnerText;

            sql = @"Select  field0001 As FNumber, field0008 AS FName
                    From v3x.dbo.formmain_2894
                    Where field0002 ='-4875734478274671070'  and field0005='{0}'
                    Order by field0001 ASC";

            sql = string.Format(sql, ClassID);
            runner = new SQLServerHelper();
            DataTable dt = runner.ExecuteSql(sql);
            result = iTR.Lib.Common.DataTableToXml(dt, "GetList","","List");
            return result;
        }

    }
}
