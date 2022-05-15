using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UICheck1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }

        private void flatButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            
            flatStatusBar1.Text = "waiting for login...";
            Boolean isOkay = false;

            dbConnector aConnector = new dbConnector();
            isOkay = aConnector.checkCredentials(textBox1.Text, textBox2.Text);
            
            if (isOkay == true) {
                flatStatusBar1.Text = "Login successful";
                this.Visible = false;
                MainMenu mmx = new MainMenu {
                    Visible = true
                };

            }
            else {
                flatStatusBar1.Text = "Error, please enter the correct credentials";
            }

        }
    }
}
