using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath> [pageIndex]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int pageIndex = 0;

        // Optional page index argument
        if (args.Length >= 3 && !int.TryParse(args[2], out pageIndex))
        {
            Console.Error.WriteLine("Invalid page index argument.");
            return;
        }

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a simple stylesheet
            StyleSheet style = new StyleSheet();
            style.ID = diagram.StyleSheets.Count + 1;

            // Example: set character color to green
            Aspose.Diagram.Char charProp = new Aspose.Diagram.Char();
            charProp.Color.Value = "#00FF00";
            style.Chars.Add(charProp);

            // Example: set line color to red
            style.Line.LineColor.Value = "#FF0000";

            // Example: set fill foreground color to blue
            style.Fill.FillForegnd.Value = "#0000FF";

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(style);

            // Attempt to apply the stylesheet to the specified page index
            // This may throw if the page index is out of range
            Page targetPage = diagram.Pages[pageIndex];
            targetPage.ApplyStyle(style.ID, style.ID, style.ID);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (ArgumentOutOfRangeException ex)
        {
            // Specific handling for non‑existent page index
            Console.Error.WriteLine($"Error: The specified page index {pageIndex} does not exist. {ex.Message}");
        }
        catch (Exception ex)
        {
            // General error handling for any other Aspose or I/O issues
            Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}