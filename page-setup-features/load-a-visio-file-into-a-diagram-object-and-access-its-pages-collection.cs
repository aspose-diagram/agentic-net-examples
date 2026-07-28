using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string filePath = "sample.vsdx";

            // Load the Visio diagram from the file using the Diagram constructor
            Diagram diagram = new Diagram(filePath);

            // Access the Pages collection of the loaded diagram
            var pages = diagram.Pages;

            // Example usage: iterate through pages and output their IDs and names
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
