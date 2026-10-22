using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Expect at least the input diagram path.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [shapeId]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Optional shape ID (Visio shape ID is a long).
        long shapeId = 0;
        bool hasShapeId = false;
        if (args.Length >= 2 && long.TryParse(args[1], out long parsedId))
        {
            shapeId = parsedId;
            hasShapeId = true;
        }

        try
        {
            // Load the diagram.
            Diagram diagram = new Diagram(inputPath);

            // For simplicity, work with the first page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            // Retrieve the target shape.
            Shape shape;
            if (hasShapeId)
            {
                shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Shape with ID {shapeId} not found on the first page.");
                    return;
                }
            }
            else
            {
                // If no ID provided, take the first shape that has an ActiveX control.
                shape = null;
                foreach (Shape s in page.Shapes)
                {
                    if (s.ActiveXControl != null)
                    {
                        shape = s;
                        break;
                    }
                }

                if (shape == null)
                {
                    Console.Error.WriteLine("No shape with an ActiveX control was found on the first page.");
                    return;
                }
            }

            // Use the helper to obtain the concrete ActiveX control.
            object concreteControl = ActiveXHelper.GetConcreteActiveXControl(shape);
            if (concreteControl == null)
            {
                Console.WriteLine("The selected shape does not contain an ActiveX control.");
                return;
            }

            // Demonstrate handling of a specific control type.
            if (concreteControl is CommandButtonActiveXControl button)
            {
                // Modify a property specific to CommandButtonActiveXControl.
                button.Caption = "Clicked!";
                Console.WriteLine("CommandButton caption updated.");
            }
            else if (concreteControl is CheckBoxActiveXControl checkBox)
            {
                // Toggle the checkbox state.
                // Checked = 1, Unchecked = 0 (cast to enum).
                checkBox.Value = (CheckValueType)1; // Checked
                Console.WriteLine("CheckBox set to Checked.");
            }
            else if (concreteControl is TextBoxActiveXControl textBox)
            {
                // Update the displayed text.
                textBox.Text = "Hello, Aspose!";
                Console.WriteLine("TextBox text updated.");
            }
            else
            {
                Console.WriteLine($"ActiveX control of type {concreteControl.GetType().Name} retrieved.");
            }

            // Save the modified diagram to a new file.
            string outputPath = Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "Modified_" + Path.GetFileName(inputPath));
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose or runtime exceptions.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

/// <summary>
/// Helper class that abstracts casting of ActiveXControl objects to their concrete types.
/// </summary>
static class ActiveXHelper
{
    /// <summary>
    /// Returns the concrete ActiveX control instance based on the control's <see cref="ControlType"/>.
    /// If the control type is not recognized, the original <see cref="ActiveXControl"/> instance is returned.
    /// </summary>
    /// <param name="shape">The shape containing the ActiveX control.</param>
    /// <returns>The concrete control object or null if no control is present.</returns>
    public static object GetConcreteActiveXControl(Shape shape)
    {
        if (shape == null)
            throw new ArgumentNullException(nameof(shape));

        // Retrieve the generic ActiveXControl reference.
        var genericControl = shape.ActiveXControl;
        if (genericControl == null)
            return null;

        // Determine the concrete type via the ControlType enumeration.
        switch (genericControl.Type)
        {
            case ControlType.CommandButton:
                return (CommandButtonActiveXControl)genericControl;
            case ControlType.CheckBox:
                return (CheckBoxActiveXControl)genericControl;
            case ControlType.TextBox:
                return (TextBoxActiveXControl)genericControl;
            case ControlType.Image:
                return (ImageActiveXControl)genericControl;
            case ControlType.ComboBox:
                return (ComboBoxActiveXControl)genericControl;
            case ControlType.ListBox:
                return (ListBoxActiveXControl)genericControl;
            case ControlType.SpinButton:
                return (SpinButtonActiveXControl)genericControl;
            case ControlType.ScrollBar:
                return (ScrollBarActiveXControl)genericControl;
            case ControlType.RadioButton:
                return (RadioButtonActiveXControl)genericControl;
            case ControlType.Label:
                return (LabelActiveXControl)genericControl;
            case ControlType.ToggleButton:
                return (ToggleButtonActiveXControl)genericControl;
            // Add additional cases as new control types become available.
            default:
                // Return the generic control if a specific mapping is not defined.
                return genericControl;
        }
    }
}