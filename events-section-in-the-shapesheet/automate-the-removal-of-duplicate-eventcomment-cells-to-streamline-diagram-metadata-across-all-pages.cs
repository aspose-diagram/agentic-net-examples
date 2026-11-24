using System;
using System.IO;
using System.Collections.Generic;
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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // HashSet to keep track of unique EventComment values.
                HashSet<string> seenComments = new HashSet<string>(StringComparer.Ordinal);

                // Iterate over all pages.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Access the closest substitute for the EventComment cell.
                        // According to the API guidance, use EventDrop.Ufe.F as the nearest equivalent.
                        string comment = shape.Event?.EventDrop?.Ufe?.F;

                        // If there is no comment, continue to next shape.
                        if (string.IsNullOrEmpty(comment))
                            continue;

                        // If this comment has already been encountered, clear it to remove the duplicate.
                        if (seenComments.Contains(comment))
                        {
                            // Clear the duplicate comment.
                            shape.Event.EventDrop.Ufe.F = string.Empty;
                        }
                        else
                        {
                            // Record the first occurrence of this comment.
                            seenComments.Add(comment);
                        }
                    }
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