using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.SQL.DatabaseTool.Application
{
    public static class PositionHelper
    {
        public static Vector2 CenterControlOnX(int _positionY, Label _control)
        {
            return new Vector2(( Console.WindowWidth / 2 ) - ( _control.Text.Length / 2 + 2 ), _positionY);
        }
    }
}
