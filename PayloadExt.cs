using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;

namespace QRCoderArt
{
    class PayloadExt
    {
        //ГОСТ Р 56042-2014
        //https://docs.cntd.ru/document/1200110981
        //https://sbqr.ru/standard/files/standart.pdf
        public class RussiaPaymentOrder : PayloadGenerator.Payload
        {
            //base
            private readonly string Name;
            private readonly string PersonalAcc;
            private readonly string BankName;
            private readonly string BIC;
            private readonly string CorrespAcc;
            //extend
            private readonly string Sum;
            private readonly string Purpose, PayeeINN, PayerINN, DrawerStatus, KPP, CBC, OKTMO, PaytReason, ТaxPeriod, DocNo;
            private readonly DateTime? DocDate;
            private readonly string TaxPaytKind;
            //other
            private readonly string LastName, FirstName, MiddleName, PayerAddress, PersonalAccount, DocIdx, PensAcc, Contract, PersAcc, Flat, Phone, PayerIdType, PayerIdNum, ChildFio, BirthDate, PaymTerm, PaymPeriod, Category, ServiceName, CounterId, CounterVal, QuittId, QuittDate, InstNum, ClassNum, SpecFio, AddAmount, RuleId, ExecId, RegType, UIN, TechCode;


            private readonly string ssid, password, authenticationMode;
            private readonly bool isHiddenSsid;

            /// <summary>
            /// Generates a WiFi payload. Scanned by a QR Code scanner app, the device will connect to the WiFi.
            /// </summary>
            /// <param name="ssid">SSID of the WiFi network</param>
            /// <param name="password">Password of the WiFi network</param>
            /// <param name="authenticationMode">Authentification mode (WEP, WPA, WPA2)</param>
            /// <param name="isHiddenSSID">Set flag, if the WiFi network hides its SSID</param>
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
                this.PayerAddress = PayerAddress;
                this.Sum = Sum;
            }
//ST00011|Name=ООО «Три кита»|PersonalAcc=40702810138250123017|BankName=ОАО "БАНК"|BIC=044525225|CorrespAcc=30101810400000000225
//       |PayeeINN=6200098765|LastName = Иванов | FirstName = Иван | MiddleName = Иванович | Purpose = Оплата членского взноса
//       |PayerAddress=г.Рязань ул.Ленина д.10 кв.15|Sum= 100000
            public override string ToString()
            {
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
