using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram
            var diagram = new Diagram("input.vsdx");

            // Determine the number of pages in the diagram
            int pageCount = diagram.Pages.Count;

            // Ensure there are at least two pages to reorder
            if (pageCount > 1)
            {
                // Retrieve the last page (zero‑based index)
                var lastPage = diagram.Pages[pageCount - 1];

                // Move the last page to the second position (index 1)
                lastPage.MoveTo(1);
            }

            // Save the diagram with the pages reordered
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
