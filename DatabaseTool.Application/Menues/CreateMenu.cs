using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Oiski.SQL.DatabaseTool.Application.Menues
{
    public static class CreateMenu
    {
        /// <summary>
        /// The <see cref="Window"/> container that contains the functionality for establishing a visual <see cref="DatabaseTool"/> menu
        /// </summary>
        public static Window Container { get; } = new Window("Create Menu");

        /// <summary>
        /// Initialize the visual <see cref="DatabaseTool"/> menu
        /// </summary>
        public static void Init ()
        {
            ColorableLabel header = Container.CreateControl<ColorableLabel>("Oiski's Database Tool", new Vector2());
            header.Position = PositionHelper.CenterControlOnX(0, header);

            ColorableLabel nameLabel = Container.CreateControl<ColorableLabel>("Database Name", new Vector2(0, header.Position.y + 5));
            ColorableTextField nameTextField = Container.CreateControl<ColorableTextField>(string.Empty, new Vector2(nameLabel.Position.x + nameLabel.Size.x - 1, nameLabel.Position.y));
            nameTextField.TextColor = new RenderColor(ConsoleColor.Green, ConsoleColor.Black);

            ColorableLabel pathLabel = Container.CreateControl<ColorableLabel>("Database Path", new Vector2(0, nameLabel.Position.y + 3));
            ColorableTextField pathTextField = Container.CreateControl<ColorableTextField>(string.Empty, new Vector2(pathLabel.Position.x + pathLabel.Size.x - 1, pathLabel.Position.y));
            pathTextField.TextColor = new RenderColor(ConsoleColor.Green, ConsoleColor.Black);

            ColorableOption createDatabaseButton = Container.CreateControl<ColorableOption>("Create Database", new Vector2());
            createDatabaseButton.Position = PositionHelper.CenterControlOnX(pathLabel.Position.y + 5, createDatabaseButton);
            createDatabaseButton.OnSelect += (s) =>
            {
                //  Make sure the user typed somehting in the text fields before attempting to create a database
                if ( !string.IsNullOrWhiteSpace(nameTextField.Text) && !string.IsNullOrWhiteSpace(pathTextField.Text) && Program.Tool == null )
                {
                    Container.Show(false);
                    LoadingScreen.Show("Creating Database. Standby...");

                    Program.Tool = new DatabaseTool(nameTextField.Text, pathTextField.Text);

                    if ( Program.Tool != null && !Program.Tool.CreateDatabase() )
                    {
                        //  Error Handling


                        InfoScreen.Show(2, new string[,] { { "Database Created", "False", "Error" }, { "Error", "See Config File", string.Empty } });
                        Process.Start("notepad.exe", $"{Program.Tool.PathToDatabase}\\{Program.Tool.DBName}_Log.txt");
                        Program.Tool = null;
                    }
                    else    //  If Succesful
                    {
                        Program.Settings = new MySettingsCollection($"{Program.Tool.DBName}_Settings");
                        Program.Settings.AddSetting("ConnectionString", Program.Tool.ConnectionString);
                        Program.Settings.Save(pathTextField.Text);

                        bool mdfExists = Program.Tool.Exists();
                        bool configExists = Program.Settings.Load(Program.Tool.PathToDatabase);
                        InfoScreen.Show(2, new string[,] { { "Database Created", $"{mdfExists}", ( ( mdfExists ) ? ( "Success" ) : ( "Error" ) ) }, { "Config File Created", $"{configExists}", ( ( configExists ) ? ( "Success" ) : ( "Error" ) ) } });
                    }

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
