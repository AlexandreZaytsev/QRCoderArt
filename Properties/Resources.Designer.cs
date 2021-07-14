// ***********************************************************************
// Assembly         : QRCoderArt
// Author           : zaytsev
// Created          : 07-09-2021
//
// Last Modified By : zaytsev
// Last Modified On : 07-14-2021
// ***********************************************************************
// <copyright file="Resources.Designer.cs" company="">
//     MIT ©  2021
// </copyright>
// <summary></summary>
// ***********************************************************************

namespace QRCoderArt.Properties {
    using System;


    /// <summary>
    /// Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {

        /// <summary>
        /// The resource man
        /// </summary>
        private static global::System.Resources.ResourceManager resourceMan;

        /// <summary>
        /// The resource culture
        /// </summary>
        private static global::System.Globalization.CultureInfo resourceCulture;

        /// <summary>
        /// Initializes a new instance of the <see cref="Resources" /> class.
        /// </summary>
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }

        /// <summary>
        /// Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        /// <value>The resource manager.</value>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("QRCoderArt.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        /// Перезаписывает свойство CurrentUICulture текущего потока для всех
        /// обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        /// <value>The culture.</value>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        /// <summary>
        /// Ищет локализованную строку, похожую на Demo project WinForm application
        /// creating a file with a picture of a QR code based on QRCoder (1.4.1).
        /// - the ability to create a label (top layer)
        /// - or a background image (background) based on ArtQRCode.cs.
        /// using
        /// 1. Select payload
        /// 2. Select construction
        /// 3. Fill in the parameters
        /// Payload interface - dynamic (Reflection) QRCoder.dll
        /// QR code picture generator QRCoder 1.4.1 QRCoder.dll + QRCoderDemo
        /// (Raffael Herrmann)
        /// https://github.com/codebude/QRCoder
        /// ArtQRCode.cs file backgro [остаток строки не уместился]";.
        /// </summary>
        /// <value>The about string.</value>
        internal static string AboutString {
            get {
                return ResourceManager.GetString("AboutString", resourceCulture);
            }
        }

        /// <summary>
        /// Поиск локализованного ресурса типа System.Drawing.Icon, аналогичного (Значок).
        /// </summary>
        /// <value>The qr.</value>
        internal static System.Drawing.Icon qr {
            get {
                object obj = ResourceManager.GetObject("qr", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }

        /// <summary>
        /// Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        /// <value>The QR1.</value>
        internal static System.Drawing.Bitmap qr1 {
            get {
                object obj = ResourceManager.GetObject("qr1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }

        /// <summary>
        /// Поиск локализованного ресурса типа System.Drawing.Icon, аналогичного (Значок).
        /// </summary>
        /// <value>The ric.</value>
        internal static System.Drawing.Icon ric_ {
            get {
                object obj = ResourceManager.GetObject("ric_", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }

        /// <summary>
        /// Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        /// <value>The smart.</value>
        internal static System.Drawing.Bitmap smart {
            get {
                object obj = ResourceManager.GetObject("smart", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
