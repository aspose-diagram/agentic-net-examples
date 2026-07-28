using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (first argument) or default.
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Output CSV file path (second argument) or default.
                string outputCsv = args.Length > 1 ? args[1] : "output.csv";

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                // Create a CSV writer.
                using (var writer = new StreamWriter(outputCsv, false, Encoding.UTF8))
                {
                    // Write CSV header.
                    writer.WriteLine("ShapeID,Text");

                    // Iterate through all pages.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Retrieve plain text from the shape.
                            string text = shape.Text.Value.Text ?? string.Empty;

                            // Clean text to avoid breaking CSV format.
                            text = text.Replace("\r", " ")
                                       .Replace("\n", " ")
                                       .Replace(",", " ");

                            // Write shape ID and text to CSV.
                            writer.WriteLine($"{shape.ID},{text}");
                        }
                    }
                }

                // Dispose the diagram (optional, as Diagram implements IDisposable).
                diagram.Dispose();

                Console.WriteLine($"Extraction completed. CSV saved to '{outputCsv}'.");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }