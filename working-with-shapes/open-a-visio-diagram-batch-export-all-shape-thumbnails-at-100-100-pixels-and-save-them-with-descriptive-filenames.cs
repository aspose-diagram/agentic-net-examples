using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file and output folder.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <outputFolder>");
            return;
        }

        // Assign input file path and guard its existence.
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign output folder path and ensure it exists.
        string outputFolder = args[1];
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram.
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Build a descriptive filename using the shape's universal name (if any) and its ID.
                    string baseName = string.IsNullOrWhiteSpace(shape.NameU) ? $"Shape_{shape.ID}" : shape.NameU;
                    // Replace any invalid filename characters with an underscore.
                    foreach (char c in Path.GetInvalidFileNameChars())
                        baseName = baseName.Replace(c, '_');
                    // Append the shape ID to guarantee uniqueness.
                    string fileName = $"{baseName}_{shape.ID}.png";
                    string outputPath = Path.Combine(outputFolder, fileName);

                    try
                    {
                        // Configure image export options: PNG format with a 100×100 pixel page size.
                        ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                        imgOptions.PageSize = new PageSize(100f, 100f); // Width, Height in pixels.

                        // Export the shape thumbnail to the output file.
                        shape.ToImage(outputPath, imgOptions);
                    }
                    catch (Exception ex)
                    {
                        // Log any errors that occur while exporting an individual shape.
                        Console.Error.WriteLine($"Error exporting shape ID {shape.ID}: {ex.Message}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during diagram loading or overall processing.
            Console.Error.WriteLine($"Processing failed: {ex.Message}");
        }
    }
}