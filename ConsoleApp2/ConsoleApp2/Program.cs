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

                        
            //Wczytywanie csv
            var fi = new FileInfo(csvpath);
            using (var stream = new StreamReader(fi.OpenRead()))

            {
                string line = null;
                while ((line = stream.ReadLine()) != null)
                {
                    string[] kolumny = line.Split(',');
                    Console.WriteLine(line);
                }
            }
            //stream.Dispose();
            //XML

            var list = new List<Student>();
            var st = new Student

            {
                Imie = "Jan",
                Nazwisko = "Kowalski",
                Email = "kowalski@wp.pl"
            };

            list.Add(st);
            FileStream writer = new FileStream(xmlpath, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<Student>),
                                       new XmlRootAttribute("uczelnia"));
            serializer.Serialize(writer, list);
            serializer.Serialize(writer, list);
        }
    }
}
