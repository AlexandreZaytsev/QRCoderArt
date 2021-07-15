// ***********************************************************************
// Assembly         : QRCoderArt
// Author           : NigelThorne
// Created          : 07-09-2021
//
// Last Modified By : zaytsev
// Last Modified On : 07-14-2021
// ***********************************************************************
// <copyright file="ArtQRCode.cs" company="">
//     MIT ©  2021
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Drawing;
using System.Drawing.Drawing2D;

// pull request raised to extend library used. 
namespace QRCoder
{
    /// <summary>
    /// Class ArtQRCode.
    /// Implements the <see cref="QRCoder.AbstractQRCode" />
    /// Implements the <see cref="System.IDisposable" />
    /// </summary>
    /// <seealso cref="QRCoder.AbstractQRCode" />
    /// <seealso cref="System.IDisposable" />
    public class ArtQRCode : AbstractQRCode, IDisposable
    {
        /// <summary>
        /// Constructor without params to be used in COM Objects connections
        /// </summary>
        public ArtQRCode() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArtQRCode" /> class.
        /// </summary>
        /// <param name="data">The data.</param>
        public ArtQRCode(QRCodeData data) : base(data) { }

        /// <summary>
        /// Gets the graphic.
        /// </summary>
        /// <param name="pixelsPerModule">The pixels per module.</param>
        /// <returns>Bitmap.</returns>
        public Bitmap GetGraphic(int pixelsPerModule)
        {
            return this.GetGraphic(pixelsPerModule, (pixelsPerModule * 8) / 10, Color.Black, Color.White);
        }

        /// <summary>
        /// Gets the graphic.
        /// </summary>
        /// <param name="pixelSize">Size of the pixel.</param>
        /// <param name="darkColor">Color of the dark.</param>
        /// <param name="lightColor">Color of the light.</param>
        /// <param name="backgroundImage">The background image.</param>
        /// <returns>Bitmap.</returns>
        public Bitmap GetGraphic(int pixelSize, Color darkColor, Color lightColor, Bitmap backgroundImage = null)
        {
            //            return this.GetGraphic(10, 7, Color.Black, Color.White, backgroundImage: backgroundImage);
            return this.GetGraphic(10, pixelSize, darkColor, lightColor, backgroundImage: backgroundImage);
        }

        /// <summary>
        /// Gets the graphic.
        /// </summary>
        /// <param name="pixelsPerModule">The pixels per module.</param>
        /// <param name="pixelSize">Size of the pixel.</param>
        /// <param name="darkColor">Color of the dark.</param>
        /// <param name="lightColor">Color of the light.</param>
        /// <param name="drawQuietZones">if set to <c>true</c> [draw quiet zones].</param>
        /// <param name="reticleImage">The reticle image.</param>
        /// <param name="backgroundImage">The background image.</param>
        /// <returns>Bitmap.</returns>
        public Bitmap GetGraphic(
            int pixelsPerModule,
            int pixelSize,
            Color darkColor,
            Color lightColor,
            bool drawQuietZones = false,
            Bitmap reticleImage = null,
            Bitmap backgroundImage = null)
        {
            int numModules = this.QrCodeData.ModuleMatrix.Count - (drawQuietZones ? 0 : 8);
            var offset = (drawQuietZones ? 0 : 4);
            int size = numModules * pixelsPerModule;
            var moduleMargin = pixelsPerModule - pixelSize;

            Bitmap bitmap = backgroundImage ?? new Bitmap(size, size);
            bitmap = Resize(bitmap, size);


            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                using (SolidBrush lightBrush = new SolidBrush(lightColor))
                {
                    using (SolidBrush darkBrush = new SolidBrush(darkColor))
                    {
                        // make background transparent if you don't have an image
                        if (backgroundImage == null)
                        {
                            using (var brush = new SolidBrush(Color.Transparent))
                            {
                                graphics.FillRectangle(brush, new Rectangle(0, 0, size, size));
                            }
                        }

                        var darkModulePixel = MakeDotPixel(pixelsPerModule, pixelSize, darkBrush);
                        var lightModulePixel = MakeDotPixel(pixelsPerModule, pixelSize, lightBrush);

                        for (int x = 0; x < numModules; x += 1)
                        {
                            for (int y = 0; y < numModules; y += 1)
                            {
                                var rectangleF = new Rectangle(x * pixelsPerModule, y * pixelsPerModule, pixelsPerModule, pixelsPerModule);

                                var pixelIsDark = this.QrCodeData.ModuleMatrix[offset + y][offset + x];
                                var solidBrush = pixelIsDark ? darkBrush : lightBrush;
                                var pixelImage = pixelIsDark ? darkModulePixel : lightModulePixel;

                                if (!IsPartOfReticle(x, y, numModules, offset))
                                    graphics.DrawImage(pixelImage, rectangleF);
                                else if (reticleImage == null)
                                    graphics.FillRectangle(solidBrush, rectangleF);
                            }
                        }

                        if (reticleImage != null)
                        {
                            var reticleSize = 7 * pixelsPerModule;
                            graphics.DrawImage(reticleImage, new Rectangle(0, 0, reticleSize, reticleSize));
                            graphics.DrawImage(reticleImage, new Rectangle(size - reticleSize, 0, reticleSize, reticleSize));
                            graphics.DrawImage(reticleImage, new Rectangle(0, size - reticleSize, reticleSize, reticleSize));
                        }

                        _ = graphics.Save();
                    }
                }
            }
            return bitmap;
        }

