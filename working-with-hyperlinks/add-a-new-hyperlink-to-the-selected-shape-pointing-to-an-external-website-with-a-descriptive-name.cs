using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: inputPath, shapeNameU, hyperlinkUrl, hyperlinkName, outputPath
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <shapeNameU> <hyperlinkUrl> <hyperlinkName> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string shapeNameU = args[1];
        string hyperlinkUrl = args[2];
        string hyperlinkName = args[3];
        string outputPath = args[4];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the shape with the specified NameU across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape != null && shape.NameU == shapeNameU)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null) break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Shape with NameU '{shapeNameU}' not found in the diagram.");
                    return;
                }

                // Create a new hyperlink instance
                Hyperlink link = new Hyperlink();
                // Set the descriptive name for the hyperlink
                link.Name = hyperlinkName;
                // Set the external URL address
                link.Address.Value = hyperlinkUrl;

                // Add the hyperlink to the shape's Hyperlinks collection
                targetShape.Hyperlinks.Add(link);

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Hyperlink added successfully. Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}