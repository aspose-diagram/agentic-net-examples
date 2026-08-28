using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

public static class ActiveXHelper
{
    /// <summary>
    /// Retrieves the concrete ActiveX control of the requested type from the specified shape.
    /// Throws if the shape does not contain an ActiveX control or if the control cannot be cast to T.
    /// </summary>
    /// <typeparam name="T">Concrete ActiveX control type (e.g., CommandButtonActiveXControl).</typeparam>
    /// <param name="shape">The shape that holds the ActiveX control.</param>
    /// <returns>Instance of the concrete ActiveX control.</returns>
    public static T GetActiveXControl<T>(Shape shape) where T : ActiveXControl
    {
        if (shape == null) throw new ArgumentNullException(nameof(shape));

        var control = shape.ActiveXControl;
        if (control == null)
            throw new InvalidOperationException("The shape does not contain an ActiveX control.");

        if (control is T typedControl)
            return typedControl;

        throw new InvalidCastException(
            $"ActiveX control type mismatch. Expected: {typeof(T).Name}, Actual: {control.GetType().Name}.");
    }

    /// <summary>
    /// Returns the concrete ActiveX control based on the ControlType enumeration.
    /// If the type is not recognized, the original ActiveXControl instance is returned.
    /// </summary>
    /// <param name="shape">The shape that holds the ActiveX control.</param>
    /// <returns>Concrete ActiveXControl instance or null if none exists.</returns>
    public static ActiveXControl GetConcreteControl(Shape shape)
    {
        if (shape == null) throw new ArgumentNullException(nameof(shape));

        var control = shape.ActiveXControl;
        if (control == null) return null;

        switch (control.Type)
        {
            case ControlType.CommandButton:
                return (CommandButtonActiveXControl)control;
            case ControlType.ComboBox:
                return (ComboBoxActiveXControl)control;
            case ControlType.CheckBox:
                return (CheckBoxActiveXControl)control;
            case ControlType.ListBox:
                return (ListBoxActiveXControl)control;
            case ControlType.TextBox:
                return (TextBoxActiveXControl)control;
            case ControlType.SpinButton:
                return (SpinButtonActiveXControl)control;
            case ControlType.RadioButton:
                return (RadioButtonActiveXControl)control;
            case ControlType.Label:
                return (LabelActiveXControl)control;
            case ControlType.Image:
                return (ImageActiveXControl)control;
            case ControlType.ToggleButton:
                return (ToggleButtonActiveXControl)control;
            case ControlType.ScrollBar:
                return (ScrollBarActiveXControl)control;
            default:
                // Unknown or unhandled type – return the base instance.
                return control;
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
