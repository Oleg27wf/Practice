using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using MySql.Data.MySqlClient;
using System.Management;

namespace practice_login_and_register
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            //Loading form timer
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(5000);
            InitializeComponent();
            t.Abort();
        }
        public void StartForm()
        {
            Application.Run(new LoadForm());
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //get ip of user(refers to the Network Configuration)
            string Host = System.Net.Dns.GetHostName();
            //get host user(refers to the Network Configuration)
            string IP = System.Net.Dns.GetHostByName(Host).AddressList[0].ToString();
            //show Ip and Host in tb
            textBox6.Text = IP;
            textBox8.Text = Host;
            textBox6.ReadOnly = true;
            textBox8.ReadOnly = true;

            //information about processor
            ManagementObjectSearcher searcher8 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

            foreach (ManagementObject queryObj in searcher8.Get())
            {
                textBox1.Text = string.Format("{0}", queryObj["Name"]);
                textBox3.Text=string.Format("{0}", queryObj["NumberOfCores"]);
                textBox1.ReadOnly = true;
                textBox3.ReadOnly = true;
            }
            // information about your VideoController
            ManagementObjectSearcher searcher11 =  new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_VideoController");
            foreach (ManagementObject queryObj in searcher11.Get())
            {
                textBox2.Text = string.Format("{0}", queryObj["Caption"]);
                textBox2.ReadOnly = true;
                textBox4.Text = string.Format("{0} Gb", Math.Round(System.Convert.ToDouble(queryObj["AdapterRam"]) / 1024 / 1024 / 1024, 2));
                textBox4.ReadOnly = true;
                
            }
            // information about RAM
            ManagementObjectSearcher searcher12 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject queryObj in searcher12.Get())
            {
                textBox13.Text = string.Format("{0} Gb,Speed:{1}", Math.Round(System.Convert.ToDouble(queryObj["Capacity"]) / 1024 /1024 /1024,2), queryObj["Speed"]);
                textBox13.ReadOnly = true;
            }

            //HardDrive information
            ManagementObjectSearcher searcher13 =  new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject queryObj in searcher13.Get())
            {
                textBox5.Text=string.Format(" Model: {0};  {1} Gb",
                queryObj["Model"],  
                Math.Round(System.Convert.ToDouble(queryObj["Size"]) / 1024 / 1024 / 1024, 2));
                textBox5.ReadOnly = true;
               
            }
            //OC Caption
            ManagementObjectSearcher searcher5 =new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject queryObj in searcher5.Get())
            {
                textBox14.Text = string.Format(" {0}", queryObj["Caption"]);
                textBox14.ReadOnly = true;
            }
            //Network Configuration
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                textBox12.Text = string.Format("{0}", queryObj["Caption"]);
                textBox12.ReadOnly = true;
                if (queryObj["DefaultIPGateway"] == null)
                   textBox11.Text=string.Format("{0}", queryObj["DefaultIPGateway"]);
                else
                {
                    String[] arrDefaultIPGateway = (String[])(queryObj["DefaultIPGateway"]);
                    foreach (String arrValue in arrDefaultIPGateway)
                    {
                        textBox11.Text=string.Format("DefaultIPGateway{0}", arrValue);
                    }
                }
                if (queryObj["IPSubnet"] == null)
                textBox7.Text = string.Format("{0}", queryObj["IPSubnet"]);
                else
                {
                    String[] arrIPSubnet = (String[])(queryObj["IPSubnet"]);
                    foreach (String arrValue in arrIPSubnet)
                    {
                        textBox7.Text = string.Format("IpSubnet: {0}", arrValue);
                    }
                }
                textBox9.Text= string.Format("{0}", queryObj["MACAddress"]);
                textBox10.Text = string.Format("{0}", queryObj["ServiceName"]);
                // assign property="readonly"
                textBox11.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBox10.ReadOnly = true;
                textBox9.ReadOnly = true;
                ChangeColor();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //Close Window
        private void labelClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //changing color of textbox
        public void ChangeColor()
        {
            textBox1.BackColor = Color.White;
            textBox2.BackColor = Color.White;
            textBox3.BackColor = Color.White;
            textBox4.BackColor = Color.White;
            textBox5.BackColor = Color.White;
            textBox6.BackColor = Color.White;
            textBox7.BackColor = Color.White;
            textBox8.BackColor = Color.White;
            textBox9.BackColor = Color.White;
            textBox10.BackColor = Color.White;
            textBox11.BackColor = Color.White;
            textBox12.BackColor = Color.White;
            textBox13.BackColor = Color.White;
            textBox14.BackColor = Color.White;
        }
    }
}