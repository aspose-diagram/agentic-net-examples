using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (lifecycle rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the page to retrieve
            string pageName = "Page-1";

            // Retrieve the Page object by its name
            Page targetPage = null;
            foreach (Page page in diagram.Pages)
            {
                if (page.Name == pageName)
                {
                    targetPage = page;
                    break;
                }
            }

            // Output result
            if (targetPage != null)
            {
                Console.WriteLine($"Found page: {targetPage.Name}, ID: {targetPage.ID}");
            }
            else
            {
                Console.WriteLine($"Page '{pageName}' not found.");
            }

            // Save the diagram if modifications were made (optional)
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
