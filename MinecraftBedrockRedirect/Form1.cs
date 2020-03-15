using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DNS.Server;
using System.Diagnostics;

namespace MinecraftBedrockRedirect
{
    public partial class Form1 : Form
    {
        private XMLConfig xmlConfig;

        private bool flag;
        private MasterFile masterFile;
        private DnsServer server;

        delegate void TextboxDelegate(object obj);
        public Form1()
        {
            InitializeComponent();

            xmlConfig = new XMLConfig();

            textBox1.Text = xmlConfig.getServerAddress();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && !flag)
            {
                string serverAddress = textBox1.Text;

                IPAddress iPAddress;
                if (!IPAddress.TryParse(serverAddress, out iPAddress))
                {
                    MessageBox.Show("不是有效的IP地址", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                flag = true;
                button1.Enabled = false;
                button2.Enabled = true;
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is RadioButton)
                    {
                        RadioButton radioButton = (RadioButton)item;
                        radioButton.Enabled = false;
                    }
                }

                xmlConfig.setServerAddress(serverAddress);
                List<String> officalServerAddress = xmlConfig.getOfficelServerAddress();
                startServer(serverAddress, officalServerAddress);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flag)
            {
                flag = false;
                button1.Enabled = true;
                button2.Enabled = false;
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is RadioButton)
                    {
                        RadioButton radioButton = (RadioButton)item;
                        radioButton.Enabled = true;
                    }
                }

                stopServer();
            }
        }

        private void startServer(string serverAddress, List<string> officalServerAddress)
        {
            masterFile = new MasterFile();
            server = new DnsServer(masterFile, "223.5.5.5");

            if (radioButton1.Checked)
            {
                masterFile.AddIPAddressResourceRecord(officalServerAddress[0], serverAddress);
            }
            else if (radioButton2.Checked)
            {
                masterFile.AddIPAddressResourceRecord(officalServerAddress[1], serverAddress);
            }
            else if (radioButton3.Checked)
            {
                masterFile.AddIPAddressResourceRecord(officalServerAddress[2], serverAddress);
            }
            else if (radioButton4.Checked)
            {
                masterFile.AddIPAddressResourceRecord(officalServerAddress[3], serverAddress);
            }
            else if (radioButton5.Checked)
            {
                masterFile.AddIPAddressResourceRecord(officalServerAddress[4], serverAddress);
            }

            server.Listen();
        }

        private void stopServer()
        {
            server.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/KatyushaScarlet/Minecraft-Bedrock-DNS-Redirect/releases");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://Katyusha.net");
        }
    }
}
