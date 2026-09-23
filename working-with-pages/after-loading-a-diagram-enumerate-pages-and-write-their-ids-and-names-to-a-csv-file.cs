using Aspose.Diagram;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Create CSV file and write header
            using (StreamWriter writer = new StreamWriter("pages.csv"))
            {
                writer.WriteLine("PageId,PageName");

                // Enumerate all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Write each page's ID and Name to the CSV
                    writer.WriteLine($"{page.ID},{EscapeCsv(page.Name)}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to escape CSV fields that contain commas, quotes, or newlines
    static string EscapeCsv(string field)
    {
        if (field.Contains("\"") || field.Contains(",") || field.Contains("\n"))
        {
            field = field.Replace("\"", "\"\"");
            return $"\"{field}\"";
        }
        return field;
    }
}
