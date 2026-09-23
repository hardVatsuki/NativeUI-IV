// NativeUI for Grand Theft Auto IV: The Complete Edition (1.2.0.59)
// Made by ItsClonkAndre, fork by hardVatsuki
// Version 1.0

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

using GTA;

namespace NativeUI {
    
    public class Resource {

        #region Variables
        private static string basePath = Path.Combine(Game.InstallFolder, "scripts", typeof(Resource).Namespace);
        private static string imageNameMenuTitleImage = "MenuTitleImage.png";
        private static string iconNameMenuArrowUp = "MenuArrowUp.png";
        private static string iconNameMenuArrowDown = "MenuArrowDown.png";
        private static string iconNameCheckBoxChecked = "CheckBoxChecked.png";
        private static string iconNameCheckBoxUnchecked = "CheckBoxUnchecked.png";
        private static string iconNameListArrowLeft = "ListArrowLeft.png";
        private static string iconNameListArrowRight = "ListArrowRight.png";

        protected internal class MenuResource {

            protected internal static PointF position = new PointF(30f, 18f);
            protected internal static float widgth = 432f;

            protected internal static Keys upKey = Keys.NumPad8;
            protected internal static Keys downKey = Keys.NumPad2;
            protected internal static Keys leftKey = Keys.NumPad4;
            protected internal static Keys rightKey = Keys.NumPad6;
            protected internal static Keys acceptKey = Keys.NumPad5;
            protected internal static Keys backKey = Keys.NumPad0;

            protected internal static bool canControlCharacter = true;
            protected internal static bool enableControllerSupport = false;
            protected internal static bool enableMenuSounds = true;
            protected internal static bool infiniteScroll = true;

            protected internal static int titleImageFrameRate = 0;
            protected internal static int maxItemsVisibleAtOnce = 12;
            protected internal static PointF offset = new PointF(0f, 0f);
            protected internal static PointF titleTextOffset = new PointF(0f, 0f);

            protected internal static bool showTitle = true;
            protected internal static bool showDescription = true;
            protected internal static bool showArrows = true;
            protected internal static bool showGaps = true;

            protected internal static Color titleBackColor = Color.FromArgb(255, 0, 0, 0);
            protected internal static Texture titleImage = LoadTexture(basePath, imageNameMenuTitleImage);
            protected internal static Color titleTextColor = Color.FromArgb(255, 255, 255, 255);
            protected internal static GTA.Font titleTextFont = new GTA.Font("Calibri", 42f, FontScaling.Pixel, true, false) { Effect = FontEffect.None };
            protected internal static Color titleLineColor = Color.FromArgb(0, 0, 0, 0);

            protected internal static Color descriptionBackColor = Color.FromArgb(255, 0, 0, 0);
            protected internal static Color descriptionTextColor = Color.FromArgb(255, 45, 110, 184);
            protected internal static GTA.Font descriptionTextFont = new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None };
            protected internal static Color selectedIndexTextColor = Color.FromArgb(255, 45, 110, 184);
            protected internal static GTA.Font selectedIndexTextFont = new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None };

