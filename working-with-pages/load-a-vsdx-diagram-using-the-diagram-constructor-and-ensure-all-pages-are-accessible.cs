using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSDX file
            string filePath = "sample.vsdx";

            // Load the diagram using the Diagram constructor
            Diagram diagram = new Diagram(filePath);

            // Ensure all pages are accessible by iterating through them
            Console.WriteLine($"Total pages: {diagram.Pages.Count}");
            foreach (Page page in diagram.Pages)
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
