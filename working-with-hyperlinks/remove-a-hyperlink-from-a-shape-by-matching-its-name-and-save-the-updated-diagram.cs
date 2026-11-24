using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect four arguments: input diagram path, shape name, hyperlink name, output diagram path
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputPath> <shapeName> <hyperlinkName> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string shapeName = args[1];
        string hyperlinkName = args[2];
        string outputPath = args[3];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the shape with the specified name (NameU)
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == shapeName)
                    {
                        targetShape = shape;
                        break;
                    }
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine($"Shape with name '{shapeName}' not found.");
                return;
            }

            // Ensure the shape has hyperlinks collection
            if (targetShape.Hyperlinks == null || targetShape.Hyperlinks.Count == 0)
            {
                Console.Error.WriteLine($"Shape '{shapeName}' does not contain any hyperlinks.");
                return;
            }

            // Find and remove the hyperlink matching the specified name
            bool removed = false;
            for (int i = 0; i < targetShape.Hyperlinks.Count; i++)
            {
                Hyperlink hl = targetShape.Hyperlinks[i];
                if (hl.Name == hyperlinkName)
                {
                    // Remove the hyperlink at the found index
                    targetShape.Hyperlinks.RemoveAt(i);
                    removed = true;
                    break;
                }
            }

            if (!removed)
            {
                Console.Error.WriteLine($"Hyperlink with name '{hyperlinkName}' not found on shape '{shapeName}'.");
                return;
            }

            // Save the updated diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Hyperlink removed and diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}