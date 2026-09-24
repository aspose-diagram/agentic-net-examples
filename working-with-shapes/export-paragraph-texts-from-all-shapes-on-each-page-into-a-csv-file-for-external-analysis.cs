using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string visioPath = "input.vsdx";

                // Path to the CSV file that will contain the exported texts
                string csvPath = "output.csv";

                // Load the diagram
                Diagram diagram = new Diagram(visioPath);

                // Prepare the CSV file for writing
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Write CSV header
                    writer.WriteLine("PageName,ShapeID,ShapeName,ParagraphText");

                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        string pageName = page.Name ?? string.Empty;

                        // Iterate through each shape on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            long shapeId = shape.ID;
                            string shapeName = shape.Name ?? string.Empty;

                            // Retrieve the plain text of the shape
                            string rawText = shape.Text.Value.Text ?? string.Empty;

                            // Escape CSV special characters
                            string escapedText = rawText.Replace("\"", "\"\"");
                            if (escapedText.Contains(",") || escapedText.Contains("\"") ||
                                escapedText.Contains("\n") || escapedText.Contains("\r"))
                            {
                                escapedText = $"\"{escapedText}\"";
                            }

                            // Write a CSV line for the current shape
                            writer.WriteLine($"{pageName},{shapeId},{shapeName},{escapedText}");
                        }
                    }
                }

                // Clean up
                diagram.Dispose();

                Console.WriteLine($"Export completed. CSV saved to: {csvPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }