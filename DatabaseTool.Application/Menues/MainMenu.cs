using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.ConsoleTech.Engine.Controls;
using Oiski.SQL.DatabaseTool.Application.Menues;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Oiski.SQL.DatabaseTool.Application
{
    public static class MainMenu
    {
        private static readonly Menu menu = new Menu();
        public static RenderColor ControlTextColor { get; set; } = new RenderColor(ConsoleColor.Cyan, ConsoleColor.Black);
        public static RenderColor ControlBorderColor { get; set; } = new RenderColor(ConsoleColor.Blue, ConsoleColor.Black);

        public static ColorableLabel Header { get; set; }
        public static ColorableOption CreateDB { get; set; }
        public static ColorableOption AttachDB { get; set; }
        public static ColorableOption AssembleDB { get; set; }
        public static ColorableOption PopulateDB { get; set; }
        public static ColorableOption AssembleProcedures { get; set; }
        public static ColorableOption DeleteTablesAndProcedures { get; set; }
        public static ColorableOption DeleteTrace { get; set; }

        public static void Init()
        {
            Console.SetWindowSize(100, 27);
            //OiskiEngine.Configuration.Size = new Vector2(Console.WindowWidth, Console.WindowHeight);

            Header = new ColorableLabel("Oiski's Database Tool", ControlTextColor, ControlBorderColor, _attachToEngine: false);
            Header.Position = CenterControlOnX(0, Header);

            CreateDB = new ColorableOption("Create Database", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 0)
            };
            CreateDB.Position = CenterControlOnX(Header.Position.y + 4, CreateDB);
            CreateDB.BorderStyle(BorderArea.Horizontal, '~');
            CreateDB.OnSelect += (s) =>
            {
                OiskiEngine.Input.ResetSlection();
                Show(false);
                CreateDBMenu.Show();
            };

            AttachDB = new ColorableOption("Attach Database", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 1)
            };
            AttachDB.Position = CenterControlOnX(CreateDB.Position.y + 3, AttachDB);
            AttachDB.OnSelect += (s) =>
            {
                OiskiEngine.Input.ResetSlection();
                Show(false);
                AttachDBMenu.Show();
            };

            AssembleDB = new ColorableOption("Assemble Tables", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 2)
            };
            AssembleDB.Position = CenterControlOnX(AttachDB.Position.y + 3, AssembleDB);

            PopulateDB = new ColorableOption("Populate With Test Data", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 3)
            };
            PopulateDB.Position = CenterControlOnX(AssembleDB.Position.y + 3, PopulateDB);

            AssembleProcedures = new ColorableOption("Assemble Procedures", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 4)
            };
            AssembleProcedures.Position = CenterControlOnX(PopulateDB.Position.y + 3, AssembleProcedures);

            DeleteTablesAndProcedures = new ColorableOption("Delete Tables & Procedures", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 5)
            };
            DeleteTablesAndProcedures.Position = CenterControlOnX(AssembleProcedures.Position.y + 3, DeleteTablesAndProcedures);

            DeleteTrace = new ColorableOption("Delete Trace", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 6)
            };
            DeleteTrace.Position = CenterControlOnX(DeleteTablesAndProcedures.Position.y + 3, DeleteTrace);

            menu.Controls.AddControl(Header);
            menu.Controls.AddControl(CreateDB);
            menu.Controls.AddControl(AttachDB);
            menu.Controls.AddControl(AssembleDB);
            menu.Controls.AddControl(PopulateDB);
            menu.Controls.AddControl(AssembleProcedures);
            menu.Controls.AddControl(DeleteTablesAndProcedures);
            menu.Controls.AddControl(DeleteTrace);
        }

        private static Vector2 CenterControlOnX(int _positionY, Label _control, Label _control2 = null)
        {
            return new Vector2(( Console.WindowWidth / 2 ) - ( ( _control.Text.Length / 2 + 2 ) + ( ( _control2 != null ) ? ( _control2.Text.Length / 2 + 2 ) : ( 0 ) ) ), _positionY);
        }

        public static void Show(bool _show = true)
        {
            if ( _show )
            {
                Console.SetWindowSize(100, 27);
                OiskiEngine.Configuration.Size = new Vector2(Console.WindowWidth, Console.WindowHeight);
            }

            menu.Show(_show);
            OiskiEngine.Input.ResetSlection();
        }
    }
}
