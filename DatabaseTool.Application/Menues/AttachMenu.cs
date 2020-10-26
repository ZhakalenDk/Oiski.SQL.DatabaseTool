﻿using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using System;
using System.IO;

namespace Oiski.SQL.DatabaseTool.Application.Menues
{
    public static class AttachMenu
    {
        /// <summary>
        /// The <see cref="Window"/> container that contains the functionality for establishing a visual <see cref="DatabaseTool"/> menu
        /// </summary>
        public static Window Container { get; } = new Window("Attach Menu");

        /// <summary>
        /// Initialize the visual <see cref="DatabaseTool"/> menu
        /// </summary>
        public static void Init()
        {
            ColorableLabel header = Container.CreateControl<ColorableLabel>("Oiski's Database Tool", new Vector2());
            header.Position = PositionHelper.CenterControlOnX(0, header);

            ColorableLabel nameLabel = Container.CreateControl<ColorableLabel>("Database Name", new Vector2(0, header.Position.y + 5));
            ColorableTextField nameTextField = Container.CreateControl<ColorableTextField>(string.Empty, new Vector2(nameLabel.Position.x + nameLabel.Size.x - 1, nameLabel.Position.y));
            nameTextField.TextColor = new RenderColor(ConsoleColor.Green, ConsoleColor.Black);

            ColorableLabel pathLabel = Container.CreateControl<ColorableLabel>("Database Path", new Vector2(0, nameLabel.Position.y + 3));
            ColorableTextField pathTextField = Container.CreateControl<ColorableTextField>(string.Empty, new Vector2(pathLabel.Position.x + pathLabel.Size.x - 1, pathLabel.Position.y));
            pathTextField.TextColor = new RenderColor(ConsoleColor.Green, ConsoleColor.Black);

            ColorableOption createDatabaseButton = Container.CreateControl<ColorableOption>("Attach Database", new Vector2());
            createDatabaseButton.Position = PositionHelper.CenterControlOnX(pathLabel.Position.y + 5, createDatabaseButton);
            createDatabaseButton.OnSelect += (s) =>
            {
                //  Make sure that the user typed somehting in the text fields before trying to attach a database
                if ( !string.IsNullOrWhiteSpace(nameTextField.Text) && !string.IsNullOrWhiteSpace(pathTextField.Text) )
                {
                    Container.Show(false);
                    LoadingScreen.Show("Attaching Database. Standby...");
                    Program.Tool = null;

                    string dbPath = $"{pathTextField.Text}\\{nameTextField.Text}.mdf";
                    string configPath = $"{pathTextField.Text}\\{nameTextField.Text}_Settings.xml";
                    bool mdfExists = File.Exists(dbPath);
                    bool configExists = File.Exists(configPath);

                    if ( mdfExists && configExists )
                    {
                        Program.Tool = new DatabaseTool(nameTextField.Text, pathTextField.Text);
                        Program.Settings = new MySettingsCollection($"{Program.Tool.DBName}_Settings");
                        Program.Settings.AddSetting("ConnectionString", Program.Tool.ConnectionString);
                        Program.Settings.Save(pathTextField.Text);
                    }

                    InfoScreen.Show(2, new string[,] { { "Database Attached", $"{mdfExists}", ( ( mdfExists ) ? ( "Success" ) : ( "Error" ) ) }, { "Config File Attached", $"{configExists}", ( ( configExists ) ? ( "Success" ) : ( "Error" ) ) } });

                    LoadingScreen.Show(null, false);
                }
            };

            ColorableOption backButton = Container.CreateControl<ColorableOption>("Back", new Vector2());
            backButton.Position = PositionHelper.CenterControlOnX(createDatabaseButton.Position.y + 3, backButton);
            backButton.OnSelect += (s) =>
            {
                MainMenu.Container.Show();
                Container.Show(false);
            };
        }
    }
}
