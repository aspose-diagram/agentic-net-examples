using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input file path and regex pattern.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <regexPattern>");
            return;
        }

        string inputPath = args[0];
        string pattern = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Compile the regular expression once for efficiency.
            Regex regex = new Regex(pattern, RegexOptions.Compiled);

            Console.WriteLine($"Shapes with EventComment matching pattern \"{pattern}\":");

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Aspose.Diagram does not expose an EventComment cell directly.
                    // Use the closest available event cell (EventDrop) as a substitute.
                    // This retrieves the formula stored in the EventDrop cell.
                    string eventCommentFormula = shape.Event.EventDrop.Ufe.F ?? string.Empty;

                    // Apply the regular expression to the formula text.
                    if (regex.IsMatch(eventCommentFormula))
                    {
                        // Output shape identification details.
                        Console.WriteLine($"Page {page.ID}, Shape ID {shape.ID}, Name \"{shape.NameU}\"");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}