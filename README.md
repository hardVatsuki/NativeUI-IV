# About
NativeUI IV is a library to create your on Rockstar-like menus for Grand Theft Auto IV.

Current Version: 1.0  
Download now on [NexusMods](https://www.nexusmods.com/gta4/mods/1455)
Check [Wiki](https://github.com/hardVatsuki/NativeUI-IV/wiki) to learn how to create your own NativeUI IV menu.

# Changelog
**1.0**  
Added customization of all possible elements of menus and its' items (colors, fonts, icons)  
Added Menu GetCurrentMenu() function to get currently opened menu  
Added Menu GetAllMenus() function to get all menus  
Added Menu Restore() function to restore all menu properties to default  
Added Menu BackKey property to return to parent menu  
Added Menu InfiniteScroll property; available options: false, true  
Added Menu Offset property to move menu up/down/left/right  
Added Menu TitleTextOffset property to title text up/down/left/right  
Added Menu ShowTitle property; available options: false, true  
Added Menu ShowDescription property; available options: false, true  
Added Menu ShowArrows property; available options: false, true  
Added Menu ShowGaps property; available options: false, true  
Added Item Restore() function to restore all item properties to default  
Added Item ShowDescriptionLine property; available options: false, true  
Added Item ShowDescription property; available options: false, true  
Added Item.CheckBox Mode property; available options: Icon, Text  
Added Item.List InfiniteScroll property; available options: false, true  
Added Item.List ShowArrows property; available options: Never, EnabledSelected, Always; will not affect Item.List with empty item list  
Added Item.Slider; supports colors, position and size changes  
Fixed the issue when NativeUI mods override each others' settings (https://github.com/ClonkAndre/NativeUI-IV/issues/2)  
Fixed the issue when clicking on nested menu triggers first item of that nested menu (https://github.com/ClonkAndre/NativeUI-IV/issues/3)  
Fixed the issue when having 2 or more NativeUI mods causes input duplication in all menus  
Fixed the issue when opening a menu with GIF without hiding any menu prior causes GIF to speed up  
Fixed the issue when height of item description background did not change  
Fixed the issue when input registration with gamepad was inconsistent  
Fixed the issue when menu would crash if it was scrolled up too many times from top to bottom with InfiniteScroll property enabled  
Fixed the issue when menu would not resize correctly when it was opened and it's MaxItemsVisibleAtOnce property got changed  
Fixed the issue when NativeUI icons looked incorrect and blurry  
Changed icons positions of menu items for more UI consistency  
Changed default resources, now textures are located in "scripts\NativeUI" directory  
Changed default controls, now they are bound to NumPad keys  
Changed Menu DisablePlayerMovementWhenMenuIsOpened property to CanControlCharacter  
Changed Menu EnableControllerSupport property, if it is true then character controls will be disabled regardless of CanControlCharacter property  
Changed Menu AnimatedBannerFrameRate property to TitleImageFrameRate, now it accepts exact number of GIF's framerate  
Changed Item, disabled color is now split into disabled selected and disabled unselected colors  
Changed Item, now Button, CheckBox and List support more icons for each item state  
Renamed UIMenu to Menu  
Renamed UI.UIMenuItem to Item.Button  
Renamed UI.UIMenuCheckboxItem to Item.CheckBox  
Renamed UI.UIMenuListItem to Item.List  
Renamed Menu AddItem() function to Add()  
Renamed Menu AddItems() function to AddRange()  
Renamed Menu RemoveItem(Base item) function to Remove()  
Renamed Menu RemoveItem(int index) function to RemoveAt()  
Renamed Menu RemoveAllItems() function to RemoveAll()  
Renamed Item.List AddItem() function to Add()  
Renamed Item.List AddItems() function to AddRange()  
Renamed Item.List RemoveItem(string item) function to Remove()  
Renamed Item.List RemoveItem(int index) function to RemoveAt()  
Renamed Item.List RemoveAllItems() function to RemoveAll()  
Removed Menu.HideAllMenus() function, now Menu.Hide() function does the same action  
Removed Menu DisablePhoneWhenMenuIsOpened property    
Removed Menu.Options, now each option is a property of menu/item instance  
**0.8.1**  
Fixed System.ArgumentOutOfRange exception when menu item count is below MaxItemsVisibleAtOnce value  
**0.8**  
The menu is now scrollable! You can set how many items should be visible at once with this menu property: "MaxItemsVisibleAtOnce" (Thanks to @Teki for helping me with this). With this feature, you can now add as many items as you want to the menu.  
**0.7**  
Now with animated banner support!  
**0.6**  
The letter "g" will no longer be cut off from the text.  
**0.5**  
Changed the class name "NativeUI.Menu" to "NativeUI.UIMenu" to prevent conflictions between "System.Windows.Forms.Menu" and "NativeUI.Menu".  
Added the ability to bind menus to default items. With that, you can create an easy nested menu system.  
**0.4**  
Added sounds for navigating through the menu or clicking on items. Sounds can be enabled or disabled with: NativeUI.Menu.Options.enableMenuSounds.  
**0.3**  
Selected item index and max item (1 / 7) now changes it's position dynamically through its lenght.  
Added the ability to use custom icons for default menu items.  
**0.2**  
Added a list item.  
**0.1**  
Release.

# Credits
This is a fork of [original NativeUI IV](https://github.com/ClonkAndre/NativeUI-IV) mod by [ClonkAndre](https://github.com/ClonkAndre)  
While heavily modified, my code is still based on his work.
