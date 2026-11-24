using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the input file into a memory stream.
            MemoryStream inputStream = new MemoryStream();
            using (FileStream fileStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read))
            {
                fileStream.CopyTo(inputStream);
            }
            inputStream.Position = 0; // Reset for reading.

            // Load the diagram from the memory stream inside a using block.
            using (Diagram diagram = new Diagram(inputStream))
            {
                // Iterate over all pages in the diagram.
                for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                {
                    Page page = diagram.Pages[pageIndex];

                    // Iterate over all shapes on the current page.
                    for (int shapeIndex = 0; shapeIndex < page.Shapes.Count; shapeIndex++)
                    {
                        Shape shape = page.Shapes[shapeIndex];

                        // Ensure the shape has a Hyperlinks collection before iterating.
                        if (shape.Hyperlinks != null)
                        {
                            // Iterate over each hyperlink attached to the shape.
                            for (int linkIndex = 0; linkIndex < shape.Hyperlinks.Count; linkIndex++)
                            {
                                Hyperlink link = shape.Hyperlinks[linkIndex];

                                // Modify the hyperlink address if it is not null or empty.
                                if (link.Address != null && !string.IsNullOrEmpty(link.Address.Value))
                                {
                                    // Example modification: append a query string to the existing address.
                                    link.Address.Value = link.Address.Value + "?updated=1";
                                }

                                // Optionally, modify the description as well (example).
                                if (link.Description != null && !string.IsNullOrEmpty(link.Description.Value))
                                {
                                    link.Description.Value = "Updated: " + link.Description.Value;
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram back to a new memory stream.
                MemoryStream outputStream = new MemoryStream();
                diagram.Save(outputStream, SaveFileFormat.Vsdx);
                outputStream.Position = 0; // Reset before writing to file.

                // Write the updated diagram bytes to the output file.
                File.WriteAllBytes(outputPath, outputStream.ToArray());
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
            return;
        }

        Console.WriteLine("Diagram processing completed successfully.");
    }
}