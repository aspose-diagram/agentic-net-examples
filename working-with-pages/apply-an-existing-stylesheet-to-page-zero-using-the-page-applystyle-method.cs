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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Load the Visio diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a new StyleSheet that will be applied to the page.
                // ------------------------------------------------------------
                StyleSheet style = new StyleSheet();

                // Assign a unique ID (ID must be > 0).
                style.ID = diagram.StyleSheets.Count + 1;

                // Optionally give the style a name for readability.
                style.Name = "MyCustomStyle";

                // ----- Text style (Char) -----
                // Set text color to dark green.
                Aspose.Diagram.Char textChar = new Aspose.Diagram.Char();
                textChar.Color.Value = "#006400"; // dark green
                textChar.IX = 0; // first character style slot
                style.Chars.Add(textChar);

                // ----- Line style -----
                // Set line color to red and weight to 2 pt.
                style.Line.LineColor.Value = "#FF0000"; // red
                style.Line.LineWeight.Value = 2.0; // points

                // ----- Fill style -----
                // Set fill foreground color to light blue and solid fill pattern.
                style.Fill.FillForegnd.Value = "#ADD8E6"; // light blue
                style.Fill.FillPattern.Value = 1; // solid fill

                // Add the stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(style);

                // ------------------------------------------------------------
                // Apply the newly created stylesheet to the first page (index 0).
                // ------------------------------------------------------------
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                Page page = diagram.Pages[0];

                // ApplyStyle expects the IDs for text, line, and fill styles.
                // Using the same stylesheet ID for all three applies the same style.
                page.ApplyStyle(style.ID, style.ID, style.ID);

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related exceptions.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}