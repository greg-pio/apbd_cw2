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
            string logpath = @"C:\Users\ger\Desktop\log.txt";
            DateTime localDate = DateTime.Now;

            if (!File.Exists(logpath))
            {
                using (StreamWriter sw = File.CreateText(logpath))
                {
                    sw.WriteLine("New log file created " + localDate);
                }
            }

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

            try
            {
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
                            Nazwisko = kolumny[1],
                            Studia = kolumny[2],
                            Typ = kolumny[3],
                            Index = kolumny[4],
                            Data_urodzenia = kolumny[5],
                            EMail = kolumny[6],
                            ImieMatki = kolumny[7],
                            ImieOjca = kolumny[8]
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
            } catch (ArgumentException e)
            {
                Console.WriteLine("Podana sciezka jest niepoprawna.");
                using (StreamWriter sw = File.AppendText(logpath))
                {
                    sw.WriteLine("Podana sciezka jest niepoprawna.");
                    sw.WriteLine(e);
                }
            } catch (FileNotFoundException f)
            {
                Console.WriteLine("Plik nie istnieje");
                using (StreamWriter sw = File.AppendText(logpath))
                {
                    sw.WriteLine("Plik nie istnieje");
                    sw.WriteLine(f);
                }
            }
        }
    }
}
