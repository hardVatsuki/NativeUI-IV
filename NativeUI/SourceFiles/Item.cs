// NativeUI for Grand Theft Auto IV: The Complete Edition (1.2.0.59)
// Made by ItsClonkAndre, fork by hardVatsuki
// Version 1.0

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using GTA;

using static NativeUI.EventHandler;
using static NativeUI.Resource;

namespace NativeUI {
    /// <summary>
    /// All menu items.
    /// </summary>
    public abstract class Item {
        
        /// <summary>
        /// All control types.
        /// </summary>
        public enum ControlType
        {
            /// <summary>
            /// Button: Item with title.
            /// </summary>
            Button,
            /// <summary>
            /// CheckBox: Item with title and checkbox.
            /// </summary>
            CheckBox,
            /// <summary>
            /// List: Item with title and list of items.
            /// </summary>
            List,
            /// <summary>
            /// Slider: Item with title and slider.
            /// </summary>
            Slider
        }

        /// <summary>
        /// Base class for all UI Elements.
        /// </summary>
        #region Variables
        protected internal bool showDescriptionLine = ItemResource.showDescriptionLine;
        protected internal bool showDescription = ItemResource.showDescription;

        private ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) titleBackColors = ItemResource.titleBackColors;
        private string titleText;
        private ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) titleTextColors = ItemResource.titleTextColors;
        private ((GTA.Font Selected, GTA.Font Unselected) Enabled, (GTA.Font Selected, GTA.Font Unselected) Disabled) titleTextFonts = ItemResource.titleTextFonts;

        private ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) descriptionLineColors = ItemResource.descriptionLineColors;
        private ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) descriptionBackColors = ItemResource.descriptionBackColors;
        protected internal string descriptionText;
        private ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) descriptionTextColors = ItemResource.descriptionTextColors;
        private ((GTA.Font Selected, GTA.Font Unselected) Enabled, (GTA.Font Selected, GTA.Font Unselected) Disabled) descriptionTextFonts = ItemResource.descriptionTextFonts;

        protected internal bool isEnabled;
        protected internal bool isSelected;
        protected internal ControlType type;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets description line visibility.
        /// </summary>
        public bool ShowDescriptionLine
        {
            get { return showDescriptionLine; }
            set { showDescriptionLine = value; }
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
        /// Title back colors.
        /// </summary>
        public ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) TitleBackColors
        {
            get { return titleBackColors; }
            set { titleBackColors = value; }
        }
        /// <summary>
        /// The title text of the element.
        /// </summary>
        public string TitleText
        {
            get { return titleText; }
            set { titleText = value; }
        }
        /// <summary>
        /// Title text colors.
        /// </summary>
        public ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) TitleTextColors
        {
            get { return titleTextColors; }
            set { titleTextColors = value; }
        }
        /// <summary>
        /// Title text fonts.
        /// </summary>
        public ((GTA.Font Selected, GTA.Font Unselected) Enabled, (GTA.Font Selected, GTA.Font Unselected) Disabled) TitleTextFonts
        {
            get { return titleTextFonts; }
            set { titleTextFonts = value; }
        }

        /// <summary>
        /// Description line colors.
        /// </summary>
        public ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) DescriptionLineColors
        {
            get { return descriptionLineColors; }
            set { descriptionLineColors = value; }
        }
        /// <summary>
        /// Description back colors.
        /// </summary>
        public ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) DescriptionBackColors
        {
            get { return descriptionBackColors; }
            set { descriptionBackColors = value; }
        }
        /// <summary>
        /// The description text of the element.
        /// </summary>
        public string DescriptionText
        {
            get { return descriptionText; }
            set { descriptionText = value; }
        }
        /// <summary>
        /// Description text colors.
        /// </summary>
        public ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) DescriptionTextColors
        {
            get { return descriptionTextColors; }
            set { descriptionTextColors = value; }
        }
        /// <summary>
        /// Description text fonts.
        /// </summary>
        public ((GTA.Font Selected, GTA.Font Unselected) Enabled, (GTA.Font Selected, GTA.Font Unselected) Disabled) DescriptionTextFonts
        {
            get { return descriptionTextFonts; }
            set { descriptionTextFonts = value; }
        }

        /// <summary>
        /// Gets or sets if the element is enabled or not.
        /// </summary>
        public bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }
        /// <summary>
        /// Gets if the current item is selected or not.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
        }
        /// <summary>
        /// The type of the item.
        /// </summary>
        public ControlType Type
        {
            get { return type; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Gets color for title back.
        /// </summary>
        /// <returns>Returns specific color on set conditions, otherwise returns transparent color.</returns>
        private Color GetTitleBackColor()
        {
            if (isEnabled && isSelected) {
                return titleBackColors.Enabled.Selected;
            }
            else if (isEnabled && !isSelected) {
                return titleBackColors.Enabled.Unselected;
            }
            else if (!isEnabled && isSelected) {
                return titleBackColors.Disabled.Selected;
            }
            else if (!isEnabled && !isSelected) {
                return titleBackColors.Disabled.Unselected;
            }

            return Color.FromArgb(0, 0, 0, 0);
        }
        /// <summary>
        /// Gets color for title text.
        /// </summary>
        /// <returns>Returns specific color on set conditions, otherwise returns transparent color.</returns>
        private Color GetTitleTextColor()
        {
            if (isEnabled && isSelected) {
                return titleTextColors.Enabled.Selected;
            }
            else if (isEnabled && !isSelected) {
                return titleTextColors.Enabled.Unselected;
            }
            else if (!isEnabled && isSelected) {
                return titleTextColors.Disabled.Selected;
            }
            else if (!isEnabled && !isSelected) {
                return titleTextColors.Disabled.Unselected;
            }

            return Color.FromArgb(0, 0, 0, 0);
        }
        /// <summary>
        /// Gets font for title text.
        /// </summary>
        /// <returns>Returns specific font on set conditions, otherwise returns null.</returns>
        private GTA.Font GetTitleTextFont()
        {
            if (isEnabled && isSelected) {
                return titleTextFonts.Enabled.Selected;
            }
            else if (isEnabled && !isSelected) {
                return titleTextFonts.Enabled.Unselected;
            }
            else if (!isEnabled && isSelected) {
                return titleTextFonts.Disabled.Selected;
            }
            else if (!isEnabled && !isSelected) {
                return titleTextFonts.Disabled.Unselected;
            }

            return null;
        }
        /// <summary>
        /// Gets color for description line.
        /// </summary>
        /// <returns>Returns specific color on set conditions, otherwise returns transparent color.</returns>
        protected internal Color GetDescriptionLineColor()
        {
            if (isEnabled && isSelected) {
                return descriptionLineColors.Enabled.Selected;
            }
            else if (isEnabled && !isSelected) {
                return descriptionLineColors.Enabled.Unselected;
            }
            else if (!isEnabled && isSelected) {
                return descriptionLineColors.Disabled.Selected;
            }
            else if (!isEnabled && !isSelected) {
                return descriptionLineColors.Disabled.Unselected;
            }

            return Color.FromArgb(0, 0, 0, 0);
        }
        /// <summary>
        /// Gets color for description back.
        /// </summary>
        /// <returns>Returns specific color on set conditions, otherwise returns transparent color.</returns>
        protected internal Color GetDescriptionBackColor()
        {
            if (isEnabled && isSelected) {
                return descriptionBackColors.Enabled.Selected;
            }
            else if (isEnabled && !isSelected) {
                return descriptionBackColors.Enabled.Unselected;
            }
            else if (!isEnabled && isSelected) {
                return descriptionBackColors.Disabled.Selected;
            }
            else if (!isEnabled && !isSelected) {
                return descriptionBackColors.Disabled.Unselected;
            }

            return Color.FromArgb(0, 0, 0, 0);
        }
        /// <summary>
        /// Gets color for description text.
        /// </summary>
        /// <returns>Returns specific color on set conditions, otherwise returns transparent color.</returns>
        protected internal Color GetDescriptionTextColor()
        {
            if (isEnabled && isSelected) {
                return descriptionTextColors.Enabled.Selected;
            }
            else if (isEnabled && !isSelected) {
                return descriptionTextColors.Enabled.Unselected;
            }
            else if (!isEnabled && isSelected) {
                return descriptionTextColors.Disabled.Selected;
            }
            else if (!isEnabled && !isSelected) {
                return descriptionTextColors.Disabled.Unselected;
            }

            return Color.FromArgb(0, 0, 0, 0);
        }
        /// <summary>
        /// Gets font for description text.
        /// </summary>
        /// <returns>Returns specific font on set conditions, otherwise returns null.</returns>
        protected internal GTA.Font GetDescriptionTextFont()
        {
            if (isEnabled && isSelected) {
                return descriptionTextFonts.Enabled.Selected;
            }
            else if (isEnabled && !isSelected) {
                return descriptionTextFonts.Enabled.Unselected;
            }
            else if (!isEnabled && isSelected) {
                return descriptionTextFonts.Disabled.Selected;
            }
            else if (!isEnabled && !isSelected) {
                return descriptionTextFonts.Disabled.Unselected;
            }

            return null;
        }
        /// <summary>
        /// Restores shared item properties.
        /// </summary>
        private void _Restore()
        {
            ShowDescriptionLine = ItemResource.showDescriptionLine;
            ShowDescription = ItemResource.showDescription;

            TitleBackColors = ItemResource.titleBackColors;
            TitleTextColors = ItemResource.titleTextColors;
            TitleTextFonts = ItemResource.titleTextFonts;

            DescriptionLineColors = ItemResource.descriptionLineColors;
            DescriptionBackColors = ItemResource.descriptionBackColors;
            DescriptionTextColors = ItemResource.descriptionTextColors;
            DescriptionTextFonts = ItemResource.descriptionTextFonts;
        }
        /// <summary>
        /// Restores item.
        /// </summary>
        public abstract void Restore();
        #endregion

        #region Drawing
        protected internal abstract void DrawItem(GraphicsEventArgs args, PointF position, SizeF size);
        #endregion

        /// <summary>
        /// Default menu button item.
        /// </summary>
        public class Button : Item
        {
            /// <summary>
            /// Icon locations.
            /// </summary>
            public enum IconLocations
            {
                /// <summary>
                /// Icon will be placed on the left side.
                /// </summary>
                Left,
                /// <summary>
                /// Icon will be placed on the right side.
                /// </summary>
                Right
            }

            #region Variables
            private bool icon = ItemResource.Button.icon;
            private ((Texture Selected, Texture Unselected) Enabled, (Texture Selected, Texture Unselected) Disabled) icons = ItemResource.Button.icons;
            private IconLocations iconLocation = ItemResource.Button.iconLocation;

            protected internal Menu nestedMenu;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets if button icon should be displayed.
            /// </summary>
            public bool Icon
            {
                get { return icon; }
                set { icon = value; }
            }
            /// <summary>
            /// The icons for this item.
            /// </summary>
            public ((Texture Selected, Texture Unselected) Enabled, (Texture Selected, Texture Unselected) Disabled) Icons
            {
                set { icons = value; }
            }
            /// <summary>
            /// The location of the icon.
            /// </summary>
            public IconLocations IconLocation
            {
                get { return iconLocation; }
                set { iconLocation = value; }
            }

            /// <summary>
            /// Gets or sets the nested menu for this item.
            /// <para>If the user clicks on this item, the nested menu will be displayed.</para>
            /// <para>The nested menu will not display if this item is disabled.</para>
            /// </summary>
            public Menu NestedMenu
            {
                get { return nestedMenu; }
                set {
                    if (value != null) {
                        nestedMenu = value;
                    }
                }
            }
            #endregion

            #region Methods
            /// <summary>
            /// Gets texture for icon.
            /// </summary>
            /// <returns>Returns specific texture on set conditions, otherwise returns null.</returns>
            private Texture GetIcon()
            {
                if (isEnabled && isSelected) {
                    return icons.Enabled.Selected;
                }
                else if (isEnabled && !isSelected) {
                    return icons.Enabled.Unselected;
                }
                else if (!isEnabled && isSelected) {
                    return icons.Disabled.Selected;
                }
                else if (!isEnabled && !isSelected) {
                    return icons.Disabled.Unselected;
                }

                return null;
            }
            /// <summary>
            /// Gets title text position on axis X.
            /// </summary>
            /// <returns>Returns specific float on set conditions, otherwise returns 0f.</returns>
            private float GetTitleTextX()
            {
                switch (iconLocation) {
                    case IconLocations.Left:
                        return 34f;
                    case IconLocations.Right:
                        return 11f;
                }

                return 0f;
            }
            /// <summary>
            /// Gets icon position on axis X.
            /// </summary>
            /// <returns>Returns specific float on set conditions, otherwise returns 0f.</returns>
            private float GetIconX()
            {
                switch (iconLocation) {
                    case IconLocations.Left:
                        return 0f;
                    case IconLocations.Right:
                        return 394f;
                }

                return 0f;
            }
            /// <summary>
            /// Restores button item.
            /// </summary>
            public override void Restore()
            {
                _Restore();

                Icon = ItemResource.Button.icon;
                Icons = ItemResource.Button.icons;
                IconLocation = ItemResource.Button.iconLocation;
            }
            #endregion

            #region Events
            /// <summary>
            /// Raises when the user clicks on this item. It doesn't raise when the item is not enabled.
            /// </summary>
            public event ClickEventHandler OnClick;
            #endregion

            #region EventRaisers
            /// <summary>
            /// Generates OnClick event.
            /// </summary>
            public void PerformClick()
            {
                OnClick?.Invoke();
            }
            #endregion

            #region Constructor
            /// <summary>
            /// Creates new menu button item.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            public Button(Menu menu, string titleText, string descriptionText, bool isEnabled) {
                TitleText = titleText;
                DescriptionText = descriptionText;
                IsEnabled = isEnabled;

                type = ControlType.Button;
                menu.items.Add(this);
            }
            /// <summary>
            /// Creates new menu button item with nested menu.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            /// <param name="nestedMenu">The nested menu of this item.</param>
            public Button(Menu menu, string titleText, string descriptionText, bool isEnabled, Menu nestedMenu) {
                TitleText = titleText;
                DescriptionText = descriptionText;
                IsEnabled = isEnabled;
                NestedMenu = nestedMenu;

                type = ControlType.Button;
                menu.items.Add(this);
            }
            /// <summary>
            /// Creates new menu button item with callback.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            /// <param name="onClick">The callback of this item.</param>
            public Button(Menu menu, string titleText, string descriptionText, bool isEnabled, ClickEventHandler onClick) {
                TitleText = titleText;
                DescriptionText = descriptionText;
                IsEnabled = isEnabled;
                OnClick += onClick;

                type = ControlType.Button;
                menu.items.Add(this);
            }
            /// <summary>
            /// Creates new menu button item with nested menu and callback.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            /// <param name="nestedMenu">The nested menu of this item.</param>
            /// <param name="onClick">The callback of this item.</param>
            public Button(Menu menu, string titleText, string descriptionText, bool isEnabled, Menu nestedMenu, ClickEventHandler onClick) {
                TitleText = titleText;
                DescriptionText = descriptionText;
                IsEnabled = isEnabled;
                NestedMenu = nestedMenu;
                OnClick += onClick;

                type = ControlType.Button;
                menu.items.Add(this);
            }
            #endregion

            #region Drawing
            protected internal override void DrawItem(GraphicsEventArgs args, PointF position, SizeF size)
            {
                Size titleTextSize = TextRenderer.MeasureText(titleText, GetTitleTextFont().WindowsFont); // Get title text size

                args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y, size.Width, size.Height), GetTitleBackColor());
                if (icon && GetIcon() != null) {
                    args.Graphics.DrawText(titleText, new RectangleF(position.X + GetTitleTextX(), position.Y + 5f, 385f, titleTextSize.Height), TextAlignment.Left, GetTitleTextColor(), GetTitleTextFont());
                    args.Graphics.DrawSprite(GetIcon(), new RectangleF(position.X + GetIconX(), position.Y, 38f.ApplySizeOffset(), 38f.ApplySizeOffset()));
                }
                else {
                    args.Graphics.DrawText(titleText, new RectangleF(position.X + 11f, position.Y + 5f, 420f, titleTextSize.Height), TextAlignment.Left, GetTitleTextColor(), GetTitleTextFont());
                }
            }
            #endregion
        }

        /// <summary>
        /// Default menu checkbox item.
        /// </summary>
        public class CheckBox : Item
        {
            /// <summary>
            /// All toggle modes.
            /// </summary>
            public enum ToggleMode
            {
                /// <summary>
                /// Text: use text.
                /// </summary>
                Icon,
                /// <summary>
                /// Icon: use icons.
                /// </summary>
                Text
            }

            #region Variables
            private ToggleMode mode = ItemResource.CheckBox.mode;

            protected internal bool isChecked;

            private static (((Texture Checked, Texture Unchecked) Selected, (Texture Checked, Texture Unchecked) Unselected) Enabled, ((Texture Checked, Texture Unchecked) Selected, (Texture Checked, Texture Unchecked) Unselected) Disabled) icons = ItemResource.CheckBox.icons;

            private (string Checked, string Unchecked) toggleTexts = ItemResource.CheckBox.toggleTexts;
            private (((Color Checked, Color Unchecked) Selected, (Color Checked, Color Unchecked) Unselected) Enabled, ((Color Checked, Color Unchecked) Selected, (Color Checked, Color Unchecked) Unselected) Disabled) toggleTextColors = ItemResource.CheckBox.toggleTextColors;
            private (((GTA.Font Checked, GTA.Font Unchecked) Selected, (GTA.Font Checked, GTA.Font Unchecked) Unselected) Enabled, ((GTA.Font Checked, GTA.Font Unchecked) Selected, (GTA.Font Checked, GTA.Font Unchecked) Unselected) Disabled) toggleTextFonts = ItemResource.CheckBox.toggleTextFonts;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets item mode.
            /// </summary>
            public ToggleMode Mode
            {
                get { return mode; }
                set { mode = value; }
            }

            /// <summary>
            /// Gets or sets if the item is checked.
            /// </summary>
            public bool IsChecked
            {
                get { return isChecked; }
                set { isChecked = value; }
            }

            /// <summary>
            /// The icons for this item.
            /// </summary>
            public (((Texture Checked, Texture Unchecked) Selected, (Texture Checked, Texture Unchecked) Unselected) Enabled, ((Texture Checked, Texture Unchecked) Selected, (Texture Checked, Texture Unchecked) Unselected) Disabled) Icons
            {
                set { icons = value; }
            }

            /// <summary>
            /// The toggle texts for this item.
            /// </summary>
            public (string Checked, string Unchecked) ToggleTexts
            {
                get { return ToggleTexts; }
                set { ToggleTexts = value; }
            }
            /// <summary>
            /// The toggle text colors for this item.
            /// </summary>
            public (((Color Checked, Color Unchecked) Selected, (Color Checked, Color Unchecked) Unselected) Enabled, ((Color Checked, Color Unchecked) Selected, (Color Checked, Color Unchecked) Unselected) Disabled) ToggleTextColors
            {
                get { return toggleTextColors; }
                set { toggleTextColors = value; }
            }
            /// <summary>
            /// The toggle text fonts for this item.
            /// </summary>
            public (((GTA.Font Checked, GTA.Font Unchecked) Selected, (GTA.Font Checked, GTA.Font Unchecked) Unselected) Enabled, ((GTA.Font Checked, GTA.Font Unchecked) Selected, (GTA.Font Checked, GTA.Font Unchecked) Unselected) Disabled) ToggleTextFonts
            {
                get { return toggleTextFonts; }
                set { toggleTextFonts = value; }
            }
            #endregion

            #region Methods
            /// <summary>
            /// Gets texture for icon.
            /// </summary>
            /// <returns>Returns specific texture on set conditions, otherwise returns null.</returns>
            private Texture GetIcon()
            {
                if (isEnabled && isSelected && isChecked) {
                    return icons.Enabled.Selected.Checked;
                }
                else if (isEnabled && isSelected && !isChecked) {
                    return icons.Enabled.Selected.Unchecked;
                }
                else if (isEnabled && !isSelected && isChecked) {
                    return icons.Enabled.Unselected.Checked;
                }
                else if (isEnabled && !isSelected && !isChecked) {
                    return icons.Enabled.Unselected.Unchecked;
                }
                else if (!isEnabled && isSelected && isChecked) {
                    return icons.Disabled.Selected.Checked;
                }
                else if (!isEnabled && isSelected && !isChecked) {
                    return icons.Disabled.Selected.Unchecked;
                }
                else if (!isEnabled && !isSelected && isChecked) {
                    return icons.Disabled.Unselected.Checked;
                }
                else if (!isEnabled && !isSelected && !isChecked) {
                    return icons.Disabled.Unselected.Unchecked;
                }

                return null;
            }
            /// <summary>
            /// Gets toggle text.
            /// </summary>
            /// <returns>Returns specific text on set conditions.</returns>
            private string GetToggleText()
            {
                if (isChecked) {
                    return toggleTexts.Checked;
                }
                else {
                    return toggleTexts.Unchecked;
                }
            }
            /// <summary>
            /// Gets color for toggle text.
            /// </summary>
            /// <returns>Returns specific color on set conditions, otherwise returns transparent color.</returns>
            private Color GetToggleTextColor()
            {
                if (isEnabled && isSelected && isChecked) {
                    return toggleTextColors.Enabled.Selected.Checked;
                }
                else if (isEnabled && isSelected && !isChecked) {
                    return toggleTextColors.Enabled.Selected.Unchecked;
                }
                else if (isEnabled && !isSelected && isChecked) {
                    return toggleTextColors.Enabled.Unselected.Checked;
                }
                else if (isEnabled && !isSelected && !isChecked) {
                    return toggleTextColors.Enabled.Unselected.Unchecked;
                }
                else if (!isEnabled && isSelected && isChecked) {
                    return toggleTextColors.Disabled.Selected.Checked;
                }
                else if (!isEnabled && isSelected && !isChecked) {
                    return toggleTextColors.Disabled.Selected.Unchecked;
                }
                else if (!isEnabled && !isSelected && isChecked) {
                    return toggleTextColors.Disabled.Unselected.Checked;
                }
                else if (!isEnabled && !isSelected && !isChecked) {
                    return toggleTextColors.Disabled.Unselected.Unchecked;
                }

                return Color.FromArgb(0, 0, 0, 0);
            }
            /// <summary>
            /// Gets font for toggle text.
            /// </summary>
            /// <returns>Returns specific font on set conditions, otherwise returns null.</returns>
            private GTA.Font GetToggleTextFont()
            {
                if (isEnabled && isSelected && isChecked) {
                    return toggleTextFonts.Enabled.Selected.Checked;
                }
                else if (isEnabled && isSelected && !isChecked) {
                    return toggleTextFonts.Enabled.Selected.Unchecked;
                }
                else if (isEnabled && !isSelected && isChecked) {
                    return toggleTextFonts.Enabled.Unselected.Checked;
                }
                else if (isEnabled && !isSelected && !isChecked) {
                    return toggleTextFonts.Enabled.Unselected.Unchecked;
                }
                else if (!isEnabled && isSelected && isChecked) {
                    return toggleTextFonts.Disabled.Selected.Checked;
                }
                else if (!isEnabled && isSelected && !isChecked) {
                    return toggleTextFonts.Disabled.Selected.Unchecked;
                }
                else if (!isEnabled && !isSelected && isChecked) {
                    return toggleTextFonts.Disabled.Unselected.Checked;
                }
                else if (!isEnabled && !isSelected && !isChecked) {
                    return toggleTextFonts.Disabled.Unselected.Unchecked;
                }

                return null;
            }
            /// <summary>
            /// Restores checkbox item.
            /// </summary>
            public override void Restore()
            {
                _Restore();

                Mode = ItemResource.CheckBox.mode;

                Icons = ItemResource.CheckBox.icons;

                ToggleTextColors = ItemResource.CheckBox.toggleTextColors;
                ToggleTextFonts = ItemResource.CheckBox.toggleTextFonts;
            }
            #endregion

            #region Events
            /// <summary>
            /// Raises when the check state changes. It doesn't raise when the item is not enabled.
            /// </summary>
            public event CheckedChangedEventHandler OnCheckedChanged;
            #endregion

            #region EventRaisers
            /// <summary>
            /// Generates OnCheckedChanged event.
            /// </summary>
            public void CheckedChangedRaiser()
            {
                OnCheckedChanged?.Invoke(isChecked); // RaiseEvent
            }
            #endregion

            #region Constructor
            /// <summary>
            /// Creates new menu checkbox item.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="isChecked">Sets if the checkbox should be checked from the beginning.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            public CheckBox(Menu menu, string titleText, string descriptionText, bool isChecked, bool isEnabled)
            {
                TitleText = titleText;
                DescriptionText = descriptionText;
                IsChecked = isChecked;
                IsEnabled = isEnabled;

                type = ControlType.CheckBox;
                menu.items.Add(this);
            }
            /// <summary>
            /// Creates new menu checkbox item with callback.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="isChecked">Sets if the checkbox should be checked from the beginning.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            /// <param name="onCheckedChanged">The callback of this item.</param>
            public CheckBox(Menu menu, string titleText, string descriptionText, bool isChecked, bool isEnabled, CheckedChangedEventHandler onCheckedChanged)
            {
                TitleText = titleText;
                DescriptionText = descriptionText;
                IsChecked = isChecked;
                IsEnabled = isEnabled;
                OnCheckedChanged += onCheckedChanged;

                type = ControlType.CheckBox;
                menu.items.Add(this);
            }
            #endregion

            #region Drawing
            protected internal override void DrawItem(GraphicsEventArgs args, PointF position, SizeF size)
            {
                Size titleTextSize = TextRenderer.MeasureText(titleText, GetTitleTextFont().WindowsFont); // Get title text size

                args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y, size.Width, size.Height), GetTitleBackColor());
                args.Graphics.DrawText(titleText, new RectangleF(position.X + 11f, position.Y + 5f, 380f, titleTextSize.Height), TextAlignment.Left, GetTitleTextColor(), GetTitleTextFont());
                if (mode == ToggleMode.Icon) {
                    args.Graphics.DrawSprite(GetIcon(), new RectangleF(position.X + 394f, position.Y, 38f.ApplySizeOffset(), 38f.ApplySizeOffset()));
                }
                else if (mode == ToggleMode.Text) {
                    Size toggleTextSize = TextRenderer.MeasureText(GetToggleText(), GetToggleTextFont().WindowsFont);
                    float toggleTextWidth = Math.Min(200f, toggleTextSize.Width - (GetToggleText().Length * 2) - (GetToggleText().Length == 0 ? 0 : 10f));

                    args.Graphics.DrawText(GetToggleText(), new RectangleF(position.X + 421f - toggleTextWidth, position.Y + 5f, toggleTextWidth, toggleTextSize.Height), TextAlignment.Right, GetToggleTextColor(), GetToggleTextFont());
                }
            }
            #endregion
        }

        /// <summary>
        /// Default menu list item.
        /// </summary>
        public class List : Item
        {
            /// <summary>
            /// All types of arrows visibility.
            /// </summary>
            public enum ArrowsVisibility
            {
                /// <summary>
                /// Never: never show arrows.
                /// </summary>
                Never,
                /// <summary>
                /// Selected: show arrows only if list item is enabled and selected.
                /// </summary>
                EnabledSelected,
                /// <summary>
                /// Always: always show arrows.
                /// </summary>
                Always
            }

            #region Variables
            private float maximumListWidth = ItemResource.List.maximumListWidth;
            private ArrowsVisibility showArrows = ItemResource.List.showArrows;
            protected internal bool infiniteScroll = ItemResource.List.infiniteScroll;

            protected internal IList<string> itemList = new List<string>();

            private ((Texture Selected, Texture Unselected) Enabled, (Texture Selected, Texture Unselected) Disabled) iconsArrowLeft = ItemResource.List.iconsArrowLeft;
            private ((Texture Selected, Texture Unselected) Enabled, (Texture Selected, Texture Unselected) Disabled) iconsArrowRight = ItemResource.List.iconsArrowRight;

            private ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) selectedTextColors = ItemResource.List.selectedTextColors;
            private ((GTA.Font Selected, GTA.Font Unselected) Enabled, (GTA.Font Selected, GTA.Font Unselected) Disabled) selectedTextFonts = ItemResource.List.selectedTextFonts;
            
            protected internal int selectedIndex;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the maximum width of the list.
            /// </summary>
            public float MaximumListWidth
            {
                get { return maximumListWidth; }
                set {
                    if (value < 1) {
                        maximumListWidth = ItemResource.List.maximumListWidth;
                    }
                    else {
                        maximumListWidth = value;
                    }
                }
            }
            /// <summary>
            /// This gets or sets arrows visibility.
            /// </summary>
            public ArrowsVisibility ShowArrows
            {
                get { return showArrows; }
                set { showArrows = value; }
            }
            /// <summary>
            /// This will enable or disable infinite scrolling in list.
            /// </summary>
            public bool InfiniteScroll
            {
                get { return infiniteScroll; }
                set { infiniteScroll = value; }
            }

            /// <summary>
            /// Gets the item list.
            /// </summary>
            public IList<string> ItemList
            {
                get { return itemList; }
                private set { itemList = value; }
            }

            /// <summary>
            /// The left arrow icons for this item.
            /// </summary>
            public ((Texture Selected, Texture Unselected) Enabled, (Texture Selected, Texture Unselected) Disabled) IconsArrowLeft
            {
                set { iconsArrowLeft = value; }
            }
            /// <summary>
            /// The right arrow icons for this item.
            /// </summary>
            public ((Texture Selected, Texture Unselected) Enabled, (Texture Selected, Texture Unselected) Disabled) IconsArrowRight
            {
                set { iconsArrowRight = value; }
            }

            /// <summary>
            /// Selected text colors of the element.
            /// </summary>
            public ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) SelectedTextColors
            {
                get { return selectedTextColors; }
                set { selectedTextColors = value; }
            }
            /// <summary>
            /// Selected text fonts of the element.
            /// </summary>
            public ((GTA.Font Selected, GTA.Font Unselected) Enabled, (GTA.Font Selected, GTA.Font Unselected) Disabled) SelectedTextFonts
            {
                get { return selectedTextFonts; }
                set { selectedTextFonts = value; }
            }
            
            /// <summary>
            /// Gets or sets the selected index of the list.
            /// </summary>
            public int SelectedIndex
            {
                get { return selectedIndex; }
                set {
                    if (itemList.Count != 0) {
                        if (value < 0) {
                            selectedIndex = 0;
                        }
                        else if (value >= itemList.Count) {
                            selectedIndex = itemList.Count - 1;
                        }
                        else {
                            selectedIndex = value;
                        }
                    }
                }
            }
            #endregion

            #region Methods
            /// <summary>
            /// Adds an items to the list of items.
            /// </summary>
            /// <param name="item">The item that should be added to the list of items.</param>
            public void Add(string item)
            {
                itemList.Add(item);
            }
            /// <summary>
            /// Adds an array of items to the list of items.
            /// </summary>
            /// <param name="items">The array of items that should be added to the list of items.</param>
            public void AddRange(string[] items)
            {
                try {
                    for (int i = 0; i < items.Length; i++) {
                        itemList.Add(items[i]);
                    }
                }
                catch (Exception ex) {
                    Game.Console.Print("NativeUI - Error while adding items to the list of items. Details: " + ex.Message);
                }
            }
            /// <summary>
            /// This removes the specific item from the list of items through it's name.
            /// </summary>
            /// <param name="item">The item that should be removed</param>
            /// <returns>Returns true if the item was deleted, otherwise false.</returns>
            public bool Remove(string item)
            {
                try {
                    for (int i = 0; i < itemList.Count; i++) {
                        if (itemList[i] == item) {
                            itemList.RemoveAt(i);
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex) {
                    Game.Console.Print("NativeUI - Error while removing an item from the list through it's name. Details: " + ex.Message);
                    return false;
                }
            }
            /// <summary>
            /// This removes the specific item from the list of items through it's index.
            /// </summary>
            /// <param name="index">The index where the item is at</param>
            /// <returns>Returns true if the item was deleted, otherwise false.</returns>
            public bool RemoveAt(int index)
            {
                try {
                    for (int i = 0; i < itemList.Count; i++) {
                        if (i == index) {
                            itemList.RemoveAt(i);
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex) {
                    Game.Console.Print("NativeUI - Error while removing an item from the list through it's index. Details: " + ex.Message);
                    return false;
                }
            }
            /// <summary>
            /// This removes all items from the list of items.
            /// </summary>
            public void RemoveAll()
            {
                itemList.Clear();
            }
            /// <summary>
            /// Gets left arrow texture for icon.
            /// </summary>
            /// <returns>Returns specific texture on set conditions, otherwise returns null.</returns>
            private Texture GetArrowLeftIcon()
            {
                if (selectedIndex == 0 && !infiniteScroll && isSelected) {
                    return iconsArrowLeft.Disabled.Selected;
                }
                else if (selectedIndex == 0 && !infiniteScroll && !isSelected) {
                    return iconsArrowLeft.Disabled.Unselected;
                }
                
                if (isEnabled && isSelected) {
                    return iconsArrowLeft.Enabled.Selected;
                }
                else if (isEnabled && !isSelected) {
                    return iconsArrowLeft.Enabled.Unselected;
                }
                else if (!isEnabled && isSelected) {
                    return iconsArrowLeft.Disabled.Selected;
                }
                else if (!isEnabled && !isSelected) {
                    return iconsArrowLeft.Disabled.Unselected;
                }

                return null;
            }
            /// <summary>
            /// Gets right arrow texture for icon.
            /// </summary>
            /// <returns>Returns specific texture on set conditions, otherwise returns null.</returns>
            private Texture GetArrowRightIcon()
            {
                if (selectedIndex == (itemList.Count - 1) && !infiniteScroll && isSelected) {
                    return iconsArrowRight.Disabled.Selected;
                }
                else if (selectedIndex == (itemList.Count - 1) && !infiniteScroll && !isSelected) {
                    return iconsArrowRight.Disabled.Unselected;
                }

                if (isEnabled && isSelected) {
                    return iconsArrowRight.Enabled.Selected;
                }
                else if (isEnabled && !isSelected) {
                    return iconsArrowRight.Enabled.Unselected;
                }
                else if (!isEnabled && isSelected) {
                    return iconsArrowRight.Disabled.Selected;
                }
                else if (!isEnabled && !isSelected) {
                    return iconsArrowRight.Disabled.Unselected;
                }

                return null;
            }
            /// <summary>
            /// Gets color for selected text.
            /// </summary>
            /// <returns>Returns specific color on set conditions, otherwise returns transparent color.</returns>
            private Color GetSelectedTextColor()
            {
                if (isEnabled && isSelected) {
                    return selectedTextColors.Enabled.Selected;
                }
                else if (isEnabled && !isSelected) {
                    return selectedTextColors.Enabled.Unselected;
                }
                else if (!isEnabled && isSelected) {
                    return selectedTextColors.Disabled.Selected;
                }
                else if (!isEnabled && !isSelected) {
                    return selectedTextColors.Disabled.Unselected;
                }

                return Color.FromArgb(0, 0, 0, 0);
            }
            /// <summary>
            /// Gets font for selected text.
            /// </summary>
            /// <returns>Returns specific font on set conditions, otherwise returns null.</returns>
            private GTA.Font GetSelectedTextFont()
            {
                if (isEnabled && isSelected) {
                    return selectedTextFonts.Enabled.Selected;
                }
                else if (isEnabled && !isSelected) {
                    return selectedTextFonts.Enabled.Unselected;
                }
                else if (!isEnabled && isSelected) {
                    return selectedTextFonts.Disabled.Selected;
                }
                else if (!isEnabled && !isSelected) {
                    return selectedTextFonts.Disabled.Unselected;
                }

                return null;
            }
            /// <summary>
            /// Restores list item.
            /// </summary>
            public override void Restore()
            {
                _Restore();

                MaximumListWidth = ItemResource.List.maximumListWidth;
                ShowArrows = ItemResource.List.showArrows;
                InfiniteScroll = ItemResource.List.infiniteScroll;

                IconsArrowLeft = ItemResource.List.iconsArrowLeft;
                IconsArrowRight = ItemResource.List.iconsArrowRight;

                SelectedTextColors = ItemResource.List.selectedTextColors;
                SelectedTextFonts = ItemResource.List.selectedTextFonts;
            }
            #endregion

            #region Events
            /// <summary>
            /// Raises when the user clicks on this item. It doesn't raise when the item is not enabled.
            /// </summary>
            public event ClickEventHandler OnClick;
            /// <summary>
            /// Raises when the selected index of the list changes.
            /// </summary>
            public event SelectedIndexChangedEventHandler OnSelectedIndexChanged;
            #endregion

            #region EventRaisers
            /// <summary>
            /// Generates OnClick event.
            /// </summary>
            public void PerformClick()
            {
                OnClick?.Invoke();
            }
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
            /// Creates new menu list item.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            public List(Menu menu, string titleText, string descriptionText, bool isEnabled)
            {
                TitleText = titleText;
                DescriptionText = descriptionText;
                IsEnabled = isEnabled;

                type = ControlType.List;
                menu.items.Add(this);
            }
            /// <summary>
            /// Creates new menu list item with list of items.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="itemList">Your custom list of items.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            public List(Menu menu, string titleText, string descriptionText, List<string> itemList, bool isEnabled)
            {
                TitleText = titleText;
                DescriptionText = descriptionText;
                ItemList = itemList;
                IsEnabled = isEnabled;

                type = ControlType.List;
                menu.items.Add(this);
            }
            /// <summary>
            /// Creates new menu list item with callbacks.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            /// <param name="onClick">The callback of this item.</param>
            /// <param name="onSelectedIndexChanged">The callback of this item.</param>
            public List(Menu menu, string titleText, string descriptionText, bool isEnabled, ClickEventHandler onClick, SelectedIndexChangedEventHandler onSelectedIndexChanged)
            {
                TitleText = titleText;
                DescriptionText = descriptionText;
                IsEnabled = isEnabled;
                OnClick += onClick;
                OnSelectedIndexChanged += onSelectedIndexChanged;

                type = ControlType.List;
                menu.items.Add(this);
            }
            /// <summary>
            /// Creates new menu list item with list of items with callbacks.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="itemList">Your custom list of items.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            /// <param name="onClick">The callback of this item.</param>
            /// <param name="onSelectedIndexChanged">The callback of this item.</param>
            public List(Menu menu, string titleText, string descriptionText, List<string> itemList, bool isEnabled, ClickEventHandler onClick, SelectedIndexChangedEventHandler onSelectedIndexChanged)
            {
                TitleText = titleText;
                DescriptionText = descriptionText;
                ItemList = itemList;
                IsEnabled = isEnabled;
                OnClick += onClick;
                OnSelectedIndexChanged += onSelectedIndexChanged;

                type = ControlType.List;
                menu.items.Add(this);
            }
            #endregion

            #region Drawing
            protected internal override void DrawItem(GraphicsEventArgs args, PointF position, SizeF size)
            {
                Size titleTextSize = TextRenderer.MeasureText(titleText, GetTitleTextFont().WindowsFont); // Get title text size

                string selectedText = "-";
                if (itemList.Count != 0) {
                    selectedText = itemList[selectedIndex];
                }

                Size selectedTextSize = TextRenderer.MeasureText(selectedText, GetSelectedTextFont().WindowsFont);
                float selectedTextWidth = Math.Min(maximumListWidth, selectedTextSize.Width - (selectedText.Length * 2) - (selectedText.Length == 0 ? 0 : 10f));

                args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y, size.Width, size.Height), GetTitleBackColor());
                args.Graphics.DrawText(titleText, new RectangleF(position.X + 11f, position.Y + 5f, 365f - selectedTextWidth, titleTextSize.Height), TextAlignment.Left, GetTitleTextColor(), GetTitleTextFont());

                if (itemList.Count != 0 && (isEnabled && isSelected && showArrows == ArrowsVisibility.EnabledSelected || showArrows == ArrowsVisibility.Always)) {
                    args.Graphics.DrawSprite(GetArrowLeftIcon(), new RectangleF(position.X + 381f - (selectedTextWidth + 4), position.Y, 38f.ApplySizeOffset(), 38f.ApplySizeOffset()));
                    args.Graphics.DrawText(selectedText, new RectangleF(position.X + 408f - selectedTextWidth, position.Y + 5f, selectedTextWidth, selectedTextSize.Height), TextAlignment.Center, GetSelectedTextColor(), GetSelectedTextFont());
                    args.Graphics.DrawSprite(GetArrowRightIcon(), new RectangleF(position.X + 396f, position.Y, 38f.ApplySizeOffset(), 38f.ApplySizeOffset()));
                }
                else {
                    args.Graphics.DrawText(selectedText, new RectangleF(position.X + 421f - selectedTextWidth, position.Y + 5f, selectedTextWidth, selectedTextSize.Height), TextAlignment.Right, GetSelectedTextColor(), GetSelectedTextFont());
                }
            }
            #endregion
        }

        /// <summary>
        /// Default menu slider item.
        /// </summary>
        public class Slider : Item
        {
            #region Variables
            private PointF sliderPositionOffset = ItemResource.Slider.sliderPositionOffset;
            private SizeF sliderSizeOffset = ItemResource.Slider.sliderSizeOffset;
            private SizeF sliderBorderSize = ItemResource.Slider.sliderBorderSize;
            protected internal bool infiniteScroll = ItemResource.Slider.infiniteScroll;

            protected internal int minimumValue = 0;
            protected internal int maximumValue = 1;
            protected internal int currentValue = 0;

            private ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) sliderBorderColors = ItemResource.Slider.sliderBorderColors;
            private ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) sliderBackColors = ItemResource.Slider.sliderBackColors;
            private ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) sliderForeColors = ItemResource.Slider.sliderForeColors;
            #endregion

            #region Properties
            /// <summary>
            /// Gets or sets the position offsets of the slider.
            /// </summary>
            public PointF SliderPositionOffset
            {
                get { return sliderPositionOffset; }
                set { sliderPositionOffset = value; }
            }
            /// <summary>
            /// Gets or sets the size offsets of the slider.
            /// </summary>
            public SizeF SliderSizeOffset
            {
                get { return sliderSizeOffset; }
                set { sliderSizeOffset = value; }
            }
            /// <summary>
            /// Gets or sets the size of the slider border.
            /// </summary>
            public SizeF SliderBorderSize
            {
                get { return sliderBorderSize; }
                set { sliderBorderSize = value; }
            }
            /// <summary>
            /// This will enable or disable infinite scrolling in slider.
            /// </summary>
            public bool InfiniteScroll
            {
                get { return infiniteScroll; }
                set { infiniteScroll = value; }
            }

            /// <summary>
            /// Gets or sets the minimum value of the slider.
            /// </summary>
            public int MinimumValue
            {
                get { return minimumValue; }
                set { 
                    if (value >= maximumValue) {
                        minimumValue = maximumValue - 1;
                    }
                    else {
                        minimumValue = value;
                    }
                }
            }
            /// <summary>
            /// Gets or sets the maximum value of the slider.
            /// </summary>
            public int MaximumValue
            {
                get { return maximumValue; }
                set { 
                    if (value <= minimumValue) {
                        maximumValue = minimumValue + 1;
                    }
                    else {
                        maximumValue = value;
                    }
                }
            }
            /// <summary>
            /// Gets or sets the current value of the slider.
            /// </summary>
            public int CurrentValue
            {
                get { return currentValue; }
                set {
                    if (value < minimumValue) {
                        currentValue = minimumValue;
                    }
                    else if (value > maximumValue) {
                        currentValue = maximumValue;
                    }
                    else {
                        currentValue = value;
                    }
                }
            }

            /// <summary>
            /// The border colors of slider item.
            /// </summary>
            public ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) SliderBorderColors
            {
                get { return sliderBorderColors; }
                set { sliderBorderColors = value; }
            }
            /// <summary>
            /// The back colors of slider item.
            /// </summary>
            public ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) SliderBackColors
            {
                get { return sliderBackColors; }
                set { sliderBackColors = value; }
            }
            /// <summary>
            /// The fore colors of slider item.
            /// </summary>
            public ((Color Selected, Color Unselected) Enabled, (Color Selected, Color Unselected) Disabled) SliderForeColors
            {
                get { return sliderForeColors; }
                set { sliderForeColors = value; }
            }
            #endregion
            
            /// <summary>
            /// Gets color for slider border.
            /// </summary>
            /// <returns>Returns specific color on set conditions, otherwise returns transparent color.</returns>
            private Color GetSliderBorderColor()
            {
                if (isEnabled && isSelected) {
                    return sliderBorderColors.Enabled.Selected;
                }
                else if (isEnabled && !isSelected) {
                    return sliderBorderColors.Enabled.Unselected;
                }
                else if (!isEnabled && isSelected) {
                    return sliderBorderColors.Disabled.Selected;
                }
                else if (!isEnabled && !isSelected) {
                    return sliderBorderColors.Disabled.Unselected;
                }

                return Color.FromArgb(0, 0, 0, 0);
            }
            #region Methods
            /// <summary>
            /// Gets color for slider background.
            /// </summary>
            /// <returns>Returns specific color on set conditions, otherwise returns transparent color.</returns>
            private Color GetSliderBackColor()
            {
                if (isEnabled && isSelected) {
                    return sliderBackColors.Enabled.Selected;
                }
                else if (isEnabled && !isSelected) {
                    return sliderBackColors.Enabled.Unselected;
                }
                else if (!isEnabled && isSelected) {
                    return sliderBackColors.Disabled.Selected;
                }
                else if (!isEnabled && !isSelected) {
                    return sliderBackColors.Disabled.Unselected;
                }

                return Color.FromArgb(0, 0, 0, 0);
            }
            /// <summary>
            /// Gets color for slider foreground.
            /// </summary>
            /// <returns>Returns specific color on set conditions, otherwise returns transparent color.</returns>
            private Color GetSliderForeColor()
            {
                if (isEnabled && isSelected) {
                    return sliderForeColors.Enabled.Selected;
                }
                else if (isEnabled && !isSelected) {
                    return sliderForeColors.Enabled.Unselected;
                }
                else if (!isEnabled && isSelected) {
                    return sliderForeColors.Disabled.Selected;
                }
                else if (!isEnabled && !isSelected) {
                    return sliderForeColors.Disabled.Unselected;
                }

                return Color.FromArgb(0, 0, 0, 0);
            }
            /// <summary>
            /// Restores slider item.
            /// </summary>
            public override void Restore()
            {
                _Restore();

                SliderPositionOffset = ItemResource.Slider.sliderPositionOffset;
                SliderSizeOffset = ItemResource.Slider.sliderSizeOffset;
                SliderBorderSize = ItemResource.Slider.sliderBorderSize;
                InfiniteScroll = ItemResource.Slider.infiniteScroll;

                SliderBorderColors = ItemResource.Slider.sliderBorderColors;
                SliderBackColors = ItemResource.Slider.sliderBackColors;
                SliderForeColors = ItemResource.Slider.sliderForeColors;
            }
            #endregion

            #region Events
            /// <summary>
            /// Raises when the user clicks on this item. It doesn't raise when the item is not enabled.
            /// </summary>
            public event ClickEventHandler OnClick;
            /// <summary>
            /// Raises when the current value of the slider changes.
            /// </summary>
            public event SelectedIndexChangedEventHandler OnCurrentValueChanged;
            #endregion

            #region EventRaisers
            /// <summary>
            /// Generates OnClick event.
            /// </summary>
            public void PerformClick()
            {
                OnClick?.Invoke();
            }
            /// <summary>
            /// Generates OnCurrentValueChanged event.
            /// </summary>
            public void CurrentValueChangedRaiser()
            {
                OnCurrentValueChanged?.Invoke(currentValue); // RaiseEvent
            }
            #endregion

            #region Constructor
            /// <summary>
            /// Creates new menu slider item.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="minimumValue">The minimum value of this item.</param>
            /// <param name="maximumValue">The maximum value of this item.</param>
            /// <param name="currentValue">The current value of this item.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            public Slider(Menu menu, string titleText, string descriptionText, int minimumValue, int maximumValue, int currentValue, bool isEnabled)
            {
                TitleText = titleText;
                DescriptionText = descriptionText;
                MinimumValue = minimumValue;
                MaximumValue = maximumValue;
                CurrentValue = currentValue;
                IsEnabled = isEnabled;

                type = ControlType.Slider;
                menu.items.Add(this);
            }
            /// <summary>
            /// Creates new menu slider item with callbacks.
            /// </summary>
            /// <param name="menu">The menu of this item.</param>
            /// <param name="titleText">The title text of this item.</param>
            /// <param name="descriptionText">The description text of this item.</param>
            /// <param name="minimumValue">The minimum value of this item.</param>
            /// <param name="maximumValue">The maximum value of this item.</param>
            /// <param name="currentValue">The current value of this item.</param>
            /// <param name="isEnabled">Sets if the item should be enabled from the beginning, or not.</param>
            /// <param name="onClick">The callback of this item.</param>
            /// <param name="onCurrentValueChanged">The callback of this item.</param>
            public Slider(Menu menu, string titleText, string descriptionText, int minimumValue, int maximumValue, int currentValue, bool isEnabled, ClickEventHandler onClick, SelectedIndexChangedEventHandler onCurrentValueChanged)
            {
                TitleText = titleText;
                DescriptionText = descriptionText;
                MinimumValue = minimumValue;
                MaximumValue = maximumValue;
                CurrentValue = currentValue;
                IsEnabled = isEnabled;
                OnClick += onClick;
                OnCurrentValueChanged += onCurrentValueChanged;

                type = ControlType.Slider;
                menu.items.Add(this);
            }
            #endregion

            #region Drawing
            protected internal override void DrawItem(GraphicsEventArgs args, PointF position, SizeF size)
            {
                Size titleTextSize = TextRenderer.MeasureText(titleText, GetTitleTextFont().WindowsFont); // Get title text size

                PointF sliderPosition = new PointF(position.X + 237f - (sliderSizeOffset.Width / 2) + sliderPositionOffset.X, position.Y + 12f - (sliderSizeOffset.Height / 2) + sliderPositionOffset.Y);
                SizeF sliderSize = new SizeF(ItemResource.Slider.sliderSize.Width + sliderSizeOffset.Width, ItemResource.Slider.sliderSize.Height + sliderSizeOffset.Height);
                float sliderForeWidth = (currentValue - minimumValue) * (sliderSize.Width / (maximumValue - minimumValue));

                args.Graphics.DrawRectangle(new RectangleF(position.X, position.Y, size.Width, size.Height), GetTitleBackColor());
                args.Graphics.DrawText(titleText, new RectangleF(position.X + 11f, position.Y + 5f, 420f, titleTextSize.Height), TextAlignment.Left, GetTitleTextColor(), GetTitleTextFont());
                
                args.Graphics.DrawRectangle(new RectangleF(sliderPosition.X - sliderBorderSize.Width, sliderPosition.Y - sliderBorderSize.Height, sliderSize.Width + (sliderBorderSize.Width * 2), sliderSize.Height + (sliderBorderSize.Height * 2)), GetSliderBorderColor());
                args.Graphics.DrawRectangle(new RectangleF(sliderPosition.X, sliderPosition.Y, sliderSize.Width, sliderSize.Height), GetSliderBackColor());
                args.Graphics.DrawRectangle(new RectangleF(sliderPosition.X, sliderPosition.Y, sliderForeWidth, sliderSize.Height), GetSliderForeColor());
            }
            #endregion
        }

    }
}
