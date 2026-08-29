using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output CSV file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramTextExtractor <inputVisioFile> <outputCsvFile>");
                return;
            }

            string inputPath = args[0];
            string outputCsvPath = args[1];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Prepare to write CSV
            using (StreamWriter writer = new StreamWriter(outputCsvPath))
            {
                // Write CSV header
                writer.WriteLine("PageNumber,TextContent");

                // Iterate through each page in the diagram
                int pageNumber = 1;
                foreach (Page page in diagram.Pages)
                {
                    // Accumulate plain text from all shapes on the current page
                    string pageText = string.Empty;

                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text from the shape
                        string shapeText = shape.Text.Value.Text;

                        // Ensure the text is not null or whitespace
                        if (!string.IsNullOrWhiteSpace(shapeText))
                        {
                            // Clean up line breaks and commas to keep CSV format simple
                            shapeText = shapeText.Replace("\r\n", " ").Replace("\n", " ").Replace(",", " ");

                            // Append a space between texts from different shapes
                            if (pageText.Length > 0)
                                pageText += " ";

                            pageText += shapeText;
                        }
                    }

                    // Write the page number and its aggregated text to the CSV
                    writer.WriteLine($"{pageNumber},\"{pageText}\"");

                    pageNumber++;
                }
            }

            Console.WriteLine($"Text extraction completed. CSV saved to: {outputCsvPath}");
        }
    }