using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the source Visio diagram inside a using block.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page.
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    // Collect shapes that contain OLE (foreign) data.
                    var oleShapes = new System.Collections.Generic.List<Aspose.Diagram.Shape>();
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Identify foreign (OLE) shapes.
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null && shape.ForeignData.Value.Length > 0)
                        {
                            oleShapes.Add(shape);
                        }
                    }

                    // Process each OLE shape.
                    foreach (Aspose.Diagram.Shape oleShape in oleShapes)
                    {
                        // Generate a unique image file name for the exported OLE object.
                        string imageFileName = $"OleShape_{oleShape.ID}_{Guid.NewGuid():N}.png";
                        string imagePath = Path.Combine(outputDir, imageFileName);

                        // Export the OLE shape to an image file (PNG format).
                        oleShape.ToImage(imagePath, new ImageSaveOptions(SaveFileFormat.Png));

                        // OPTIONAL: Remove the original OLE shape from the page.
                        // Commented out because adding a replacement image shape requires additional API calls not covered here.
                        // page.Shapes.Remove(oleShape);
                    }
                }

                // Save the modified diagram as a copy.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram copied and OLE objects exported as images to: {outputDir}");
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}