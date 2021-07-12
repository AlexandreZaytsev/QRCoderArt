# QRCoderArt
Demo project WinForm приложение - создание файла с картинкой QR кода на базе QRCoder (1.4.1). С возможностью создания лейбла (верхний слой) или фоновой картинки (подложка) на основе ArtQRCode.cs. Интерфейс полезной нагрузки (Payload) - динамический (Reflection) QRCoder.dll 

- генератор картинки QR кода QRCoder 1.4.1 QRCoder.dll + QRCoderDemo (Raffael Herrmann)
  (https://github.com/codebude/QRCoder) 
- файл ArtQRCode.cs фоновая картика (NigelThorne)
  (https://github.com/codebude/QRCoder/pull/295)
  (https://github.com/codebude/QRCoder/tree/91839cfe9c445832a61a993893eccfab9e264ee8/QRCoder) 

идея: 
- прочитать параметрв коструктора -> динамически создать контролы по типу
- при изменении значений контролов -> инициализировать параметры конструктора с приведением типов

_Windows/собирал под  .NET Framework 4.0_  
_Использовано динамическое создание интерфейса полезной нагрузки payload (одна динамическая панель под все виды)(без сохранения параметров) reflection QRCoder.dll_
***
1. Выберите payload  
2. Выберите construction
3. Заполните параметры  

ps
пока не смог воспроизвести конструкторы с вложенными классами - не хватает знаний <br>
pps
если сможете помочь - будет - супер
 
![QRCoderArtInfo](https://user-images.githubusercontent.com/16114000/124744118-168d2700-df27-11eb-9cea-66fa37da21a5.png)

***
ps.
current state 07.07.2021 ([+] ok; [ ] trying to implement)

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
- [ ] SwissQrCode 
  - (Iban iban, Currency currency, Contact creditor, Reference reference, AdditionalInformation additionalInformation = null, Contact debitor = null, decimal? amount = null, DateTime? requestedDateOfPayment = null, Contact ultimateCreditor = null, string alternativeProcedure1 = null, string alternativeProcedure2 = null)  
***still in operation (several nested classes)***
- [ ] Girocode
  - (string iban, string bic, string name, decimal amount, string remittanceInformation = "", TypeOfRemittance typeOfRemittance = TypeOfRemittance.Unstructured, string purposeOfCreditTransfer = "", string messageToGirocodeUser = "", GirocodeVersion version = GirocodeVersion.Version1, GirocodeEncoding encoding = GirocodeEncoding.ISO_8859_1)
  ***still in work (not yet figured out)***
- [ ] BezahlCode
  - (AuthorityType authority, string name, string account = "", string bnc = "", string iban = "", string bic = "", string reason = "") : this(authority, name, account, bnc, iban, bic, 0, string.Empty, 0, null, null, string.Empty, string.Empty, null, reason, 0, string.Empty, Currency.EUR, null, 1)  
  ***still in work (not yet figured out)***
  - (AuthorityType authority, string name, string account, string bnc, decimal amount, string periodicTimeunit = "", int periodicTimeunitRotation = 0, DateTime? periodicFirstExecutionDate = null, DateTime? periodicLastExecutionDate = null, string reason = "", int postingKey = 0, Currency currency = Currency.EUR, DateTime? executionDate = null) : this(authority, name, account, bnc, string.Empty, string.Empty, amount, periodicTimeunit, periodicTimeunitRotation, periodicFirstExecutionDate, periodicLastExecutionDate, string.Empty, string.Empty, null, reason, postingKey, string.Empty, currency, executionDate, 2)  
***default variables (account, bnc, amount) are not specified***
  - (AuthorityType authority, string name, string iban, string bic, decimal amount, string periodicTimeunit = "", int periodicTimeunitRotation = 0, DateTime? periodicFirstExecutionDate = null, DateTime? periodicLastExecutionDate = null, string creditorId = "", string mandateId = "", DateTime? dateOfSignature = null, string reason = "", string sepaReference = "", Currency currency = Currency.EUR, DateTime? executionDate = null) : this(authority, name, string.Empty, string.Empty, iban, bic, amount, periodicTimeunit, periodicTimeunitRotation, periodicFirstExecutionDate, periodicLastExecutionDate, creditorId, mandateId, dateOfSignature, reason, 0, sepaReference, currency, executionDate, 3)
  ***still in work (not yet figured out)***
  - (AuthorityType authority, string name, string account, string bnc, string iban, string bic, decimal amount, string periodicTimeunit = "", int periodicTimeunitRotation = 0, DateTime? periodicFirstExecutionDate = null, DateTime? periodicLastExecutionDate = null, string creditorId = "", string mandateId = "", DateTime? dateOfSignature = null, string reason = "", int postingKey = 0, string sepaReference = "", Currency currency = Currency.EUR, DateTime? executionDate = null, int internalMode = 0)  
***default variables (account, bnc, amount) are not specified***
- [x] CalendarEvent
  - (string subject, string description, string location, DateTime start, DateTime end, bool allDayEvent, EventEncoding encoding = EventEncoding.Universal)
- [x] OneTimePassword
  - the constructor is not used here  
***default variables (secret, period) are not specified***
- [ ] ShadowSocksConfig
  - (string hostname, int port, string password, Method method, string tag = null)   
***default variables (port) are not specified***
  - (string hostname, int port, string password, Method method, string plugin, string pluginOption, string tag = null)  
***default variables (port) are not specified***
  - (string hostname, int port, string password, Method method, Dictionary<string, string> parameters, string tag = null)  
***still in work (not yet figured out)***
- [ ] MoneroTransaction
  - (string address, float? txAmount = null, string txPaymentId = null, string recipientName = null, string txDescription = null)  
***default variables (address) are not specified***
- [X] SlovenianUpnQr
  - (string payerName, string payerAddress, string payerPlace, string recipientName, string recipientAddress, string recipientPlace, string recipientIban, string description, double amount, string recipientSiModel = "SI00", string recipientSiReference = "", string code = "OTHR") 








