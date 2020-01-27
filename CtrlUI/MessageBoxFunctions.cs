﻿using ArnoldVinkCode;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows;
using static ArnoldVinkCode.ProcessWin32Functions;
using static CtrlUI.AppVariables;
using static CtrlUI.ImageFunctions;
using static LibraryShared.Classes;
using static LibraryShared.SoundPlayer;

namespace CtrlUI
{
    partial class WindowMain
    {
        //Show and close Messagebox Popup
        public async Task<DataBindString> Popup_Show_MessageBox(string Question, string Subtitle, string Description, List<DataBindString> Answers)
        {
            try
            {
                //Check if the message box is already open
                if (!vMessageBoxOpen)
                {
                    //Play the opening sound
                    PlayInterfaceSound(vInterfaceSoundVolume, "PromptOpen", false);

                    //Save the previous focus element
                    Popup_PreviousFocusSave(vMessageBoxElementFocus, null);
                }

                //Reset messagebox variables
                vMessageBoxCancelled = false;
                vMessageBoxResult = null;
                vMessageBoxOpen = true;

                //Set messagebox question
                if (!string.IsNullOrWhiteSpace(Question))
                {
                    grid_MessageBox_Question.Text = Question;
                    grid_MessageBox_Question.Visibility = Visibility.Visible;
                }
                else
                {
                    grid_MessageBox_Question.Text = string.Empty;
                    grid_MessageBox_Question.Visibility = Visibility.Collapsed;
                }

                //Set messagebox subtitle
                if (!string.IsNullOrWhiteSpace(Subtitle))
                {
                    grid_MessageBox_Subtitle.Text = Subtitle;
                    grid_MessageBox_Subtitle.Visibility = Visibility.Visible;
                }
                else
                {
                    grid_MessageBox_Subtitle.Text = string.Empty;
                    grid_MessageBox_Subtitle.Visibility = Visibility.Collapsed;
                }

                //Set messagebox description
                if (!string.IsNullOrWhiteSpace(Description))
                {
                    grid_MessageBox_Description.Text = Description;
                    grid_MessageBox_Description.Visibility = Visibility.Visible;
                }
                else
                {
                    grid_MessageBox_Description.Text = string.Empty;
                    grid_MessageBox_Description.Visibility = Visibility.Collapsed;
                }

                //Set the messagebox answers
                lb_MessageBox.ItemsSource = Answers;

                //Show the popup with animation
                AVAnimations.Ani_Visibility(grid_Popup_MessageBox, true, true, 0.10);
                AVAnimations.Ani_Opacity(grid_Video_Background, 0.08, true, false, 0.10);
                AVAnimations.Ani_Opacity(grid_Main, 0.08, true, false, 0.10);

                if (vTextInputOpen) { AVAnimations.Ani_Opacity(grid_Popup_TextInput, 0.02, true, false, 0.10); }
                //if (vMessageBoxOpen) { AVAnimations.Ani_Opacity(grid_Popup_MessageBox, 0.02, true, false, 0.10); }
                if (vFilePickerOpen) { AVAnimations.Ani_Opacity(grid_Popup_FilePicker, 0.02, true, false, 0.10); }
                if (vPopupOpen) { AVAnimations.Ani_Opacity(vPopupElementTarget, 0.02, true, false, 0.10); }
                if (vColorPickerOpen) { AVAnimations.Ani_Opacity(grid_Popup_ColorPicker, 0.02, true, false, 0.10); }
                if (vSearchOpen) { AVAnimations.Ani_Opacity(grid_Popup_Search, 0.02, true, false, 0.10); }
                if (vMainMenuOpen) { AVAnimations.Ani_Opacity(grid_Popup_MainMenu, 0.02, true, false, 0.10); }

                //Focus on first listbox answer
                await ListboxFocus(lb_MessageBox, true, false, -1);

                //Wait for user messagebox input
                while (vMessageBoxResult == null && !vMessageBoxCancelled) { await Task.Delay(500); }
                if (vMessageBoxCancelled) { return null; }

                //Close and reset the popup
                await Popup_Close_MessageBox();
            }
            catch { }
            return vMessageBoxResult;
        }

