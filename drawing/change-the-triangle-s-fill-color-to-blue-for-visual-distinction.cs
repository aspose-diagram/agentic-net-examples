using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [outputVisioFile]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional argument or default)
        string outputPath = args.Length >= 2 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "output.vsdx");

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (or you can select by name if needed)
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // Find the first shape whose master name is "Triangle"
                Shape triangleShape = null;
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shp = page.Shapes[i];
                    if (shp.Master != null && string.Equals(shp.Master.Name, "Triangle", StringComparison.OrdinalIgnoreCase))
                    {
                        triangleShape = shp;
                        break;
                    }
                }

                if (triangleShape == null)
                {
                    Console.Error.WriteLine("No triangle shape found in the diagram.");
                    return;
                }

                // Set solid fill pattern
                triangleShape.Fill.FillPattern.Value = 1; // 1 = solid

                // Set the foreground fill color to blue (hex code)
                triangleShape.Fill.FillForegnd.Value = "#0000FF";

                // Save the modified diagram to the output file in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}