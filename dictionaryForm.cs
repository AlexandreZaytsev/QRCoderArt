using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRCoderArt
{
    public partial class dictionaryForm : Form
    {
        public Dictionary<String, String> property;// = new Dictionary<String, String>();
        //        public Dictionary<String, String> property;
        private string cntrlName;
        private string cntrlParentName;

        public dictionaryForm()
        {
            CallBack_GetParam.callbackEventHandler = new CallBack_GetParam.callbackEvent(this.callbackReload);    //subscribe (listen) to the general notification
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<String, String> property = new Dictionary<String, String>();
            property.Clear();
            for (int i = 0; i < dataGridView2.RowCount; i++)
            {
                if (dataGridView2[0, i].Value !=null && dataGridView2[1, i].Value != null) 
                {
                    if (!property.ContainsKey(dataGridView2[0, i].Value.ToString())) 
                    {
                        property.Add(dataGridView2[0, i].Value.ToString(), dataGridView2[1, i].Value.ToString());
                    }
                }
            }
            CallBack_SetParam.callbackEventHandler(cntrlName, cntrlParentName, property);  //send a general notification
            this.Close();
        }
       /*-----------------------------------------------------------------------------------------------------------------------------------------------
               CALLBACK return
       ------------------------------------------------------------------------------------------------------------------------------------------------*/
        private void callbackReload(string controlName,string cotrolParentName, Dictionary<String, String> param)
        {
            cntrlName = controlName;
            cntrlParentName = cotrolParentName;

            property = new Dictionary<string, string>(param);

            //   Dictionary<String, String> param

            dataGridView1.Dock = DockStyle.Fill;            // Set up the DataGridView. 
            dataGridView1.AutoGenerateColumns = true;       // Automatically generate the DataGridView columns.
                                                            //            bindingSource1.DataSource = param.Cast<BindingSource>();//.Where(some linq query); ;              // Set up the data source.
                                                            //            dataGridView1.DataSource = bindingSource1;
                                                            //            dataGridView1.DataSource = (from entry in param 
                                                            //                               //         orderby entry.Key
                                                            //                                        select new { entry.Key, entry.Value }).ToList();
                                                            //      dataGridView1.DataSource = (from entry in param select entry).ToList();



            dataGridView1.DataSource = property.ToArray();
            dataGridView1.Update();
       //     dataGridView1.Rows[1].ReadOnly = false;

            //        dataGridView1.Columns[0].ReadOnly = false;
            //        dataGridView1.Columns[1].ReadOnly = false;

            // Automatically resize the visible rows.
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
//            dataGridView1.BorderStyle = BorderStyle.Fixed3D;    // Set the DataGridView control's border.        
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;  // Put the cells in edit mode when user enters them.


/*

            dataGridView1.DataSource = param;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.Update();
     //       dataGridView1.b
*/
        }
    }
    /*-----------------------------------------------------------------------------------------------------------------------------------------------
           CALLBACK
   ------------------------------------------------------------------------------------------------------------------------------------------------*/
    //general notification
    public static class CallBack_SetParam
    {
        public delegate void callbackEvent(string controlName, string controlParentName, Dictionary<String, String> par);
        public static callbackEvent callbackEventHandler;
    }
}
