using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputFolder>");
            return;
        }

        string inputPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all masters (stencil items) in the diagram.
            for (int i = 0; i < diagram.Masters.Count; i++)
            {
                Master master = diagram.Masters[i];
                string masterName = master.Name ?? $"Master_{i}";
                // Create a safe file name for the PNG.
                string safeName = string.Join("_", masterName.Split(Path.GetInvalidFileNameChars()));
                string outputPath = Path.Combine(outputFolder, $"{safeName}.png");

                // Attempt to export the built‑in thumbnail (master.Icon) if available.
                if (master.Icon != null && master.Icon.Length > 0)
                {
                    File.WriteAllBytes(outputPath, master.Icon);
                }
                else
                {
                    // Fallback: create a temporary shape from the master and render it to PNG.
                    // Add a temporary page to host the shape.
                    Page tempPage = new Page();
                    diagram.Pages.Add(tempPage);

                    // Add the shape using the master name. The method returns the shape ID.
                    long shapeId = tempPage.AddShape(0, 0, master.Name);

                    // Retrieve the shape object.
                    Shape shape = tempPage.Shapes.GetShape(shapeId);

                    // Configure PNG export options.
                    ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);

                    // Export the shape to a PNG file.
                    shape.ToImage(outputPath, pngOptions);

                    // Remove the temporary page to keep the diagram clean.
                    diagram.Pages.RemoveAt(diagram.Pages.Count - 1);
                }

                Console.WriteLine($"Exported thumbnail for master '{masterName}' to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}