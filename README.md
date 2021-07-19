# QRCoderArt
Demo project WinForm application - creating a file with a picture of QR code based on QRCoder (1.4.1). With the possibility of creating a label (top layer) or background picture (substrate) based on ArtQRCode.cs. Payload Interface - QRCoder.dll Dynamic (Reflection)

- QR Code Picture Generator QRCoder.dll (base 1.4.1) + QRCoderDemo (Raffael Herrmann)
  (https://github.com/codebude/QRCoder) 
- file ArtQRCode.cs background picture (NigelThorne)
  (https://github.com/codebude/QRCoder/pull/295)
  (https://github.com/codebude/QRCoder/tree/91839cfe9c445832a61a993893eccfab9e264ee8/QRCoder) 

idea:
- read the parameters of the payload designer (attribute [obsolete] - checked)
- dynamically create parameter type controls
- If control values change - > initialize payload constructor parameters with type mapping

_Windows/collected under the .NET Framework 4.0_  
_Uses dynamic creation of the payload interface (one dynamic panel for all views) (without saving parameters) reflection QRCoder.dll_
***
1. Select payload
2. Select construction
3. If there are no errors, a QR picture of the code will be created
3. If there are errors, follow the troubleshooting guidelines = Complete the settings  

![QRCoderArtInfo](https://user-images.githubusercontent.com/16114000/124744118-168d2700-df27-11eb-9cea-66fa37da21a5.png)
![payload](https://user-images.githubusercontent.com/16114000/125486591-23b995d2-3a1e-44b3-bf25-cbca1393af51.png)
***
ps.  
current state 19.07.2021 ([+] ok; [ ] trying to implement)  
pps.  
all payloads of the version are supported qrcoder.dll (1.4.1) - but there are very few default values or restrictions on the ranges of values through attributes  
ppps.  
I tried to use clean code - but a beginner in c#, you can definitely write better and easier ))  

- [x] WiFi
  - (string ssid, string password, Authentication authenticationMode, bool isHiddenSSID = false)
- [x] Mail 
  - (string mailReceiver, MailEncoding encoding = MailEncoding.MAILTO)
  - (string mailReceiver, string subject, MailEncoding encoding = MailEncoding.MAILTO)
  - (string mailReceiver, string subject, string message, MailEncoding encoding = MailEncoding.MAILTO)
- [x] SMS
  - (string number, SMSEncoding encoding = SMSEncoding.SMS)
  - (string number, string subject, SMSEncoding encoding = SMSEncoding.SMS)
- [x] MMS
  - (string number, MMSEncoding encoding = MMSEncoding.MMS)
  - (string number, string subject, MMSEncoding encoding = MMSEncoding.MMS)
- [x] Geolocation
  - (string latitude, string longitude, GeolocationEncoding encoding = GeolocationEncoding.GEO)
- [x] PhoneNumber
  - (string number)
- [x] SkypeCall
  - (string skypeUsername)
- [x] Url
  - (string url)
- [x] WhatsAppMessage
  - (string number, string message)
  - (string message)
- [x] Bookmark
  - (string url, string title)
- [x] ContactData
  - (ContactOutputType outputType, string firstname, string lastname, string nickname = null, string phone = null, string mobilePhone = null, string workPhone = null, string email = null, DateTime? birthday = null, string website = null, string street = null, string houseNumber = null, string city = null, string zipCode = null, string country = null, string note = null, string stateRegion = null, AddressOrder addressOrder = AddressOrder.Default, string org = null)
- [x] BitcoinLikeCryptoCurrencyAddress
  - (BitcoinLikeCryptoCurrencyType currencyType, string address, double? amount, string label = null, string message = null)
- [x] SwissQrCode 
  - (Iban iban, Currency currency, Contact creditor, Reference reference, AdditionalInformation additionalInformation = null, Contact debitor = null, decimal? amount = null, DateTime? requestedDateOfPayment = null, Contact ultimateCreditor = null, string alternativeProcedure1 = null, string alternativeProcedure2 = null)  
***it is necessary to set the values of the payload parameters (see the html error message window)***
- [x] Girocode
  - (string iban, string bic, string name, decimal amount, string remittanceInformation = "", TypeOfRemittance typeOfRemittance = TypeOfRemittance.Unstructured, string purposeOfCreditTransfer = "", string messageToGirocodeUser = "", GirocodeVersion version = GirocodeVersion.Version1, GirocodeEncoding encoding = GirocodeEncoding.ISO_8859_1)   
  ***it is necessary to set the values of the payload parameters (see the html error message window)***
- [x] BezahlCode
  - (AuthorityType authority, string name, string account = "", string bnc = "", string iban = "", string bic = "", string reason = "") : this(authority, name, account, bnc, iban, bic, 0, string.Empty, 0, null, null, string.Empty, string.Empty, null, reason, 0, string.Empty, Currency.EUR, null, 1)     
  ***it is necessary to set the values of the payload parameters (see the html error message window)***
  - (AuthorityType authority, string name, string account, string bnc, decimal amount, string periodicTimeunit = "", int periodicTimeunitRotation = 0, DateTime? periodicFirstExecutionDate = null, DateTime? periodicLastExecutionDate = null, string reason = "", int postingKey = 0, Currency currency = Currency.EUR, DateTime? executionDate = null) : this(authority, name, account, bnc, string.Empty, string.Empty, amount, periodicTimeunit, periodicTimeunitRotation, periodicFirstExecutionDate, periodicLastExecutionDate, string.Empty, string.Empty, null, reason, postingKey, string.Empty, currency, executionDate, 2)     
***it is necessary to set the values of the payload parameters (see the html error message window)***
  - (AuthorityType authority, string name, string iban, string bic, decimal amount, string periodicTimeunit = "", int periodicTimeunitRotation = 0, DateTime? periodicFirstExecutionDate = null, DateTime? periodicLastExecutionDate = null, string creditorId = "", string mandateId = "", DateTime? dateOfSignature = null, string reason = "", string sepaReference = "", Currency currency = Currency.EUR, DateTime? executionDate = null) : this(authority, name, string.Empty, string.Empty, iban, bic, amount, periodicTimeunit, periodicTimeunitRotation, periodicFirstExecutionDate, periodicLastExecutionDate, creditorId, mandateId, dateOfSignature, reason, 0, sepaReference, currency, executionDate, 3)   
  ***it is necessary to set the values of the payload parameters (see the html error message window)***
  - (AuthorityType authority, string name, string account, string bnc, string iban, string bic, decimal amount, string periodicTimeunit = "", int periodicTimeunitRotation = 0, DateTime? periodicFirstExecutionDate = null, DateTime? periodicLastExecutionDate = null, string creditorId = "", string mandateId = "", DateTime? dateOfSignature = null, string reason = "", int postingKey = 0, string sepaReference = "", Currency currency = Currency.EUR, DateTime? executionDate = null, int internalMode = 0)     
***it is necessary to set the values of the payload parameters (see the html error message window)***
- [x] CalendarEvent
  - (string subject, string description, string location, DateTime start, DateTime end, bool allDayEvent, EventEncoding encoding = EventEncoding.Universal)
- [x] OneTimePassword
  - the constructor is not used here     
***it is necessary to set the values of the payload parameters (see the html error message window)***
- [x] ShadowSocksConfig
  - (string hostname, int port, string password, Method method, string tag = null)      
***it is necessary to set the values of the payload parameters (see the html error message window)***
  - (string hostname, int port, string password, Method method, string plugin, string pluginOption, string tag = null)  
***it is necessary to set the values of the payload parameters (see the html error message window)***
  - (string hostname, int port, string password, Method method, Dictionary<string, string> parameters, string tag = null)  
***it is necessary to set the values of the payload parameters (see the html error message window)***
- [x] MoneroTransaction
  - (string address, float? txAmount = null, string txPaymentId = null, string recipientName = null, string txDescription = null)  
***it is necessary to set the values of the payload parameters (see the html error message window)***
- [X] SlovenianUpnQr
  - (string payerName, string payerAddress, string payerPlace, string recipientName, string recipientAddress, string recipientPlace, string recipientIban, string description, double amount, string recipientSiModel = "SI00", string recipientSiReference = "", string code = "OTHR") 








