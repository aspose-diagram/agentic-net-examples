using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, new PNG image path, output diagram path
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagram.vsdx> <newImage.png> <outputDiagram.vsdx>");
            return;
        }

        string diagramPath = args[0];
        string pngPath = args[1];
        string outputPath = args[2];

        // Guard: verify input diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Guard: verify PNG image file exists
        if (!File.Exists(pngPath))
        {
            Console.Error.WriteLine($"Error: PNG image file not found at '{pngPath}'.");
            return;
        }

        // Load PNG image bytes into memory
        byte[] pngBytes;
        try
        {
            pngBytes = File.ReadAllBytes(pngPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading PNG file: {ex.Message}");
            return;
        }

        // Load the Visio diagram inside a using block
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                bool placeholderFound = false;

                // Iterate through all pages and shapes to locate the first foreign (image) shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image placeholder shapes by Type == Foreign
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Replace the embedded image data with the new PNG bytes
                            shape.ForeignData.Value = pngBytes;
                            placeholderFound = true;
                            // Exit loops after first replacement
                            break;
                        }
                    }

                    if (placeholderFound)
                        break;
                }

                if (!placeholderFound)
                {
                    Console.Error.WriteLine("Warning: No foreign (image) shape found in the diagram to replace.");
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose.Diagram operation failed: {ex.Message}");
            return;
        }

        Console.WriteLine("Diagram image replacement completed successfully.");
    }
}