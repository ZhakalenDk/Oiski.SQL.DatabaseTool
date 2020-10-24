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
        /// <summary>
        /// The <see cref="Window"/> container that contains the functionality for establishing a visual <see cref="DatabaseTool"/> menu
        /// </summary>
        public static Window Container { get; } = new Window("Info Screen");

        /// <summary>
        /// The <see cref="RenderColor"/> to apply when simulating a succesful task completion
        /// </summary>
        public static RenderColor SuccesColor { get; set; } = new RenderColor(ConsoleColor.Green, ConsoleColor.Black);
        /// <summary>
        /// The <see cref="RenderColor"/> to apply when simulating an unsuccessful task completion
        /// </summary>
        public static RenderColor ErrorColor { get; set; } = new RenderColor(ConsoleColor.Red, ConsoleColor.Black);

        /// <summary>
        /// Initialize the visual <see cref="DatabaseTool"/> menu
        /// </summary>
        public static void Init()
        {
            Container.AllowMultiMarker = false;
            ColorableLabel header = Container.CreateControl<ColorableLabel>("Oiski's Database Tool", new Vector2());
            header.Position = PositionHelper.CenterControlOnX(0, header);
        }

        /// <summary>
        /// Display the <see cref="InfoScreen"/>.
        /// <br/>
        /// <paramref name="_infoInOrder"/> is read as: <strong>{{"Label Text", "Text Field Text", "State"}}</strong> and <paramref name="_infoInOrder"/> can't be higher or lower than <paramref name="_infoAmount"/>.
        /// <br/>
        /// <br/>
        /// <strong><paramref name="_infoInOrder"/> Value Types</strong>
        /// <list type="bullet">
        /// <item><strong>Label Text:</strong> <i>The Text that will be displayed as 'header' text</i></item>
        /// <item><strong>Text Field Text:</strong> <i>The Message</i></item>
        /// <item><strong>State:</strong> <i>Defines the color of the message text. [<strong>Success</strong>=Green] [<strong>Error</strong>=Red] [<strong>Default</strong>=Yellow]</i></item>
        /// </list>
        /// </summary>
        /// <param name="_infoAmount">The amount of <see cref="ColorableLabel"/>/<see cref="ColorableTextField"/> combi control to intantiate</param>
        /// <param name="_infoInOrder">The values to apply to each instantiated <see cref="ColorableLabel"/>/<see cref="ColorableTextField"/> combi control</param>
        public static void Show(int _infoAmount, string[,] _infoInOrder)
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

        /// <summary>
        /// Hide the <see cref="InfoScreen"/>
        /// </summary>
        public static void Hide()
        {
            Container.Show(false);

            for ( int i = 1; i < Container.GetCollection.GetControls.Count; i++ )
            {
                Container.GetCollection.RemoveControl(Container.GetCollection[i]);
                i = 0;
            }
        }
    }
}
