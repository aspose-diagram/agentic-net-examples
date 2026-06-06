using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio file into a Diagram object
            string filePath = "sample.vsdx";
            Diagram diagram = new Diagram(filePath);

            // Access the Pages collection
            var pages = diagram.Pages;

            // Example: iterate through pages and output their names
            foreach (Page page in pages)
            {
                Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
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
