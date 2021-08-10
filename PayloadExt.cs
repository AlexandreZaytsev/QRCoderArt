using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
//using System.Threading.Tasks;
using QRCoder;
//using System.ComponentModel.DataAnnotations;

namespace QRCoderArt
{
    class PayloadExt
    {
        /// <summary>
        /// Generates a RussiaPaymentOrder payload. Standards of financial transactions. Two-dimensional barcode symbols for payments by individuals.
        /// </summary>
        //ГОСТ Р 56042-2014
        //https://docs.cntd.ru/document/1200110981
        //https://sbqr.ru/standard/files/standart.pdf
        public class RussiaPaymentOrder : PayloadGenerator.Payload
        {
            //base
            private characterSets CharacterSets;
            //            [Required(ErrorMessage = "Name must be a filled string max. 160 characters", AllowEmptyStrings = true)]
            private readonly string Name;// { get; set; }       // 1-160 char
            private readonly string PersonalAcc;                // 20 digit (UInt64)               
            private readonly string BankName;                   // 1-45 char
            private readonly string BIC;                        // 9 digit (UInt32)
            private readonly string CorrespAcc = "0";             // up to 20 digit (0-default) (UInt64)
            //extend
            private readonly string Sum;
            private readonly string Purpose;
            private readonly string PayeeINN;
            private readonly string PayerINN;
            private readonly string DrawerStatus;
            private readonly string KPP;
            private readonly string CBC;
            private readonly string OKTMO;
            private readonly string PaytReason;
            private readonly string ТaxPeriod;
            private readonly string DocNo;
            private readonly DateTime? DocDate;
            private readonly string TaxPaytKind;
            //other
            private readonly string LastName;
            private readonly string FirstName;
            private readonly string MiddleName;
            private readonly string PayerAddress;
            private readonly string PersonalAccount;
            private readonly string DocIdx;
            private readonly string PensAcc;
            private readonly string Contract;
            private readonly string PersAcc;
            private readonly string Flat;
            private readonly string Phone;
            private readonly string PayerIdType;
            private readonly string PayerIdNum;
            private readonly string ChildFio;
            private readonly DateTime? BirthDate;
            private readonly string PaymTerm;
            private readonly string PaymPeriod;
            private readonly string Category;
            private readonly string ServiceName;
            private readonly string CounterId;
            private readonly string CounterVal;
            private readonly string QuittId;
            private readonly DateTime? QuittDate;
            private readonly string InstNum;
            private readonly string ClassNum;
            private readonly string SpecFio;
            private readonly string AddAmount;
            private readonly string RuleId;
            private readonly string ExecId;
            private readonly string RegType;
            private readonly string UIN;
            private readonly techCode TechCode;

            public RussiaPaymentOrder(characterSets CharacterSets, string Name, string PersonalAcc, string BankName, string BIC, string CorrespAcc = "0")
            {
                this.CharacterSets = CharacterSets;
                this.Name = Name;
                this.PersonalAcc = PersonalAcc;
                this.BankName = BankName;
                this.BIC = BIC;
                this.CorrespAcc = CorrespAcc;
            }
            public RussiaPaymentOrder(characterSets CharacterSets, string Name, string PersonalAcc, string BankName, string BIC, string CorrespAcc = "0",
                                      string PayeeINN="", string LastName="", string FirstName="", string MiddleName="", string Purpose="", string PayerAddress="", string Sum="0")
            {
                this.CharacterSets = CharacterSets;
                this.Name = Name;
                this.PersonalAcc = PersonalAcc;
                this.BankName = BankName;
                this.BIC = BIC;
                this.CorrespAcc = CorrespAcc;

                if (!string.IsNullOrEmpty(PayeeINN) && PayeeINN.Length <= 12 && !Regex.IsMatch(PayeeINN.Replace(" ", ""), @"^[0-9]+$"))
                    throw new RussiaPaymentOrderException("PayeeINN must be a filled 1-10(12) digits.");

                this.PayeeINN = PayeeINN;
                this.LastName = LastName;
                this.FirstName = FirstName;
                this.MiddleName = MiddleName;
                this.Purpose = Purpose;
                this.PayerAddress = PayerAddress;
                this.Sum = Sum;

                /*
                if (string.IsNullOrEmpty(Name))
                    throw new RussiaPaymentOrderException("Name must be a filled string max. 160 characters.");
                if (string.IsNullOrEmpty(PersonalAcc))
                    throw new RussiaPaymentOrderException("PersonalAcc must be a filled string max. 20 characters.");
                if (string.IsNullOrEmpty(BankName))
                    throw new RussiaPaymentOrderException("BankName must be a filled string max. 45 characters.");
                if (string.IsNullOrEmpty(BIC))
                    throw new RussiaPaymentOrderException("BIC must be a filled string max. 9 characters.");
                if (string.IsNullOrEmpty(CorrespAcc))
                    throw new RussiaPaymentOrderException("CorrespAcc must be a filled string max. 20 characters.");
*/

            }

