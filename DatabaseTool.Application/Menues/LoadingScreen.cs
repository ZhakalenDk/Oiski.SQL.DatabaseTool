using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.SQL.DatabaseTool.Application.Menues
{
    public static class LoadingScreen
    {
        public static Window Container { get; } = new Window("Loading Screen");
        private static ColorableLabel message = null;
        public static void Init ()
        {
            Container.ResetMarker = false;

            message = Container.CreateControl<ColorableLabel>("Loading...", new Vector2());
            message.Position = PositionHelper.CenterControlOnX(0, message);
            message.Position = PositionHelper.CenterControlOnY(message.Position.x);
        }

        public static void Show (string _message, bool _show = true)
        {
            if ( _message != null )
            {
                message.Text = _message;
                message.Position = PositionHelper.CenterControlOnX(0, message);
                message.Position = PositionHelper.CenterControlOnY(message.Position.x);
            }

            Container.Show(_show);
        }
    }
}
