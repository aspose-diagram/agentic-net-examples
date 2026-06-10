using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (load rule)
            var diagram = new Diagram("input.vsdx");

            // Prepare a CSV file for writing page information
            using (var writer = new StreamWriter("pages.csv"))
            {
                // Write CSV header
                writer.WriteLine("PageId,PageName");

                // Enumerate all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Write each page's Id and Name to the CSV
                    writer.WriteLine($"{page.ID},{EscapeCsv(page.Name)}");
                }
            }

            // Dispose the diagram when done
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper to escape commas and quotes in CSV fields
    static string EscapeCsv(string field)
    {
        if (field.Contains("\""))
            field = field.Replace("\"", "\"\"");

        if (field.Contains(",") || field.Contains("\"") || field.Contains("\n"))
            field = $"\"{field}\"";

        return field;
    }
}
