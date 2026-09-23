using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there are at least two pages to reorder
            int pageCount = diagram.Pages.Count;
            if (pageCount > 1)
            {
                // Retrieve the last page (zero‑based index)
                Page lastPage = diagram.Pages[pageCount - 1];

                // Move the last page to the second position (index 1)
                lastPage.MoveTo(1);
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
