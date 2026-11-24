using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <OutputFolder>");
            return;
        }

        string visioPath = args[0];
        string outputFolder = args[1];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{visioPath}'.");
            return;
        }

        // Guard: ensure the output folder exists or create it.
        try
        {
            Directory.CreateDirectory(outputFolder);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating output folder '{outputFolder}': {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath);

            // Iterate over all masters (stencil items) in the diagram.
            for (int i = 0; i < diagram.Masters.Count; i++)
            {
                Master master = diagram.Masters[i];
                string masterName = master.Name ?? $"Master_{i}";
                string safeFileName = GetSafeFileName(masterName) + ".png";
                string outputPath = Path.Combine(outputFolder, safeFileName);

                // Attempt to use the built‑in thumbnail (Icon) if available.
                if (master.Icon != null && master.Icon.Length > 0)
                {
                    try
                    {
                        File.WriteAllBytes(outputPath, master.Icon);
                        Console.WriteLine($"Thumbnail saved for master '{masterName}' to '{outputPath}'.");
                        continue; // Thumbnail written, move to next master.
                    }
                    catch (Exception ex)
                    {
                        Console.Error.WriteLine($"Failed to write Icon for master '{masterName}': {ex.Message}");
                        // Fall back to rendering the master shape.
                    }
                }

                // Fallback: render the master onto a temporary page and export as PNG.
                try
                {
                    // Add a temporary page to host the shape.
                    Page tempPage = new Page();
                    diagram.Pages.Add(tempPage);

                    // Place a shape based on the master at (0,0).
                    long shapeId = tempPage.AddShape(0, 0, master.Name);
                    Shape shape = tempPage.Shapes.GetShape(shapeId);

                    // Export the shape to PNG using ImageSaveOptions.
                    ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                    shape.ToImage(outputPath, imgOptions);
                    Console.WriteLine($"Rendered thumbnail saved for master '{masterName}' to '{outputPath}'.");

                    // Remove the temporary page to keep the diagram clean.
                    diagram.Pages.RemoveAt(diagram.Pages.Count - 1);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Error rendering thumbnail for master '{masterName}': {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any Aspose.Diagram related exceptions.
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }

    // Helper method to sanitize file names.
    private static string GetSafeFileName(string name)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
        {
            name = name.Replace(c, '_');
        }
        return name;
    }
}