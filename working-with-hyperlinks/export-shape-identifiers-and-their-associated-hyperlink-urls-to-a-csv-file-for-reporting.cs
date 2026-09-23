using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Path for the generated CSV report
                string outputCsv = "shape_hyperlinks.csv";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Create or overwrite the CSV file
                using (StreamWriter writer = new StreamWriter(outputCsv))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeId,HyperlinkUrl");

                    // Iterate through all pages and shapes
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Ensure the shape has a Hyperlinks collection
                            if (shape.Hyperlinks != null)
                            {
                                // Export each hyperlink associated with the shape
                                foreach (Hyperlink link in shape.Hyperlinks)
                                {
                                    // Retrieve the URL; if missing, write an empty string
                                    string url = link.Address?.Value ?? string.Empty;

                                    // Write a CSV line with shape identifier and hyperlink URL
                                    writer.WriteLine($"{shape.ID},{url}");
                                }
                            }
                        }
                    }
                }

                Console.WriteLine($"Export completed. CSV saved to: {outputCsv}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }