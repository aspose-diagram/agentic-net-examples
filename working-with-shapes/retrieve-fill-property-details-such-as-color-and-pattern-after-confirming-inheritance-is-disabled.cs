using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
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

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                Console.Error.WriteLine("Unable to retrieve a shape from the page.");
                return;
            }

            // ------------------------------------------------------------
            // Disable inheritance for fill properties by explicitly setting
            // the current values back to the cells.
            // ------------------------------------------------------------
            shape.Fill.FillForegnd.Value = shape.Fill.FillForegnd.Value;
            shape.Fill.FillBkgnd.Value = shape.Fill.FillBkgnd.Value;
            shape.Fill.FillPattern.Value = shape.Fill.FillPattern.Value;

            // ------------------------------------------------------------
            // Retrieve fill details after inheritance is disabled
            // ------------------------------------------------------------
            string foreColor = shape.Fill.FillForegnd.Value;   // e.g., "#FF0000"
            string backColor = shape.Fill.FillBkgnd.Value;    // e.g., "#00FF00"
            int pattern = shape.Fill.FillPattern.Value;       // pattern index

            // Output the retrieved fill information
            Console.WriteLine("Fill Details for Shape ID {0} (Name: {1}):", shape.ID, shape.Name);
            Console.WriteLine($"  Foreground Color (FillForegnd): {foreColor}");
            Console.WriteLine($"  Background Color (FillBkgnd): {backColor}");
            Console.WriteLine($"  Fill Pattern (FillPattern): {pattern}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}