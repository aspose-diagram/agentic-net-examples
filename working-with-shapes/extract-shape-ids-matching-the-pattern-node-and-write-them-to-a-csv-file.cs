using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        // Output CSV file path
        string outputPath = "shape_ids.csv";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Prepare a list to hold matching shape IDs
                var matchingIds = new System.Collections.Generic.List<long>();

                // Regular expression for the pattern "Node_*"
                var regex = new Regex(@"^Node_.*$", RegexOptions.Compiled);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a NameU before matching
                        if (!string.IsNullOrEmpty(shape.NameU) && regex.IsMatch(shape.NameU))
                        {
                            matchingIds.Add(shape.ID);
                        }
                    }
                }

                // Write the IDs to a CSV file
                using (var writer = new StreamWriter(outputPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID");
                    // Write each ID on a new line
                    foreach (long id in matchingIds)
                    {
                        writer.WriteLine(id);
                    }
                }

                Console.WriteLine($"Extracted {matchingIds.Count} shape IDs to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}