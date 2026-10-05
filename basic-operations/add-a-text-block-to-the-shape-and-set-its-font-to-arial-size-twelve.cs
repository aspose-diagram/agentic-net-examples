using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Get the first page
            Page page = diagram.Pages[0];

            // Ensure the page has at least one shape
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes.");
                return;
            }

            // Retrieve the first shape on the page
            Shape shape = null;
            foreach (Shape s in page.Shapes)
            {
                shape = s;
                break;
            }

            if (shape == null)
            {
                Console.Error.WriteLine("Failed to retrieve a shape from the page.");
                return;
            }

            // Clear any existing text in the shape
            shape.Text.Value.Clear();

            // Add new text to the shape
            shape.Text.Value.Add(new Txt("Sample Text"));

            // Create a character formatting object for the added text
            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
            // Add the character formatting to the shape's Char collection
            shape.Chars.Add(ch);
            // Set the character index (0 for the first character run)
            ch.IX = 0;
            // Set the font name to Arial
            ch.FontName.Value = "Arial";
            // Set the font size to 12 points (converted to inches)
            ch.Size.Value = 12.0 / 72.0;
            // Optionally, set the text color to black
            ch.Color.Value = "#000000";

            // Save the modified diagram to a new file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}