using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (replace with actual file path)
            string sourcePath = "input.vsdx";

            // Load the multi‑page diagram
            Diagram sourceDiagram = new Diagram(sourcePath);

            // Ensure the output directory exists
            string outputDir = "output";
            Directory.CreateDirectory(outputDir);

            // Iterate through each page in the source diagram
            for (int i = 0; i < sourceDiagram.Pages.Count; i++)
            {
                // Create a new empty diagram
                Diagram pageDiagram = new Diagram();

                // Remove the default empty page that is created with a new diagram
                Page defaultPage = pageDiagram.Pages[0];
                pageDiagram.Pages.Remove(defaultPage);

                // Add the current page from the source diagram to the new diagram
                pageDiagram.Pages.Add(sourceDiagram.Pages[i]);

                // Build the CSV file name for the current page (1‑based index)
                string csvPath = Path.Combine(outputDir, $"Page_{i + 1}.csv");

                // Export the single‑page diagram to CSV
                pageDiagram.Save(csvPath, SaveFileFormat.Csv);
            }

            Console.WriteLine("Export completed.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
