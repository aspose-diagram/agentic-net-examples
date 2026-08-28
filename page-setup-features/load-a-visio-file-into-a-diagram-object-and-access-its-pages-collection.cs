using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio file into a Diagram object using the built‑in constructor
            var diagram = new Diagram("sample.vsdx");

            // Access the Pages collection of the loaded diagram
            var pages = diagram.Pages;

            // Example: iterate through the pages and output basic information
            foreach (var page in pages)
            {
                Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
            }

            // Dispose the diagram when done (optional, as Diagram implements IDisposable)
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
