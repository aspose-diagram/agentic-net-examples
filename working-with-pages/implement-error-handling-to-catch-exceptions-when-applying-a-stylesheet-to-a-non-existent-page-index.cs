using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Index of the page we want to style (0‑based)
            int pageIndex = 5;

            // IDs of the styles to apply (use -1 for defaults you don't want to change)
            int textStyleId = 1;
            int lineStyleId = 2;
            int fillStyleId = 3;

            try
            {
                // Attempt to retrieve the page. GetPage throws if the page does not exist.
                Page page = diagram.Pages.GetPage(pageIndex);

                // Apply the specified styles to the whole page.
                page.ApplyStyle(textStyleId, lineStyleId, fillStyleId);
            }
            catch (Exception ex)
            {
                // Handle the case where the page index is invalid or any other error occurs.
                Console.WriteLine($"Error applying style to page index {pageIndex}: {ex.Message}");
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
