using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through pages and shapes to find a shape with an ActiveX control
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ActiveXControl != null)
                    {
                        // Example: attempt to cast to CommandButtonActiveXControl using the helper
                        try
                        {
                            CommandButtonActiveXControl button = CastActiveXControl<CommandButtonActiveXControl>(shape);
                            // Modify a property as a demonstration
                            button.Caption = "Updated Caption";
                            Console.WriteLine($"Shape ID {shape.ID} - CommandButton caption set to '{button.Caption}'.");
                        }
                        catch (InvalidCastException)
                        {
                            // The control is not a CommandButton; handle other types as needed
                            Console.WriteLine($"Shape ID {shape.ID} contains an ActiveX control of type {shape.ActiveXControl.Type}, not a CommandButton.");
                        }
                    }
                }
            }

            // Save the modified diagram (example output path)
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "ModifiedOutput.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or IO exceptions
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Generic helper that casts a shape's ActiveXControl to the specified concrete type.
    /// Throws InvalidCastException if the underlying control type does not match.
    /// </summary>
    /// <typeparam name="T">Concrete ActiveXControl type (e.g., CommandButtonActiveXControl).</typeparam>
    /// <param name="shape">Shape containing the ActiveX control.</param>
    /// <returns>Typed ActiveX control instance.</returns>
    static T CastActiveXControl<T>(Shape shape) where T : ActiveXControl
    {
        if (shape == null) throw new ArgumentNullException(nameof(shape));

        ActiveXControl activeX = shape.ActiveXControl;
        if (activeX == null)
            throw new InvalidOperationException("The shape does not contain an ActiveX control.");

        // Determine the expected ControlType based on the generic type argument
        ControlType expectedType;

        if (typeof(T) == typeof(CommandButtonActiveXControl))
            expectedType = ControlType.CommandButton;
        else if (typeof(T) == typeof(ImageActiveXControl))
            expectedType = ControlType.Image;
        else if (typeof(T) == typeof(CheckBoxActiveXControl))
            expectedType = ControlType.CheckBox;
        else if (typeof(T) == typeof(TextBoxActiveXControl))
            expectedType = ControlType.TextBox;
        else if (typeof(T) == typeof(ComboBoxActiveXControl))
            expectedType = ControlType.ComboBox;
        else if (typeof(T) == typeof(ListBoxActiveXControl))
            expectedType = ControlType.ListBox;
        else if (typeof(T) == typeof(SpinButtonActiveXControl))
            expectedType = ControlType.SpinButton;
        else if (typeof(T) == typeof(ScrollBarActiveXControl))
            expectedType = ControlType.ScrollBar;
        else if (typeof(T) == typeof(RadioButtonActiveXControl))
            expectedType = ControlType.RadioButton;
        else if (typeof(T) == typeof(LabelActiveXControl))
            expectedType = ControlType.Label;
        else if (typeof(T) == typeof(ToggleButtonActiveXControl))
            expectedType = ControlType.ToggleButton;
        else
            throw new NotSupportedException($"Casting to type {typeof(T).Name} is not supported by the helper.");

        // Verify that the actual control type matches the expected type
        if (activeX.Type != expectedType)
            throw new InvalidCastException($"ActiveX control type mismatch. Expected {expectedType}, but found {activeX.Type}.");

        // Safe cast to the concrete type
        return (T)activeX;
    }
}