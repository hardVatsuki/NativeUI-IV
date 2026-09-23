// NativeUI for Grand Theft Auto IV: The Complete Edition (1.2.0.59)
// Made by ItsClonkAndre, fork by hardVatsuki
// Version 1.0

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;

using GTA;
using GTA.Native;

using static NativeUI.EventHandler;
using static NativeUI.Resource;

namespace NativeUI {
    /// <summary>
    /// Create a new instance of this class in order to create a menu.
    /// </summary>
    public class Menu {

        #region Static
        #region Variables
        /// <summary>
        /// A collection of all menus.
        /// </summary>
        private static Collection<Menu> menus = new Collection<Menu>();
        /// <summary>
        /// Game times since last controller input of each button.
        /// </summary>
        private static int[] controllerInputGameTimes = new int[6] { 0, 0, 0, 0, 0, 0 };
        /// <summary>
        /// Delay between controller input of each button.
        /// </summary>
        private static int controllerInputDelay = 175;
        #endregion

        #region Methods
        /// <summary>
        /// Starts drawing the menu on screen.
        /// </summary>
        /// <param name="targetMenu">The menu that you want to be drawn</param>
        public static void Show(Menu menu) {
            if (menu != null) {
                Hide();
                menu.selectedIndex = 0;
                menu.viewRangeStart = 0;
                menu.viewRangeEnd = menu.maxItemsVisibleAtOnce - 1;
                menu.isMenuOpened = true;
                if (menu.animatonHelper != null) { menu.animatonHelper.StartLoopingGIF(); };
                if (!menu.canControlCharacter || menu.enableControllerSupport) { Game.LocalPlayer.CanControlCharacter = false; };
            }
        }
        /// <summary>
        /// Hides all menus.
        /// </summary>
        public static void Hide() {
            for (int i = 0; i < menus.Count; i++) {
                if (menus[i].isMenuOpened) {
                    menus[i].isMenuOpened = false;
                    if (menus[i].animatonHelper != null) { menus[i].animatonHelper.StopLoopingGIF(); };
                    if (!menus[i].canControlCharacter || menus[i].enableControllerSupport) { Game.LocalPlayer.CanControlCharacter = true; };
                }
            }
        }
        /// <summary>
        /// Checks if any menu of specified script is opened right now.
        /// </summary>
        /// <returns>Returns true if there is a menu of specified script opened, otherwise false.</returns>
        public static bool IsAnyMenuOpen(Script script) {
            for (int i = 0; i < menus.Count; i++) {
                if (menus[i].isMenuOpened && menus[i].script == script) {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Gets currently opened menu of specified script.
        /// </summary>
        /// <returns>Returns Menu if there is a menu of specified script opened, otherwise null.</returns>
        public static Menu GetCurrentMenu(Script script) {
            for (int i = 0; i < menus.Count; i++) {
                if (menus[i].isMenuOpened && menus[i].script == script) {
                    return menus[i];
                }
            }
            return null;
        }
        /// <summary>
        /// Gets all menus of specified script.
        /// </summary>
        /// <returns>Returns all menus of specified script.</returns>
        public static List<Menu> GetAllMenus(Script script) {
            List<Menu> _menus = new List<Menu>();
            for (int i = 0; i < menus.Count; i++) {
                if (menus[i].script == script) {
                    _menus.Add(menus[i]);
                }
            }
            return _menus;
        }
        /// <summary>
        /// This method needs to be called everytime from your PerFrameDrawing method in order for the menu to be drawn.
        /// </summary>
        /// <param name="args">The GraphicsEventArgs from your PerFrameDrawing method.</param>
        protected internal static void ProcessDrawing(GraphicsEventArgs args) {
            for (int i = 0; i < menus.Count; i++) {
                if (menus[i].isMenuOpened) {
                    menus[i].DrawMenu(args);
                    break;
                }
            }
        }
        /// <summary>
        /// This method needs to be called everytime you press a key from your KeyDown method in order for the menu to work.
        /// </summary>
        /// <param name="args">The GTA.KeyEventArgs from your KeyDown method.</param>
        protected internal static void ProcessKeyPress(GTA.KeyEventArgs args)
        {
            for (int i = 0; i < menus.Count; i++) {
                if (menus[i].isMenuOpened) {
                    menus[i].KeyPress(args);
                    break;
                }
            }
        }
        /// <summary>
        /// This method needs to be called everytime from your Tick method in order for the controller navigation to work.
        /// </summary>
        protected internal static void ProcessController()
        {
            for (int i = 0; i < menus.Count; i++) {
                if (menus[i].isMenuOpened) {
                    menus[i].GetControllerInput();
                    break;
                }
            }
        }
        #endregion
        #endregion

        #region Variables
        private Keys upKey = MenuResource.upKey;
        private Keys downKey = MenuResource.downKey;
        private Keys leftKey = MenuResource.leftKey;
        private Keys rightKey = MenuResource.rightKey;
        private Keys acceptKey = MenuResource.acceptKey;
        private Keys backKey = MenuResource.backKey;

        private bool canControlCharacter = MenuResource.canControlCharacter;
        private bool enableControllerSupport = MenuResource.enableControllerSupport;
        private bool enableMenuSounds = MenuResource.enableMenuSounds;
        private bool infiniteScroll = MenuResource.infiniteScroll;

        private int titleImageFrameRate = MenuResource.titleImageFrameRate;
        private int maxItemsVisibleAtOnce = MenuResource.maxItemsVisibleAtOnce;
        private PointF offset = MenuResource.offset;
        private PointF titleTextOffset = MenuResource.titleTextOffset;

        private bool showTitle = MenuResource.showTitle;
        private bool showDescription = MenuResource.showDescription;
        private bool showArrows = MenuResource.showArrows;
        private bool showGaps = MenuResource.showGaps;

        private Script script;

        private Color titleBackColor = MenuResource.titleBackColor;
        private Texture titleImage = MenuResource.titleImage;
        private AnimationHelper animatonHelper;
        private string titleText;
        private Color titleTextColor = MenuResource.titleTextColor;
        private GTA.Font titleTextFont = MenuResource.titleTextFont;
        private Color titleLineColor = MenuResource.titleLineColor;

        private Color descriptionBackColor = MenuResource.descriptionBackColor;
        private string descriptionText;
        private Color descriptionTextColor = MenuResource.descriptionTextColor;
        private GTA.Font descriptionTextFont = MenuResource.descriptionTextFont;
        private Color selectedIndexTextColor = MenuResource.selectedIndexTextColor;
        private GTA.Font selectedIndexTextFont = MenuResource.selectedIndexTextFont;

        protected internal Collection<Item> items;
        private Color noItemsBackColor = MenuResource.noItemsBackColor;
        private string noItemsText = MenuResource.noItemsText;
        private Color noItemsTextColor = MenuResource.noItemsTextColor;
        private GTA.Font noItemsTextFont = MenuResource.noItemsTextFont;

        private Color arrowsUpDownBackColor = MenuResource.arrowsUpDownBackColor;
        private (Texture Enabled, Texture Disabled) iconsArrowUp = MenuResource.iconsArrowUp;
        private (Texture Enabled, Texture Disabled) iconsArrowDown = MenuResource.iconsArrowDown;

        private bool isMenuOpened;
        private int selectedIndex;
        private int viewRangeStart;
        private int viewRangeEnd;
        #endregion

        #region Properties
        /// <summary>
        /// Key for navigating up in the menu.
        /// </summary>
        public Keys UpKey
        {
            get { return upKey; }
            set { upKey = value; }
        }
        /// <summary>
        /// Key for navigating down in the menu.
        /// </summary>
        public Keys DownKey
        {
            get { return downKey; }
            set { downKey = value; }
        }
        /// <summary>
        /// Key for navigating left in the menu.
        /// </summary>
        public Keys LeftKey
        {
            get { return leftKey; }
            set { leftKey = value; }
        }
        /// <summary>
        /// Key for navigating right in the menu.
        /// </summary>
        public Keys RightKey
        {
            get { return rightKey; }
            set { rightKey = value; }
        }
        /// <summary>
        /// Key to confirm the selection in the menu.
        /// </summary>
        public Keys AcceptKey
        {
            get { return acceptKey; }
            set { acceptKey = value; }
        }
        /// <summary>
        /// Key to go back to previous menu.
        /// </summary>
        public Keys BackKey
        {
            get { return backKey; }
            set { backKey = value; }
        }

        /// <summary>
        /// This will disable the movement of the player when the menu is opened and re-enables it when the menu closes.
        /// <para>It will also disable the phone.</para>
        /// <para>WARNING: If the player drives a vehicle, the vehicle WILL stop instantly!</para>
        /// </summary>
        public bool CanControlCharacter
        {
            get { return canControlCharacter; }
            set { canControlCharacter = value; }
        }
        /// <summary>
        /// This enables the support for navigating through the menu with a controller.
        /// </summary>
        public bool EnableControllerSupport
        {
            get { return enableControllerSupport; }
            set {
                enableControllerSupport = value;

                if (enableControllerSupport && isMenuOpened) {
                    Game.LocalPlayer.CanControlCharacter = false;
                }
                else if (!enableControllerSupport && isMenuOpened && canControlCharacter) {
                    Game.LocalPlayer.CanControlCharacter = true;
                }
            }
        }
        /// <summary>
        /// This will enable or disable menu sounds.
        /// </summary>
        public bool EnableMenuSounds
        {
            get { return enableMenuSounds; }
            set { enableMenuSounds = value; }
        }
        /// <summary>
        /// This will enable or disable infinite scrolling in menu.
        /// </summary>
        public bool InfiniteScroll
        {
            get { return infiniteScroll; }
            set { infiniteScroll = value; }
        }

        /// <summary>
        /// Gets or sets the frame rate of title image.
        /// </summary>
        public int TitleImageFrameRate
        {
            get { return titleImageFrameRate; }
            set { titleImageFrameRate = value; }
        }
        /// <summary>
        /// Gets or sets how much items the menu can display at once.
        /// </summary>
        public int MaxItemsVisibleAtOnce
        {
            get { return maxItemsVisibleAtOnce; }
            set {
                if (value < 1) {
                    maxItemsVisibleAtOnce = 1;
                }
                else if (value > MenuResource.maxItemsVisibleAtOnce) {
                    maxItemsVisibleAtOnce = MenuResource.maxItemsVisibleAtOnce;
                }
                else {
                    maxItemsVisibleAtOnce = value;
                }

                int _whilesCount = maxItemsVisibleAtOnce - 1;

                int _viewRangeStart = selectedIndex;
                while (_viewRangeStart > -1 && _whilesCount > 0) {
                    _viewRangeStart--;
                    _whilesCount--;
                }

                int _viewRangeEnd = selectedIndex;
                while (_viewRangeEnd < items.Count && _whilesCount > 0) {
                    _viewRangeEnd++;
                    _whilesCount--;
                }

                viewRangeStart = _viewRangeStart;
                viewRangeEnd = _viewRangeEnd;
            }
        }
        /// <summary>
        /// Gets or sets menu offset.
        /// </summary>
        public PointF Offset
        {
            get { return offset; }
            set { offset = value; }
        }
        /// <summary>
        /// Gets or sets title text offset.
        /// </summary>
        public PointF TitleTextOffset
        {
            get { return titleTextOffset; }
            set { titleTextOffset = value; }
        }

        /// <summary>
        /// Gets or sets title visibility.
        /// </summary>
        public bool ShowTitle
        {
            get { return showTitle; }
            set { showTitle = value; }
        }
        /// <summary>
        /// Gets or sets description visibility.
        /// </summary>
        public bool ShowDescription
        {
            get { return showDescription; }
            set { showDescription = value; }
        }
        /// <summary>
        /// Gets or sets arrows up-down visibility.
        /// </summary>
        public bool ShowArrows
        {
            get { return showArrows; }
            set { showArrows = value; }
        }
        /// <summary>
        /// Gets or sets gaps visibility.
        /// </summary>
        public bool ShowGaps
        {
            get { return showGaps; }
            set { showGaps = value; }
        }

        /// <summary>
        /// Sets script instance.
        /// </summary>
        private Script Script
        {
            set { script = value; }
        }

        /// <summary>
        /// Gets or sets title back color.
        /// </summary>
        public Color TitleBackColor
        {
            get { return titleBackColor; }
            set { titleBackColor = value; }
        }
        /// <summary>
        /// Sets title image.
        /// </summary>
        public object TitleImage
        {
            set {
                if (value != null) {
                    if ((value is Texture _texture || value is Image _image) && animatonHelper != null) {
                        if (isMenuOpened) { animatonHelper.StopLoopingGIF(); }
                        animatonHelper.AnimatedTextureReturner -= AnimatonHelper_AnimatedTextureReturner;
                        animatonHelper.Dispose();
                        animatonHelper = null;
                    }

                    if (value is Texture texture) {
                        titleImage = texture;
                    }
                    else if (value is Image image) {
                        animatonHelper = new AnimationHelper(image, titleImageFrameRate);
                        animatonHelper.AnimatedTextureReturner += AnimatonHelper_AnimatedTextureReturner;
                        titleImage = animatonHelper.GetFirstFrameOfImage();
                        if (isMenuOpened) { animatonHelper.StartLoopingGIF(); }
                    }
                }
            }
        }
        /// <summary>
        /// Gets or sets title text.
        /// </summary>
        public string TitleText
        {
            get { return titleText; }
            set { titleText = value; }
        }
        /// <summary>
        /// Gets or sets title text color.
        /// </summary>
        public Color TitleTextColor
        {
            get { return titleTextColor; }
            set { titleTextColor = value; }
        }
        /// <summary>
        /// Gets or sets title text font.
        /// </summary>
        public GTA.Font TitleTextFont
        {
            get { return titleTextFont; }
            set { titleTextFont = value; }
        }
        /// <summary>
        /// Gets or sets title line color.
        /// </summary>
        public Color TitleLineColor
        {
            get { return titleLineColor; }
            set { titleLineColor = value; }
        }

        /// <summary>
        /// Gets or sets description background color.
        /// </summary>
        public Color DescriptionBackColor
        {
            get { return descriptionBackColor; }
            set { descriptionBackColor = value; }
        }
        /// <summary>
        /// Gets or sets description text.
        /// </summary>
        public string DescriptionText
        {
            get { return descriptionText; }
            set { descriptionText = value; }
        }
        /// <summary>
        /// Gets or sets description text color.
        /// </summary>
        public Color DescriptionTextColor
        {
            get { return descriptionTextColor; }
            set { descriptionTextColor = value; }
        }
        /// <summary>
        /// Gets or sets description text font.
        /// </summary>
        public GTA.Font DescriptionTextFont
        {
            get { return descriptionTextFont; }
            set { descriptionTextFont = value; }
        }
        /// <summary>
        /// Gets or sets selected index text color.
        /// </summary>
        public Color SelectedIndexTextColor
        {
            get { return selectedIndexTextColor; }
            set { selectedIndexTextColor = value; }
        }
        /// <summary>
        /// Gets or sets selected index text font.
        /// </summary>
        public GTA.Font SelectedIndexTextFont
        {
            get { return selectedIndexTextFont; }
            set { selectedIndexTextFont = value; }
        }

        /// <summary>
        /// A collection of all items currently available in this menu.
        /// </summary>
        public Collection<Item> Items
        {
            get { return items; }
        }
        /// <summary>
        /// Gets or sets no items back color.
        /// </summary>
        public Color NoItemsBackColor
        {
            get { return noItemsBackColor; }
            set { noItemsBackColor = value; }
        }
        /// <summary>
        /// Gets or sets no items text.
        /// </summary>
        public string NoItemsText
        {
            get { return noItemsText; }
            set { noItemsText = value; }
        }
        /// <summary>
        /// Gets or sets no items text color.
        /// </summary>
        public Color NoItemsTextColor
        {
            get { return noItemsTextColor; }
            set { noItemsTextColor = value; }
        }
        /// <summary>
        /// Gets or sets no items text font.
        /// </summary>
        public GTA.Font NoItemsTextFont
        {
            get { return noItemsTextFont; }
            set { noItemsTextFont = value; }
        }

        /// <summary>
        /// Gets or sets up-down arrows background color.
        /// </summary>
        public Color ArrowsUpDownBackColor
        {
            get { return arrowsUpDownBackColor; }
            set { arrowsUpDownBackColor = value; }
        }
        /// <summary>
        /// Sets up arrow icons.
        /// </summary>
        public (Texture Enabled, Texture Disabled) IconsArrowUp
        {
            set { iconsArrowUp = value; }
        }
        /// <summary>
        /// Sets down arrow icons.
        /// </summary>
        public (Texture Enabled, Texture Disabled) IconsArrowDown
        {
            set { iconsArrowDown = value; }
        }

        /// <summary>
        /// Gets the visibility state of this menu.
        /// </summary>
        public bool IsMenuOpened
        {
            get { return isMenuOpened; }
        }
        /// <summary>
        /// Gets the currently selected item index of this menu.
        /// </summary>
        public int SelectedIndex
        {
            get { return selectedIndex; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add an item to the menu.
        /// </summary>
        /// <param name="item">Adds the item to the menu</param>
        public void Add(Item item) {
            items.Add(item);
        }
        /// <summary>
        /// Adds an array of items to the menu.
        /// </summary>
        /// <param name="items">The array with items</param>
        public void AddRange(Item[] items) {
            try {
                for (int i = 0; i < items.Length; i++) {
                    this.items.Add(items[i]);
                }
            }
            catch (Exception ex) {
                Game.Console.Print("NativeUI: Error while adding items to the menu. Details: " + ex.Message);
            }
        }
        /// <summary>
        /// This removes the specific item from the list.
        /// </summary>
        /// <param name="item">The item to be removed</param>
        /// <returns>Returns true if the item was deleted, otherwise false.</returns>
        public bool Remove(Item item) {
            try {
                for (int i = 0; i < items.Count; i++) {
                    if (items[i] == item) {
                        items.Remove(items[i]);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex) {
                Game.Console.Print("NativeUI: Error while removing an item from the list. Details: " + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// This removes the specific item from the list through it's index.
        /// </summary>
        /// <param name="index">The index where the item is at</param>
        /// <returns>Returns true if the item was deleted, otherwise false.</returns>
        public bool RemoveAt(int index) {
            try {
                for (int i = 0; i < items.Count; i++) {
                    if (i == index) {
                        items.RemoveAt(i);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex) {
                Game.Console.Print("NativeUI: Error while removing an item from the list through it's index. Details: " + ex.Message);
                return false;
            }
        }
        /// <summary>
        /// This removes all items from the menu.
        /// </summary>
        public void RemoveAll() {
            items.Clear();
        }
        /// <summary>
        /// Gets up arrow texture for icon.
        /// </summary>
        private Texture GetArrowUpIcon()
        {
            if (selectedIndex == 0 && !infiniteScroll) {
                return iconsArrowUp.Disabled;
            }

            return iconsArrowUp.Enabled;
        }
        /// <summary>
        /// Gets down arrow texture for icon.
        /// </summary>
        private Texture GetArrowDownIcon()
        {
            if (selectedIndex == (items.Count - 1) && !infiniteScroll) {
                return iconsArrowDown.Disabled;
            }

            return iconsArrowDown.Enabled;
        }
        /// <summary>
        /// Restores menu.
        /// </summary>
        public void Restore()
        {
            UpKey = MenuResource.upKey;
            DownKey = MenuResource.downKey;
            LeftKey = MenuResource.leftKey;
            RightKey = MenuResource.rightKey;
            AcceptKey = MenuResource.acceptKey;
            BackKey = MenuResource.backKey;
            
            CanControlCharacter = MenuResource.canControlCharacter;
            EnableControllerSupport = MenuResource.enableControllerSupport;
            EnableMenuSounds = MenuResource.enableMenuSounds;
            InfiniteScroll = MenuResource.infiniteScroll;

            TitleImageFrameRate = MenuResource.titleImageFrameRate;
            MaxItemsVisibleAtOnce = MenuResource.maxItemsVisibleAtOnce;
            Offset = MenuResource.offset;
            TitleTextOffset = MenuResource.titleTextOffset;

            ShowTitle = MenuResource.showTitle;
            ShowDescription = MenuResource.showDescription;
            ShowArrows = MenuResource.showArrows;
            ShowGaps = MenuResource.showGaps;
            
            TitleBackColor = MenuResource.titleBackColor;
            TitleImage = MenuResource.titleImage;
            TitleTextColor = MenuResource.titleTextColor;
            TitleTextFont = MenuResource.titleTextFont;
            TitleLineColor = MenuResource.titleLineColor;
            
            DescriptionBackColor = MenuResource.descriptionBackColor;
            DescriptionTextColor = MenuResource.descriptionTextColor;
            DescriptionTextFont = MenuResource.descriptionTextFont;
            SelectedIndexTextColor = MenuResource.selectedIndexTextColor;
            SelectedIndexTextFont = MenuResource.selectedIndexTextFont;
            
            NoItemsBackColor = MenuResource.noItemsBackColor;
            NoItemsText = MenuResource.noItemsText;
            NoItemsTextColor = MenuResource.noItemsTextColor;
            NoItemsTextFont = MenuResource.noItemsTextFont;
            
            ArrowsUpDownBackColor = MenuResource.arrowsUpDownBackColor;
            IconsArrowUp = MenuResource.iconsArrowUp;
            IconsArrowDown = MenuResource.iconsArrowDown;
        }
        #endregion

        #region Events
        /// <summary>
        /// Raises when the selected index of this menu changes.
        /// </summary>
        public event SelectedIndexChangedEventHandler OnSelectedIndexChanged;
        #endregion

        #region EventRaisers
        /// <summary>
        /// Generates OnSelectedIndexChanged event.
        /// </summary>
        public void SelectedIndexChangedRaiser()
        {
            OnSelectedIndexChanged?.Invoke(selectedIndex); // RaiseEvent
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor of the Menu class.
        /// </summary>
        /// <param name="script">The script of the menu.</param>
        /// <param name="titleText">The title text of the menu.</param>
        /// <param name="descriptionText">The description text of the menu. (Under the image)</param>
        public Menu(Script script, string titleText, string descriptionText)
        {
            try {
                Script = script;
                TitleText = titleText;
                DescriptionText = descriptionText;

                items = new Collection<Item>();
                menus.Add(this);
            }
            catch (Exception ex) {
                Game.Console.Print("NativeUI: Error while creating menu. Details: " + ex.Message);
            }
        }
        /// <summary>
        /// Constructor of the Menu class.
        /// </summary>
        /// <param name="script">The script of the menu.</param>
        /// <param name="titleText">The title text of the menu.</param>
        /// <param name="descriptionText">The description text of the menu. (Under the image)</param>
        /// <param name="titleImage">The title image. Not GIF friendly. Only static images. Recommended size: 432x97.</param>
        public Menu(Script script, string titleText, string descriptionText, Texture titleImage)
        {
            try {
                Script = script;
                TitleText = titleText;
                DescriptionText = descriptionText;
                TitleImage = titleImage;

                items = new Collection<Item>();
                menus.Add(this);
            }
            catch (Exception ex) {
                Game.Console.Print("NativeUI: Error while creating menu with texture. Details: " + ex.Message);
            }
        }
        /// <summary>
        /// Constructor of the Menu class.
        /// </summary>
        /// <param name="script">The script of the menu.</param>
        /// <param name="titleText">The title text of the menu.</param>
        /// <param name="descriptionText">The description text of the menu. (Under the image)</param>
        /// <param name="titleImage">The title image. Supports GIFs. Recommended size: 432x97.</param>
        public Menu(Script script, string titleText, string descriptionText, Image titleImage)
        {
            try {
                Script = script;
                TitleText = titleText;
                DescriptionText = descriptionText;
                TitleImage = titleImage;

                items = new Collection<Item>();
                menus.Add(this);
            }
            catch (Exception ex)
            {
                Game.Console.Print("NativeUI: Error while creating menu with image. Details: " + ex.Message);
            }
        }
        #endregion

        #region Drawing
        private void DrawMenu(GraphicsEventArgs args) {
            PointF position = new PointF(MenuResource.position.X + offset.X, MenuResource.position.Y + offset.Y);
            float width = MenuResource.widgth;
            float itemHeight = ItemResource.height;

            #region Title
            if (showTitle) {
                args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y, width, 98f), titleBackColor);
                args.Graphics.DrawSprite(titleImage, new RectangleF(position.X, position.Y, 432f.ApplySizeOffset(), 98f.ApplySizeOffset()));
                args.Graphics.DrawText(titleText, new RectangleF(position.X + titleTextOffset.X, position.Y + 27f + titleTextOffset.Y, width, TextRenderer.MeasureText(titleText, titleTextFont.WindowsFont).Height + 10), TextAlignment.Center, titleTextColor, titleTextFont);
                args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y + 96f, width, 2f), titleLineColor);
                
                position.Y += 98f;
            }
            #endregion

            #region Description
            if (showDescription) {
                args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y, width, 38f), descriptionBackColor);
                args.Graphics.DrawText(descriptionText, new RectangleF(position.X + 9f, position.Y + 6f, TextRenderer.MeasureText(descriptionText, descriptionTextFont.WindowsFont).Width, TextRenderer.MeasureText(descriptionText, descriptionTextFont.WindowsFont).Height), TextAlignment.Left, descriptionTextColor, descriptionTextFont);

                string selectedIndexText = "0 / 0";
                if (items.Count != 0) {
                    selectedIndexText = string.Format("{0} / {1}", (selectedIndex + 1).ToString(), items.Count.ToString());
                }
                Size selectedIndexTextSize = TextRenderer.MeasureText(selectedIndexText, selectedIndexTextFont.WindowsFont);
                float xValue = selectedIndexTextSize.Width - (selectedIndexText.Length * 2) - (selectedIndexText.Length == 0 ? 0 : 40f);
                args.Graphics.DrawText(selectedIndexText, new RectangleF(position.X + 381f - xValue, position.Y + 6f, selectedIndexTextSize.Width, selectedIndexTextSize.Height), TextAlignment.Center, selectedIndexTextColor, selectedIndexTextFont);

                position.Y += 38f;
            }
            #endregion

            #region Items
            if (items.Count != 0) {
                for (int i = 0; i < items.Count; i++) {
                    if (selectedIndex == i) {
                        items[i].isSelected = true;
                    }
                    else {
                        items[i].isSelected = false;
                    }

                    if (!(i >= viewRangeStart && i <= viewRangeStart + viewRangeEnd)) { continue; }

                    items[i].DrawItem(args, position, new SizeF(width, itemHeight));

                    position.Y += itemHeight;
                }
            }
            else {
                args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y, width, itemHeight), noItemsBackColor);
                args.Graphics.DrawText(noItemsText, new RectangleF(position.X + 11f, position.Y + 5f, TextRenderer.MeasureText(noItemsText, noItemsTextFont.WindowsFont).Width, TextRenderer.MeasureText(noItemsText, noItemsTextFont.WindowsFont).Height), TextAlignment.Left, noItemsTextColor, noItemsTextFont);
                
                position.Y += itemHeight;
            }
            #endregion

            if (items.Count != 0) {
                #region Arrows
                if (showArrows) {
                    if (showGaps) {
                        position.Y += 1f;
                    }

                    args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y, width, 38f), arrowsUpDownBackColor);
                    args.Graphics.DrawSprite(GetArrowUpIcon(), new RectangleF(position.X + 197f, position.Y - 8f, 38f.ApplySizeOffset(), 38f.ApplySizeOffset()));
                    args.Graphics.DrawSprite(GetArrowDownIcon(), new RectangleF(position.X + 197f, position.Y + 8f, 38f.ApplySizeOffset(), 38f.ApplySizeOffset()));

                    position.Y += 38f;
                }
                #endregion

                #region Item Description
                if (items[selectedIndex].showDescription) {
                    if (showGaps) {
                        position.Y += 5f;
                    }
                    
                    #region Item Description Line
                    if (items[selectedIndex].showDescriptionLine) {
                        args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y, width, 2f), items[selectedIndex].GetDescriptionLineColor());
                        
                        position.Y += 2f;
                    }
                    #endregion

                    int itemDescriptionTextLinesCount = items[selectedIndex].descriptionText.Split('\n').Length;

                    args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y, width, 38f + (26f * (itemDescriptionTextLinesCount - 1))), items[selectedIndex].GetDescriptionBackColor());
                    args.Graphics.DrawText(items[selectedIndex].descriptionText, new RectangleF(position.X + 9f, position.Y + 5f, width, 35f * itemDescriptionTextLinesCount), TextAlignment.Left, items[selectedIndex].GetDescriptionTextColor(), items[selectedIndex].GetDescriptionTextFont());
                }
                #endregion
            }
        }
        #endregion

