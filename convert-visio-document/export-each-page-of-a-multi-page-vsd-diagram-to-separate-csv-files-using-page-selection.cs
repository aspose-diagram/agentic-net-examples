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

            // Path to the source Visio file (multi‑page diagram)
            string sourcePath = "input.vsdx";

            // Load the diagram
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Iterate through each page in the source diagram
            for (int i = 0; i < sourceDiagram.Pages.Count; i++)
            {
                // Retrieve the current page
                Page page = sourceDiagram.Pages[i];

                // Create a new empty diagram
                Diagram pageDiagram = new Diagram();

                // Remove the automatically created empty page
                if (pageDiagram.Pages.Count > 0)
                {
                    Page emptyPage = pageDiagram.Pages[0];
                    pageDiagram.Pages.Remove(emptyPage);
                }

                // Add the selected page from the source diagram
                pageDiagram.Pages.Add(page);

                // Define output CSV file name (e.g., Page_1.csv, Page_2.csv, ...)
                string outputCsv = $"Page_{i + 1}.csv";

                // Save the new diagram as CSV (only the added page will be exported)
                pageDiagram.Save(outputCsv, SaveFileFormat.Csv);

                // Clean up the temporary diagram instance
                pageDiagram.Dispose();
            }

            // Clean up the source diagram
            sourceDiagram.Dispose();

            Console.WriteLine("Export completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
