using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio file into a Diagram object.
            // Replace "sample.vsdx" with the path to your Visio file.
            Diagram diagram = new Diagram("sample.vsdx");

            // Access the collection of pages in the diagram.
            PageCollection pages = diagram.Pages;

            // Example: iterate through pages and print their names.
            foreach (Page page in pages)
            {
                Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
