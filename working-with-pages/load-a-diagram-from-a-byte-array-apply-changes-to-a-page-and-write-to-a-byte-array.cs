using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input file path (first argument) or default.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Read the diagram file into a byte array.
        byte[] inputBytes = File.ReadAllBytes(inputPath);

        try
        {
            // Load the diagram from the byte array using a MemoryStream.
            using (MemoryStream inputStream = new MemoryStream(inputBytes))
            using (Diagram diagram = new Diagram(inputStream))
            {
                // ----- Modify the first page -----
                // Retrieve the first page (index 0).
                Page page = diagram.Pages[0];

                // Add a rectangle shape to the page.
                // Parameters: pinX, pinY, width, height, master name, page index.
                long shapeId = diagram.AddShape(2.0, 2.0, 1.0, 0.5, "Rectangle", 0);

                // Retrieve the newly added shape to set its properties.
                Shape shape = page.Shapes.GetShape(shapeId);

                // Clear any existing text and add new text.
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Added via code"));

                // ----- Save the modified diagram to a byte array -----
                using (MemoryStream outputStream = new MemoryStream())
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);
                    byte[] outputBytes = outputStream.ToArray();

                    // If an output path is provided, write the byte array to a file.
                    if (args.Length > 1)
                    {
                        string outputPath = args[1];
                        File.WriteAllBytes(outputPath, outputBytes);
                        Console.WriteLine($"Diagram saved to {outputPath}, size {outputBytes.Length} bytes.");
                    }
                    else
                    {
                        // Otherwise, just report the size of the resulting byte array.
                        Console.WriteLine($"Output byte array size: {outputBytes.Length} bytes.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}