using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
//            [Required(ErrorMessage = "Name must be a filled string max. 160 characters")]
            private readonly string Name;
            private readonly string PersonalAcc;
            private readonly string BankName;
            private readonly string BIC;
            private readonly string CorrespAcc;
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
            private readonly string TechCode;

            public RussiaPaymentOrder(string Name, string PersonalAcc, string BankName, string BIC, string CorrespAcc)
            {
                this.Name = Name;
                this.PersonalAcc = PersonalAcc;
                this.BankName = BankName;
                this.BIC = BIC;
                this.CorrespAcc = CorrespAcc;
            }
            public RussiaPaymentOrder(string Name, string PersonalAcc, string BankName, string BIC, string CorrespAcc, 
                                      string PayeeINN, string LastName, string FirstName, string MiddleName, string Purpose, string PayerAddress, string Sum)
            {
                this.Name = Name;
                this.PersonalAcc = PersonalAcc;
                this.BankName = BankName;
                this.BIC = BIC;
                this.CorrespAcc = CorrespAcc;
                this.PayeeINN = PayeeINN;
                this.LastName = LastName;
                this.FirstName = FirstName;
                this.MiddleName = MiddleName;
                this.Purpose = Purpose;
                this.PayerAddress = PayerAddress;
                this.Sum = Sum;
            }

            public override string ToString()
            {
                if (Name.Length == 0)
                    throw new Exception("Name must be a filled string max. 160 characters");
                if (PersonalAcc.Length == 0)
                    throw new Exception("PersonalAcc must be a filled string max. 20 characters");
                if (BankName.Length == 0)
                    throw new Exception("BankName must be a filled string max. 45 characters");
                if (BIC.Length == 0)
                    throw new Exception("BIC must be a filled string max. 9 characters");
                if (CorrespAcc.Length == 0)
                    throw new Exception("CorrespAcc must be a filled string max. 20 characters");

                return
                        $"ST00011|Name={this.Name}" +
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
            }

        }
    }
}
