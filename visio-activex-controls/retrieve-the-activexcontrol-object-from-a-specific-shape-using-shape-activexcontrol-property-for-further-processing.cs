using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and shape ID.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramPath> <shapeId>");
            return;
        }

        string diagramPath = args[0];
        string shapeIdArg = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Parse shape ID (long).
        if (!long.TryParse(shapeIdArg, out long shapeId))
        {
            Console.Error.WriteLine($"Error: Invalid shape ID '{shapeIdArg}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: Diagram contains no pages.");
                    return;
                }

                // Retrieve the first page (adjust as needed for specific pages).
                Page page = diagram.Pages[0];

                // Retrieve the shape by its unique ID.
                Shape shape = page.Shapes.GetShape(shapeId);
                if (shape == null)
                {
                    Console.Error.WriteLine($"Error: No shape found with ID {shapeId} on page 0.");
                    return;
                }

                // Verify the shape contains an ActiveX control.
                if (shape.ActiveXControl == null)
                {
                    Console.Error.WriteLine($"Info: Shape ID {shapeId} does not contain an ActiveX control.");
                    return;
                }

                // Output the generic control type.
                ControlType ctrlType = shape.ActiveXControl.Type;
                Console.WriteLine($"Shape ID {shapeId} contains an ActiveX control of type: {ctrlType}");

                // Process specific control types.
                switch (ctrlType)
                {
                    case ControlType.CommandButton:
                        // Cast to CommandButtonActiveXControl to access its properties.
                        CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)shape.ActiveXControl;
                        Console.WriteLine($"CommandButton Caption: {cmdBtn.Caption}");
                        // Example modification (uncomment if needed):
                        // cmdBtn.Caption = "New Caption";
                        break;

                    case ControlType.CheckBox:
                        // Cast to CheckBoxActiveXControl.
                        CheckBoxActiveXControl chkBox = (CheckBoxActiveXControl)shape.ActiveXControl;
                        // The Value property indicates checked state (CheckValueType).
                        CheckValueType chkValue = chkBox.Value;
                        bool isChecked = chkValue == CheckValueType.Checked;
                        Console.WriteLine($"CheckBox is {(isChecked ? "checked" : "unchecked")}");
                        // Example toggle (uncomment if needed):
                        // chkBox.Value = isChecked ? (CheckValueType)0 : CheckValueType.Checked;
                        break;

                    case ControlType.TextBox:
                        // Cast to TextBoxActiveXControl.
                        TextBoxActiveXControl txtBox = (TextBoxActiveXControl)shape.ActiveXControl;
                        Console.WriteLine($"TextBox Text: {txtBox.Text}");
                        // Example modification (uncomment if needed):
                        // txtBox.Text = "Updated text";
                        break;

                    // Add handling for other control types as needed.
                    default:
                        Console.WriteLine("No specific handling implemented for this control type.");
                        break;
                }

                // Optionally, save the diagram after any modifications.
                // string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "output.vsdx");
                // diagram.Save(outputPath, SaveFileFormat.Vsdx);
                // Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Exception: {ex.Message}");
        }
    }
}