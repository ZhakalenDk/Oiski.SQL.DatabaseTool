using System;
using System.IO;
using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.ConsoleTech.Engine.Controls;
using Oiski.SQL.DatabaseTool.Application.Menues;

namespace Oiski.SQL.DatabaseTool.Application
{
    class Program
    {
        public static DatabaseTool Tool { get; set; } = null;
        public static MySettingsCollection Settings { get; set; } = null;
        static void Main()
        {
            /*[Major Features], [Minor Features], [Fixes], [Code Revisions] | Each Section resets at 100*/
            Console.Title = "Oiski's Database Tool - v1.0.1.0";
            Console.SetWindowSize(100, 31);

            #region Initializaion
            MainMenu.Init();
            CreateMenu.Init();
            AttachMenu.Init();
            LoadingScreen.Init();
            InfoScreen.Init();
            MainMenu.Container.Show();
            #endregion

            #region Engine Configuration
            OiskiEngine.Input.SetNavigation("Horizontal", false);

            ColorRenderer renderer = new ColorRenderer()
            {
                DefaultColor = new RenderColor(ConsoleColor.DarkBlue, ConsoleColor.Black)
            };

            OiskiEngine.ChangeRenderer(renderer);
            #endregion

            OiskiEngine.Run();
        }
    }
}
