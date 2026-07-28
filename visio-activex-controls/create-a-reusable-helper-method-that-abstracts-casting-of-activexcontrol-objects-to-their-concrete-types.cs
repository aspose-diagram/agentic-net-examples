using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

namespace DiagramActiveXHelper
{
    /// <summary>
    /// Provides utility methods for working with ActiveX controls.
    /// </summary>
    public static class ActiveXHelper
    {
        /// <summary>
        /// Casts an <see cref="ActiveXControl"/> to the specified concrete type.
        /// Throws <see cref="InvalidCastException"/> if the control is not of the requested type.
        /// </summary>
        /// <typeparam name="T">Concrete ActiveX control type (e.g., CommandButtonActiveXControl).</typeparam>
        /// <param name="control">The base ActiveXControl instance.</param>
        /// <returns>The control cast to the concrete type.</returns>
        public static T CastTo<T>(ActiveXControl control) where T : ActiveXControl
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            if (control is T typedControl)
                return typedControl;

            throw new InvalidCastException(
                $"ActiveXControl of type '{control.GetType().Name}' cannot be cast to '{typeof(T).Name}'.");
        }

        /// <summary>
        /// Retrieves the concrete ActiveX control based on its <see cref="ControlType"/> enumeration.
        /// Returns the control as the appropriate derived class, or the original instance if the type is unknown.
        /// </summary>
        /// <param name="control">The base ActiveXControl instance.</param>
        /// <returns>The control cast to its concrete class.</returns>
        public static ActiveXControl GetConcreteControl(ActiveXControl control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            switch (control.Type)
            {
                case ControlType.CommandButton:
                    return CastTo<CommandButtonActiveXControl>(control);
                case ControlType.ComboBox:
                    return CastTo<ComboBoxActiveXControl>(control);
                case ControlType.CheckBox:
                    return CastTo<CheckBoxActiveXControl>(control);
                case ControlType.ListBox:
                    return CastTo<ListBoxActiveXControl>(control);
                case ControlType.TextBox:
                    return CastTo<TextBoxActiveXControl>(control);
                case ControlType.SpinButton:
                    return CastTo<SpinButtonActiveXControl>(control);
                case ControlType.RadioButton:
                    return CastTo<RadioButtonActiveXControl>(control);
                case ControlType.Label:
                    return CastTo<LabelActiveXControl>(control);
                case ControlType.Image:
                    return CastTo<ImageActiveXControl>(control);
                case ControlType.ToggleButton:
                    return CastTo<ToggleButtonActiveXControl>(control);
                case ControlType.ScrollBar:
                    return CastTo<ScrollBarActiveXControl>(control);
                default:
                    // Unknown or unsupported type – return as‑is.
                    return control;
            }
        }
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            try
            {

                // Load or create a diagram (placeholder path)
                Diagram diagram = new Diagram();

                // Assume we have a shape that contains an ActiveX control
                // For demonstration, retrieve the first shape on the active page
                if (diagram.ActivePage.Shapes.Count > 0)
                {
                    var shape = diagram.ActivePage.Shapes[0];

                    // Ensure the shape actually has an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Use the helper to get the concrete control
                        ActiveXControl concrete = ActiveXHelper.GetConcreteControl(shape.ActiveXControl);

                        // Example: if it's a command button, set its caption
                        if (concrete is CommandButtonActiveXControl button)
                        {
                            button.Caption = "Clicked!";
                            Console.WriteLine("Command button caption set.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Shape does not contain an ActiveX control.");
                    }
                }
                else
                {
                    Console.WriteLine("Diagram contains no shapes.");
                }

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }
}