            protected internal static Color noItemsBackColor = Color.FromArgb(170, 10, 10, 10);
            protected internal static string noItemsText = "There are no items in this menu.";
            protected internal static Color noItemsTextColor = Color.FromArgb(255, 255, 255, 255);
            protected internal static GTA.Font noItemsTextFont = new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None };

            protected internal static Color arrowsUpDownBackColor = Color.FromArgb(225, 0, 0, 0);
            protected internal static (Texture Enabled, Texture Disabled) iconsArrowUp = (
                Enabled: LoadTextureWithColor(basePath, iconNameMenuArrowUp, Color.FromArgb(255, 255, 255, 255)),
                Disabled: LoadTextureWithColor(basePath, iconNameMenuArrowUp, Color.FromArgb(255, 150, 150, 150))
            );
            protected internal static (Texture Enabled, Texture Disabled) iconsArrowDown = (
                Enabled: LoadTextureWithColor(basePath, iconNameMenuArrowDown, Color.FromArgb(255, 255, 255, 255)),
                Disabled: LoadTextureWithColor(basePath, iconNameMenuArrowDown, Color.FromArgb(255, 150, 150, 150))
            );

        }

        protected internal class ItemResource {

            protected internal static float height = 38f;

            protected internal static bool showDescriptionLine = true;
            protected internal static bool showDescription = true;

            protected internal static ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) titleBackColors = (
                Enabled: (
                    Selected: Color.FromArgb(255, 245, 245, 245),
                    Unselected: Color.FromArgb(170, 10, 10, 10)
                ),
                Disabled: (
                    Selected: Color.FromArgb(255, 245, 245, 245), 
                    Unselected: Color.FromArgb(170, 10, 10, 10)
                )
            );
            protected internal static ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) titleTextColors = (
                Enabled: (
                    Selected: Color.FromArgb(255, 5, 5, 5),
                    Unselected: Color.FromArgb(255, 255, 255, 255)
                ),
                Disabled: (
                    Selected: Color.FromArgb(255, 150, 150, 150), 
                    Unselected: Color.FromArgb(255, 150, 150, 150)
                )
            );
            protected internal static ((GTA.Font Selected, GTA.Font Unselected) Enabled, (GTA.Font Selected, GTA.Font Unselected) Disabled) titleTextFonts = (
                Enabled: (
                    Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None },
                    Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None }
                ),
                Disabled: (
                    Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None },
                    Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None }
                )
            );

            protected internal static ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) descriptionLineColors = (
                Enabled: (
                    Selected: Color.FromArgb(255, 0, 0, 0),
                    Unselected: Color.FromArgb(255, 0, 0, 0)
                ),
                Disabled: (
                    Selected: Color.FromArgb(255, 0, 0, 0),
                    Unselected: Color.FromArgb(255, 0, 0, 0)
                )
            );
            protected internal static ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) descriptionBackColors = (
                Enabled: (
                    Selected: Color.FromArgb(100, 10, 10, 10),
                    Unselected: Color.FromArgb(100, 10, 10, 10)
                ),
                Disabled: (
                    Selected: Color.FromArgb(100, 10, 10, 10),
                    Unselected: Color.FromArgb(100, 10, 10, 10)
                )
            );
            protected internal static ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) descriptionTextColors = (
                Enabled: (
                    Selected: Color.FromArgb(255, 255, 255, 255),
                    Unselected: Color.FromArgb(255, 255, 255, 255)
                ),
                Disabled: (
                    Selected: Color.FromArgb(255, 255, 255, 255),
                    Unselected: Color.FromArgb(255, 255, 255, 255)
                )
            );
            protected internal static ((GTA.Font Selected, GTA.Font Unselected) Enabled, (GTA.Font Selected, GTA.Font Unselected) Disabled) descriptionTextFonts = (
                Enabled: (
                    Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None },
                    Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None }
                ),
                Disabled: (
                    Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None },
                    Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None }
                )
            );

            protected internal class Button {

                protected internal static bool icon = false;
                protected internal static ((Texture Selected, Texture Unselected) Enabled, (Texture Selected, Texture Unselected) Disabled) icons = (
                    Enabled: (
                        Selected: null,
                        Unselected: null
                    ),
                    Disabled: (
                        Selected: null,
                        Unselected: null
                    )
                );
                protected internal static Item.Button.IconLocations iconLocation = Item.Button.IconLocations.Left;

            }

            protected internal class CheckBox {

                protected internal static Item.CheckBox.ToggleMode mode = Item.CheckBox.ToggleMode.Icon;

                protected internal static (((Texture Checked, Texture Unchecked) Selected, (Texture Checked, Texture Unchecked) Unselected) Enabled, ((Texture Checked, Texture Unchecked) Selected, (Texture Checked, Texture Unchecked) Unselected) Disabled) icons = (
                    Enabled: (
                        Selected: (
                            Checked: LoadTextureWithColor(basePath, iconNameCheckBoxChecked, Color.FromArgb(255, 0, 0, 0)),
                            Unchecked: LoadTextureWithColor(basePath, iconNameCheckBoxUnchecked, Color.FromArgb(255, 0, 0, 0))
                        ),
                        Unselected: (
                            Checked: LoadTextureWithColor(basePath, iconNameCheckBoxChecked, Color.FromArgb(255, 255, 255, 255)),
                            Unchecked: LoadTextureWithColor(basePath, iconNameCheckBoxUnchecked, Color.FromArgb(255, 255, 255, 255))
                        )
                    ),
                    Disabled: (
                        Selected: (
                            Checked: LoadTextureWithColor(basePath, iconNameCheckBoxChecked, Color.FromArgb(255, 150, 150, 150)),
                            Unchecked: LoadTextureWithColor(basePath, iconNameCheckBoxUnchecked, Color.FromArgb(255, 150, 150, 150))
                        ),
                        Unselected: (
                            Checked: LoadTextureWithColor(basePath, iconNameCheckBoxChecked, Color.FromArgb(255, 150, 150, 150)),
                            Unchecked: LoadTextureWithColor(basePath, iconNameCheckBoxUnchecked, Color.FromArgb(255, 150, 150, 150))
                        )
                    )
                );

                protected internal static (string Checked, string Unchecked) toggleTexts = (
                    Checked: "On",
                    Unchecked: "Off"
                );
                protected internal static (((Color Checked, Color Unchecked) Selected, (Color Checked, Color Unchecked) Unselected) Enabled, ((Color Checked, Color Unchecked) Selected, (Color Checked, Color Unchecked) Unselected) Disabled) toggleTextColors = (
                    Enabled: (
                        Selected: (
                            Checked: Color.FromArgb(255, 0, 0, 0),
                            Unchecked: Color.FromArgb(255, 0, 0, 0)
                        ),
                        Unselected: (
                            Checked: Color.FromArgb(255, 255, 255, 255),
                            Unchecked: Color.FromArgb(255, 255, 255, 255)
                        )
                    ),
                    Disabled: (
                        Selected: (
                            Checked: Color.FromArgb(255, 150, 150, 150),
                            Unchecked: Color.FromArgb(255, 150, 150, 150)
                        ),
                        Unselected: (
                            Checked: Color.FromArgb(255, 150, 150, 150),
                            Unchecked: Color.FromArgb(255, 150, 150, 150)
                        )
                    )
                );
                protected internal static (((GTA.Font Checked, GTA.Font Unchecked) Selected, (GTA.Font Checked, GTA.Font Unchecked) Unselected) Enabled, ((GTA.Font Checked, GTA.Font Unchecked) Selected, (GTA.Font Checked, GTA.Font Unchecked) Unselected) Disabled) toggleTextFonts = (
                    Enabled: (
                        Selected: (
                            Checked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None },
                            Unchecked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None }
                        ),
                        Unselected: (
                            Checked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None },
                            Unchecked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None }
                        )
                    ),
                    Disabled: (
                        Selected: (
                            Checked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None },
                            Unchecked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None }
                        ),
                        Unselected: (
                            Checked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None },
                            Unchecked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None }
                        )
                    )
                );

            }

            protected internal class List {

                protected internal static float maximumListWidth = 200f;
                protected internal static bool infiniteScroll = true;
                protected internal static Item.List.ArrowsVisibility showArrows = Item.List.ArrowsVisibility.EnabledSelected;

                protected internal static ((Texture Selected, Texture Unselected) Enabled, (Texture Selected, Texture Unselected) Disabled) iconsArrowLeft = (
                    Enabled: (
                        Selected: LoadTextureWithColor(basePath, iconNameListArrowLeft, Color.FromArgb(255, 0, 0, 0)),
                        Unselected: LoadTextureWithColor(basePath, iconNameListArrowLeft, Color.FromArgb(255, 255, 255, 255))
                    ),
                    Disabled: (
                        Selected: LoadTextureWithColor(basePath, iconNameListArrowLeft, Color.FromArgb(255, 150, 150, 150)),
                        Unselected: LoadTextureWithColor(basePath, iconNameListArrowLeft, Color.FromArgb(255, 150, 150, 150))
                    )
                );
                protected internal static ((Texture Selected, Texture Unselected) Enabled, (Texture Selected, Texture Unselected) Disabled) iconsArrowRight = (
                    Enabled: (
                        Selected: LoadTextureWithColor(basePath, iconNameListArrowRight, Color.FromArgb(255, 0, 0, 0)),
                        Unselected: LoadTextureWithColor(basePath, iconNameListArrowRight, Color.FromArgb(255, 255, 255, 255))
                    ),
                    Disabled: (
                        Selected: LoadTextureWithColor(basePath, iconNameListArrowRight, Color.FromArgb(255, 150, 150, 150)),
                        Unselected: LoadTextureWithColor(basePath, iconNameListArrowRight, Color.FromArgb(255, 150, 150, 150))
                    )
                );

                protected internal static ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) selectedTextColors = (
                    Enabled: (
                        Selected: Color.FromArgb(255, 5, 5, 5),
                        Unselected: Color.FromArgb(255, 255, 255, 255)
                    ),
                    Disabled: (
                        Selected: Color.FromArgb(255, 150, 150, 150),
                        Unselected: Color.FromArgb(255, 150, 150, 150)
                    )
                );
                protected internal static ((GTA.Font Selected, GTA.Font Unselected) Enabled, (GTA.Font Selected, GTA.Font Unselected) Disabled) selectedTextFonts = (
                    Enabled: (
                        Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None },
                        Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None }
                    ),
                    Disabled: (
                        Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None },
                        Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.None }
                    )
                );
            }

            protected internal class Slider {

                protected internal static SizeF sliderSize = new SizeF(184f, 14f);

                protected internal static bool infiniteScroll = true;

                protected internal static PointF sliderPositionOffset = new PointF(0f, 0f);
                protected internal static SizeF sliderSizeOffset = new SizeF(0f, 0f);
                protected internal static SizeF sliderBorderSize = new SizeF(0f, 0f);

                protected internal static ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) sliderBorderColors = (
                    Enabled: (
                        Selected: Color.FromArgb(0, 0, 0, 0),
                        Unselected: Color.FromArgb(0, 0, 0, 0)
                    ),
                    Disabled: (
                        Selected: Color.FromArgb(0, 0, 0, 0),
                        Unselected: Color.FromArgb(0, 0, 0, 0)
                    )
                );
                protected internal static ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) sliderBackColors = (
                    Enabled: (
                        Selected: Color.FromArgb(255, 127, 127, 127),
                        Unselected: Color.FromArgb(255, 127, 127, 127)
                    ),
                    Disabled: (
                        Selected: Color.FromArgb(255, 127, 127, 127),
                        Unselected: Color.FromArgb(255, 127, 127, 127)
                    )
                );
                protected internal static ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) sliderForeColors = (
                    Enabled: (
                        Selected: Color.FromArgb(255, 5, 5, 5),
                        Unselected: Color.FromArgb(255, 255, 255, 245)
                    ),
                    Disabled: (
                        Selected: Color.FromArgb(255, 190, 190, 190),
                        Unselected: Color.FromArgb(255, 190, 190, 190)
                    )
                );

            }

        }
        #endregion

        #region Properties
        /// <summary>
        /// Returns base path of NativeUI.
        /// </summary>
        public static string NativeUIBasePath
        {
            get { return basePath; }
        }
        /// <summary>
        /// Returns the filename of iconArrowUp.
        /// </summary>
        public static string IconNameMenuArrowUp
        {
            get { return iconNameMenuArrowUp; }
        }
        /// <summary>
        /// Returns the filename of iconArrowDown.
        /// </summary>
        public static string IconNameMenuArrowDown
        {
            get { return iconNameMenuArrowDown; }
        }
        /// <summary>
        /// Returns the filename of checked icon of CheckBox item.
        /// </summary>
        public static string IconNameCheckBoxChecked
        {
            get { return iconNameCheckBoxChecked; }
        }
        /// <summary>
        /// Returns the filename of unchecked icon of CheckBox item.
        /// </summary>
        public static string IconNameCheckBoxUnchecked
        {
            get { return iconNameCheckBoxUnchecked; }
        }
        /// <summary>
        /// Returns the filename of left arrow icon of List item.
        /// </summary>
        public static string IconNameListArrowLeft
        {
            get { return iconNameListArrowLeft; }
        }
        /// <summary>
        /// Returns the filename of right arrow icon of List item.
        /// </summary>
        public static string IconNameListArrowRight
        {
            get { return iconNameListArrowRight; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Loads image with specified path and name.
        /// </summary>
        public static Image LoadImage(string basePath, string fileName)
        {
            string file = Path.Combine(basePath, fileName);

            return Image.FromFile(file);
        }
        /// <summary>
        /// Loads texture with specified path and name.
        /// </summary>
        public static Texture LoadTexture(string basePath, string fileName)
        {
            string file = Path.Combine(basePath, fileName);

            byte[] bytes = File.ReadAllBytes(file);
            return new Texture(bytes);
        }
        /// <summary>
        /// Loads texture with specified path, name and color.
        /// </summary>
        public static Texture LoadTextureWithColor(string basePath, string fileName, Color color)
        {
            string file = Path.Combine(basePath, fileName);
            
            using (Image image = Image.FromFile(file)) {
                using (Bitmap bmp = LoadBitmap(image, color)) {
                    return new Texture(AnimationHelper.BitmapToByte(bmp));
                }
            }
        }
        /// <summary>
        /// Loads bitmap with specified image and color.
        /// </summary>
        private static Bitmap LoadBitmap(Image image, Color color)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);

            using (System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bmp)) {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                float[][] matrix = {
                    new float[] { 0, 0, 0, 0, 0 },
                    new float[] { 0, 0, 0, 0, 0 },
                    new float[] { 0, 0, 0, 0, 0 },
                    new float[] { 0, 0, 0, 1, 0 },
                    new float[] { color.R / 255f, color.G / 255f, color.B / 255f, 0, 1 }
                };

                using (ImageAttributes attributes = new ImageAttributes()) {
                    attributes.SetColorMatrix(new ColorMatrix(matrix));
                    graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
            }

            return bmp;
        }
        #endregion
    }

    internal static class SpriteSizeOffset {

        /// <summary>
        /// Applies size offset.
        /// </summary>
        internal static float ApplySizeOffset(this float _float)
        {
            return _float - 1f + 0.002f;
        }

    }

}