using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Load an existing Visio diagram
            var diagram = new Aspose.Diagram.Diagram("input.vsdx");

            // Ensure there are at least two pages to reorder
            if (diagram.Pages.Count >= 2)
            {
                // Get the last page (zero‑based index)
                int lastPageIndex = diagram.Pages.Count - 1;
                var lastPage = diagram.Pages[lastPageIndex];

                // Move the last page to the second position (index 1)
                lastPage.MoveTo(1);
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", Aspose.Diagram.SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
