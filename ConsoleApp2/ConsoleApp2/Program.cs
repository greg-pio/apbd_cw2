using Cw2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace ConsoleApp2
{
    class Program
    {
        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
            w.WriteLine("  :");
            w.WriteLine($"  :{logMessage}");
            w.WriteLine("-------------------------------");
        }

        static void Main(string[] args)
        {
            string csvpath;
            string xmlpath;
            string filetype;
            string logpath = @"C:\Users\ger\Desktop\log.txt";           

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
                        int pos = Array.IndexOf(kolumny, null);

                        if (kolumny.Length != 9 || pos > -1)
                        {
                            using (StreamWriter sw = File.AppendText(logpath))
                            {
                                Log("Blad. Pomijam studenta: " + kolumny[4], sw);
                            }
                        }
                        else
                        {
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

                        
                        
                        //int pos = Array.IndexOf(kolumny, null);





                        /*
                        if (kolumny.Length != 9)
                        {
                            using (StreamWriter sw = File.AppendText(logpath))
                            {
                                Log("Za malo kolumn. Pomijam studenta: " + st.Index, sw);
                            }
                        }
                        else
                            list.Add(st);
                        */


                        /*
                        if (list.Contains((st))
                        {
                            using (StreamWriter sw = File.AppendText(logpath))
                            {
                                Log("Test1", sw);
                            }
                        }
                        else
                            list.Add(st);
                        */

                        /*
                        if (kolumny.Length < 9)
                        {
                            using (StreamWriter sw = File.AppendText(logpath))
                            {
                                sw.WriteLine("Za malo kolumn. Pomijam studenta: " + st.Index);
                            }
                        }
                        else
                            list.Add(st);
                        */


                        /*
                        int pos = Array.IndexOf(kolumny, null);
                        if (kolumny.Length < 9)
                        {
                            using (StreamWriter sw = File.AppendText(logpath))
                            {
                                sw.WriteLine("Za malo kolumn. Pomijam studenta: " + st.Index);
                            }
                        } else if (pos > -1)
                        {
                            using (StreamWriter sw = File.AppendText(logpath))
                            {
                                sw.WriteLine("Wystepuja puste pola. Pomijam studenta: " + st.Index);
                            }
                        } else
                        {
                            list.Add(st);
                        } 
                        */



                    }
                    stream.Dispose();

                    FileStream writer = new FileStream(xmlpath, FileMode.Create);
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Student>),
                                               new XmlRootAttribute("uczelnia"));
                    serializer.Serialize(writer, list);
                }
            } catch (ArgumentException e)
            {
                Console.WriteLine("Podana sciezka jest niepoprawna.");
                using (StreamWriter sw = File.AppendText(logpath))
                {
                    Log("Podana sciezka jest niepoprawna.", sw);
                }
            } catch (FileNotFoundException f)
            {
                Console.WriteLine("Plik nie istnieje");
                using (StreamWriter sw = File.AppendText(logpath))
                {
                    Log("Plik nie istnieje.", sw);
                }
            }
        }
    }
}
