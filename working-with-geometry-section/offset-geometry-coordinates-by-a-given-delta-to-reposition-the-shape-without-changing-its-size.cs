using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: inputPath, outputPath, deltaX, deltaY
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisio> <outputVisio> <deltaX> <deltaY>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard for input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse delta values
        if (!double.TryParse(args[2], out double deltaX))
        {
            Console.Error.WriteLine($"Invalid deltaX value: {args[2]}");
            return;
        }

        if (!double.TryParse(args[3], out double deltaY))
        {
            Console.Error.WriteLine($"Invalid deltaY value: {args[3]}");
            return;
        }

        try
        {
            // Load diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Offset shape position using PinX and PinY (preserves size)
                        shape.XForm.PinX.Value += deltaX; // changed: use XForm for position
                        shape.XForm.PinY.Value += deltaY; // changed: use XForm for position

                        // Refresh shape data to apply changes
                        shape.RefreshData();
                    }
                }

                // Save the modified diagram to the output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Geometry offset applied successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
