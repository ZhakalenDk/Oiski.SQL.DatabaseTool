using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.ConsoleTech.Engine.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.SQL.DatabaseTool.Application.Menues
{
    public class LoadingWindow
    {
        private static readonly Menu menu = new Menu();
        public static RenderColor ControlTextColor { get; set; } = new RenderColor(ConsoleColor.Cyan, ConsoleColor.Black);
        public static RenderColor ControlBorderColor { get; set; } = new RenderColor(ConsoleColor.Blue, ConsoleColor.Black);

        public static ColorableLabel Message { get; private set; }

        public static void Init()
        {
            #region Header
            Message = new ColorableLabel("Loading...", ControlTextColor, ControlBorderColor, _attachToEngine: false);
            Message.Position = PositionHelper.CenterControlOnX(0, Message);
            Message.Position = new Vector2(Message.Position.x, Message.Position.y + 3);
            #endregion

            menu.Controls.AddControl(Message);
        }

        public static void Show(bool _show = true)
        {
            menu.Show(_show);
            OiskiEngine.Input.ResetSlection();
        }

        public static void Show(string _loadingMessage)
        {
            OiskiEngine.Input.SetNavigation("Vertical", true);
            OiskiEngine.Input.SetSelect(false);
            Message.Text = _loadingMessage;
            Message.Position = PositionHelper.CenterControlOnX(0, Message);
            Message.Position = new Vector2(Message.Position.x, Message.Position.y + 3);
            Show();
        }
    }
}
