using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace FivesLivraria.Data
{
    [XmlRoot("Acessos", Namespace = "http://fiveslivraria.no-ip.org", IsNullable = false)]
    public class CaixaAcesso
    {
        public Acessos acessos;

        public bool Exists(int userId, DateTime date)
        {
            foreach (Acesso acesso in this.acessos)
            {
                if (acesso.userId == userId && date.ToShortDateString() == acesso.dataAcesso.ToShortDateString())
                    return true;
            }
            return false;
        }
    }

    public class Acesso
    {
        [XmlAttribute]
        public DateTime dataAcesso;
        [XmlAttribute]
        public int userId;
        [XmlAttribute]
        public string userName;
    }

    public class Acessos : List<Acesso> { }

    public class SchemaManager
    {
        public void SaveFile(string fileName, CaixaAcesso caixaAcesso)
        {
            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + fileName, FileMode.Create);
            XmlTextWriter tw = new XmlTextWriter(fs, Encoding.Default);
            XmlSerializer serializer = new XmlSerializer(typeof(CaixaAcesso));
            serializer.Serialize(tw, caixaAcesso);
            fs.Close();
        }

        public CaixaAcesso LoadFile(string path, string fileName)
        {
            if (path.Length == 0)
                path = AppDomain.CurrentDomain.BaseDirectory;

            if (!path.EndsWith("\\"))
                path += "\\";

            try
            {
                FileStream fs = new FileStream(path + fileName, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(CaixaAcesso));
                serializer.UnknownNode += new XmlNodeEventHandler(serializer_UnknownNode);
                serializer.UnknownAttribute += new XmlAttributeEventHandler(serializer_UnknownAttribute);
                CaixaAcesso l = (CaixaAcesso)serializer.Deserialize(fs);
                fs.Dispose();

                return l;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("Error reading schema file '{0}': {1}", fileName, ex.Message));
            }

        }

        private void serializer_UnknownNode(object sender, XmlNodeEventArgs e)
        {
            Console.WriteLine("Unknown node: " + e.Name + "='" + e.Text + "'");
        }
        private void serializer_UnknownAttribute(object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            Console.WriteLine("Unknown attribute: " + attr.Name + "='" + attr.Value + "'");
        }
    }
}
