using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output CSV path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputCsvPath>");
            return;
        }

        string diagramPath = args[0];
        string csvPath = args[1];

        // Guard for input file existence
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Open a StreamWriter for CSV output
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // Write CSV header
                writer.WriteLine("PageID,PageName");

                // Enumerate pages explicitly typing the iterator
                foreach (Page page in diagram.Pages)
                {
                    // Write page ID and name, escaping commas if needed
                    string id = page.ID.ToString();
                    string name = page.Name?.Replace(",", " "); // simple escape for commas
                    writer.WriteLine($"{id},{name}");
                }
            }

            Console.WriteLine($"Page information successfully written to {csvPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}