using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MinecraftBedrockRedirect
{
    class XMLConfig
    {
        private string fileName = "config.xml";
        public XMLConfig()
        {
            //判断配置文件是否存在，若不存在，则创建
            if (!File.Exists(fileName))
            {
                XElement root = new XElement("MinecraftBedrockRedirect");
                XElement server = new XElement("Server");
                server.SetElementValue("Address", "127.0.0.1");

                XElement officalServer = new XElement("OfficalServer");
                officalServer.SetElementValue("server1", "mco.cubecraft.net");
                officalServer.SetElementValue("server2", "mco.mineplex.com");
                officalServer.SetElementValue("server3", "hivebedrock.network");
                officalServer.SetElementValue("server4", "play.inpvp.net");
                officalServer.SetElementValue("server5", "mco.lbsg.net");

                root.Add(server);
                root.Add(officalServer);
                root.Save(fileName);
            }
        }

        public void setServerAddress(string serverAddress)
        {
            XDocument document = XDocument.Load(fileName);
            XElement root = document.Root;
            XElement server = root.Element("Server");
            XElement address = server.Element("Address");
            address.Value = serverAddress;

            root.Save(fileName);
        }

        public string getServerAddress()
        {
            XDocument document = XDocument.Load(fileName);
            XElement root = document.Root;
            XElement server = root.Element("Server");
            XElement address = server.Element("Address");

            return address.Value;
        }

        public List<String> getOfficelServerAddress()
        {
            XDocument document = XDocument.Load(fileName);
            XElement root = document.Root;
            XElement officalServer = root.Element("OfficalServer");

            List<String> list = new List<string>();
            foreach (var item in officalServer.Elements())
            {
                list.Add(item.Value);
            }

            return list;
        }
    }
}
