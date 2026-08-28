using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSDX diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages to ensure they are accessible
            foreach (Page page in diagram.Pages)
            {
                // Example access: output page ID and name
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
