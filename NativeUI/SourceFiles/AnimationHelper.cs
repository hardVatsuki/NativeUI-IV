// NativeUI for Grand Theft Auto IV: The Complete Edition (1.2.0.59)
// Made by ItsClonkAndre, fork by hardVatsuki
// Version 1.0

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;

using GTA;

namespace NativeUI {
    /// <summary>
    /// Helper class for GIFs.
    /// </summary>
    internal class AnimationHelper : IDisposable {

        #region Variables
        private List<Texture> images;
        private Image targetImage;
        private FrameDimension targetImageDimension;
        private int targetImageFrames;
        private int frameRate = 0;

        private Task task;
        private CancellationTokenSource cancellationTokenSource;
        private int index = 0;

        private bool disposed;
        #endregion

        #region Methods
        protected internal static byte[] BitmapToByte(Bitmap bmp)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
                bmp.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
        }
        #endregion

        #region Events
        protected internal delegate void AnimatedTextureReturnerDelegate(Texture texture);
        protected internal event AnimatedTextureReturnerDelegate AnimatedTextureReturner;
        #endregion

        #region Constructor
        protected internal AnimationHelper(Image image, int animatedBannerFrameRate)
        {
            images = new List<Texture>();
            targetImage = image;
            targetImageDimension = new FrameDimension(targetImage.FrameDimensionsList[0]);
            targetImageFrames = targetImage.GetFrameCount(targetImageDimension);
            frameRate = animatedBannerFrameRate;

            // Load images
            for (int i = 0; i < (targetImageFrames - 1); i++) {
                targetImage.SelectActiveFrame(targetImageDimension, i);
                using (Bitmap bmp = new Bitmap(targetImage)) {
                    images.Add(new Texture(BitmapToByte(bmp)));
                }
            }
        }
        #endregion

        protected internal Texture GetFirstFrameOfImage()
        {
            return images[0];
        }

        protected internal void StartLoopingGIF()
        {
            cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            index = 0;
            targetImage.SelectActiveFrame(targetImageDimension, index);

            task = Task.Run(() => {
                try {
                    while (!cancellationToken.IsCancellationRequested) {
                        // Looping through all frames
                        if ((images.Count - 1) != 0) {
                            AnimatedTextureReturner?.Invoke(images[index]);
                            if (index >= (images.Count - 1)) {
                                index = 0;
                            }
                            else {
                                index++;
                            }
                        }
                    
                        // Delay
                        int millisecondsTimeout = frameRate != 0 ? 1000 / frameRate : 1000 / 30;
                        Task.Delay(millisecondsTimeout, cancellationToken).Wait();
                    }
                }
                catch (OperationCanceledException) {}
                finally {}
            }, cancellationToken);
        }

        protected internal void StopLoopingGIF()
        {
            if (cancellationTokenSource != null) {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
        }

        public void Dispose()
        {
            if (disposed) { return; };
            disposed = true;

            if (task != null) { task = null; }

            if (images != null) {
                for (int i = 0; i < images.Count; i++) {
                    images[i]?.Dispose();
                }
                images.Clear();
            }

            if (targetImage != null) {
                targetImage?.Dispose();
                targetImage = null;
            }
        }

    }
}
