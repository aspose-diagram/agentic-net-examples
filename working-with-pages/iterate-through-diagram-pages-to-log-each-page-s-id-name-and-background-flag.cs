using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            var diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Log the page's ID, name, and background flag
                Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}, IsBackground: {page.Background}");
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
