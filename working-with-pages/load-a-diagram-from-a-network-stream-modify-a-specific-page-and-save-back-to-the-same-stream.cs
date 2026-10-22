using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: first argument is the path to the Visio file,
        // second optional argument is the zero‑based page index to modify.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> [PageIndex]");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        // Parse page index; default to the first page (index 0).
        int pageIndex = 0;
        if (args.Length >= 2 && !int.TryParse(args[1], out pageIndex))
        {
            Console.Error.WriteLine("Error: Invalid page index argument.");
            return;
        }

        try
        {
            // Open the file as a read/write stream.
            using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                // Load the diagram from the stream.
                using (Diagram diagram = new Diagram(stream))
                {
                    // Validate requested page index.
                    if (pageIndex < 0 || pageIndex >= diagram.Pages.Count)
                    {
                        Console.Error.WriteLine($"Error: Page index {pageIndex} is out of range. Diagram has {diagram.Pages.Count} pages.");
                        return;
                    }

                    // Retrieve the target page.
                    Page page = diagram.Pages[pageIndex];

                    // Add a rectangle shape to the page.
                    // Parameters: pinX, pinY, width, height, master name, isCalculate flag.
                    long shapeId = page.AddShape(1.0, 1.0, 2.0, 1.0, "Rectangle", false); // BOOL.False replaced with false

                    // Retrieve the newly added shape.
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Update the shape's text.
                    // Clear existing text runs and add a new one.
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Modified by program"));

                    // Reset stream position and truncate before saving.
                    stream.Position = 0;
                    stream.SetLength(0);

                    // Save the modified diagram back to the same stream in VSDX format.
                    diagram.Save(stream, SaveFileFormat.Vsdx);
                }
            }

            Console.WriteLine("Diagram loaded, modified, and saved successfully.");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
