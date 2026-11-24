using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expecting arguments: <inputVisioPath> <shapeNameU> <hyperlinkAddress> <tooltipText>
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <shapeNameU> <hyperlinkAddress> <tooltipText>");
            return;
        }

        string inputPath = args[0];
        string targetShapeNameU = args[1];
        string hyperlinkAddress = args[2];
        string tooltipText = args[3];

        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape by its universal name (NameU)
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (string.Equals(shape.NameU, targetShapeNameU, StringComparison.OrdinalIgnoreCase))
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null) break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with NameU '{targetShapeNameU}' not found.");
                return;
            }

            // Create a new hyperlink instance
            Hyperlink link = new Hyperlink();

            // Set the hyperlink address (e.g., URL or page reference)
            link.Address.Value = hyperlinkAddress;

            // Set the description property which appears as tooltip on mouse hover
            link.Description.Value = tooltipText;

            // Add the hyperlink to the shape's Hyperlinks collection
            targetShape.Hyperlinks.Add(link);

            // Prepare output path (append "_modified" before extension)
            string directory = Path.GetDirectoryName(inputPath) ?? "";
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_modified.vsdx");

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Hyperlink added with tooltip. Saved modified diagram to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}