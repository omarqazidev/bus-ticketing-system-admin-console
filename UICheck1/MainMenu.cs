using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UICheck1 {
    public partial class MainMenu : Form {

        //Variables For Insert - START
        private List<String> attributeNames1;
        private List<String> dataToInsert1;
        private int numOfAttributesLeft1;
        private int atAttribute1;
        private Boolean Insertable1 = false;
        //Variables For Insert - END

        //Variables For Update - START
        private List<String> attributeNames2;
        private List<String> dataToBeUpdated;
        private List<String> updatedData;
        private int numOfAttributesLeft2;
        private int atAttribute2;
        private Boolean Updatable1 = false;
        private Boolean readyForUpdate = false;
        //Variables For Update - END


        public MainMenu() {
            InitializeComponent();
        }

        private void flatTabControl1_SelectedIndexChanged(object sender, EventArgs e) {
            if (flatTabControl1.SelectedIndex == 0) {
                flatStatusBar1.Text = "Click 'Get Tables', Select a table, Then click 'Read'";
            } else if (flatTabControl1.SelectedIndex == 1) {
                flatStatusBar1.Text = "Click 'Get Tables', Select a table, Then click 'Insert' to start adding data";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) {

        }

        private void flatButton1_Click(object sender, EventArgs e) {

            if (flatComboBox1.SelectedItem != null) {
                dbConnector aConnector = new dbConnector();
                DataSet ds = aConnector.getInGrid(flatComboBox1.SelectedItem.ToString());
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Refresh();
            } else {
                MessageBox.Show("Please select a table first");

            }


        }

        private void flatStatusBar1_Click(object sender, EventArgs e) {

        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void flatLabel2_Click(object sender, EventArgs e) {

        }

        private void flatLabel1_Click(object sender, EventArgs e) {

        }

        private void flatButton2_Click(object sender, EventArgs e) {

        }

        private void flatClose1_Click(object sender, EventArgs e) {

        }

        private void View_Click(object sender, EventArgs e) {

        }

        private void Insert_Click(object sender, EventArgs e) {

        }

        private void formSkin1_Click(object sender, EventArgs e) {

        }

        private void flatButton2_Click_1(object sender, EventArgs e) {
            dbConnector aConnector = new dbConnector();
            List<String> lst = aConnector.getTableNames();
            flatComboBox1.DataSource = lst;

        }

        private void flatButton4_Click(object sender, EventArgs e) {
            dbConnector aConnector = new dbConnector();
            List<String> lst = aConnector.getTableNames();

            flatComboBox2.DataSource = lst;
        }

        private void flatButton5_Click(object sender, EventArgs e) {

            if (flatComboBox2.SelectedItem != null) {
                dbConnector aConnector = new dbConnector();

                attributeNames1 = aConnector.getAttributeNames(flatComboBox2.SelectedItem.ToString());

                List<String> l = attributeNames1;

                //var message = string.Join(Environment.NewLine, l);
                //MessageBox.Show(message);

                numOfAttributesLeft1 = attributeNames1.Count();
                atAttribute1 = 0;


                flatLabel1.Text = attributeNames1.ElementAt(atAttribute1);
                Insertable1 = true;

                dataToInsert1 = new List<string>();
                listBox1.DataSource = null;


            } else {
                MessageBox.Show("Please select a table first");

            }

        }

        private void flatButton3_Click(object sender, EventArgs e) {
            if (Insertable1 == true) {



                if (textBox2.Text != "") {
                    dataToInsert1.Add(textBox2.Text);
                } else {
                    dataToInsert1.Add("");
                }

                textBox2.Text = "";
                numOfAttributesLeft1--;
                atAttribute1++;

                if (numOfAttributesLeft1 == 1) {
                    flatButton3.Text = "Finish";
                    flatLabel1.Text = attributeNames1.ElementAt(atAttribute1);

                } else if (numOfAttributesLeft1 == 0) {

                    flatLabel1.Text = "[ attribute name ]";
                    flatButton3.Text = "Next";
                    Insertable1 = false;


                } else if (numOfAttributesLeft1 > 1) {
                    flatButton3.Text = "Next";
                    flatLabel1.Text = attributeNames1.ElementAt(atAttribute1);
                }




            }
        }

        private void flatButton6_Click(object sender, EventArgs e) {

            if (dataToInsert1 != null) {
                listBox1.DataSource = dataToInsert1;
            }

        }

        private void flatButton7_Click(object sender, EventArgs e) {

            dbConnector aConnector = new dbConnector();
            Boolean result = aConnector.insertData(flatComboBox2.SelectedItem.ToString(), dataToInsert1);

            if (result == true) {
                MessageBox.Show("Data insertion successful");
            } else if (result == false) {
                MessageBox.Show("Data insertion failed,");
            }

            listBox1.DataSource = null;

        }

        private void flatComboBox2_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void flatButton11_Click(object sender, EventArgs e) {
            dbConnector aConnector = new dbConnector();
            List<String> lst = aConnector.getTableNames();

            flatComboBox3.DataSource = lst;
        }

        private void flatButton12_Click(object sender, EventArgs e) {

            if (flatComboBox3.SelectedItem != null) {
                dbConnector aConnector = new dbConnector();

                DataSet ds = aConnector.getInGrid(flatComboBox3.SelectedItem.ToString());
                dataGridView2.DataSource = ds.Tables[0];
                dataGridView2.Refresh();


            } else {
                MessageBox.Show("Please select a table first");

            }

        }

        private void flatButton9_Click(object sender, EventArgs e) {
            if (dataGridView2.DataSource != null) {

                dataToBeUpdated = new List<string>();
                updatedData = new List<string>();
                readyForUpdate = false;

                for (int i = 0; i < dataGridView2.ColumnCount; i++) {
                    //MessageBox.Show("" + dataGridView2.CurrentRow.Cells[i].Value.ToString());
                    dataToBeUpdated.Add("" + dataGridView2.CurrentRow.Cells[i].Value.ToString());
                }

                //var message = string.Join(Environment.NewLine, dataToBeUpdated);
                //MessageBox.Show(message);

                //----------------------------------------------------------------------------------------------

                dbConnector aConnector = new dbConnector();
                attributeNames2 = aConnector.getAttributeNames(flatComboBox3.SelectedItem.ToString());
                List<String> l2 = attributeNames2;
                numOfAttributesLeft2 = attributeNames2.Count();
                atAttribute2 = 0;
                flatLabel2.Text = attributeNames2.ElementAt(atAttribute2);
                textBox1.Text = dataToBeUpdated.ElementAt(atAttribute2);
                Updatable1 = true;

            } else {
                MessageBox.Show("Please read the data first");

            }


        }

        private void flatButton10_Click(object sender, EventArgs e) {

            if (Updatable1 == true) {

                atAttribute2++;
                numOfAttributesLeft2--;

                updatedData.Add(textBox1.Text);

                if (numOfAttributesLeft2 == 1) {
                    flatLabel2.Text = attributeNames2.ElementAt(atAttribute2);
                    textBox1.Text = dataToBeUpdated.ElementAt(atAttribute2);
                    flatButton10.Text = "Finish";

                } else if (numOfAttributesLeft2 == 0) {

                    flatLabel2.Text = "[ attribute name ]";
                    flatButton10.Text = "Next";
                    textBox1.Text = "";
                    Updatable1 = false;
                    readyForUpdate = true;

                } else if (numOfAttributesLeft2 > 1) {
                    flatLabel2.Text = attributeNames2.ElementAt(atAttribute2);
                    textBox1.Text = dataToBeUpdated.ElementAt(atAttribute2);
                    flatButton10.Text = "Next";
                }

            } else {
                MessageBox.Show("Please select a row to update");
            }


        }
        //ejkdjqwnhdjhnqjwndjkqwnjkdnkqjlwndklqwkldqwl
        private void flatButton8_Click(object sender, EventArgs e) {
            if (flatComboBox2 != null && dataGridView2.DataSource != null && readyForUpdate == true) {
                dbConnector aConnector = new dbConnector();

                var dataBefore = string.Join(Environment.NewLine, dataToBeUpdated);
                var dataAfter = string.Join(Environment.NewLine, updatedData);


                DialogResult dialogResult = MessageBox.Show("Are you sure you want to update this information" +
                    Environment.NewLine + Environment.NewLine +
                    "FROM: " + Environment.NewLine + dataBefore +
                    Environment.NewLine + Environment.NewLine +
                    "TO: " + Environment.NewLine + dataAfter
                    , "Update Warning", MessageBoxButtons.YesNo);


                if (dialogResult == DialogResult.Yes) {
                    Boolean result = false;
                    result = aConnector.updateData(flatComboBox3.SelectedItem.ToString(), attributeNames2, updatedData, dataToBeUpdated);
                    if (result == true) {
                        MessageBox.Show("Data updation successful");
                    } else if (result == false) {
                        MessageBox.Show("Data updation failed,");
                    }
                } else if (dialogResult == DialogResult.No) {
                    MessageBox.Show("Data not updated");
                }


            } else {
                MessageBox.Show("Please make sure everything is in order before updating");

            }
        }

        private void flatComboBox3_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void flatButton13_Click(object sender, EventArgs e) {
            dbConnector aConnector = new dbConnector();
            List<String> lstx = aConnector.getTableNames();
            flatComboBox4.DataSource = lstx;
        }

        private void flatButton15_Click(object sender, EventArgs e) {
            if (flatComboBox4.SelectedItem != null) {
                dbConnector aConnector = new dbConnector();
                DataSet ds = aConnector.getInGrid(flatComboBox4.SelectedItem.ToString());
                dataGridView3.DataSource = ds.Tables[0];
                dataGridView3.Refresh();
            } else {
                MessageBox.Show("Please select a table first");

            }
        }

        private void flatButton14_Click(object sender, EventArgs e) {

            if (dataGridView3.DataSource != null && dataGridView3.SelectedCells != null) {
                dbConnector aConnector = new dbConnector();

                String tableName = flatComboBox4.SelectedItem.ToString();
                String attributeName = dataGridView3.Columns[0].HeaderText;
                String attributeValue = dataGridView3.CurrentRow.Cells[0].Value.ToString();

                MessageBox.Show("" + aConnector.deleteData(tableName, attributeName, attributeValue));
            }



        }

        private void flatButton16_Click(object sender, EventArgs e) {
            MessageBox.Show("" + dataGridView3.Columns[0].HeaderText);
            MessageBox.Show("" + dataGridView3.CurrentRow.Cells[0].Value.ToString());
        }

        private void flatButton16_Click_1(object sender, EventArgs e) {

            dbConnector aConnector = new dbConnector();
            DataSet ds = aConnector.getCustomTable("viewHumanInterations");
            dataGridView4.DataSource = ds.Tables[0];
            dataGridView4.Refresh();
        }

        private void flatButton17_Click(object sender, EventArgs e) {
            if (dataGridView5.DataSource != null && dataGridView5.SelectedCells != null) {
                dbConnector aConnector = new dbConnector();
                String discountValue = flatNumeric1.Value.ToString();
                String trip_fare = dataGridView5.CurrentRow.Cells[1].Value.ToString();
                String trip_id = dataGridView5.CurrentRow.Cells[0].Value.ToString();
                aConnector.callDiscountProcedure(discountValue, trip_fare, trip_id);
                flatButton18_Click(sender, e);
            }
        }

        private void flatButton18_Click(object sender, EventArgs e) {
            dbConnector aConnector = new dbConnector();
            DataSet ds = aConnector.getCustomTable("Trip");
            dataGridView5.DataSource = ds.Tables[0];
            dataGridView5.Refresh();
        }




    }
}
