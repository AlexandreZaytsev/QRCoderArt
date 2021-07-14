using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QRCoderArt
{
    public partial class FormDictionary : Form
    {
        //        public Dictionary<String, String> property;// = new Dictionary<String, String>();
        private string cntrlName;
        private string cntrlParentName;

        public FormDictionary()
        {
            CallBack_GetParam.callbackEventHandler = new CallBack_GetParam.callbackEvent(this.CallbackReload);    //subscribe (listen) to the general notification
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Dictionary<String, String> property = new Dictionary<String, String>();
            property.Clear();
            for (int i = 0; i < dataGridViewProperty.RowCount; i++)
            {
                if (dataGridViewProperty[0, i].Value != null && dataGridViewProperty[1, i].Value != null)
                {
                    if (!property.ContainsKey(dataGridViewProperty[0, i].Value.ToString()))
                    {
                        property.Add(dataGridViewProperty[0, i].Value.ToString(), dataGridViewProperty[1, i].Value.ToString());
                    }
                }
            }
            CallBack_SetParam.callbackEventHandler(cntrlName, cntrlParentName, property);  //send a general notification
            this.Close();
        }
        /*-----------------------------------------------------------------------------------------------------------------------------------------------
                CALLBACK return
        ------------------------------------------------------------------------------------------------------------------------------------------------*/
        private void CallbackReload(string controlName, string cotrolParentName, Dictionary<String, String> param)
        {
            cntrlName = controlName;
            cntrlParentName = cotrolParentName;
            //!!!param -readonly

            //            dataGridViewProperty.DataSource = (from t in param select new { t.Key, t.Value }).ToList();
            var pairs = from t in param select new { t.Key, t.Value };
            foreach (var pair in pairs)
            {
                dataGridViewProperty.Rows.Add(pair.Key, pair.Value);
            }
            //dataGridViewProperty.Dock = DockStyle.Fill;            // Set up the DataGridView. 
            //dataGridViewProperty.AutoGenerateColumns = true;       // Automatically generate the DataGridView columns.
            //dataGridViewProperty.Update();
            //dataGridViewProperty resize the visible rows.
            //dataGridViewProperty.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            //dataGridViewProperty.EditMode = DataGridViewEditMode.EditOnEnter;  // Put the cells in edit mode when user enters them.
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