            public override string ToString()
            {
                if (string.IsNullOrEmpty(Name) && PersonalAcc.Length <= 160)
                    throw new Exception("Name must be a filled string 1-160 characters");
                if (!(!string.IsNullOrEmpty(PersonalAcc) && PersonalAcc.Length == 20 && Regex.IsMatch(PersonalAcc.Replace(" ", ""), @"^[0-9]+$")))
                    throw new Exception("PersonalAcc must be a filled strong 20 digits");
                if (string.IsNullOrEmpty(BankName) && BankName.Length <= 45)
                    throw new Exception("BankName must be a filled string 1-45 characters");
                if (!(!string.IsNullOrEmpty(BIC) && BIC.Length == 9 && Regex.IsMatch(BIC.Replace(" ", ""), @"^[0-9]+$")))
                    throw new Exception("BIC must be a filled strong 9 digits");
                if (!(!string.IsNullOrEmpty(CorrespAcc) && CorrespAcc.Length <= 20 && Regex.IsMatch(CorrespAcc.Replace(" ", ""), @"^[0-9]+$")))
                    throw new Exception("CorrespAcc must be a filled 1-20 digits or 0 value if empty");

                string ret = $"ST0001" + ((int)this.CharacterSets).ToString() + $"|Name={this.Name}" +
                    $"|PersonalAcc={this.PersonalAcc}" +
                    $"|BankName={this.BankName}" +
                    $"|BIC={this.BIC}" +
                    $"|CorrespAcc={this.CorrespAcc}" +
                    $"|PayeeINN={this.PayeeINN}" +
                    $"|LastName={this.LastName}" +
                    $"|FirstName={this.FirstName}" +
                    $"|MiddleName={this.MiddleName}" +
                    $"|Purpose={this.Purpose}" +
                    $"|PayerAddress={this.PayerAddress}" +
                    $"|Sum={this.Sum}"
                    ;

                string page = this.CharacterSets.ToString().Replace("_", "-");
                return Encoding.GetEncoding(page).GetString(Encoding.Convert(Encoding.Default, Encoding.GetEncoding(page), Encoding.GetEncoding(page).GetBytes(ret)));
            }

            /// <summary>
            /// Перечень значений технического кода платежа
            /// </summary>
            public enum techCode
            {
                Мобильная_связь_стационарный_телефон = 01,
                Коммунальные_услуги_ЖКХAFN = 02,
                ГИБДД_налоги_пошлины_бюджетные_платежи = 03,
                Охранные_услуги = 04,
                Услуги_оказываемые_УФМС = 05,
                ПФР = 06,
                Погашение_кредитов = 07,
                Образовательные_учреждения = 08,
                Интернет_и_ТВ = 09,
                Электронные_деньги = 10,
                Отдых_и_путешествия = 11,
                Инвестиции_и_страхование = 12,
                Спорт_и_здоровье = 13,
                Благотворительные_и_общественные_организации = 14,
                Прочие_услуги = 15
            }

            public enum characterSets
            {
                windows_1251 = 1,       // Encoding.GetEncoding("windows-1251")
                utf_8 = 2,              // Encoding.UTF8                          
                koi8_r = 3              // Encoding.GetEncoding("koi8-r")

            }

            public class RussiaPaymentOrderException : Exception
            {
                public RussiaPaymentOrderException()
                {
                }
                public RussiaPaymentOrderException(string message)
                    : base(message)
                {
                }
                public RussiaPaymentOrderException(string message, Exception inner)
                    : base(message, inner)
                {
                }
            }

        }
    }
}
