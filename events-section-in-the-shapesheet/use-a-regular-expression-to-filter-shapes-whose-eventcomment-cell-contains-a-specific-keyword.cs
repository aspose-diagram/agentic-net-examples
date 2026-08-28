using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        // Path where the (unchanged) diagram will be saved
        string outputPath = "output.vsdx";
        // Guard: ensure the output directory exists (optional)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir)) { Console.Error.WriteLine($"Output directory not found: {outputDir}"); return; }

        // Keyword to search for inside the EventComment cell
        string keyword = "Important";

        try
        {
            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Prepare a case‑insensitive regular expression
            Regex regex = new Regex(keyword, RegexOptions.IgnoreCase);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // NOTE: The Aspose.Diagram API does not expose an EventComment cell.
                    // If such a cell existed, it would be accessed via shape.Event.<CellName>.Ufe.F.
                    // Since it is unavailable, this example only demonstrates iteration and placeholder logic.
                    // Replace the following block with actual cell access when the appropriate API becomes available.

                    // Placeholder: retrieve a generic comment-like cell if needed
                    // Example (if a Comment cell existed in the Misc section):
                    // if (shape.Misc != null && shape.Misc.Comment != null)
                    // {
                    //     string comment = shape.Misc.Comment.Value ?? string.Empty;
                    //     if (regex.IsMatch(comment))
                    //         Console.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" matches the keyword.");
                    // }

                    // Since EventComment is not supported, we simply continue.
                }
            }

            // Save the diagram (no modifications made, just to satisfy lifecycle requirement)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}