﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using iTR.Lib;

namespace TR.OAWebServie
{
    public class Common
    {

        private static string AuthCode = "1d340262-52e0-413f-b0e7-fc6efadc2ee5";//将来采用不对称密钥加密

        public static Boolean CheckAuthCode(string callType, string xmlString)
        {
            Boolean result = false;
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            string decodeCode = "";

            XmlNode vNode = doc.SelectSingleNode(callType + "/AuthCode");
            if (vNode == null || vNode.InnerText.Trim().Length == 0)
                throw new Exception("授权代码不能为空");
            else
            {
                decodeCode = vNode.InnerText;
                decodeCode = iTR.Lib.Common.DecryptDES(decodeCode, iTR.Lib.Common.DesKey);
                result = AuthCode == decodeCode ? true : false;
            }
            if (!result)
                throw new Exception("授权码不正确");
            return result;
        }
    }
}