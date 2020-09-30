using System;
using System.Drawing;
using System.Windows.Forms;
using EasyModbus;

namespace ModbusBoxes
{
    public partial class Form1 : Form
    {
        ModbusClient modbusClient;
        WarningDisplay WarDisplay;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WarDisplay = new WarningDisplay();
            modbusClient = new ModbusClient("192.168.10.65", 502);    //Ip-Address and Port of Modbus-TCP-Server
            modbusClient.Connect();                                                    //Connect to Server 
            if (modbusClient.Connected)
            {
                WarDisplay.Data = "=======================";
                WarDisplay.Data = "  Connection stablish ";
                WarDisplay.Data = "=======================";
                txtWarning.Text = WarDisplay.Data;
            }
            TData.Enabled = true;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            modbusClient.Disconnect();//Disconnect from Server
        }

        private void TData_Tick(object sender, EventArgs e)
        {
            int[] readHoldingRegisters = modbusClient.ReadHoldingRegisters(0, 10);    //Read 10 Holding Registers from Server, starting with Address 1
            WarDisplay.Data = " ";
            string sensors =Convert.ToInt32( intToArrayBool(readHoldingRegisters[1])).ToString("00000");
            txtsensors.Text = sensors;
            if (sensors[0] == '1')
            {
                //other
               pother.BackColor = Color.LawnGreen;
                WarDisplay.Data = "other SENSOR : [TRUE]";
            }
            else
            {
                pother.BackColor = Color.IndianRed;
                WarDisplay.Data = "other SENSOR : [FALSE]";
            }
            if (sensors[1] == '1')
            {
                //reset
                preset.BackColor = Color.LawnGreen;
                WarDisplay.Data = "reset SENSOR : [TRUE]";
            }
            else
            {
                preset.BackColor = Color.IndianRed;
                WarDisplay.Data = "reset SENSOR : [FALSE]";

            }
            if (sensors[2] == '1')
            {
                //stop
                pstop.BackColor = Color.LawnGreen;
                WarDisplay.Data = "stop SENSOR : [TRUE]";
            }
            else
            {
                pstop.BackColor = Color.IndianRed;
                WarDisplay.Data = "stop SENSOR : [FALSE]";
            }


        if (sensors[3] == '1')
                {
                //RUN
                pstart.BackColor = Color.LawnGreen;
                WarDisplay.Data = "start SENSOR : [TRUE]";
            }
        else
                {
                pstart.BackColor = Color.IndianRed;
                WarDisplay.Data = "start SENSOR : [FALSE]";
            }

        if (sensors[4] == '1')
                    {
                //inductive
                pSensor.BackColor = Color.LawnGreen;
                WarDisplay.Data = "STOP SENSOR : [TRUE]";
            }
        else
         {
                pSensor.BackColor = Color.IndianRed;
                WarDisplay.Data = "STOP SENSOR : [FALSE]";
            }
            txtDistance.Text = readHoldingRegisters[2].ToString();

            WarDisplay.Data = "DISTANCE SENSOR : " + txtDistance.Text;
            txtWarning.Text = WarDisplay.Data;
            txtWarning.SelectionStart = txtWarning.TextLength;
            txtWarning.ScrollToCaret();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private string intToArrayBool( int input )
        {
            bool[] output = new bool[16];
            return Convert.ToString(input, 2);
        }
    }
}
