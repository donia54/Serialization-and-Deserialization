using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;


namespace Serialization
{
    public class Program
    {

        public static string serializeToXML(Employee e)
        {
            string result = "";
            XmlSerializer serializer = new XmlSerializer(e.GetType());
            using(StringWriter sw=new StringWriter())
            {
                using(XmlWriter writer=XmlWriter.Create(sw,new XmlWriterSettings { Indent=true})) 
                {
                    serializer.Serialize(writer, e);
                    result = sw.ToString();
                }
            }
            return result;
        }
        public static Employee deserializeFromXML(string xmlContent)
        {
            Employee employee = null;
            XmlSerializer serializer = new XmlSerializer(typeof(Employee));
            using (TextReader reader=new StringReader(xmlContent))
            {
                employee = serializer.Deserialize(reader) as Employee;
            }

            return employee;
        }

        public static string serializeToBinary(Employee e)
        {
            using (var stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, e);
                stream.Flush();
                stream.Position = 0;
                return Convert.ToBase64String(stream.ToArray());
            }
        }
        public static Employee deserializeFromBinary(string binaryContent)
        {
            byte[] bytes = Convert.FromBase64String(binaryContent);
            using(var stream =new MemoryStream(bytes))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(stream) as Employee;
            }
     
        }

        public static string serializeToJSON(Employee e)
        {
            // return JsonConvert.SerializeObject(e); using NewSoftJson

            //using JSONNET
            return System.Text.Json.JsonSerializer.Serialize(e, new JsonSerializerOptions { WriteIndented = true });
        }
        public static Employee deserializeFromJSON(string jsonContent)
        {
            return JsonConvert.DeserializeObject<Employee>(jsonContent); //using NewSoftJson

        }


        static void Main(string[] args)
        {

            Employee e = new Employee
            {
                Id = 1,
                Name = "Donia Aref",
                Benefits = { "Blah ", "Blah" }
            };

           #region XML Serialization 
            string XmlContent = serializeToXML(e);
            Console.WriteLine(XmlContent);
            File.WriteAllText("employee.xml", XmlContent);
            // XML Deserialization
            XmlContent = File.ReadAllText("employee.xml");
            Employee e1 = deserializeFromXML(XmlContent);
            #endregion


            #region Binary Serialization
            string binaryContent = serializeToBinary(e);
            Console.WriteLine(binaryContent);
            File.WriteAllText("employee.bin", binaryContent);
            // binary Deserialization
            binaryContent = File.ReadAllText("employee.bin");
            Employee e2 = deserializeFromBinary(binaryContent);
            #endregion


            #region JSON Serialization
            string jsonContent = serializeToJSON(e);
            Console.WriteLine(jsonContent);
            File.WriteAllText("employee.json", jsonContent);

            // JSON Deserialization
           jsonContent = File.ReadAllText("employee.json");
            Employee e3 = deserializeFromJSON(jsonContent);

            #endregion

            Console.ReadKey();


        }
    }
}
