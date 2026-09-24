using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for Aspose.Diagram operations

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output CSV file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: ShapeInheritanceReport <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        // Guard: verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        // Guard: ensure output directory exists (create if missing)
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output directory: {ex.Message}");
                return;
            }
        }

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Open a StreamWriter for the CSV output (overwrite if exists)
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Write CSV header
                writer.WriteLine("PageIndex,ShapeID,ShapeName,MasterName,IsDeleted");

                // Iterate through each page in the diagram
                for (int pageIdx = 0; pageIdx < diagram.Pages.Count; pageIdx++)
                {
                    Page page = diagram.Pages[pageIdx];

                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape ID (as long) and convert to string
                        string shapeId = shape.ID.ToString();

                        // Retrieve the universal name of the shape (may be empty)
                        string shapeName = shape.NameU ?? string.Empty;

                        // Retrieve master name if the shape has a master; otherwise empty
                        string masterName = shape.Master != null ? shape.Master.Name ?? string.Empty : string.Empty;

                        // Determine deletion status using BOOL enum (TRUE = deleted)
                        bool isDeleted = shape.Del == BOOL.True;

                        // Compose CSV line with proper escaping for commas
                        string csvLine = $"{pageIdx},{shapeId},\"{shapeName}\",\"{masterName}\",{isDeleted}";
                        writer.WriteLine(csvLine);
                    }
                }
            }

            Console.WriteLine($"CSV report generated successfully at: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}