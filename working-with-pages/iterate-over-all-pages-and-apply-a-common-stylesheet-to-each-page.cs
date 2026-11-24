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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a common stylesheet that will be applied to every page.
                // ------------------------------------------------------------
                StyleSheet commonStyle = new StyleSheet();

                // Assign a unique ID based on the current count.
                commonStyle.ID = diagram.StyleSheets.Count + 1;

                // Example style settings – adjust as needed.
                // Set fill foreground color.
                commonStyle.Fill.FillForegnd.Value = "#D3E4CD"; // light greenish

                // Set line color and weight.
                commonStyle.Line.LineColor.Value = "#5B9BD5"; // blue
                commonStyle.Line.LineWeight.Value = 0.5;      // points (in inches)

                // Set character (text) color.
                Aspose.Diagram.Char charStyle = new Aspose.Diagram.Char();
                charStyle.IX = 0;
                charStyle.Color.Value = "#000000"; // black
                commonStyle.Chars.Add(charStyle);

                // Add the stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(commonStyle);

                // ------------------------------------------------------------
                // Iterate over all pages and apply the common stylesheet.
                // ------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    // Apply the stylesheet to characters, paragraphs, and lines.
                    page.ApplyStyle(commonStyle.ID, commonStyle.ID, commonStyle.ID);
                }

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}