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

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through pages and shapes to find a shape that contains an ActiveX control
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape has an ActiveX control attached
                    if (shape.ActiveXControl != null)
                    {
                        // Retrieve the ActiveX control object
                        ActiveXControl activeX = shape.ActiveXControl;

                        // Determine the concrete control type via the Type property
                        ControlType controlType = activeX.Type;

                        // Output the results
                        Console.WriteLine($"Shape ID: {shape.ID}");
                        Console.WriteLine($"ActiveX Control Type: {controlType}");

                        // Since the requirement is to determine the concrete type, stop after the first found control
                        return;
                    }
                }
            }

            // If no ActiveX control was found in the diagram
            Console.WriteLine("No ActiveX control found in the provided diagram.");
        }
        catch (Exception ex)
        {
            // Write any Aspose.Diagram related errors to the error stream
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}