        /// <summary>
        /// If the pixelSize is bigger than the pixelsPerModule or may end up filling the Module making a traditional QR code.
        /// </summary>
        /// <param name="pixelsPerModule">The pixels per module.</param>
        /// <param name="pixelSize">Size of the pixel.</param>
        /// <param name="brush">The brush.</param>
        /// <returns>Bitmap.</returns>
        private Bitmap MakeDotPixel(int pixelsPerModule, int pixelSize, SolidBrush brush)
        {
            // draw a dot
            var bitmap = new Bitmap(pixelSize, pixelSize);
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillEllipse(brush, new Rectangle(0, 0, pixelSize, pixelSize));
                _ = graphics.Save();
            }

            var pixelWidth = Math.Min(pixelsPerModule, pixelSize);
            var margin = Math.Max((pixelsPerModule - pixelWidth) / 2, 0);

            // center the dot in the module and crop to stay the right size.
            var cropped = new Bitmap(pixelsPerModule, pixelsPerModule);
            using (var graphics = Graphics.FromImage(cropped))
            {
                graphics.DrawImage(bitmap, new Rectangle(margin, margin, pixelWidth, pixelWidth),
                    new RectangleF(((float)pixelSize - pixelWidth) / 2, ((float)pixelSize - pixelWidth) / 2, pixelWidth, pixelWidth),
                    GraphicsUnit.Pixel);
                _ = graphics.Save();
            }

            return cropped;
        }

        /// <summary>
        /// Determines whether [is part of reticle] [the specified x].
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="numModules">The number modules.</param>
        /// <param name="offset">The offset.</param>
        /// <returns><c>true</c> if [is part of reticle] [the specified x]; otherwise, <c>false</c>.</returns>
        private bool IsPartOfReticle(int x, int y, int numModules, int offset)
        {
            var cornerSize = 11 - offset;
            return
                (x < cornerSize && y < cornerSize) ||
                (x > (numModules - cornerSize - 1) && y < cornerSize) ||
                (x < cornerSize && y > (numModules - cornerSize - 1));
        }

        /// <summary>
        /// Resize to a square bitmap, but maintain the aspect ratio by padding transparently.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="newSize">The new size.</param>
        /// <returns>Bitmap.</returns>
        private Bitmap Resize(Bitmap image, int newSize)
        {
            float scale = Math.Min((float)newSize / image.Width, (float)newSize / image.Height);
            var scaledWidth = (int)(image.Width * scale);
            var scaledHeight = (int)(image.Height * scale);
            var offsetX = (newSize - scaledWidth) / 2;
            var offsetY = (newSize - scaledHeight) / 2;

            var scaledImage = new Bitmap(image, new Size(scaledWidth, scaledHeight));

            var bm = new Bitmap(newSize, newSize);

            using (Graphics graphics = Graphics.FromImage(bm))
            {
                using (var brush = new SolidBrush(Color.Transparent))
                {
                    graphics.FillRectangle(brush, new Rectangle(0, 0, newSize, newSize));

                    graphics.InterpolationMode = InterpolationMode.High;
                    graphics.CompositingQuality = CompositingQuality.HighQuality;
                    graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    graphics.DrawImage(scaledImage, new Rectangle(offsetX, offsetY, scaledWidth, scaledHeight));
                }
            }

            return bm;
        }
    }
}

//#endif