using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Define the output file path
        string outputPath = "output.vsdx";

        // Guard: ensure the output directory exists
        string outputDir = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(outputDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outputDir}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // Create a new empty diagram
            // -----------------------------------------------------------------
            Diagram diagram = new Diagram();

            // Use the first (default) page
            Page page = diagram.Pages[0];

            // -----------------------------------------------------------------
            // Add a multi‑line text shape
            // -----------------------------------------------------------------
            double pinX = 2.0;
            double pinY = 5.0;
            double width = 4.0;
            double height = 2.0;
            string multiLineText = "First line\nSecond line\nThird line";

            // AddText returns a Shape instance containing the text
            Shape textShape = page.AddText(pinX, pinY, width, height, multiLineText);

            // -----------------------------------------------------------------
            // Create a custom stylesheet (will be used for the shape)
            // -----------------------------------------------------------------
            StyleSheet customStyle = new StyleSheet();
            // Assign a unique ID (Visio expects IDs to start at 1)
            customStyle.ID = diagram.StyleSheets.Count + 1;

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(customStyle);

            // Apply the stylesheet to the text shape
            textShape.TextStyle = customStyle;

            // -----------------------------------------------------------------
            // Set line height (paragraph spacing) on the shape's paragraphs
            // -----------------------------------------------------------------
            double desiredLineSpacing = 1.5; // 150% line height

            // Apply the line spacing to all existing paragraphs in the shape
            for (int i = 0; i < textShape.Paras.Count; i++)
            {
                // SpLine.Value sets the line spacing (in lines)
                textShape.Paras[i].SpLine.Value = desiredLineSpacing;
            }

            // -----------------------------------------------------------------
            // Verify that the line spacing was applied correctly
            // -----------------------------------------------------------------
            bool allMatch = true;
            for (int i = 0; i < textShape.Paras.Count; i++)
            {
                double actual = textShape.Paras[i].SpLine.Value;
                if (Math.Abs(actual - desiredLineSpacing) > 0.0001)
                {
                    allMatch = false;
                    Console.Error.WriteLine($"Paragraph {i} line spacing mismatch. Expected: {desiredLineSpacing}, Actual: {actual}");
                }
            }

            if (allMatch)
            {
                Console.WriteLine($"Line spacing verification succeeded. All paragraphs have SpLine = {desiredLineSpacing}");
            }
            else
            {
                throw new Exception("Line spacing verification failed.");
            }

            // -----------------------------------------------------------------
            // Save the diagram to a VSDX file
            // -----------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
