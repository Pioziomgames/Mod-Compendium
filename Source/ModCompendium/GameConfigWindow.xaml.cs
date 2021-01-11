﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.WindowsAPICodePack.Dialogs;
using ModCompendiumLibrary.Configuration;
using ModCompendiumLibrary;

namespace ModCompendium
{
    /// <summary>
    /// Interaction logic for GameConfigWindow.xaml
    /// </summary>
    public partial class GameConfigWindow : Window
    {
        private readonly GameConfig mConfig;

        public GameConfigWindow(GameConfig config)
        {
            InitializeComponent();
            DataContext = config;
            mConfig = config;

            // Add game specific settings
            if (config.Game == Game.Persona3 || config.Game == Game.Persona4)
            {
                var p34Config = (Persona34GameConfig)config;

                // Add extra row
                ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());

                // Dvd root directory path label
                {
                    var dvdRootPathLabel = new Label()
                    {
                        Content = "ISO Path",
                        ToolTip = "Path to an unmodified ISO of " + config.Game.ToString(),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 25,
                        Width = 120
                    };

                    Grid.SetRow(dvdRootPathLabel, 2);
                    Grid.SetColumn(dvdRootPathLabel, 0);
                    ConfigPropertyGrid.Children.Add(dvdRootPathLabel);
                }

                // Dvd root directory text box
                TextBox dvdRootPathTextBox;
                {
                    dvdRootPathTextBox = new TextBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 20,
                        TextWrapping = TextWrapping.Wrap,
                        Width = 291,
                    };

                    dvdRootPathTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(Persona34GameConfig.DvdRootOrIsoPath)));

