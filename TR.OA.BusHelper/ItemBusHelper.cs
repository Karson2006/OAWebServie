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
            string sql = "", result = "",EmployeeeID="";
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            XmlNode node = doc.SelectSingleNode("GetList/EmployeeID");
            if (node != null)
                EmployeeeID = node.InnerText;

            sql = @" Select ID,field0001,field0002,field0004,field0009,field0012,field0020,field0018,field0019,field0015,field0013
                          From v3x.dbo.formmain_6185
                          Where field0016 = {0}";
            sql = string.Format(sql, EmployeeeID);
            runner = new SQLServerHelper();
            DataTable dt = runner.ExecuteSql(sql);
            result = iTR.Lib.Common.DataTableToXml(dt, "GetList","","List");
            return result;
        }

    }
}
