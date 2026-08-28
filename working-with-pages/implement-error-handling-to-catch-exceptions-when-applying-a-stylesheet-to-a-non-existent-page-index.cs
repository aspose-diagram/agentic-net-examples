using System.IO;
using System;
using Aspose.Diagram;

class ApplyStyleWithErrorHandling
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Index of the page we want to style
            int targetPageIndex = 5; // Example index; may be out of range

            try
            {
                // Verify that the page index exists in the document
                if (targetPageIndex < 0 || targetPageIndex >= diagram.Pages.Count)
                    throw new ArgumentOutOfRangeException(
                        nameof(targetPageIndex),
                        $"Page index {targetPageIndex} does not exist. Valid range is 0 to {diagram.Pages.Count - 1}.");

                // Retrieve the page using the GetPage method (by ID, which is the same as the index)
                Page page = diagram.Pages.GetPage(targetPageIndex);

                // Define style IDs (use -1 for defaults you do not want to change)
                int textStyleId = 0;   // Example text style ID
                int lineStyleId = -1;  // Keep existing line style
                int fillStyleId = -1;  // Keep existing fill style

                // Apply the style to the page
                page.ApplyStyle(textStyleId, lineStyleId, fillStyleId);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // Handle the case where the page index is invalid
                Console.WriteLine($"Invalid page index: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Handle any other unexpected errors
                Console.WriteLine($"An error occurred while applying the style: {ex.Message}");
            }

            // Optionally, save the modified diagram (replace with desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
