using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
        string outputPath = "output_with_outline.vsdx";

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];

                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the plain text of the shape
                    string shapeText = shape.Text.Value.ToString();

                    // Continue only if the shape contains text
                    if (string.IsNullOrWhiteSpace(shapeText))
                        continue;

                    // Clear existing text runs and add the original text back as a single run
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(shapeText));

                    // Clear any existing character formatting
                    shape.Chars.Clear();

                    // Create a new character formatting entry for the text
                    Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
                    shape.Chars.Add(charFormat);
                    charFormat.IX = 0; // Apply to the first character run

                    // Set the inner text color to white
                    charFormat.Color.Value = "#FFFFFF";

                    // Apply a black background to simulate an outline effect
                    if (shape.TextBlock != null && shape.TextBlock.TextBkgnd != null && shape.TextBlock.TextBkgnd.Ufe != null)
                    {
                        // Set background color to black
                        shape.TextBlock.TextBkgnd.Ufe.F = "#000000";

                        // Make the background fully opaque (0% transparency)
                        shape.TextBlock.TextBkgndTrans.Value = 0.0;
                    }
                }
            }

            // Save the modified diagram in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}