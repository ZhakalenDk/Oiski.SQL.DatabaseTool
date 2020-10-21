using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.ConsoleTech.Engine.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.SQL.DatabaseTool.Application.Menues
{
    public static class AttachDBMenu
    {
        private static readonly Menu menu = new Menu();
        public static RenderColor ControlTextColor { get; set; } = new RenderColor(ConsoleColor.Cyan, ConsoleColor.Black);
        public static RenderColor ControlBorderColor { get; set; } = new RenderColor(ConsoleColor.Blue, ConsoleColor.Black);

        public static ColorableLabel Header { get; private set; }
        public static ColorableLabel DBNameLabel { get; private set; }
        public static ColorableTextField DBNameTextField { get; private set; }
        public static ColorableLabel DBPathLabel { get; private set; }
        public static ColorableTextField DBPathTextField { get; private set; }
        public static ColorableLabel DBConfigLabel { get; private set; }
        public static ColorableTextField DBConfigTextField { get; private set; }
        public static ColorableOption CreateDB { get; private set; }
        public static ColorableOption BackButton { get; private set; }

        private static int previousSelected;

        public static void Init()
        {
            //Console.SetWindowSize(250, 27);

            #region Header
            Header = new ColorableLabel("Oiski's Database Tool", ControlTextColor, ControlBorderColor, _attachToEngine: false);
            Header.Position = PositionHelper.CenterControlOnX(0, Header);
            #endregion

            #region Database Name Controls
            DBNameLabel = new ColorableLabel("Database Name", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                Position = new Vector2(5, Header.Position.y + 4)
            };
            DBNameLabel.BorderStyle(BorderArea.Horizontal, '~');

            DBNameTextField = new ColorableTextField(string.Empty, new RenderColor(ConsoleColor.Green, ConsoleColor.Black), ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 0),
                EraseTextOnSelect = true,
                ResetAfterFirstWrite = true
            };
            DBNameTextField.BorderStyle(BorderArea.Horizontal, '~');
            DBNameTextField.Position = new Vector2(DBNameLabel.Position.x + DBNameLabel.Text.Length + 1, DBNameLabel.Position.y);
            #endregion

            #region Database Path Controls
            DBPathLabel = new ColorableLabel("Database Path", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                Position = new Vector2(5, DBNameTextField.Position.y + 3)
            };

            DBPathTextField = new ColorableTextField(string.Empty, new RenderColor(ConsoleColor.Green, ConsoleColor.Black), ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 1),
                EraseTextOnSelect = true,
                ResetAfterFirstWrite = true
            };
            DBPathTextField.Position = new Vector2(DBNameLabel.Position.x + DBNameLabel.Text.Length + 1, DBNameLabel.Position.y + 3);
            #endregion

            #region Database Config Controls
            DBConfigLabel = new ColorableLabel("Database Config Name", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                Position = new Vector2(5, DBPathTextField.Position.y + 3)
            };

            DBConfigTextField = new ColorableTextField(string.Empty, new RenderColor(ConsoleColor.Green, ConsoleColor.Black), ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 2),
                EraseTextOnSelect = true,
                ResetAfterFirstWrite = true
            };
            DBConfigTextField.Position = new Vector2(DBConfigLabel.Position.x + DBConfigLabel.Text.Length + 1, DBPathTextField.Position.y + 3);
            #endregion

            #region Attach & Back Button
            CreateDB = new ColorableOption("Attach Database", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 3),
                Position = new Vector2(DBPathTextField.Position.x, DBConfigTextField.Position.y + 6)
            };
            CreateDB.OnSelect += (s) =>
            {
                if ( !string.IsNullOrWhiteSpace(DBNameTextField.Text) && !string.IsNullOrWhiteSpace(DBPathTextField.Text) && !string.IsNullOrWhiteSpace(DBConfigTextField.Text) )
                {
                    Program.Tool = new DatabaseTool(DBNameTextField.Text, DBPathTextField.Text);

                    
                }
            };

            BackButton = new ColorableOption("Back", ControlTextColor, ControlBorderColor, _attachToEngine: false)
            {
                SelectedIndex = new Vector2(0, 4),
                Position = new Vector2(CreateDB.Position.x, CreateDB.Position.y + 3)
            };

            BackButton.OnSelect += (s) =>
            {
                s.BorderStyle(BorderArea.Horizontal, '-');
                Show(false);
                MainMenu.Show();
            };
            #endregion

            menu.Controls.AddControl(Header);
            menu.Controls.AddControl(DBNameLabel);
            menu.Controls.AddControl(DBNameTextField);
            menu.Controls.AddControl(DBPathLabel);
            menu.Controls.AddControl(DBPathTextField);
            menu.Controls.AddControl(DBConfigLabel);
            menu.Controls.AddControl(DBConfigTextField);
            menu.Controls.AddControl(CreateDB);
            menu.Controls.AddControl(BackButton);
        }

        public static void Show(bool _show = true)
        {
            if ( _show )
            {
                OiskiEngine.Configuration.Size = new Vector2(Console.WindowWidth, Console.WindowHeight);
                menu.OnTarget = OnTarget;
                OiskiEngine.Input.AtTarget += menu.OnTarget;
            }
            else
            {
                OiskiEngine.Input.AtTarget -= menu.OnTarget;
            }

            DBNameLabel.BorderStyle(BorderArea.Horizontal, '~');
            BackButton.BorderStyle(BorderArea.Horizontal, '-');
            menu.Show(_show);
            previousSelected = DBNameLabel.IndexID;
            OiskiEngine.Input.ResetSlection();
        }

        private static void OnTarget(SelectableControl _control)
        {
            if ( menu.Controls.FindControl(item => item.IndexID == previousSelected) is ColorableLabel previousLabel )
            {
                previousLabel.BorderStyle(BorderArea.Horizontal, '-');
            }

            if ( menu.Controls.FindControl(item => item.IndexID == _control.IndexID - 1) is ColorableLabel label )
            {
                label.BorderStyle(BorderArea.Horizontal, '~');
                previousSelected = label.IndexID;
            }
        }
    }
}
