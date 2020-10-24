using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Oiski.SQL.DatabaseTool.Application.Menues
{
    public static class MainMenu
    {
        public static Window Container { get; } = new Window("Main Menu");
        public static void Init ()
        {
            ColorableLabel header = Container.CreateControl<ColorableLabel>("Oiski's Database Tool", new Vector2());
            header.Position = PositionHelper.CenterControlOnX(0, header);

            ColorableOption newDatabaseButton = Container.CreateControl<ColorableOption>("Create Database", new Vector2(header.Position.x, header.Position.y + 5));
            newDatabaseButton.Position = PositionHelper.CenterControlOnX(newDatabaseButton.Position.y, newDatabaseButton);
            newDatabaseButton.OnSelect += (s) =>
            {
                CreateMenu.Container.Show();
                Container.Show(false);
            };

            ColorableOption attachDatabaseButton = Container.CreateControl<ColorableOption>("Attach Database", new Vector2(header.Position.x, newDatabaseButton.Position.y + 3));
            attachDatabaseButton.Position = PositionHelper.CenterControlOnX(attachDatabaseButton.Position.y, attachDatabaseButton);
            attachDatabaseButton.OnSelect += (s) =>
            {
                AttachMenu.Container.Show();
                Container.Show(false);
            };

            ColorableOption assembleDatabaseButton = Container.CreateControl<ColorableOption>("Assemble Tables", new Vector2(header.Position.x, attachDatabaseButton.Position.y + 3));
            assembleDatabaseButton.Position = PositionHelper.CenterControlOnX(assembleDatabaseButton.Position.y, assembleDatabaseButton);
            assembleDatabaseButton.OnSelect += (s) =>
            {
                if ( Program.Tool != null )
                {
                    Container.ResetMarker = false;
                    Container.Show(false);
                    LoadingScreen.Show("Creating Tables. Standby...");
                    Program.Tool.AssembleDatabase();
                    Container.Show();
                    Container.ResetMarker = true;
                    LoadingScreen.Show(null, false);
                }
                else
                {
                    InfoScreen.Show(1, new string[,] { { "Error", "No Database Found", "error" } });
                    Container.Show(false);
                }
            };

            ColorableOption populateDatabaseButton = Container.CreateControl<ColorableOption>("Populate Tables", new Vector2(header.Position.x, assembleDatabaseButton.Position.y + 3));
            populateDatabaseButton.Position = PositionHelper.CenterControlOnX(populateDatabaseButton.Position.y, populateDatabaseButton);
            populateDatabaseButton.OnSelect += (s) =>
            {
                if ( Program.Tool != null )
                {
                    Container.ResetMarker = false;
                    Container.Show(false);
                    LoadingScreen.Show("Populating Database. Standby...");
                    Program.Tool.PopulateDatabase();
                    Container.Show();
                    Container.ResetMarker = true;
                    LoadingScreen.Show(null, false);
                }
                else
                {
                    InfoScreen.Show(1, new string[,] { { "Error", "No Database Found", "error" } });
                    Container.Show(false);
                }
            };

            ColorableOption assembleProceduresButton = Container.CreateControl<ColorableOption>("Assemble Procedures", new Vector2(header.Position.x, populateDatabaseButton.Position.y + 3));
            assembleProceduresButton.Position = PositionHelper.CenterControlOnX(assembleProceduresButton.Position.y, assembleProceduresButton);
            assembleProceduresButton.OnSelect += (s) =>
            {
                if ( Program.Tool != null )
                {
                    Container.ResetMarker = false;
                    Container.Show(false);
                    LoadingScreen.Show("Creating Procedures. Standby...");
                    Program.Tool.CreateProcedures();
                    Container.Show();
                    Container.ResetMarker = true;
                    LoadingScreen.Show(null, false);
                }
                else
                {
                    InfoScreen.Show(1, new string[,] { { "Error", "No Database Found", "error" } });
                    Container.Show(false);
                }
            };

            ColorableOption deleteDataButton = Container.CreateControl<ColorableOption>("Delete Data", new Vector2(header.Position.x, assembleProceduresButton.Position.y + 3));
            deleteDataButton.Position = PositionHelper.CenterControlOnX(deleteDataButton.Position.y, deleteDataButton);
            deleteDataButton.OnSelect += (s) =>
            {
                if ( Program.Tool != null )
                {
                    Container.ResetMarker = false;
                    Container.Show(false);
                    LoadingScreen.Show("Deleting Data. Standby...");
                    Program.Tool.DeleteData();
                    Container.Show();
                    Container.ResetMarker = true;
                    LoadingScreen.Show(null, false);
                }
                else
                {
                    InfoScreen.Show(1, new string[,] { { "Error", "No Database Found", "error" } });
                    Container.Show(false);
                }
            };

            ColorableOption deleteDatabaseButton = Container.CreateControl<ColorableOption>("Delete Database", new Vector2(header.Position.x, deleteDataButton.Position.y + 3));
            deleteDatabaseButton.Position = PositionHelper.CenterControlOnX(deleteDatabaseButton.Position.y, deleteDatabaseButton);
            deleteDatabaseButton.OnSelect += (s) =>
            {
                if ( Program.Tool != null )
                {
                    Container.ResetMarker = false;
                    Container.Show(false);
                    LoadingScreen.Show("Deleting Trace. Standby...");
                    Program.Tool.DeleteDatabase(true);
                    if ( File.Exists($"{Program.Tool.PathToDatabase}\\{Program.Tool.DBName}_Settings.xml") )
                    {
                        File.Delete(( $"{Program.Tool.PathToDatabase}\\{Program.Tool.DBName}_Settings.xml" ));
                    }
                    Program.Tool = null;
                    Container.Show();
                    Container.ResetMarker = true;
                    LoadingScreen.Show(null, false);
                }
                else
                {
                    InfoScreen.Show(1, new string[,] { { "Error", "No Database Found", "error" } });
                    Container.Show(false);
                }
            };

            ColorableOption exitButton = Container.CreateControl<ColorableOption>("Close Application", new Vector2(header.Position.x, deleteDatabaseButton.Position.y + 3));
            exitButton.Position = PositionHelper.CenterControlOnX(exitButton.Position.y, exitButton);
            exitButton.OnSelect += (s) => Environment.Exit(0);
        }
    }
}
