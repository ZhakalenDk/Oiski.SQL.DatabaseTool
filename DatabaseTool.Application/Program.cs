using System;
using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.SQL.DatabaseTool.Application.Menues;

namespace Oiski.SQL.DatabaseTool.Application
{
    class Program
    {
        public static DatabaseTool Tool { get; set; }
        static void Main()
        {
            MainMenu.Init();
            CreateDBMenu.Init();
            AttachDBMenu.Init();
            MainMenu.Show();

            ColorRenderer renderer = new ColorRenderer()
            {
                DefaultColor = new RenderColor(ConsoleColor.DarkBlue, ConsoleColor.Black)
            };

            OiskiEngine.ChangeRenderer(renderer);
            OiskiEngine.Run();
        }
    }
}
