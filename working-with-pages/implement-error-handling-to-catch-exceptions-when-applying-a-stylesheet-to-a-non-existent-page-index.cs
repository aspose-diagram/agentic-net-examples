using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <outputVisioPath> <pageIndex>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string pageIndexArg = args[2];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse page index
        if (!int.TryParse(pageIndexArg, out int pageIndex) || pageIndex < 0)
        {
            Console.Error.WriteLine($"Invalid page index: {pageIndexArg}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Create a simple stylesheet
            StyleSheet style = new StyleSheet();
            // Assign a unique ID (Visio IDs start at 1)
            style.ID = diagram.StyleSheets.Count + 1;

            // Example: set line color and fill foreground
            style.Line.LineColor.Value = "#FF0000"; // Red line
            style.Fill.FillForegnd.Value = "#00FF00"; // Green fill

            // Add the stylesheet to the diagram
            diagram.StyleSheets.Add(style);

            // Attempt to retrieve the target page
            Page targetPage = diagram.Pages[pageIndex]; // May throw if index is out of range

            // Apply the stylesheet to the page
            // ApplyStyle takes (StyleID, CharStyleID, ParagraphStyleID)
            targetPage.ApplyStyle(style.ID, style.ID, style.ID);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
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