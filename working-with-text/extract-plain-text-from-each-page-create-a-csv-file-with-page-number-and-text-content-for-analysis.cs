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
            Console.WriteLine("Usage: <inputVisioPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputCsvPath = args[1];

        // Load the Visio diagram
        Diagram diagram = new Diagram(inputPath);

        // Prepare CSV content
        StringBuilder csvBuilder = new StringBuilder();
        csvBuilder.AppendLine("PageNumber,TextContent");

        int pageNumber = 1;
        foreach (Page page in diagram.Pages)
        {
            StringBuilder pageTextBuilder = new StringBuilder();

            // Iterate all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Skip deleted shapes
                if (shape.Del == BOOL.True)
                    continue;

                // Retrieve plain text from the shape
                string text = shape.Text.Value.Text;

                if (!string.IsNullOrWhiteSpace(text))
                {
                    // Clean text to avoid CSV line breaks and commas
                    string cleaned = text.Replace("\r\n", " ")
                                         .Replace("\n", " ")
                                         .Replace(",", " ");
                    pageTextBuilder.Append(cleaned);
                    pageTextBuilder.Append(' ');
                }
            }

            // Trim trailing space and escape double quotes
            string pageText = pageTextBuilder.ToString().Trim();
            pageText = pageText.Replace("\"", "\"\"");

            // Append CSV line for the current page
            csvBuilder.AppendLine($"{pageNumber},\"{pageText}\"");
            pageNumber++;
        }

        // Write CSV to file
        File.WriteAllText(outputCsvPath, csvBuilder.ToString());
        Console.WriteLine($"CSV file created at: {outputCsvPath}");
    }
}
