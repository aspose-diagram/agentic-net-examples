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

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Configure print options
            PrintSaveOptions printOptions = new PrintSaveOptions
            {
                // Print only foreground pages (background pages are ignored)
                SaveForegroundPagesOnly = true
            };

            // Iterate through all pages and ensure they have a PageSheet.
            // If a page lacks a PageSheet, it is skipped.
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                try
                {
                    // Access the PageSheet; if it is null an exception will be thrown
                    PageSheet pageSheet = diagram.Pages[i].PageSheet;

                    // Example of accessing print properties (optional)
                    // var printProps = pageSheet.PrintProps;
                    // Modify printProps here if needed
                }
                catch (NullReferenceException)
                {
                    // PageSheet is missing – skip this page
                    continue;
                }
                catch (Exception ex)
                {
                    // Log unexpected errors and continue with the next page
                    Console.WriteLine($"Error processing page {i}: {ex.Message}");
                    continue;
                }
            }

            // Print the diagram using the prepared options
            diagram.Print(printOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
