using Cw2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvpath;
            string xmlpath;
            string filetype;

            if (args.Length < 3)
            {
                csvpath = @"Data\dane.csv";
                xmlpath = @"C:\Users\ger\Desktop\wynik.xml";
                filetype = "xml";
            } else
            {
                csvpath = args[0];
                xmlpath = args[1];
                filetype = args[2];
            } 
                                    
            var fi = new FileInfo(csvpath);
            using (var stream = new StreamReader(fi.OpenRead()))

            {
                var list = new List<Student>();
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] kolumny = line.Split(',');
                    var st = new Student
                    {
                        Imie = kolumny[0],
                        Nazwisko = kolumny[1]
                    }; 
                    list.Add(st);
                }
                stream.Dispose();

                FileStream writer = new FileStream(xmlpath, FileMode.Create);
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>),
                                           new XmlRootAttribute("uczelnia"));
                serializer.Serialize(writer, list);
                serializer.Serialize(writer, list);
            }          
        }
    }
}
