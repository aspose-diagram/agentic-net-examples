using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Create a CSV file to store page information
            using (StreamWriter writer = new StreamWriter("pages.csv"))
            {
                // Write CSV header
                writer.WriteLine("PageId,PageName");

                // Enumerate all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Write each page's ID and Name to the CSV
                    writer.WriteLine($"{page.ID},{page.Name}");
                }
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
