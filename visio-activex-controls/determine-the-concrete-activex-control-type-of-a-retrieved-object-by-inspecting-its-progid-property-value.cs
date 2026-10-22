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
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
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
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes to find ActiveX controls
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // TODO: Determine concrete ActiveX control type by inspecting the ProgID property.
                        // The Aspose.Diagram documentation advises against using ProgID for type identification.
                        // The recommended approach is to use shape.ActiveXControl.Type (ControlType enum).
                        // Implementing ProgID-based detection is not supported by the current API.
                        Console.WriteLine($"Shape ID {shape.ID} contains ActiveX control. Detected type via enum: {shape.ActiveXControl.Type}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}