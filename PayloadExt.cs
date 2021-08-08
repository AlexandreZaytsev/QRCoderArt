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
        public class WiFi1 : PayloadGenerator.Payload
        {
            private readonly string ssid, password, authenticationMode;
            private readonly bool isHiddenSsid;

            /// <summary>
            /// Generates a WiFi payload. Scanned by a QR Code scanner app, the device will connect to the WiFi.
            /// </summary>
            /// <param name="ssid">SSID of the WiFi network</param>
            /// <param name="password">Password of the WiFi network</param>
            /// <param name="authenticationMode">Authentification mode (WEP, WPA, WPA2)</param>
            /// <param name="isHiddenSSID">Set flag, if the WiFi network hides its SSID</param>
            public WiFi1(string ssid, string password, Authentication authenticationMode, bool isHiddenSSID = false)
            {
                this.ssid = "";// EscapeInput(ssid);
                this.ssid = "";//isHexStyle(this.ssid) ? "\"" + this.ssid + "\"" : this.ssid;
                this.password = "";//EscapeInput(password);
                this.password = "";//isHexStyle(this.password) ? "\"" + this.password + "\"" : this.password;
                this.authenticationMode = authenticationMode.ToString();
                this.isHiddenSsid = isHiddenSSID;
            }

            public override string ToString()
            {
                return
                    $"WIFI:T:{this.authenticationMode};S:{this.ssid};P:{this.password};{(this.isHiddenSsid ? "H:true" : string.Empty)};";
            }

            public enum Authentication
            {
                WEP,
                WPA,
                nopass
            }
        }
    }
}
