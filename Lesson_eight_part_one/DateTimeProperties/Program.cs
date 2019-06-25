using System;
using System.Xml.Serialization;
using System.IO;

namespace DateTimeProperties
{

    class Program
    {
        static void SaveAsXmlFormat(DateTime obj, string fileName)
        {
            XmlSerializer xmlFormat = new XmlSerializer(typeof(DateTime));
            // Создаем файловый поток(проще говоря, создаем файл)
            Stream fStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            // В этот поток записываем сериализованные данные(записываем xml-файл)
            xmlFormat.Serialize(fStream, obj);
            fStream.Close();
        }

        static DateTime LoadFromXmlFormat(string fileName)
        {
            DateTime obj = new DateTime();
            // Считать объект Student из файла fileName формата XML
            XmlSerializer xmlFormat = new XmlSerializer(typeof(DateTime));
            Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            obj = (DateTime)xmlFormat.Deserialize(fStream);
            fStream.Close();
            return obj;
        }
       
        static void Main(string[] args)
        {
            DateTime newDate  = new DateTime(2019, 6, 24, 22, 55, 00); //  задаю открытые свойсвтва
            
            SaveAsXmlFormat(newDate, "data.xml"); // Сохраняю в файл 
            newDate = LoadFromXmlFormat("data.xml"); // загружаю из файла
            Console.WriteLine("{0}-{1}-{2}", newDate.Day, newDate.Month, newDate.Year);// вывожу
          
        }
    }

}
