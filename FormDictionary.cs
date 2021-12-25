using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QRCoderArt
{
    /// <summary>
    /// Class FormDictionary.
    /// Форма для заполнения пользовательских свойств имя-значение
    /// Implements the <see cref="System.Windows.Forms.Form" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FormDictionary : Form
    {
        /// <summary>
        /// имя CNTRL (используется в возврате)
        /// </summary>
        private string cntrlName;
        /// <summary>
        /// имя родителя CNTRL (используется в возврате)
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
        /// Прочитать и отправить текущую таблицу параметров
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

        /*--------------------------------------------------------------------------------------------  
            CALLBACK InPut (подписка на внешние сообщения)
        --------------------------------------------------------------------------------------------*/
        /// <summary>
        /// CallbackReload
        /// входящее асинхронное сообщение для подписанных слушателей с передачей текущих параметров
        /// </summary>
        /// <param name="controlName">имя CTRL</param>
        /// <param name="cotrolParentName">имя родителя CNTRL</param>
        /// <param name="parameterPairs">параметры ключ-значение</param>
        private void CallbackReload(string controlName, string cotrolParentName, Dictionary<String, String> parameterPairs)
        {
            cntrlName = controlName;
            cntrlParentName = cotrolParentName;

            //!!!param -readonly
            //dataGridViewProperty.DataSource = (from t in param select new { t.Key, t.Value }).ToList();

            var pairs = from t in parameterPairs select new { t.Key, t.Value };
            foreach (var pair in pairs)
            {
                dataGridViewProperty.Rows.Add(pair.Key, pair.Value);    //r/w - ok
            }
        }
    }

    /*--------------------------------------------------------------------------------------------  
        CALLBACK OutPut (собственные сообщения)
    --------------------------------------------------------------------------------------------*/
    //general notification
    /// <summary>
    /// Class CallBack_SetParam.
    /// исходящее асинхронное сообщение для подписанных слушателей с передачей текущих параметров 
    /// </summary>
    public static class CallBack_SetParam
    {
        /// <summary>
        /// Delegate callbackEvent
        /// </summary>
        /// <param name="controlName">имя CTRL</param>
        /// <param name="controlParentName">имя родителя CNTRL</param>
        /// <param name="parameterPairs">параметры ключ-значение</param>
        public delegate void callbackEvent(string controlName, string controlParentName, Dictionary<String, String> parameterPairs);
        /// <summary>
        /// The callback event handler
        /// </summary>
        public static callbackEvent callbackEventHandler;
    }
}
