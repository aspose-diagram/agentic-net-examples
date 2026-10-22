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

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Create a new stylesheet that will be applied to every page.
                StyleSheet commonStyle = new StyleSheet();

                // Assign a unique ID based on the current count of stylesheets.
                commonStyle.ID = diagram.StyleSheets.Count + 1;

                // Configure fill style (background color) for the stylesheet.
                commonStyle.Fill.FillForegnd.Value = "#FFCC00"; // Example fill color.
                commonStyle.Fill.FillPattern.Value = 1;        // Solid fill pattern.

                // Configure line style for the stylesheet.
                commonStyle.Line.LineColor.Value = "#000000"; // Example line color.
                commonStyle.Line.LineWeight.Value = 0.5;      // Example line weight.

                // Add the stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(commonStyle);

                // Iterate over all pages and apply the common stylesheet.
                foreach (Page page in diagram.Pages)
                {
                    // Apply the same style to text, line, and fill of the page.
                    page.ApplyStyle(commonStyle.ID, commonStyle.ID, commonStyle.ID);
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}