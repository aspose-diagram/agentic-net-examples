using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and output CSV file path
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: DiagramExport <inputVisioFile> <outputCsvFile>");
            return;
        }

        string inputPath = args[0];
        string outputCsvPath = args[1];

        // Load the Visio diagram
        using (Diagram diagram = new Diagram(inputPath))
        {
            // Prepare a StringBuilder for CSV content
            StringBuilder csvBuilder = new StringBuilder();

            // Write CSV header
            csvBuilder.AppendLine("PageName,PageID,ShapeID,ShapeName,ShapeText");

            // Iterate through each page
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve plain text from the shape
                    string text = shape.Text.Value.Text ?? string.Empty;

                    // Escape commas and quotes for CSV compliance
                    string escapedText = text.Replace("\"", "\"\"");
                    if (escapedText.Contains(",") || escapedText.Contains("\"") || escapedText.Contains("\n"))
                        escapedText = $"\"{escapedText}\"";

                    // Append a CSV line with page and shape information
                    csvBuilder.AppendLine($"{page.Name},{page.ID},{shape.ID},{shape.Name},{escapedText}");
                }
            }

            // Write the CSV content to the specified file
            File.WriteAllText(outputCsvPath, csvBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"Export completed. CSV saved to: {outputCsvPath}");
        }
    }
}
