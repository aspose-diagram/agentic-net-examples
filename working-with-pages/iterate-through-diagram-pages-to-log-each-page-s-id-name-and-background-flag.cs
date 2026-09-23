using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Log the page's ID, name, and background flag
                Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}, IsBackground: {page.Background}");
            }

            // (Optional) Save the diagram if modifications were made
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
