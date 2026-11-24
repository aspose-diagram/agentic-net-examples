using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Expect the diagram file path as the first command‑line argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard against missing file.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Locate the first shape that contains an ActiveX control.
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.ActiveXControl != null)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape with an ActiveX control was found in the diagram.");
                return;
            }

            // Attempt to cast the ActiveX control to a CommandButtonActiveXControl.
            try
            {
                CommandButtonActiveXControl button = (CommandButtonActiveXControl)targetShape.ActiveXControl;
                // Cast succeeded – modify a property.
                button.Caption = "Updated Caption";
                Console.WriteLine("ActiveX control cast succeeded; caption updated.");
            }
            catch (InvalidCastException ex)
            {
                // Handle the case where the control is not a CommandButton.
                Console.Error.WriteLine($"InvalidCastException: The ActiveX control is not a CommandButton. Details: {ex.Message}");

                // Example of handling a different control type (CheckBox).
                if (targetShape.ActiveXControl.Type == ControlType.CheckBox)
                {
                    // Safe cast because we verified the type.
                    CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)targetShape.ActiveXControl;
                    checkBox.Value = CheckValueType.Checked;
                    Console.WriteLine("Handled as CheckBoxActiveXControl and set to Checked.");
                }
                else
                {
                    Console.WriteLine($"ActiveX control type is {targetShape.ActiveXControl.Type}; no specific handling implemented.");
                }
            }

            // Save the modified diagram to a new file.
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? string.Empty, "output.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            // General error handling for Aspose operations.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}