        #region KeyPress
        private void KeyPress(GTA.KeyEventArgs args) {
            ActualKeyPress(args.Key);
        }
        private void GetControllerInput() {
            try {
                if (enableControllerSupport) {
                    if (Function.Call<bool>("IS_USING_CONTROLLER")) {
                        int gameTime = Game.GameTime;
                        if (Function.Call<bool>("IS_BUTTON_PRESSED", 0, 8) && gameTime - controllerInputGameTimes[0] >= controllerInputDelay) // DPAD_UP
                        {
                            ActualKeyPress(upKey);
                            controllerInputGameTimes[0] = gameTime;
                        }
                        else if (Function.Call<bool>("IS_BUTTON_PRESSED", 0, 9) && gameTime - controllerInputGameTimes[1] >= controllerInputDelay) // DPAD_DOWN
                        {
                            ActualKeyPress(downKey);
                            controllerInputGameTimes[1] = gameTime;
                        }
                        else if (Function.Call<bool>("IS_BUTTON_PRESSED", 0, 10) && gameTime - controllerInputGameTimes[2] >= controllerInputDelay) // DPAD_LEFT
                        {
                            ActualKeyPress(leftKey);
                            controllerInputGameTimes[2] = gameTime;
                        }
                        else if (Function.Call<bool>("IS_BUTTON_PRESSED", 0, 11) && gameTime - controllerInputGameTimes[3] >= controllerInputDelay) // DPAD_RIGHT
                        {
                            ActualKeyPress(rightKey);
                            controllerInputGameTimes[3] = gameTime;
                        }
                        else if (Function.Call<bool>("IS_BUTTON_PRESSED", 0, 16) && gameTime - controllerInputGameTimes[4] >= controllerInputDelay) // A
                        {
                            ActualKeyPress(acceptKey);
                            controllerInputGameTimes[4] = gameTime;
                        }
                        else if (Function.Call<bool>("IS_BUTTON_PRESSED", 0, 17) && gameTime - controllerInputGameTimes[5] >= controllerInputDelay) // B
                        {
                            ActualKeyPress(backKey);
                            controllerInputGameTimes[5] = gameTime;
                        }
                    }
                }
            }
            catch (Exception ex) {
                Game.Console.Print("NativeUI: unhandled exception in GetControllerInput(). Details: " + ex.ToString());
                EnableControllerSupport = false;
                Game.Console.Print("NativeUI: 'EnableControllerSupport' was disabled for this menu to prevent any more errors.");
            }
        }
        private void ActualKeyPress(Keys pressedKey) {
            if (pressedKey == upKey) { // UP
                if (items.Count != 0) {
                    if (selectedIndex == 0) {
                        if (infiniteScroll) {
                            selectedIndex = items.Count - 1;
                            int _viewRangeStart = (items.Count - 1) - (maxItemsVisibleAtOnce - 1);
                            if (_viewRangeStart < 0) {
                                viewRangeStart = 0;
                            }
                            else {
                                viewRangeStart = _viewRangeStart;
                            }
                            viewRangeEnd = items.Count - 1;
                        }
                    }
                    else {
                        selectedIndex--;
                        if (selectedIndex < viewRangeStart) {
                            viewRangeStart--;
                            viewRangeEnd--;
                        }
                    }

                    if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_HIGHLIGHT_DOWN_UP"); } // Play sound
                    SelectedIndexChangedRaiser();
                }
            }
            else if (pressedKey == downKey) { // DOWN
                if (items.Count != 0) {
                    if (selectedIndex == (items.Count - 1)) {
                        if (infiniteScroll) {
                            selectedIndex = 0;
                            viewRangeStart = 0;
                            viewRangeEnd = maxItemsVisibleAtOnce - 1;
                        }
                    }
                    else {
                        selectedIndex++;
                        if (selectedIndex > viewRangeEnd) {
                            viewRangeStart++;
                            viewRangeEnd++;
                        }
                    }

                    if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_HIGHLIGHT_DOWN_UP"); } // Play sound
                    SelectedIndexChangedRaiser();
                }
            }
            else if (pressedKey == leftKey) { // LEFT
                if (items.Count != 0) {
                    switch (items[selectedIndex].type) {

                        case Item.ControlType.List:
                            Item.List listItem = (Item.List)items[selectedIndex];
                            if (listItem.isEnabled && listItem.itemList.Count != 0) {
                                if (listItem.selectedIndex == 0) {
                                    if (listItem.infiniteScroll) {
                                        listItem.selectedIndex = (listItem.itemList.Count - 1);
                                    }
                                }
                                else {
                                    listItem.selectedIndex--;
                                }
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_SLIDER_DOWN"); } // Play sound

                                listItem.SelectedIndexChangedRaiser();
                            }
                            break;

                        case Item.ControlType.Slider:
                            Item.Slider sliderItem = (Item.Slider)items[selectedIndex];
                            if (sliderItem.isEnabled)
                            {
                                if (sliderItem.currentValue == sliderItem.minimumValue) {
                                    if (sliderItem.infiniteScroll) {
                                        sliderItem.currentValue = sliderItem.maximumValue;
                                    }
                                }
                                else {
                                    sliderItem.currentValue--;
                                }
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_SLIDER_DOWN"); } // Play sound

                                sliderItem.CurrentValueChangedRaiser();
                            }
                            break;

                    }
                }
            }
            else if (pressedKey == rightKey) { // RIGHT
                if (items.Count != 0) {
                    switch (items[selectedIndex].type) {

                        case Item.ControlType.List:
                            Item.List listItem = (Item.List)items[selectedIndex];
                            if (listItem.isEnabled && listItem.itemList.Count != 0) {
                                if (listItem.selectedIndex == (listItem.itemList.Count - 1)) {
                                    if (listItem.infiniteScroll) {
                                        listItem.selectedIndex = 0;
                                    }
                                }
                                else {
                                    listItem.selectedIndex++;
                                }
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_SLIDER_UP"); } // Play sound

                                listItem.SelectedIndexChangedRaiser();
                            }
                            break;

                        case Item.ControlType.Slider:
                            Item.Slider sliderItem = (Item.Slider)items[selectedIndex];
                            if (sliderItem.isEnabled)
                            {
                                if (sliderItem.currentValue == sliderItem.maximumValue) {
                                    if (sliderItem.infiniteScroll) {
                                        sliderItem.currentValue = sliderItem.minimumValue;
                                    }
                                }
                                else {
                                    sliderItem.currentValue++;
                                }
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_SLIDER_UP"); } // Play sound

                                sliderItem.CurrentValueChangedRaiser();
                            }
                            break;

                    }
                }
            }
            else if (pressedKey == acceptKey) { // ACCEPT
                if (items.Count != 0) {
                    switch (items[selectedIndex].type) {

                        case Item.ControlType.Button:
                            Item.Button item = (Item.Button)items[selectedIndex];
                            if (item.isEnabled) {
                                item.PerformClick();

                                if (item.nestedMenu != null) { Show(item.nestedMenu); } // Open nested menu
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_SELECT"); } // Play sound
                            }
                            else {
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_ERROR"); } // Play error sound
                            }
                            break;

                        case Item.ControlType.CheckBox:
                            Item.CheckBox chkItem = (Item.CheckBox)items[selectedIndex];
                            if (chkItem.isEnabled) {
                                chkItem.isChecked = !chkItem.isChecked;
                                if (chkItem.isChecked) {
                                    if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_TOGGLE_ON"); } // Play check sound
                                }
                                else {
                                    if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_TOGGLE_OFF"); } // Play uncheck sound
                                }
                                chkItem.CheckedChangedRaiser();
                            }
                            else {
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_ERROR"); } // Play error sound
                            }
                            break;

                        case Item.ControlType.List:
                            Item.List listItem = (Item.List)items[selectedIndex];
                            if (listItem.isEnabled && listItem.itemList.Count != 0) {
                                listItem.PerformClick();
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_SELECT"); } // Play sound
                            }
                            else {
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_ERROR"); } // Play error sound
                            }
                            break;

                        case Item.ControlType.Slider:
                            Item.Slider sliderItem = (Item.Slider)items[selectedIndex];
                            if (sliderItem.isEnabled) {
                                sliderItem.PerformClick();
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_SELECT"); } // Play sound
                            }
                            else {
                                if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_ERROR"); } // Play error sound
                            }
                            break;

                    }
                }
            }
            else if (pressedKey == backKey) { // BACK
                bool foundParentMenu = false;
                int parentMenuIndex = -1;
                int parentItemIndex = -1;
                int parentViewRangeStartIndex = -1;
                int parentViewRangeEndIndex = -1;

                for (int i = 0; i < menus.Count; i++) {
                    for (int ii = 0; ii < menus[i].items.Count; ii++) {
                        if (menus[i].items[ii].type == Item.ControlType.Button && menus[i].items[ii] is Item.Button item && item.nestedMenu != null && item.nestedMenu.isMenuOpened) {
                            foundParentMenu = true;
                            parentMenuIndex = i;
                            parentItemIndex = ii;
                            parentViewRangeStartIndex = menus[i].viewRangeStart;
                            parentViewRangeEndIndex = menus[i].viewRangeEnd;
                            break;
                        }
                    }

                    if (foundParentMenu) { break; }
                }

                if (foundParentMenu) {
                    Show(menus[parentMenuIndex]);
                    menus[parentMenuIndex].selectedIndex = parentItemIndex;
                    menus[parentMenuIndex].viewRangeStart = parentViewRangeStartIndex;
                    menus[parentMenuIndex].viewRangeEnd = parentViewRangeEndIndex;
                    if (enableMenuSounds) { Function.Call("PLAY_SOUND_FRONTEND", -1, "FRONTEND_MENU_BACK"); }
                }
            }
        }
        #endregion

        private void AnimatonHelper_AnimatedTextureReturner(Texture texture)
        {
            if (texture != null) titleImage = texture;
        }
    }
}
