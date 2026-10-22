using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                        {
                            continue;
                        }

                        // Process only shapes that contain an ActiveX control.
                        if (shape.ActiveXControl != null)
                        {
                            // Retrieve the type of the ActiveX control.
                            ControlType controlType = shape.ActiveXControl.Type;

                            // Example processing: output shape and control information.
                            Console.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" contains ActiveX control of type {controlType}.");

                            // Additional processing for specific control types can be added here.
                            // Example:
                            // if (controlType == ControlType.CommandButton)
                            // {
                            //     CommandButtonActiveXControl btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                            //     // Perform operations on btn...
                            // }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}