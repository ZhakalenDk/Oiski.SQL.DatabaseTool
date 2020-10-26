using Oiski.ConsoleTech.Engine;
using Oiski.ConsoleTech.Engine.Color.Controls;
using Oiski.ConsoleTech.Engine.Color.Rendering;
using Oiski.ConsoleTech.Engine.Controls;
using System;

namespace Oiski.SQL.DatabaseTool.Application.Menues
{
    public class Window
    {
        private readonly Menu menu = new Menu();
        private Vector2 previousSelectedPosition = new Vector2(0, 0);
        private Vector2 previousSelectedIndex = new Vector2(0, 0);

        public string Name { get; }
        public bool ResetMarker { get; set; } = true;
        public bool AllowMultiMarker { get; set; } = true;
        public ControlCollection GetCollection
        {
            get
            {
                return menu.Controls;
            }
        }
        public RenderColor DefaultTextColor { get; set; } = new RenderColor(ConsoleColor.Cyan, ConsoleColor.Black);
        public RenderColor DefaultBorderColor { get; set; } = new RenderColor(ConsoleColor.Blue, ConsoleColor.Black);

        public T CreateControl<T> (string _text, Vector2 _position) where T : Control
        {
            T control;
            if ( typeof(IColorableControl).IsAssignableFrom(typeof(T)) )
            {
                control = CreateColorControl<T>(_text, _position, DefaultTextColor, DefaultBorderColor);
            }
            else
            {
                control = CreateNonColorControl<T>(_text, _position);
            }

            if ( control is SelectableControl sControl )
            {
                SetSelectionIndex(sControl);
            }

            menu.Controls.AddControl(control);
            return control;
        }

        private void SetSelectionIndex (SelectableControl _control)
        {
            int xIndex = ( ( OiskiEngine.Input.HorizontalNavigationEnabled && !OiskiEngine.Input.VerticalNavigationEnabled ) ? ( menu.Controls.GetSelectableControls.Count ) : ( 0 ) );
            int yIndex = ( ( OiskiEngine.Input.VerticalNavigationEnabled ) ? ( menu.Controls.GetSelectableControls.Count ) : ( 0 ) );
            _control.SelectedIndex = new Vector2(xIndex, yIndex);
        }

        private T CreateNonColorControl<T> (string _text, Vector2 _position) where T : Control
        {
            T control = null;

            if ( typeof(T) == typeof(Label) )
            {
                control = new Label(_text, _position, _attachToEngine: false) as T;
            }
            else if ( typeof(T) == typeof(Option) )
            {
                control = new Option(_text, _position, _attachToEngine: false) as T;
            }
            else if ( typeof(T) == typeof(TextField) )
            {
                control = new TextField(_text, _position, _attachToEngine: false) as T;
            }

            return control;
        }

        private T CreateColorControl<T> (string _text, Vector2 _position, RenderColor _textColor, RenderColor _borderColor) where T : Control
        {
            T control = null;

            if ( typeof(T) == typeof(ColorableLabel) )
            {
                control = new ColorableLabel(_text, _textColor, _borderColor, _position, _attachToEngine: false) as T;
            }
            else if ( typeof(T) == typeof(ColorableOption) )
            {
                control = new ColorableOption(_text, _textColor, _borderColor, _position, _attachToEngine: false) as T;
            }
            else if ( typeof(T) == typeof(ColorableTextField) )
            {
                control = new ColorableTextField(_text, _textColor, _borderColor, _position, _attachToEngine: false) as T;
            }

            return control;
        }

        public void Show (bool _show = true)
        {
            if ( _show )
            {
                if ( ResetMarker )
                {
                    OiskiEngine.Input.ResetSlection();
                    var previousControl = menu.Controls.FindControl(previousSelectedIndex);

                    if ( previousControl != null )
                    {
                        previousControl.BorderStyle(BorderArea.Horizontal, '-');

                        if ( menu.Controls.FindControl(item => item.IndexID == previousControl.IndexID - 1 && item.IndexID != menu.Controls[0].IndexID) is ColorableLabel previousLabel )
                        {
                            previousLabel.BorderStyle(BorderArea.Horizontal, '-');
                        }
                    }

                    var firstControl = menu.Controls.FindControl(new Vector2(0, 0));

                    if ( firstControl != null )
                    {
                        firstControl.BorderStyle(BorderArea.Horizontal, '~');
                        if ( AllowMultiMarker )
                        {
                            if ( menu.Controls.FindControl(item => item.IndexID == firstControl.IndexID - 1 && item.IndexID != menu.Controls[0].IndexID) is ColorableLabel firstLabel )
                            {
                                firstLabel.BorderStyle(BorderArea.Horizontal, '~');
                                previousSelectedPosition = firstLabel.Position;
                            }
                        }

                    }
                }

                if ( AllowMultiMarker )
                {
                    menu.OnTarget = OnTarget;
                    OiskiEngine.Input.AtTarget += menu.OnTarget;
                }

                OiskiEngine.Input.SetNavigation("Vertical", true);
                OiskiEngine.Input.SetSelect(true);
            }
            else
            {
                if ( AllowMultiMarker )
                {
                    OiskiEngine.Input.AtTarget -= menu.OnTarget;
                }
            }

            menu.Show(_show);
        }

        private void OnTarget (SelectableControl _control)
        {
            if ( menu.Controls.FindControl(item => item.Position == previousSelectedPosition) is ColorableLabel previousLabel )
            {
                previousLabel.BorderStyle(BorderArea.Horizontal, '-');
            }

            if ( menu.Controls.FindControl(item => item.IndexID == _control.IndexID - 1 && item.IndexID != menu.Controls[0].IndexID) is ColorableLabel label )
            {
                label.BorderStyle(BorderArea.Horizontal, '~');
                previousSelectedPosition = label.Position;
            }

            previousSelectedIndex = _control.SelectedIndex;
        }

        public override string ToString ()
        {
            return Name;
        }

        public Window (string _name)
        {
            Name = _name;
        }
    }
}
