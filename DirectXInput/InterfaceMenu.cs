﻿using ArnoldVinkCode;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static DirectXInput.AppVariables;

namespace DirectXInput
{
    partial class WindowMain
    {
        //Handle main menu mouse/touch tapped
        async void lb_Menu_MousePressUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //Check if an actual ListBoxItem is clicked
                if (!AVFunctions.ListBoxItemClickCheck((DependencyObject)e.OriginalSource)) { return; }

                //Check which mouse button is pressed
                if (e.ClickCount == 1)
                {
                    vSingleTappedEvent = true;
                    await Task.Delay(500);
                    if (vSingleTappedEvent) { lb_Menu_SingleTap(); }
                }
            }
            catch { }
        }

        //Handle main menu keyboard/controller tapped
        void lb_Menu_KeyPressUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Space) { lb_Menu_SingleTap(); }
            }
            catch { }
        }

        //Handle main menu single tap
        void lb_Menu_SingleTap()
        {
            try
            {
                if (lb_Menu.SelectedIndex >= 0)
                {
                    StackPanel SelStackPanel = (StackPanel)lb_Menu.SelectedItem;
                    if (SelStackPanel.Name == "menuButtonConnection") { ShowGridPage(grid_Connection); }
                    else if (SelStackPanel.Name == "menuButtonController") { ShowGridPage(grid_Controller); }
                    else if (SelStackPanel.Name == "menuButtonBattery") { ShowGridPage(grid_Battery); }
                    else if (SelStackPanel.Name == "menuButtonIgnore") { ShowGridPage(grid_Ignore); }
                    else if (SelStackPanel.Name == "menuButtonShortcuts") { ShowGridPage(grid_Shortcuts); }
                    else if (SelStackPanel.Name == "menuButtonKeyboard") { ShowGridPage(grid_Keyboard); }
                    else if (SelStackPanel.Name == "menuButtonKeypad") { ShowGridPage(grid_Keypad); }
                    else if (SelStackPanel.Name == "menuButtonMedia") { ShowGridPage(grid_Media); }
                    else if (SelStackPanel.Name == "menuButtonSettings") { ShowGridPage(grid_Settings); }
                    else if (SelStackPanel.Name == "menuButtonDebug") { ShowGridPage(grid_Debug); }
                    else if (SelStackPanel.Name == "menuButtonHelp") { ShowGridPage(grid_Help); }
                }
            }
            catch { }
        }
    }
}