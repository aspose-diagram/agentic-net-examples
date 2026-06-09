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
                Console.WriteLine("Usage: DiagramHyperlinkExport <inputVisioFile> <outputCsvFile>");
                return;
            }

            string inputPath = args[0];
            string outputCsvPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare to write CSV
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

                        // If the shape has hyperlinks, write each one; otherwise write an empty URL
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Ensure the address cell is not null
                                string address = link.Address?.Value ?? string.Empty;
                                // Escape double quotes in the address
                                address = address.Replace("\"", "\"\"");
                                writer.WriteLine($"{shape.ID},\"{address}\"");
                            }
                        }
                        else
                        {
                            writer.WriteLine($"{shape.ID},");
                        }
                    }
                }
            }

            Console.WriteLine($"Export completed. CSV saved to: {outputCsvPath}");
        }
    }