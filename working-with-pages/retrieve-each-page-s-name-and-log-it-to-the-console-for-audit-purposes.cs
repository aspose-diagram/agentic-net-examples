using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            for (int i = 0; i < diagram.Pages.Count; i++)
            {
                // Retrieve the page object by index
                Page page = diagram.Pages[i];

                // Log the page index and its name to the console
                Console.WriteLine($"Page {i}: {page.Name}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
