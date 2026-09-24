using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to load
            string inputPath = "input.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the page named "Page-2"
            Page page = diagram.Pages.GetPage("Page-2");

            // Output information about the retrieved page
            if (page != null)
            {
                Console.WriteLine($"Page found: Name = {page.Name}, ID = {page.ID}");
            }
            else
            {
                Console.WriteLine("Page 'Page-2' not found.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
