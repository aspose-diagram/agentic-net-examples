using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output Visio file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Ensure there are at least five pages; add blank pages if needed
            while (diagram.Pages.Count < 5)
            {
                // Create a new blank page and add it to the collection
                diagram.Pages.Add(new Page());
            }

            // Retrieve the fifth page (zero‑based index 4)
            Page pageFive = diagram.Pages[4];

            // Create a new stylesheet for gradient fill
            StyleSheet gradientStyle = new StyleSheet
            {
                // Assign a unique ID based on current count
                ID = diagram.StyleSheets.Count + 1,
                // Optional: give the style a readable name
                Name = "GradientStyle"
            };

            // Set fill pattern to gradient (value 25 per API)
            gradientStyle.Fill.FillPattern.Value = 25;
            // Enable gradient fill
            gradientStyle.Fill.GradientFill.GradientEnabled.Value = BOOL.True;
            // Set gradient direction (0 = horizontal, adjust as needed)
            gradientStyle.Fill.GradientFill.GradientDir.Value = 0;
            // Clear any existing gradient stops (should be empty for new style)
            gradientStyle.Fill.GradientFill.GradientStops.Clear();
            // Add a blue start stop at position 0
            gradientStyle.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(0, MeasureConst.NUM),
                new ColorValue("#0000FF", MeasureConst.Undefined));
            // Add a green end stop at position 1
            gradientStyle.Fill.GradientFill.GradientStops.Add(
                new DoubleValue(1, MeasureConst.NUM),
                new ColorValue("#00FF00", MeasureConst.Undefined));

            // Add the new stylesheet to the diagram's collection
            diagram.StyleSheets.Add(gradientStyle);

            // Apply the stylesheet to page five (text, line, and fill styles use the same ID)
            pageFive.ApplyStyle(gradientStyle.ID, gradientStyle.ID, gradientStyle.ID);

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}