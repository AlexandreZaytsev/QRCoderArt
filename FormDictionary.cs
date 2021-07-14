// ***********************************************************************
// Assembly         : QRCoderArt
// Author           : zaytsev
// Created          : 07-14-2021
//
// Last Modified By : zaytsev
// Last Modified On : 07-14-2021
// ***********************************************************************
// <copyright file="FormDictionary.cs" company="">
//     MIT ©  2021
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QRCoderArt
{
    /// <summary>
    /// Class FormDictionary.
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDictionary : Form
    {
        //        public Dictionary<String, String> property;// = new Dictionary<String, String>();
        /// <summary>
        /// The CNTRL name
        /// </summary>
        private string cntrlName;
        /// <summary>
        /// The CNTRL parent name
        /// </summary>
        private string cntrlParentName;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormDictionary" /> class.
        /// </summary>
        public FormDictionary()
        {
            CallBack_GetParam.callbackEventHandler = new CallBack_GetParam.callbackEvent(this.CallbackReload);    //subscribe (listen) to the general notification
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the Button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
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
        /// <summary>
        /// Callbacks the reload.
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="cotrolParentName">Name of the cotrol parent.</param>
        /// <param name="param">The parameter.</param>
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
    /// <summary>
    /// Class CallBack_SetParam.
    /// </summary>
    public static class CallBack_SetParam
    {
        /// <summary>
        /// Delegate callbackEvent
        /// </summary>
        /// <param name="controlName">Name of the control.</param>
        /// <param name="controlParentName">Name of the control parent.</param>
        /// <param name="par">The par.</param>
        public delegate void callbackEvent(string controlName, string controlParentName, Dictionary<String, String> par);
        /// <summary>
        /// The callback event handler
        /// </summary>
        public static callbackEvent callbackEventHandler;
    }
}
