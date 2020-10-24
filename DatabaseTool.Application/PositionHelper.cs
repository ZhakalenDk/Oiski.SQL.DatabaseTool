using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.SQL.DatabaseTool.Application
{
    public static class PositionHelper
    {
        public static Vector2 CenterControlOnX (int _positionY, Label _control)
        {
            return new Vector2(( Console.WindowWidth / 2 ) - ( _control.Text.Length / 2 + 2 ), _positionY);
        }

        public static Vector2 CenterControlOnY (int _positionX)
        {
            return new Vector2(_positionX, ( Console.WindowHeight / 2 ) - 1);
        }

        public static void CenterCombinedControlsOnX (int _positionY, Label _a, Label _b)
        {
            _a.Position = new Vector2(Console.WindowWidth / 2 - ( ( _a.Text.Length / 2 ) + ( _b.Text.Length / 2 ) + 2 ), _positionY);
            _b.Position = new Vector2(_a.Position.x + _a.Size.x - 1, _a.Position.y);
        }
    }
}
