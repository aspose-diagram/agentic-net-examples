using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputCsvPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Prepare to write CSV output.
            using (StreamWriter writer = new StreamWriter(outputPath, false))
            {
                // Write CSV header.
                writer.WriteLine("PageID,PageName");

                // Enumerate pages explicitly using the Page type (no var).
                for (int i = 0; i < diagram.Pages.Count; i++)
                {
                    Page page = diagram.Pages[i];

                    // Retrieve page ID and name.
                    // Page.ID is an integer; convert to string for CSV.
                    string idString = page.ID.ToString();
                    // Page.Name may contain commas; enclose in double quotes and escape existing quotes.
                    string name = page.Name ?? string.Empty;
                    string escapedName = $"\"{name.Replace("\"", "\"\"")}\"";

                    // Write a CSV line.
                    writer.WriteLine($"{idString},{escapedName}");
                }
            }

            Console.WriteLine($"Page information successfully written to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}