using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath> [outputVisioFilePath]");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Determine output path (optional). If not provided, use a default name.
        string outputPath = args.Length > 1 ? args[1] : "updated_output.vsdx";

        try
        {
            // Load the diagram bytes into a memory stream.
            byte[] fileBytes = File.ReadAllBytes(inputPath);
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                // Load diagram from the memory stream.
                Diagram diagram = new Diagram(memoryStream);

                // Iterate through all pages.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate through all shapes on the page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Ensure the shape has a Hyperlinks collection.
                        if (shape.Hyperlinks != null)
                        {
                            // Iterate through each hyperlink and modify its target address.
                            for (int linkIndex = 0; linkIndex < shape.Hyperlinks.Count; linkIndex++)
                            {
                                Hyperlink link = shape.Hyperlinks[linkIndex];
                                // Set a new address for the hyperlink.
                                link.Address.Value = "https://example.com/modified";
                            }
                        }
                    }
                }

                // Reset the memory stream to overwrite with the updated diagram.
                memoryStream.Position = 0;
                memoryStream.SetLength(0);

                // Save the updated diagram back into the memory stream in VSDX format.
                diagram.Save(memoryStream, SaveFileFormat.Vsdx);

                // Write the updated diagram from the memory stream to the output file.
                File.WriteAllBytes(outputPath, memoryStream.ToArray());
            }

            Console.WriteLine($"Diagram updated and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}