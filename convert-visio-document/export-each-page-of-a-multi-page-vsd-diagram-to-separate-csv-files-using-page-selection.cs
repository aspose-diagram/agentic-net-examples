using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source multi‑page Visio diagram
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Iterate through each page in the source diagram
            for (int i = 0; i < sourceDiagram.Pages.Count; i++)
            {
                // Retrieve the current page
                Page srcPage = sourceDiagram.Pages[i];

                // Create a new empty diagram
                Diagram pageDiagram = new Diagram();

                // Remove the default empty page that is created automatically
                pageDiagram.Pages.Remove(pageDiagram.Pages[0]);

                // Add only the current source page to the new diagram
                pageDiagram.Pages.Add(srcPage);

                // Define the CSV output file name (e.g., Page_1.csv, Page_2.csv, ...)
                string outputCsv = $"Page_{i + 1}.csv";

                // Save the single‑page diagram as CSV
                pageDiagram.Save(outputCsv, SaveFileFormat.Csv);
            }

            Console.WriteLine("Export completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
