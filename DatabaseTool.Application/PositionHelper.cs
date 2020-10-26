using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Controls;
using System;

namespace Oiski.SQL.DatabaseTool.Application
{
    public static class PositionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_positionY"></param>
        /// <param name="_control"></param>
        /// <returns>Returns a <see cref="Vector2"/> that defines the center position, on the x-axis, in the screenspace window for <paramref name="_control"/></returns>
        public static Vector2 CenterControlOnX(int _positionY, Label _control)
        {
            return new Vector2(( Console.WindowWidth / 2 ) - ( _control.Text.Length / 2 + 2 ), _positionY);
        }

        /// <summary>
        /// Position <pa
        /// </summary>
        /// <param name="_positionX"></param>
        /// <returns>Returns a <see cref="Vector2"/> that defines the center position, on the y-axis, in the screenspace window for <paramref name="_control"/></returns>
        public static Vector2 CenterControlOnY(int _positionX)
        {
            return new Vector2(_positionX, ( Console.WindowHeight / 2 ) - 1);
        }

        /// <summary>
        /// Center the position of two <see cref="Label"/> <see cref="Control"/>s based on each other as a combined <see cref="Control"/>
        /// </summary>
        /// <param name="_positionY"></param>
        /// <param name="_a"></param>
        /// <param name="_b"></param>
        public static void CenterCombinedControlsOnX(int _positionY, Label _a, Label _b)
        {
            _a.Position = new Vector2(Console.WindowWidth / 2 - ( ( _a.Text.Length / 2 ) + ( _b.Text.Length / 2 ) + 2 ), _positionY);
            _b.Position = new Vector2(_a.Position.x + _a.Size.x - 1, _a.Position.y);
        }
    }
}
