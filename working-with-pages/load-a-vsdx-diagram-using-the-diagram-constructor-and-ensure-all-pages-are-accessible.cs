using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSDX diagram using the constructor that accepts a file name.
            Diagram diagram = new Diagram("sample.vsdx");

            // Iterate through all pages to ensure they are accessible.
            foreach (Page page in diagram.Pages)
            {
                // Access page properties (e.g., ID and Name) as a simple verification.
                Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
            }

            // Clean up resources.
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
