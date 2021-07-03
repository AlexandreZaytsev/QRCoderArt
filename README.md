# QRCoderArt
Demo project WinForm приложение - создание файла с картинкой QR кода на базе QRCoder (1.4.1). С возможностью создания лейбла (верхний слой) или фоновой картинки (подложка) на основе ArtQRCode.cs. Интерфейс полезной нагрузки (Payload) - динамический (Reflection) QRCoder.dll 

- генератор картинки QR кода QRCoder 1.4.1 QRCoder.dll + QRCoderDemo (Raffael Herrmann)
- (https://github.com/codebude/QRCoder) 
- файл ArtQRCode.cs фоновая картика (NigelThorne)
- (https://github.com/codebude/QRCoder/pull/295)
- (https://github.com/codebude/QRCoder/tree/91839cfe9c445832a61a993893eccfab9e264ee8/QRCoder) 

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
пока не смог воспроизвести конструкторы с вложенными классами - не хватает знаний 
pps
если сможете помочь - будет - супер
  
![info](https://user-images.githubusercontent.com/16114000/124353245-17b01280-dc0e-11eb-8c93-0678d0f841b6.png)
