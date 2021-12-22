using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MCD_LinqToXmlOlusturma
{
    class Program
    {
        static void Main(string[] args)
        {

            #region Linq ile XML e veri atama
            
            List<Ogrenci> Ogrencilerim = new List<Ogrenci>();
            for (int i = 0; i < 50; i++)
            {
                Ogrenci temp = new Ogrenci();
                temp.ID = Guid.NewGuid();
                temp.Isim = FakeData.NameData.GetFirstName();
                temp.Soyisim = FakeData.NameData.GetSurname();
                temp.Numara = FakeData.NumberData.GetNumber(100, 500);
                Ogrencilerim.Add(temp);
            }

            XDocument Doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("Ogrencilerim",
                Ogrencilerim.Select(I =>
                new XElement("Ogrenci",
                new XElement("ID", I.ID),
                new XElement("Isim", I.Isim),
                new XElement("Soyisim", I.Soyisim),
                new XElement("Numara", I.Numara)
                )
                )
                )
                );

            Doc.Save(@"c:\XML\Ogrencilerim.xml");
            
            #endregion

            #region Dokuman Okuma
            XDocument DockOku = XDocument.Load(@"c:\XML\Ogrencilerim.xml");
            List<XElement> OkunanXElements = DockOku.Descendants("Ogrenci").ToList();
            List<Ogrenci> okunanData = new List<Ogrenci>();

            foreach (XElement item in OkunanXElements)
            {
                Ogrenci temp = new Ogrenci();
                temp.ID = Guid.Parse(item.Element("ID").Value);
                temp.Isim = item.Element("Isim").Value;
                temp.Soyisim = item.Element("Soyisim").Value;
                temp.Numara = int.Parse(item.Element("Numara").Value);

                okunanData.Add(temp);
            }

            foreach (Ogrenci item in okunanData)
            {

                Console.WriteLine(item.ID);
                Console.Write(item.Isim + " ");
                Console.WriteLine(item.Soyisim);
                Console.WriteLine(item.Numara);
                Console.WriteLine("-------");
            }
            #endregion
            Console.ReadLine();
        }
    }
}
