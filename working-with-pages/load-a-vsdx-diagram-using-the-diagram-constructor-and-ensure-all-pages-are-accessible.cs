using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSDX diagram using the Diagram constructor
            Diagram diagram = new Diagram("input.vsdx");

            // Ensure all pages are accessible by iterating through the Pages collection
            foreach (Page page in diagram.Pages)
            {
                // Access page properties to confirm they are loaded
                string pageName = page.Name;
                int pageId = page.ID;

                // Example operation: output page information
                Console.WriteLine($"Page ID: {pageId}, Name: {pageName}");
            }

            // Further processing can be done here

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
