using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to track unique EventComment values (using EventDrop as proxy)
            var seenComments = new HashSet<string>(StringComparer.Ordinal);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an Event section
                    if (shape.Event == null || shape.Event.EventDrop == null)
                        continue;

                    // Retrieve the EventComment-like value from EventDrop.Ufe.F
                    string commentFormula = shape.Event.EventDrop.Ufe.F ?? string.Empty;

                    // Skip empty formulas
                    if (string.IsNullOrWhiteSpace(commentFormula))
                        continue;

                    // If this comment has already been encountered, clear the duplicate
                    if (seenComments.Contains(commentFormula))
                    {
                        // Clear the duplicate comment by setting an empty formula
                        shape.Event.EventDrop.Ufe.F = string.Empty;
                    }
                    else
                    {
                        // Record the first occurrence of this comment
                        seenComments.Add(commentFormula);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}