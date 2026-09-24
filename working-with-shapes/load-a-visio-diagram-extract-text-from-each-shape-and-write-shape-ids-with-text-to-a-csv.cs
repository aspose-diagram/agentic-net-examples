using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioTextExtractor <inputVisioPath> <outputCsvPath>");
                return;
            }

            string inputPath = args[0];
            string outputCsvPath = args[1];

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Open CSV writer
            try
            {
                using (var writer = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeId,Text");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Retrieve shape ID
                            long shapeId = shape.ID;

                            // Retrieve plain text of the shape
                            string text = shape.Text.Value.Text ?? string.Empty;

                            // Clean text for CSV (replace commas and newlines)
                            text = text.Replace(",", " ").Replace("\r", " ").Replace("\n", " ").Trim();

                            // Write CSV line
                            writer.WriteLine($"{shapeId},\"{text}\"");
                        }
                    }
                }

                Console.WriteLine($"Extraction completed. CSV saved to: {outputCsvPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write CSV: {ex.Message}");
            }
        }
    }