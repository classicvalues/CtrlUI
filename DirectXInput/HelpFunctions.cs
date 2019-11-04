﻿using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace DirectXInput
{
    partial class WindowMain
    {
        //Load - Help text
        void LoadHelp()
        {
            if (sp_Help.Children.Count == 0)
            {
                sp_Help.Children.Add(new TextBlock() { Text = "Why is my controller not working properly?", Style = (Style)App.Current.Resources["TextBlockBlack"] });
                sp_Help.Children.Add(new TextBlock() { Text = "Make sure that your connected controller is a DirectInput controller that has two thumb sticks and a D-Pad, if you are using a DualShock 1 or 2 controller enable the analog mode.", Style = (Style)App.Current.Resources["TextBlockGray"], TextWrapping = TextWrapping.Wrap });

                sp_Help.Children.Add(new TextBlock() { Text = "\r\nWhy is my controller not detected while connected?", Style = (Style)App.Current.Resources["TextBlockBlack"] });
                sp_Help.Children.Add(new TextBlock() { Text = "Some users may need to launch DirectXInput as administrator before the controller can be detected and converted to a Xbox controller.", Style = (Style)App.Current.Resources["TextBlockGray"], TextWrapping = TextWrapping.Wrap });

                sp_Help.Children.Add(new TextBlock() { Text = "\r\nWhy is my controller rumble not working at all?", Style = (Style)App.Current.Resources["TextBlockBlack"] });
                sp_Help.Children.Add(new TextBlock() { Text = "Only a few controllers like the DualShock 1, 2, 3 and 4 currently have rumble support because rumble support needs to be manually added for each controller.", Style = (Style)App.Current.Resources["TextBlockGray"], TextWrapping = TextWrapping.Wrap });

                sp_Help.Children.Add(new TextBlock() { Text = "\r\nWhy does my DualShock 3 not respond?", Style = (Style)App.Current.Resources["TextBlockBlack"] });
                sp_Help.Children.Add(new TextBlock() { Text = "If your DualShock 3 was not connected during the driver installation, you may need to reinstall the drivers while the DualShock 3 is connected to use your controller, you can install the drivers again on the 'Settings' tab.", Style = (Style)App.Current.Resources["TextBlockGray"], TextWrapping = TextWrapping.Wrap });

                sp_Help.Children.Add(new TextBlock() { Text = "\r\nCan I disconnect my controller remotely?", Style = (Style)App.Current.Resources["TextBlockBlack"] });
                sp_Help.Children.Add(new TextBlock() { Text = "When you press on the 'Guide' and 'Start' button at the same time your controller will be disconnected.", Style = (Style)App.Current.Resources["TextBlockGray"], TextWrapping = TextWrapping.Wrap });

                sp_Help.Children.Add(new TextBlock() { Text = "\r\nWhy does this app use a few percent of my cpu?", Style = (Style)App.Current.Resources["TextBlockBlack"] });
                sp_Help.Children.Add(new TextBlock() { Text = "When the application is visible and activated the animations will causes the cpu to be used by a few percent, when the app is no longer activated it will drop down to almost no cpu usage.", Style = (Style)App.Current.Resources["TextBlockGray"], TextWrapping = TextWrapping.Wrap });

                sp_Help.Children.Add(new TextBlock() { Text = "\r\nSupport and bug reporting", Style = (Style)App.Current.Resources["TextBlockBlack"] });
                sp_Help.Children.Add(new TextBlock() { Text = "When you are walking into any problems or bugs you can go to the support page here: https://support.arnoldvink.com so I can try to help you out and get everything working.", Style = (Style)App.Current.Resources["TextBlockGray"], TextWrapping = TextWrapping.Wrap });

                sp_Help.Children.Add(new TextBlock() { Text = "\r\nDeveloper donation support", Style = (Style)App.Current.Resources["TextBlockBlack"] });
                sp_Help.Children.Add(new TextBlock() { Text = "If you appreciate my project and want to support me with my projects you can make a donation through https://donation.arnoldvink.com", Style = (Style)App.Current.Resources["TextBlockGray"], TextWrapping = TextWrapping.Wrap });

                //Set the version text
                sp_Help.Children.Add(new TextBlock() { Text = "\r\nApplication made by Arnold Vink", Style = (Style)App.Current.Resources["TextBlockBlack"] });
                sp_Help.Children.Add(new TextBlock() { Text = "Version: v" + Assembly.GetEntryAssembly().FullName.Split('=')[1].Split(',')[0], Style = (Style)App.Current.Resources["TextBlockGray"], TextWrapping = TextWrapping.Wrap });
            }
        }

        void btn_Help_ProjectWebsite_Click(object sender, RoutedEventArgs e) { Process.Start("https://projects.arnoldvink.com"); }
        void btn_Help_OpenDonation_Click(object sender, RoutedEventArgs e) { Process.Start("https://donation.arnoldvink.com"); }
    }
}