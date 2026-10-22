using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-visio-file>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify OLE objects via the presence of ForeignData.
                    if (shape.ForeignData != null)
                    {
                        // Retrieve bounding box values.
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;
                        double locPinX = shape.XForm.LocPinX.Value;
                        double locPinY = shape.XForm.LocPinY.Value;
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // Calculate absolute coordinates.
                        double left = pinX - locPinX;
                        double top = pinY - locPinY;
                        double right = left + width;
                        double bottom = top + height;

                        // Log the bounding box information.
                        Console.WriteLine($"Page ID {page.ID}, Shape ID {shape.ID}:");
                        Console.WriteLine($"  Bounding Box -> Left: {left}, Top: {top}, Right: {right}, Bottom: {bottom}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}