using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

public static class ActiveXHelper
{
    // Returns the concrete ActiveXControl cast to the requested type.
    // Throws if the shape has no ActiveX control or the cast is invalid.
    public static T CastActiveXControl<T>(Shape shape) where T : ActiveXControl
    {
        if (shape == null) throw new ArgumentNullException(nameof(shape));
        if (shape.ActiveXControl == null) throw new InvalidOperationException("Shape does not contain an ActiveX control.");

        // Direct cast works for compatible types.
        if (shape.ActiveXControl is T target) return target;

        // Fallback: map ControlType to the expected concrete class.
        ControlType type = shape.ActiveXControl.Type;

        if (typeof(T) == typeof(CommandButtonActiveXControl) && type == ControlType.CommandButton)
            return (T)(object)(CommandButtonActiveXControl)shape.ActiveXControl;

        if (typeof(T) == typeof(ImageActiveXControl) && type == ControlType.Image)
            return (T)(object)(ImageActiveXControl)shape.ActiveXControl;

        if (typeof(T) == typeof(CheckBoxActiveXControl) && type == ControlType.CheckBox)
            return (T)(object)(CheckBoxActiveXControl)shape.ActiveXControl;

        if (typeof(T) == typeof(TextBoxActiveXControl) && type == ControlType.TextBox)
            return (T)(object)(TextBoxActiveXControl)shape.ActiveXControl;

        if (typeof(T) == typeof(SpinButtonActiveXControl) && type == ControlType.SpinButton)
            return (T)(object)(SpinButtonActiveXControl)shape.ActiveXControl;

        // Add additional mappings here as needed.

        throw new InvalidCastException($"ActiveX control of type '{type}' cannot be cast to '{typeof(T).Name}'.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // See classes above
    }
}
