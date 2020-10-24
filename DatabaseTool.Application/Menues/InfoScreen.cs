using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Oiski.SQL.DatabaseTool.Application.Menues
{
    public static class InfoScreen
    {
        public static Window Container { get; } = new Window("Info Screen");

        public static RenderColor SuccesColor { get; set; } = new RenderColor(ConsoleColor.Green, ConsoleColor.Black);
        public static RenderColor ErrorColor { get; set; } = new RenderColor(ConsoleColor.Red, ConsoleColor.Black);

        public static void Init ()
        {
            Container.AllowMultiMarker = false;
            ColorableLabel header = Container.CreateControl<ColorableLabel>("Oiski's Database Tool", new Vector2());
            header.Position = PositionHelper.CenterControlOnX(0, header);
        }

        public static void Show (int _infoAmount, string[,] _infoInOrder)
        {
            if ( _infoInOrder.GetLength(0) > _infoAmount || _infoInOrder.GetLength(0) < _infoAmount )
            {
                throw new Exception("The amount of info elements in _infoInOrder may not be higher or lower than _infoAmount");
            }

            if ( _infoInOrder.GetLength(1) != 3 )
            {
                throw new Exception("The length of the second dimension in _infoInOrder must be exactly 3 in length");
            }

            ColorableLabel lastCreatedControl = null;
            for ( int i = 0, posY = 5; i < _infoAmount; i++, posY += 3 )
            {
                ColorableLabel infoHeader = Container.CreateControl<ColorableLabel>(_infoInOrder[i, 0], new Vector2());
                ColorableLabel infoText = Container.CreateControl<ColorableLabel>(_infoInOrder[i, 1], new Vector2());
                PositionHelper.CenterCombinedControlsOnX(posY, infoHeader, infoText);


                switch ( _infoInOrder[i, 2].ToLower() )
                {
                    case "success":
                        infoText.TextColor = SuccesColor;
                        break;
                    case "error":
                        infoText.TextColor = ErrorColor;
                        break;
                    default:
                        infoText.TextColor = new RenderColor(ConsoleColor.Yellow, ConsoleColor.Black);
                        break;
                }

                lastCreatedControl = infoText;
            }

            ColorableOption OKButton = Container.CreateControl<ColorableOption>("OK", new Vector2());
            OKButton.Position = PositionHelper.CenterControlOnX(lastCreatedControl.Position.y + 5, OKButton);
            OKButton.OnSelect += (s) =>
            {
                MainMenu.Container.Show();
                Hide();
            };

            Container.Show();
        }

        public static void Hide ()
        {
            Container.Show(false);

            for ( int i = 1; i < Container.GetCollection.GetControls.Count; i++ )
            {
                Container.GetCollection.RemoveControl(Container.GetCollection[i]);
            }
        }
    }
}
