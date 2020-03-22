using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Cw2.Models
{
    public class Student
    {
        //[XmlAttribute(attributeName:"email")]
        //public string Email { get; set; }

        [XmlAttribute(attributeName: "indexNumber")]
        public string Index { get; set; }
        
        [XmlElement(elementName: "fname")]
        public string Imie { get; set; }

        private string _nazwisko;        
        [XmlElement(elementName: "lname")]
        public string Nazwisko
        {
            get { return _nazwisko; }
            set
            {
                if (value == null) throw new ArgumentException();
                _nazwisko = value;
            }
        }

        [XmlElement(elementName: "birthdate")]
        public string Data_urodzenia { get; set; }

        [XmlElement(elementName: "email")]
        public string EMail { get; set; }

        [XmlElement(elementName: "mothersName")]
        public string ImieMatki { get; set; }

        [XmlElement(elementName: "fathersName")]
        public string ImieOjca { get; set; }

        [XmlElement(elementName: "name")]
        public string Studia { get; set; }

        [XmlElement(elementName: "mode")]
        public string Typ { get; set; }

    }
}
