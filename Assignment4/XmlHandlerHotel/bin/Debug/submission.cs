using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;
using System.Security.Permissions;
using System.Collections.Generic;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Program
    {
        public static string xmlURL = "Hotels.xml";
        public static string xmlErrorURL = "HotelsErrors.xml";
        public static string xsdURL = "Hotels.xsd";
        public static List<string> errorList = new List<string>();
        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            //result = Verification(xmlErrorURL, xsdURL);
           // Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        
        public static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            errorList.Add(e.Message);
            Console.WriteLine("Validation Error: {0}", e.Message);
        }
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            errorList.Clear();
            XmlSchemaSet sc = new XmlSchemaSet();
            sc.Add(null, xsdUrl);
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;

            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
            XmlReader reader= XmlReader.Create(xmlUrl, settings);
            while (reader.Read())
            {}
            Console.WriteLine("The XML file validation has compelted");
            string errors = "";
            if (errorList.Count > 0)
            {
                for (int i = 0; i < errorList.Count; i++)
                {
                    errors += errorList[i].ToString() + "\n";
                }
            } else
            {
                errors = "No Error";
            }



                //return "No Error" if XML is valid. Otherwise, return the desired exception message.
                return errors;
        }

        public static string Xml2Json(string xmlUrl)
        {
            // newtonsoft documentation
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl); 
            
            string jsonText = JsonConvert.SerializeXmlNode(doc);
            
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            return jsonText;

        }
    }

}