                    Grid.SetRow(dvdRootPathTextBox, 2);
                    Grid.SetColumn(dvdRootPathTextBox, 1);
                    ConfigPropertyGrid.Children.Add(dvdRootPathTextBox);
                }

                // Dvd root directory text box button
                {
                    var dvdRootPathTextBoxButton = new Button()
                    {
                        Content = "...",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 20,
                        Height = 20
                    };

                    dvdRootPathTextBoxButton.Click += (s, e) =>
                    {
                        var file = SelectFile(new CommonFileDialogFilter("ISO file", ".iso"));
                        if (file != null)
                        {
                            p34Config.DvdRootOrIsoPath = file;
                            dvdRootPathTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                        }
                    };

                    Grid.SetRow(dvdRootPathTextBoxButton, 2);
                    Grid.SetColumn(dvdRootPathTextBoxButton, 1);
                    ConfigPropertyGrid.Children.Add(dvdRootPathTextBoxButton);
                }

                // Add extra row
                ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());

                // HostFS Mode checkbox label
                {
                    var hostFSLabel = new Label()
                    {
                        Content = "HostFS Mode",
                        ToolTip = "Outputs files instead of CVMs for use with HostFS Patch",
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                        Width = 120
                    };

                    Grid.SetRow(hostFSLabel, 4);
                    Grid.SetColumn(hostFSLabel, 0);
                    ConfigPropertyGrid.Children.Add(hostFSLabel);
                }

                // HostFS Mode Checkbox
                CheckBox hostFS;
                {
                    hostFS = new CheckBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                    };

                    hostFS.SetBinding(CheckBox.IsCheckedProperty, new Binding(nameof(Persona34GameConfig.HostFS)));

                    Grid.SetRow(hostFS, 4);
                    Grid.SetColumn(hostFS, 1);
                    ConfigPropertyGrid.Children.Add(hostFS);
                }
            }
            else if (config is ModCpkGameConfig)
            {
                var ppConfig = (ModCpkGameConfig)config;
            }
            else if (config is PKGGameConfig)
            {
                var pkgConfig = (PKGGameConfig)config;

                // Add extra row
                ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());

                // Cpk root directory path label
                {
                    var pkgPathLabel = new Label()
                    {
                        Content = "App PKG Path",
                        ToolTip = $"Path to the unencrypted {config.Game.ToString()} full game PKG",
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 25,
                        Width = 120
                    };

                    Grid.SetRow(pkgPathLabel, 2);
                    Grid.SetColumn(pkgPathLabel, 0);
                    ConfigPropertyGrid.Children.Add(pkgPathLabel);
                }

                // PKG path  text box
                TextBox pkgPathTextBox;
                {
                    pkgPathTextBox = new TextBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 20,
                        TextWrapping = TextWrapping.Wrap,
                        Width = 291,
                    };

                    pkgPathTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(Persona5RoyalGameConfig.PKGPath)));

                    Grid.SetRow(pkgPathTextBox, 2);
                    Grid.SetColumn(pkgPathTextBox, 1);
                    ConfigPropertyGrid.Children.Add(pkgPathTextBox);
                }

                // PKG Path text box button
                {
                    var pkgPathTextBoxButton = new Button()
                    {
                        Content = "...",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 20,
                        Height = 20
                    };

                    pkgPathTextBoxButton.Click += (s, e) =>
                    {
                        var file = SelectFile(new CommonFileDialogFilter("PKG file", ".pkg"));
                        if (file != null)
                        {
                            pkgConfig.PKGPath = file;
                            pkgPathTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                        }
                    };

                    Grid.SetRow(pkgPathTextBoxButton, 2);
                    Grid.SetColumn(pkgPathTextBoxButton, 1);
                    ConfigPropertyGrid.Children.Add(pkgPathTextBoxButton);
                }
            }
            else
            {
                var ppConfig = (PersonaPortableGameConfig)config;

                // Add extra row
                ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());

                // Cpk root directory path label
                {
                    var cpkRootPathLabel = new Label()
                    {
                        Content = "CPK Path",
                        ToolTip = "Path to an unmodified CPK of " + config.Game.ToString(),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 25,
                        Width = 120
                    };

                    Grid.SetRow(cpkRootPathLabel, 2);
                    Grid.SetColumn(cpkRootPathLabel, 0);
                    ConfigPropertyGrid.Children.Add(cpkRootPathLabel);
                }

                // Cpk root directory text box
                TextBox cpkRootPathTextBox;
                {
                    cpkRootPathTextBox = new TextBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 20,
                        TextWrapping = TextWrapping.Wrap,
                        Width = 291,
                    };

                    cpkRootPathTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(PersonaPortableGameConfig.CpkRootOrPath)));

                    Grid.SetRow(cpkRootPathTextBox, 2);
                    Grid.SetColumn(cpkRootPathTextBox, 1);
                    ConfigPropertyGrid.Children.Add(cpkRootPathTextBox);
                }

                // Cpk root directory text box button
                {
                    var cpkRootPathTextBoxButton = new Button()
                    {
                        Content = "...",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 20,
                        Height = 20
                    };

                    cpkRootPathTextBoxButton.Click += (s, e) =>
                    {
                        var file = SelectFile(new CommonFileDialogFilter("CPK file", ".cpk"));
                        if (file != null)
                        {
                            ppConfig.CpkRootOrPath = file;
                            cpkRootPathTextBox.GetBindingExpression(TextBox.TextProperty).UpdateTarget();
                        }
                    };

                    Grid.SetRow(cpkRootPathTextBoxButton, 2);
                    Grid.SetColumn(cpkRootPathTextBoxButton, 1);
                    ConfigPropertyGrid.Children.Add(cpkRootPathTextBoxButton);
                }

                // Add extra row
                ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());

                // Extraction checkbox label
                {
                    var cpkExtractLabel = new Label()
                    {
                        Content = "Use Extracted Files",
                        ToolTip = "Extract the contents of the CPK at the specified CPK Path",
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                        Width = 120
                    };

                    Grid.SetRow(cpkExtractLabel, 4);
                    Grid.SetColumn(cpkExtractLabel, 0);
                    ConfigPropertyGrid.Children.Add(cpkExtractLabel);
                }

                // Cpk Extraction checkbox
                CheckBox cpkExtract;
                {
                    cpkExtract = new CheckBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                    };

                    cpkExtract.SetBinding(CheckBox.IsCheckedProperty, new Binding(nameof(PersonaPortableGameConfig.Extract)));

                    Grid.SetRow(cpkExtract, 4);
                    Grid.SetColumn(cpkExtract, 1);
                    ConfigPropertyGrid.Children.Add(cpkExtract);
                }
                
                // Add extra row
                ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());

                // PC Mode checkbox label
                var pcLabel = new Label()
                {
                    Content = "PC Mode",
                    ToolTip = "Outputs files for use with the P4G PC Mod Loader",
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Height = 35,
                    Width = 120,
                    Visibility = Visibility.Hidden
                };

                Grid.SetRow(pcLabel, 5);
                Grid.SetColumn(pcLabel, 0);
                ConfigPropertyGrid.Children.Add(pcLabel);

                // PC Mode Checkbox
                CheckBox pc;
                {
                    pc = new CheckBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                        Visibility = Visibility.Hidden
                    };

                    pc.SetBinding(CheckBox.IsCheckedProperty, new Binding(nameof(PersonaPortableGameConfig.PC)));

                    Grid.SetRow(pc, 5);
                    Grid.SetColumn(pc, 1);
                    ConfigPropertyGrid.Children.Add(pc);
                }
                if (config.Game == Game.Persona4Golden)
                {
                    pc.Visibility = Visibility.Visible;
                    pcLabel.Visibility = Visibility.Visible;
                }
                else
                    pc.IsChecked = false;
            }
            if (config.Game != Game.Persona3 && config.Game != Game.Persona4)
            {
                // Add extra row
                ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());

                // Compression checkbox label
                {
                    var cpkCompressionLabel = new Label()
                    {
                        Content = "Use Compression",
                        ToolTip = "Check if CPK compression is required by " + config.Game.ToString(),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                        Width = 120
                    };

                    Grid.SetRow(cpkCompressionLabel, 3);
                    Grid.SetColumn(cpkCompressionLabel, 0);
                    ConfigPropertyGrid.Children.Add(cpkCompressionLabel);
                }

                // Cpk compression checkbox
                CheckBox cpkCompression;
                {
                    cpkCompression = new CheckBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                    };

                    cpkCompression.SetBinding(CheckBox.IsCheckedProperty, new Binding(nameof(PersonaPortableGameConfig.Compression)));

                    Grid.SetRow(cpkCompression, 3);
                    Grid.SetColumn(cpkCompression, 1);
                    ConfigPropertyGrid.Children.Add(cpkCompression);
                }
            }
            if (config.Game == Game.Persona5Royal)
            {
                // Add extra row
                ConfigPropertyGrid.RowDefinitions.Add(new RowDefinition());

                // Region label
                {
                    var regionLabel = new Label()
                    {
                        Content = "Region",
                        ToolTip = "ID must match your installed version of " + config.Game.ToString(),
                        HorizontalContentAlignment = HorizontalAlignment.Center,
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                        Width = 120
                    };

                    Grid.SetRow(regionLabel, 4);
                    Grid.SetColumn(regionLabel, 0);
                    ConfigPropertyGrid.Children.Add(regionLabel);
                }

                // Region combobox
                ComboBox region;
                {
                    region = new ComboBox()
                    {
                        VerticalContentAlignment = VerticalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Center,
                        Height = 35,
                    };
                    region.Items.Add("UP0177-CUSA17416");
                    region.Items.Add("EP0177-CUSA17419");
                    region.Items.Add("JP0005-CUSA08644");
                    //region.Items.Add("HP0177-CUSA17544");

                    region.SetBinding(ComboBox.SelectedValueProperty, new Binding(nameof(Persona5RoyalGameConfig.Region)));

                    Grid.SetRow(region, 4);
                    Grid.SetColumn(region, 1);
                    ConfigPropertyGrid.Children.Add(region);
                }
            }
        }

        private void ButtonOk_Click( object sender, RoutedEventArgs e )
        {
            Close();
        }

        private void ButtonOutputDirectoryPath_Click( object sender, RoutedEventArgs e )
        {
            var directory = SelectDirectory();
            if ( directory != null )
            {
                mConfig.OutputDirectoryPath = directory;
                OutputDirectoryPathTextBox.GetBindingExpression( TextBox.TextProperty ).UpdateTarget();
            }
        }

        private string SelectDirectory()
        {
            var dialog = new CommonOpenFileDialog();
            dialog.AllowNonFileSystemItems = true;
            dialog.IsFolderPicker = true;
            dialog.EnsurePathExists = true;
            dialog.EnsureValidNames = true;
            dialog.DefaultFileName = "Select directory";
            dialog.Title = "Select directory";

            if ( dialog.ShowDialog() == CommonFileDialogResult.Ok )
            {
                return dialog.FileName;
            }

            return null;
        }
        private string SelectFile( params CommonFileDialogFilter[] filters )
        {
            var dialog = new CommonOpenFileDialog();
            dialog.AllowNonFileSystemItems = true;
            dialog.IsFolderPicker = false;
            dialog.EnsurePathExists = true;
            dialog.EnsureValidNames = true;
            dialog.DefaultFileName = "Select file";
            dialog.Title = "Select file";
            foreach ( var filter in filters )
                dialog.Filters.Add( filter );

            if ( dialog.ShowDialog() == CommonFileDialogResult.Ok )
            {
                return dialog.FileName;
            }

            return null;
        }
    }
}
