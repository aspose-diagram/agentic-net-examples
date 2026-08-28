using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Validate arguments: input Visio file and output CSV file paths
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramHyperlinkExport <inputVisioFile> <outputCsvFile>");
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

            // Prepare CSV writer
            try
            {
                using (StreamWriter writer = new StreamWriter(outputCsvPath, false))
                {
                    // Write CSV header
                    writer.WriteLine("ShapeID,HyperlinkURL");

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Ensure the shape has hyperlinks
                            if (shape.Hyperlinks == null || shape.Hyperlinks.Count == 0)
                                continue;

                            // Iterate through each hyperlink explicitly typed
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Some hyperlinks may have empty addresses; skip if so
                                if (link.Address == null || link.Address.Value == null)
                                    continue;

                                // Write shape ID and hyperlink address to CSV
                                // Escape commas in URL if necessary
                                string url = link.Address.Value.Replace("\"", "\"\"");
                                writer.WriteLine($"{shape.ID},\"{url}\"");
                            }
                        }
                    }
                }

                Console.WriteLine($"Export completed successfully. CSV saved to: {outputCsvPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during CSV export: {ex.Message}");
            }
        }
    }