        //Close and reset messageboxpopup
        async Task Popup_Close_MessageBox()
        {
            try
            {
                //Play the closing sound
                PlayInterfaceSound(vInterfaceSoundVolume, "PromptClose", false);

                //Reset the popup variables
                vMessageBoxCancelled = true;
                //vMessageBoxResult = null;
                vMessageBoxOpen = false;

                //Hide the popup with animation
                AVAnimations.Ani_Visibility(grid_Popup_MessageBox, false, false, 0.10);

                if (!Popup_Any_Open())
                {
                    double backgroundBrightness = (double)Convert.ToInt32(ConfigurationManager.AppSettings["BackgroundBrightness"]) / 100;
                    AVAnimations.Ani_Opacity(grid_Video_Background, backgroundBrightness, true, true, 0.10);
                    AVAnimations.Ani_Opacity(grid_Main, 1, true, true, 0.10);
                }
                else if (vTextInputOpen) { AVAnimations.Ani_Opacity(grid_Popup_TextInput, 1, true, true, 0.10); }
                else if (vMessageBoxOpen) { AVAnimations.Ani_Opacity(grid_Popup_MessageBox, 1, true, true, 0.10); }
                else if (vFilePickerOpen) { AVAnimations.Ani_Opacity(grid_Popup_FilePicker, 1, true, true, 0.10); }
                else if (vPopupOpen) { AVAnimations.Ani_Opacity(vPopupElementTarget, 1, true, true, 0.10); }
                else if (vColorPickerOpen) { AVAnimations.Ani_Opacity(grid_Popup_ColorPicker, 1, true, true, 0.10); }
                else if (vSearchOpen) { AVAnimations.Ani_Opacity(grid_Popup_Search, 1, true, true, 0.10); }
                else if (vMainMenuOpen) { AVAnimations.Ani_Opacity(grid_Popup_MainMenu, 1, true, true, 0.10); }

                while (grid_Popup_MessageBox.Visibility == Visibility.Visible)
                {
                    await Task.Delay(10);
                }

                //Force focus on an element
                await Popup_PreviousFocusForce(vMessageBoxElementFocus);
            }
            catch { }
        }

        //Close application messagebox
        async Task Popup_Show_MessageBox_Shutdown()
        {
            try
            {
                List<DataBindString> Answers = new List<DataBindString>();
                DataBindString Answer1 = new DataBindString();
                Answer1.ImageBitmap = FileToBitmapImage(new string[] { "pack://application:,,,/Assets/Icons/Closing.png" }, IntPtr.Zero, -1);
                Answer1.Name = "Close CtrlUI";
                Answers.Add(Answer1);

                DataBindString Answer2 = new DataBindString();
                Answer2.ImageBitmap = FileToBitmapImage(new string[] { "pack://application:,,,/Assets/Icons/Restart.png" }, IntPtr.Zero, -1);
                Answer2.Name = "Restart my PC";
                Answers.Add(Answer2);

                DataBindString Answer3 = new DataBindString();
                Answer3.ImageBitmap = FileToBitmapImage(new string[] { "pack://application:,,,/Assets/Icons/Shutdown.png" }, IntPtr.Zero, -1);
                Answer3.Name = "Shutdown my PC";
                Answers.Add(Answer3);

                DataBindString messageResult = await Popup_Show_MessageBox("Would you like to close CtrlUI or shutdown your PC?", "If you have DirectXInput running and a controller connected you can launch CtrlUI by pressing on the 'Guide' button.", "", Answers);
                if (messageResult != null)
                {
                    if (messageResult == Answer1)
                    {
                        Popup_Show_Status("Closing", "Closing CtrlUI");
                        await Application_Exit(true);
                    }
                    else if (messageResult == Answer2)
                    {
                        Popup_Show_Status("Shutdown", "Restarting your PC");

                        //Close all other launchers
                        await CloseLaunchers(true);

                        //Restart the PC
                        await ProcessLauncherWin32Async(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\System32\shutdown.exe", "", "/r /t 0", false, true);

                        //Close CtrlUI
                        await Application_Exit(true);
                    }
                    else if (messageResult == Answer3)
                    {
                        Popup_Show_Status("Shutdown", "Shutting down your PC");

                        //Close all other launchers
                        await CloseLaunchers(true);

                        //Shutdown the PC
                        await ProcessLauncherWin32Async(Environment.GetFolderPath(Environment.SpecialFolder.Windows) + @"\System32\shutdown.exe", "", "/s /t 0", false, true);

                        //Close CtrlUI
                        await Application_Exit(true);
                    }
                }
            }
            catch { }
        }
    }
}