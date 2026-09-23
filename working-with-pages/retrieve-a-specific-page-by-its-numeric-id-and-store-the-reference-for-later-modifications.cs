using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Numeric ID of the page to retrieve (example: 2)
            int pageId = 2;

            // Retrieve the page by its numeric ID
            Page targetPage = diagram.Pages[pageId];

            // The page reference can now be used for further modifications
            // Example modification: rename the page
            targetPage.Name = "ModifiedPage";

            // Save the diagram after modifications
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
