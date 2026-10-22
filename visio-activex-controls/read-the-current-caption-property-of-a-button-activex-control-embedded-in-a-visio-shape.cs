using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: expect input file path and shape name
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <ShapeNameU>");
            return;
        }

        string visioPath = args[0];
        string targetShapeName = args[1];

        // Guard: ensure the Visio file exists
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Flag to indicate if the shape was found
            bool shapeFound = false;

            // Iterate through all pages and shapes to locate the target shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Compare the shape's NameU with the provided name
                    if (string.Equals(shape.NameU, targetShapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        shapeFound = true;

                        // Ensure the shape contains an ActiveX control
                        if (shape.ActiveXControl == null)
                        {
                            Console.WriteLine($"Shape '{targetShapeName}' does not contain an ActiveX control.");
                            return;
                        }

                        // Check if the ActiveX control is a CommandButton (Button) type
                        if (shape.ActiveXControl.Type == ControlType.CommandButton)
                        {
                            // Cast to the specific CommandButtonActiveXControl class
                            CommandButtonActiveXControl buttonCtrl = (CommandButtonActiveXControl)shape.ActiveXControl;

                            // Read the Caption property
                            string caption = buttonCtrl.Caption ?? string.Empty;

                            // Output the caption value
                            Console.WriteLine($"Caption of Button ActiveX control in shape '{targetShapeName}': {caption}");
                        }
                        else
                        {
                            Console.WriteLine($"ActiveX control in shape '{targetShapeName}' is not a CommandButton. Type: {shape.ActiveXControl.Type}");
                        }

                        // Since shape is found, exit loops
                        break;
                    }
                }

                if (shapeFound)
                    break;
            }

            if (!shapeFound)
            {
                Console.WriteLine($"Shape with NameU '{targetShapeName}' was not found in the diagram.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}