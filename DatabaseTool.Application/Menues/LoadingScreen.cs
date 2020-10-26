using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;

namespace Oiski.SQL.DatabaseTool.Application.Menues
{
    public static class LoadingScreen
    {
        /// <summary>
        /// The <see cref="Window"/> container that contains the functionality for establishing a visual <see cref="DatabaseTool"/> menu
        /// </summary>
        public static Window Container { get; } = new Window("Loading Screen");
        private static ColorableLabel message = null;

        /// <summary>
        /// Initialize the visual <see cref="DatabaseTool"/> menu
        /// </summary>
        public static void Init()
        {
            Container.ResetMarker = false;

            message = Container.CreateControl<ColorableLabel>("Loading...", new Vector2());
            message.Position = PositionHelper.CenterControlOnX(0, message);
            message.Position = PositionHelper.CenterControlOnY(message.Position.x);
        }

        /// <summary>
        /// Display the <see cref="LoadingScreen"/>, if <paramref name="_show"/> is <see langword="true"/>, with the parsed <paramref name="_message"/>. Otherwise hide the <see cref="LoadingScreen"/>
        /// </summary>
        /// <param name="_message"></param>
        /// <param name="_show"></param>
        public static void Show(string _message, bool _show = true)
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
