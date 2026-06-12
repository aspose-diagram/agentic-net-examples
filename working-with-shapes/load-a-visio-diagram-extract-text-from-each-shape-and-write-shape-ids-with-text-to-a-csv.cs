using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (change as needed or pass via command line)
                string visioPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output CSV file path
                string csvPath = args.Length > 1 ? args[1] : "output.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Prepare the CSV writer
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID,Text");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve plain text from the shape
                            string text = shape.Text.Value.Text ?? string.Empty;

                            // Escape double quotes in text and wrap the field in quotes
                            string escapedText = $"\"{text.Replace("\"", "\"\"")}\"";

                            // Write Shape ID and text to CSV
                            writer.WriteLine($"{shape.ID},{escapedText}");
                        }
                    }
                }

                Console.WriteLine($"Extraction completed. CSV saved to: {csvPath}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }