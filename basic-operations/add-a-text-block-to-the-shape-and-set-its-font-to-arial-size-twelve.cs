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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
            // Load the existing Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Get the first page (or create one if the diagram has no pages).
            Page page;
            if (diagram.Pages.Count > 0)
            {
                page = diagram.Pages[0];
            }
            else
            {
                // Create a new blank page and add it to the diagram.
                Page newPage = new Page();
                diagram.Pages.Add(newPage); // PageCollection.Add requires a Page instance.
                page = newPage;
            }

            // Add a rectangle shape to the page using DrawRectangle.
            // Parameters: pinX, pinY, width, height (all in inches).
            long shapeId = page.DrawRectangle(2.0, 2.0, 2.0, 1.0);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text and add new text to the shape.
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Sample Text"));

            // Create a character formatting object and set font to Arial, size 12 pt.
            // Font size is expressed in inches (points / 72).
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            ch.IX = 0; // Index of the character run.
            ch.FontName.Value = "Arial";
            ch.Size.Value = 12.0 / 72.0; // Convert 12 points to inches.
            shape.Chars.Add(ch);

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
