using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using GTA;
using NativeUI;
using Menu = NativeUI.Menu;

namespace NativeUIExampleMenu
{
    public class NativeUIMenu : Script
    {
        private Menu menuMain;
        private Item.Button itemMainButtons;
        private Item.Button itemMainCheckBoxes;
        private Item.Button itemMainLists;
        private Item.Button itemMainSliders;
        private Item.List itemMainTheme;

        private Menu menuMainButtons;
        private Item.Button itemMainButtonsButton1;
        private Item.Button itemMainButtonsButton2;
        private Item.Button itemMainButtonsButton3;
        private Item.Button itemMainButtonsButton4;

        private Menu menuMainCheckBoxes;
        private Item.CheckBox itemMainCheckBoxesCheckBox1;
        private Item.CheckBox itemMainCheckBoxesCheckBox2;
        private Item.CheckBox itemMainCheckBoxesCheckBox3;
        private Item.CheckBox itemMainCheckBoxesCheckBox4;

        private Menu menuMainLists;
        private Item.List itemMainListsList1;
        private Item.List itemMainListsList2;
        private Item.List itemMainListsList3;
        private Item.List itemMainListsList4;

        private Menu menuMainSliders;
        private Item.Slider itemMainSlidersSlider1;
        private Item.Slider itemMainSlidersSlider2;
        private Item.Slider itemMainSlidersSlider3;
        private Item.Slider itemMainSlidersSlider4;

        private Menu menuLast;
        private List<Menu> menus;

        private Keys menuKey;

        private string basePath = Path.Combine(Game.InstallFolder, "scripts", typeof(NativeUIMenu).Namespace);
        private string imageNameMenuMainTitleImageCustomized = "MenuTitleImageMainCustomized.gif";
        private string imageNameMenuMainTitleImageCustomizedWhite = "MenuTitleImageMainCustomizedWhite.png";
        private string iconNameExclamation = "ButtonExclamation.png";
        private string iconNameGreaterThan = "ButtonGreaterThan.png";

        private List<string> themes = new List<string> { "Default", "Customized", "Customized White", "Customized Minimal", "GTA IV" };
        private string theme = "Default";

