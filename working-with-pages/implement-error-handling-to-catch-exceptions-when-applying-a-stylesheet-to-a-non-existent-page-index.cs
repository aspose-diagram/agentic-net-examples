using System.IO;
using System;
using Aspose.Diagram;

class ApplyStyleWithErrorHandling
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define the page index we want to style
            int targetPageIndex = 5; // Example index that may not exist

            // Define style IDs (use -1 for defaults if not needed)
            int textStyleId = -1;
            int lineStyleId = -1;
            int fillStyleId = -1;

            try
            {
                // Attempt to retrieve the page by its index.
                // GetPage throws an exception if the page does not exist.
                Page page = diagram.Pages.GetPage(targetPageIndex);

                // Apply the style to the retrieved page.
                page.ApplyStyle(textStyleId, lineStyleId, fillStyleId);

                Console.WriteLine($"Style applied successfully to page index {targetPageIndex}.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Specific handling for out‑of‑range page index
                Console.WriteLine($"Error: Page index {targetPageIndex} is out of range. {ex.Message}");
            }
            catch (Exception ex)
            {
                // General fallback for any other errors
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            // Optionally save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
