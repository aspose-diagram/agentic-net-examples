using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the PageSheet associated with the current page
                PageSheet pageSheet = page.PageSheet;

                // Example: access the PageLayout for configuration or inspection
                PageLayout layout = pageSheet.PageLayout;

                // (Optional) Output some layout information for demonstration
                Console.WriteLine($"Page: {page.Name} - Layout: {layout?.ToString() ?? "None"}");
            }

            // No saving is performed as the task only requires retrieval of PageSheets

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