        public NativeUIMenu()
        {
            Interval = 100;
            Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        private void OnTick(object sender, EventArgs e)
        {
            if (menuMain == null)
            {
                menuKey = Keys.G;

                // Menus
                menuMain = new Menu(this, "NativeUI IV", "NATIVE IV SHOWCASE");
                menuMainButtons = new Menu(this, "NativeUI IV - Buttons", "BUTTONS SHOWCASE");
                menuMainCheckBoxes = new Menu(this, "NativeUI IV - Checkboxes", "CHECKBOXES SHOWCASE");
                menuMainLists = new Menu(this, "NativeUI IV - Lists", "LISTS SHOWCASE");
                menuMainSliders = new Menu(this, "NativeUI IV - Sliders", "SLIDERS SHOWCASE");

                // Items: Main Menu
                itemMainButtons = new Item.Button(menuMain, "Buttons", "Showcase of buttons", true, menuMainButtons);
                itemMainCheckBoxes = new Item.Button(menuMain, "Checkboxes", "Showcase of checkboxes", true, menuMainCheckBoxes);
                itemMainLists = new Item.Button(menuMain, "Lists", "Showcase of lists", true, menuMainLists);
                itemMainSliders = new Item.Button(menuMain, "Sliders", "Showcase of sliders", true, menuMainSliders);
                itemMainTheme = new Item.List(menuMain, "Theme", "Press Left or Right to choose a theme.\nPress Accept to apply it", themes, true, itemMainTheme_OnClick, itemMainTheme_OnSelectedIndexChanged);

                // Items: Main Menu -> Buttons
                itemMainButtonsButton1 = new Item.Button(menuMainButtons, "Button", "Clickable", true, itemMainButtonsButton1_OnClick);
                itemMainButtonsButton2 = new Item.Button(menuMainButtons, "Button", "Clickable", true, itemMainButtonsButton2_OnClick);
                itemMainButtonsButton3 = new Item.Button(menuMainButtons, "Button (disabled)", "Not clickable", false);
                itemMainButtonsButton4 = new Item.Button(menuMainButtons, "Button (disabled)", "Not clickable", false);

                // Items: Main Menu -> Checkboxes
                itemMainCheckBoxesCheckBox1 = new Item.CheckBox(menuMainCheckBoxes, "Checkbox (checked)", "Clickable, can be unchecked/checked", isChecked: true, isEnabled: true, itemMainCheckBoxesCheckBox1_OnCheckedChanged);
                itemMainCheckBoxesCheckBox2 = new Item.CheckBox(menuMainCheckBoxes, "Checkbox (unchecked)", "Clickable, can be checked/unchecked", isChecked: false, isEnabled: true, itemMainCheckBoxesCheckBox2_OnCheckedChanged);
                itemMainCheckBoxesCheckBox3 = new Item.CheckBox(menuMainCheckBoxes, "Checkbox (disabled, checked)", "Not clickable, cannot be unchecked/checked", isChecked: true, isEnabled: false);
                itemMainCheckBoxesCheckBox4 = new Item.CheckBox(menuMainCheckBoxes, "Checkbox (disabled, unchecked)", "Not clickable, cannot be checked/unchecked", isChecked: false, isEnabled: false);

                // Items: Main Menu -> Lists
                itemMainListsList1 = new Item.List(menuMainLists, "List", "Press Left or Right to navigate through the list.\nClickable", new List<string> { "Item 1", "Item 2", "Item 3" }, true, itemMainListsList1_OnClick, itemMainListsList1_OnSelectedIndexChanged);
                itemMainListsList2 = new Item.List(menuMainLists, "List (empty)", "Cannot be changed, not clickable", new List<string>(), true, itemMainListsList2_OnClick, itemMainListsList2_OnSelectedIndexChanged);
                itemMainListsList3 = new Item.List(menuMainLists, "List (disabled)", "Cannot be changed, not clickable", new List<string> { "Item 4", "Item 5", "Item 6" }, false);
                itemMainListsList4 = new Item.List(menuMainLists, "List (disabled, empty)", "Cannot be changed, not clickable", new List<string>(), false);

                // Items: Main Menu -> Sliders
                itemMainSlidersSlider1 = new Item.Slider(menuMainSliders, "Slider", "Press Left or Right to move slider, clickable", 0, 100, 0, true, itemMainSlidersSlider1_OnClick, itemMainSlidersSlider1_OnCurrentValueChanged);
                itemMainSlidersSlider2 = new Item.Slider(menuMainSliders, "Slider (half full)", "Press Left or Right to move slider, clickable", 0, 10, 5, true, itemMainSlidersSlider2_OnClick, itemMainSlidersSlider2_OnCurrentValueChanged);
                itemMainSlidersSlider3 = new Item.Slider(menuMainSliders, "Slider (disabled)", "Cannot be changed, not clickable", 0, 100, 0, false);
                itemMainSlidersSlider4 = new Item.Slider(menuMainSliders, "Slider (disabled, half full)", "Cannot be changed, not clickable", 0, 10, 5, false);

                menuLast = menuMain;
                menus = Menu.GetAllMenus(this);
            }

            if (Menu.IsAnyMenuOpen(this))
            {
                menuLast = Menu.GetCurrentMenu(this);
            }
        }

        private void OnKeyDown(object sender, GTA.KeyEventArgs e)
        {
            if (e.Key == menuKey)
            {
                if (!Menu.IsAnyMenuOpen(this))
                {
                    Menu.Show(menuLast);
                }
                else
                {
                    Menu.Hide();
                }
            }
        }

        private void itemMainButtonsButton1_OnClick()
        {
            Game.DisplayText("You clicked on \"" + itemMainButtonsButton1.TitleText + "\".", 1500);
        }

        private void itemMainButtonsButton2_OnClick()
        {
            Game.DisplayText("You clicked on \"" + itemMainButtonsButton2.TitleText + "\".", 1500);
        }

        private void itemMainCheckBoxesCheckBox1_OnCheckedChanged(bool isChecked)
        {
            Game.DisplayText("Checked status of \"" + itemMainCheckBoxesCheckBox1.TitleText + "\" is \"" + isChecked.ToString() + "\".", 1500);
        }

        private void itemMainCheckBoxesCheckBox2_OnCheckedChanged(bool isChecked)
        {
            Game.DisplayText("Checked status of \"" + itemMainCheckBoxesCheckBox2.TitleText + "\" is \"" + isChecked.ToString() + "\".", 1500);
        }

        private void itemMainListsList1_OnClick()
        {
            Game.DisplayText("You clicked on \"" + itemMainListsList1.TitleText + "\".", 1500);
        }

        private void itemMainListsList1_OnSelectedIndexChanged(int selectedIndex)
        {
            Game.DisplayText("Selected index of \"" + itemMainListsList1.TitleText + "\" is \"" + selectedIndex.ToString() + "\".", 1500);
        }

        private void itemMainListsList2_OnClick()
        {
            Game.DisplayText("This message is not supposed to be shown in game.", 1500);
        }

        private void itemMainListsList2_OnSelectedIndexChanged(int selectedIndex)
        {
            Game.DisplayText("This message is not supposed to be shown in game.", 1500);
        }

        private void itemMainSlidersSlider1_OnClick()
        {
            Game.DisplayText("You clicked on \"" + itemMainSlidersSlider1.TitleText + "\".", 1500);
        }

        private void itemMainSlidersSlider1_OnCurrentValueChanged(int currentValue)
        {
            Game.DisplayText("Current value of \"" + itemMainSlidersSlider1.TitleText + "\" is \"" + currentValue.ToString() + "\".", 1500);
        }

        private void itemMainSlidersSlider2_OnClick()
        {
            Game.DisplayText("You clicked on \"" + itemMainSlidersSlider2.TitleText + "\".", 1500);
        }

        private void itemMainSlidersSlider2_OnCurrentValueChanged(int currentValue)
        {
            Game.DisplayText("Current value of \"" + itemMainSlidersSlider2.TitleText + "\" is \"" + currentValue.ToString() + "\".", 1500);
        }

        private void itemMainTheme_OnClick()
        {
            ApplyTheme();
        }

        private void itemMainTheme_OnSelectedIndexChanged(int selectedIndex)
        {
            theme = themes[selectedIndex];
        }

        private void ApplyTheme()
        {
            for (int i = 0; i < menus.Count; i++)
            {
                menus[i].Restore();

                for (int ii = 0; ii < menus[i].Items.Count; ii++)
                {
                    menus[i].Items[ii].Restore();
                }
            }

            if (theme == themes[0]) // "Default"
            {
                itemMainButtonsButton2.TitleText = "Button";
            }

            if (theme == themes[1]) // "Customized"
            {
                menuMain.TitleImageFrameRate = 17;
                menuMain.MaxItemsVisibleAtOnce = 4;

                menuMain.TitleImage = Resource.LoadImage(basePath, imageNameMenuMainTitleImageCustomized);

                itemMainButtonsButton2.TitleText = "Button (with icon on right)";
                itemMainButtonsButton2.Icon = true;
                itemMainButtonsButton2.Icons = (
                    Enabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 0, 0, 0)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 255, 255, 255))
                    ),
                    Disabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 150, 150, 150)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 150, 150, 150))
                    )
                );
                itemMainButtonsButton2.IconLocation = Item.Button.IconLocations.Right;

                itemMainButtonsButton4.TitleText = "Button (disabled, with icon on left)";
                itemMainButtonsButton4.Icon = true;
                itemMainButtonsButton4.Icons = (
                    Enabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 0, 0, 0)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 255, 255, 255))
                    ),
                    Disabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 150, 150, 150)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 150, 150, 150))
                    )
                );

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i].InfiniteScroll = false;

                    for (int ii = 0; ii < menus[i].Items.Count; ii++)
                    {
                        switch (menus[i].Items[ii].Type)
                        {
                            case Item.ControlType.List:
                                Item.List listItem = (Item.List)menus[i].Items[ii];

                                listItem.ShowArrows = Item.List.ArrowsVisibility.Always;
                                listItem.InfiniteScroll = false;

                                break;

                            case Item.ControlType.Slider:
                                Item.Slider sliderItem = (Item.Slider)menus[i].Items[ii];

                                sliderItem.InfiniteScroll = false;

                                break;
                        }
                    }
                }
            }

            if (theme == themes[2]) // "Customized White"
            {
                menuMain.TitleImage = Resource.LoadTexture(basePath, imageNameMenuMainTitleImageCustomizedWhite);
                menuMain.MaxItemsVisibleAtOnce = 4;

                itemMainButtonsButton2.TitleText = "Button (with icon on right)";
                itemMainButtonsButton2.Icon = true;
                itemMainButtonsButton2.Icons = (
                    Enabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 255, 255, 255)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 0, 0, 0))
                    ),
                    Disabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 105, 105, 105)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 105, 105, 105))
                    )
                );
                itemMainButtonsButton2.IconLocation = Item.Button.IconLocations.Right;

                itemMainButtonsButton4.TitleText = "Button (disabled, with icon on left)";
                itemMainButtonsButton4.Icon = true;
                itemMainButtonsButton4.Icons = (
                    Enabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 255, 255, 255)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 0, 0, 0))
                    ),
                    Disabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 105, 105, 105)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 105, 105, 105))
                    )
                );

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i].InfiniteScroll = false;

                    menus[i].ShowGaps = false;

                    menus[i].TitleBackColor = Color.FromArgb(255, 255, 255, 255);
                    menus[i].TitleTextColor = Color.FromArgb(255, 0, 0, 0);
                    menus[i].TitleLineColor = Color.FromArgb(255, 0, 0, 0);

                    menus[i].DescriptionBackColor = Color.FromArgb(255, 255, 255, 255);
                    menus[i].DescriptionTextColor = Color.FromArgb(255, 0, 0, 0);
                    menus[i].SelectedIndexTextColor = Color.FromArgb(255, 0, 0, 0);

                    menus[i].NoItemsBackColor = Color.FromArgb(255, 255, 255, 255);
                    menus[i].NoItemsTextColor = Color.FromArgb(255, 0, 0, 0);

                    menus[i].ArrowsUpDownBackColor = Color.FromArgb(255, 255, 255, 255);
                    menus[i].IconsArrowUp = (
                        Enabled: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameMenuArrowUp, Color.FromArgb(255, 0, 0, 0)),
                        Disabled: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameMenuArrowUp, Color.FromArgb(255, 105, 105, 105))
                    );
                    menus[i].IconsArrowDown = (
                        Enabled: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameMenuArrowDown, Color.FromArgb(255, 0, 0, 0)),
                        Disabled: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameMenuArrowDown, Color.FromArgb(255, 105, 105, 105))
                    );

                    for (int ii = 0; ii < menus[i].Items.Count; ii++)
                    {
                        menus[i].Items[ii].TitleBackColors = (
                            Enabled: (
                                Selected: Color.FromArgb(255, 0, 0, 0),
                                Unselected: Color.FromArgb(192, 255, 255, 255)
                            ),
                            Disabled: (
                                Selected: Color.FromArgb(255, 0, 0, 0),
                                Unselected: Color.FromArgb(192, 255, 255, 255)
                            )
                        );
                        menus[i].Items[ii].TitleTextColors = (
                            Enabled: (
                                Selected: Color.FromArgb(255, 255, 255, 255),
                                Unselected: Color.FromArgb(255, 0, 0, 0)
                            ),
                            Disabled: (
                                Selected: Color.FromArgb(255, 105, 105, 105),
                                Unselected: Color.FromArgb(255, 105, 105, 105)
                            )
                        );

                        menus[i].Items[ii].DescriptionLineColors = (
                            Enabled: (
                                Selected: Color.FromArgb(255, 0, 0, 0),
                                Unselected: Color.FromArgb(255, 0, 0, 0)
                            ),
                            Disabled: (
                                Selected: Color.FromArgb(255, 0, 0, 0),
                                Unselected: Color.FromArgb(255, 0, 0, 0)
                            )
                        );
                        menus[i].Items[ii].DescriptionBackColors = (
                            Enabled: (
                                Selected: Color.FromArgb(255, 255, 255, 255),
                                Unselected: Color.FromArgb(255, 255, 255, 255)
                            ),
                            Disabled: (
                                Selected: Color.FromArgb(255, 255, 255, 255),
                                Unselected: Color.FromArgb(255, 255, 255, 255)
                            )
                        );
                        menus[i].Items[ii].DescriptionTextColors = (
                            Enabled: (
                                Selected: Color.FromArgb(255, 0, 0, 0),
                                Unselected: Color.FromArgb(255, 0, 0, 0)
                            ),
                            Disabled: (
                                Selected: Color.FromArgb(255, 0, 0, 0),
                                Unselected: Color.FromArgb(255, 0, 0, 0)
                            )
                        );


                        switch (menus[i].Items[ii].Type)
                        {
                            case Item.ControlType.CheckBox:
                                Item.CheckBox chkItem = (Item.CheckBox)menus[i].Items[ii];

                                chkItem.Icons = (
                                    Enabled: (
                                        Selected: (
                                            Checked: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameCheckBoxChecked, Color.FromArgb(255, 255, 255, 255)),
                                            Unchecked: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameCheckBoxUnchecked, Color.FromArgb(255, 255, 255, 255))
                                        ),
                                        Unselected: (
                                            Checked: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameCheckBoxChecked, Color.FromArgb(255, 0, 0, 0)),
                                            Unchecked: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameCheckBoxUnchecked, Color.FromArgb(255, 0, 0, 0))
                                        )
                                    ),
                                    Disabled: (
                                        Selected: (
                                            Checked: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameCheckBoxChecked, Color.FromArgb(255, 105, 105, 105)),
                                            Unchecked: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameCheckBoxUnchecked, Color.FromArgb(255, 105, 105, 105))
                                        ),
                                        Unselected: (
                                            Checked: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameCheckBoxChecked, Color.FromArgb(255, 105, 105, 105)),
                                            Unchecked: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameCheckBoxUnchecked, Color.FromArgb(255, 105, 105, 105))
                                        )
                                    )
                                );
                                
                                break;

                            case Item.ControlType.List:
                                Item.List listItem = (Item.List)menus[i].Items[ii];

                                listItem.ShowArrows = Item.List.ArrowsVisibility.Always;
                                listItem.InfiniteScroll = false;

                                listItem.IconsArrowLeft = (
                                    Enabled: (
                                        Selected: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameListArrowLeft, Color.FromArgb(255, 255, 255, 255)),
                                        Unselected: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameListArrowLeft, Color.FromArgb(255, 0, 0, 0))
                                    ),
                                    Disabled: (
                                        Selected: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameListArrowLeft, Color.FromArgb(255, 105, 105, 105)),
                                        Unselected: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameListArrowLeft, Color.FromArgb(255, 105, 105, 105))
                                    )
                                );
                                listItem.IconsArrowRight = (
                                    Enabled: (
                                        Selected: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameListArrowRight, Color.FromArgb(255, 255, 255, 255)),
                                        Unselected: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameListArrowRight, Color.FromArgb(255, 0, 0, 0))
                                    ),
                                    Disabled: (
                                        Selected: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameListArrowRight, Color.FromArgb(255, 105, 105, 105)),
                                        Unselected: Resource.LoadTextureWithColor(Resource.NativeUIBasePath, Resource.IconNameListArrowRight, Color.FromArgb(255, 105, 105, 105))
                                    )
                                );

                                listItem.SelectedTextColors = (
                                    Enabled: (
                                        Selected: Color.FromArgb(255, 255, 255, 255),
                                        Unselected: Color.FromArgb(255, 0, 0, 0)
                                    ),
                                    Disabled: (
                                        Selected: Color.FromArgb(255, 105, 105, 105),
                                        Unselected: Color.FromArgb(255, 105, 105, 105)
                                    )
                                );

                                break;

                            case Item.ControlType.Slider:
                                Item.Slider sliderItem = (Item.Slider)menus[i].Items[ii];

                                sliderItem.InfiniteScroll = false;

                                sliderItem.SliderForeColors = (
                                    Enabled: (
                                        Selected: Color.FromArgb(255, 255, 255, 255),
                                        Unselected: Color.FromArgb(255, 0, 0, 0)
                                    ),
                                    Disabled: (
                                        Selected: Color.FromArgb(255, 65, 65, 65),
                                        Unselected: Color.FromArgb(255, 65, 65, 65)
                                    )
                                );

                                break;
                        }
                    }
                }
            }

            if (theme == themes[3]) // "Customized Minimal"
            {
                itemMainButtonsButton2.TitleText = "Button (with icon on right)";
                itemMainButtonsButton2.Icon = true;
                itemMainButtonsButton2.Icons = (
                    Enabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 0, 0, 0)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 255, 255, 255))
                    ),
                    Disabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 150, 150, 150)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameExclamation, Color.FromArgb(255, 150, 150, 150))
                    )
                );
                itemMainButtonsButton2.IconLocation = Item.Button.IconLocations.Right;

                itemMainButtonsButton4.TitleText = "Button (disabled, with icon on left)";
                itemMainButtonsButton4.Icon = true;
                itemMainButtonsButton4.Icons = (
                    Enabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 0, 0, 0)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 255, 255, 255))
                    ),
                    Disabled: (
                        Selected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 150, 150, 150)),
                        Unselected: Resource.LoadTextureWithColor(basePath, iconNameGreaterThan, Color.FromArgb(255, 150, 150, 150))
                    )
                );

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i].ShowTitle = false;

                    menus[i].DescriptionTextColor = Color.FromArgb(255, 255, 255, 255);
                    menus[i].SelectedIndexTextColor = Color.FromArgb(255, 255, 255, 255);

                    for (int ii = 0; ii < menus[i].Items.Count; ii++)
                    {
                        menus[i].Items[ii].ShowDescription = false;
                    }
                }
            }
            
            if (theme == themes[4]) // "GTA IV"
            {
                itemMainButtonsButton4.TitleText = "Button (disabled)";

                for (int i = 0; i < menus.Count; i++)
                {
                    menus[i].Offset = new PointF(66f, 38f);
                    menus[i].TitleTextOffset = new PointF(0f, 10f);

                    menus[i].ShowDescription = false;
                    menus[i].ShowArrows = false;

                    menus[i].TitleTextColor = Color.FromArgb(255, 238, 150, 0);
                    menus[i].TitleTextFont = new GTA.Font("Calibri", 42f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 };
                    menus[i].TitleLineColor = Color.FromArgb(255, 128, 128, 128);

                    menus[i].NoItemsBackColor = Color.FromArgb(128, 0, 0, 0);
                    menus[i].NoItemsTextColor = Color.FromArgb(255, 128, 128, 128);
                    menus[i].NoItemsTextFont = new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 };

                    for (int ii = 0; ii < menus[i].Items.Count; ii++)
                    {
                        menus[i].Items[ii].ShowDescriptionLine = false;

                        menus[i].Items[ii].TitleBackColors = (
                            Enabled: (
                                Selected: Color.FromArgb(128, 0, 0, 0),
                                Unselected: Color.FromArgb(128, 0, 0, 0)
                            ),
                            Disabled: (
                                Selected: Color.FromArgb(128, 0, 0, 0),
                                Unselected: Color.FromArgb(128, 0, 0, 0)
                            )
                        );
                        menus[i].Items[ii].TitleTextColors = (
                            Enabled: (
                                Selected: Color.FromArgb(255, 238, 150, 0),
                                Unselected: Color.FromArgb(255, 128, 128, 128)
                            ),
                            Disabled: (
                                Selected: Color.FromArgb(255, 64, 64, 64),
                                Unselected: Color.FromArgb(255, 32, 32, 32)
                            )
                        );
                        menus[i].Items[ii].TitleTextFonts = (
                            Enabled: (
                                Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 },
                                Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 }
                            ),
                            Disabled: (
                                Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 },
                                Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 }
                            )
                        );

                        menus[i].Items[ii].DescriptionBackColors = (
                            Enabled: (
                                Selected: Color.FromArgb(128, 0, 0, 0),
                                Unselected: Color.FromArgb(128, 0, 0, 0)
                            ),
                            Disabled: (
                                Selected: Color.FromArgb(128, 0, 0, 0),
                                Unselected: Color.FromArgb(128, 0, 0, 0)
                            )
                        );
                        menus[i].Items[ii].DescriptionTextColors = (
                            Enabled: (
                                Selected: Color.FromArgb(255, 221, 221, 221),
                                Unselected: Color.FromArgb(255, 221, 221, 221)
                            ),
                            Disabled: (
                                Selected: Color.FromArgb(255, 221, 221, 221),
                                Unselected: Color.FromArgb(255, 221, 221, 221)
                            )
                        );
                        menus[i].Items[ii].DescriptionTextFonts = (
                            Enabled: (
                                Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 },
                                Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 }
                            ),
                            Disabled: (
                                Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 },
                                Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 }
                            )
                        );

                        switch (menus[i].Items[ii].Type)
                        {
                            case Item.ControlType.CheckBox:
                                Item.CheckBox chkItem = (Item.CheckBox)menus[i].Items[ii];

                                chkItem.Mode = Item.CheckBox.ToggleMode.Text;

                                chkItem.ToggleTextColors = (
                                    Enabled: (
                                        Selected: (
                                            Checked: Color.FromArgb(255, 238, 150, 0),
                                            Unchecked: Color.FromArgb(255, 238, 150, 0)
                                        ),
                                        Unselected: (
                                            Checked: Color.FromArgb(255, 128, 128, 128),
                                            Unchecked: Color.FromArgb(255, 128, 128, 128)
                                        )
                                    ),
                                    Disabled: (
                                        Selected: (
                                            Checked: Color.FromArgb(255, 64, 64, 64),
                                            Unchecked: Color.FromArgb(255, 64, 64, 64)
                                        ),
                                        Unselected: (
                                            Checked: Color.FromArgb(255, 32, 32, 32),
                                            Unchecked: Color.FromArgb(255, 32, 32, 32)
                                        )
                                    )
                                );
                                chkItem.ToggleTextFonts = (
                                    Enabled: (
                                        Selected: (
                                            Checked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 },
                                            Unchecked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 }
                                        ),
                                        Unselected: (
                                            Checked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 },
                                            Unchecked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 }
                                        )
                                    ),
                                    Disabled: (
                                        Selected: (
                                            Checked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 },
                                            Unchecked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 }
                                        ),
                                        Unselected: (
                                            Checked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 },
                                            Unchecked: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 }
                                        )
                                    )
                                );

                                break;
                            case Item.ControlType.List:
                                Item.List listItem = (Item.List)menus[i].Items[ii];

                                listItem.ShowArrows = Item.List.ArrowsVisibility.Never;

                                listItem.SelectedTextColors = (
                                    Enabled: (
                                        Selected: Color.FromArgb(255, 238, 150, 0),
                                        Unselected: Color.FromArgb(255, 128, 128, 128)
                                    ),
                                    Disabled: (
                                        Selected: Color.FromArgb(255, 64, 64, 64),
                                        Unselected: Color.FromArgb(255, 32, 32, 32)
                                    )
                                );
                                listItem.SelectedTextFonts = (
                                    Enabled: (
                                        Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 },
                                        Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 }
                                    ),
                                    Disabled: (
                                        Selected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 },
                                        Unselected: new GTA.Font("Calibri", 26f, FontScaling.Pixel, true, false) { Effect = FontEffect.Edge, EffectColor = Color.FromArgb(255, 0, 0, 0), EffectSize = 2 }
                                    )
                                );

                                break;
                            case Item.ControlType.Slider:
                                Item.Slider sliderItem = (Item.Slider)menus[i].Items[ii];

                                sliderItem.SliderSizeOffset = new SizeF(0f, -2f);
                                sliderItem.SliderBorderSize = new SizeF(3f, 3f);

                                sliderItem.SliderBorderColors = (
                                    Enabled: (
                                        Selected: Color.FromArgb(255, 0, 0, 0),
                                        Unselected: Color.FromArgb(255, 0, 0, 0)
                                    ),
                                    Disabled: (
                                        Selected: Color.FromArgb(255, 0, 0, 0),
                                        Unselected: Color.FromArgb(255, 0, 0, 0)
                                    )
                                );
                                sliderItem.SliderBackColors = (
                                    Enabled: (
                                        Selected: Color.FromArgb(255, 24, 24, 24),
                                        Unselected: Color.FromArgb(255, 24, 24, 24)
                                    ),
                                    Disabled: (
                                        Selected: Color.FromArgb(255, 24, 24, 24),
                                        Unselected: Color.FromArgb(255, 24, 24, 24)
                                    )
                                );
                                sliderItem.SliderForeColors = (
                                    Enabled: (
                                        Selected: Color.FromArgb(255, 238, 150, 0),
                                        Unselected: Color.FromArgb(255, 128, 128, 128)
                                    ),
                                    Disabled: (
                                        Selected: Color.FromArgb(255, 64, 64, 64),
                                        Unselected: Color.FromArgb(255, 64, 64, 64)
                                    )
                                );

                                break;
                        }
                    }
                }
            }
        }
    }
}