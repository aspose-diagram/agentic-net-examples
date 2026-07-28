using System;
using System.IO;
using Aspose.Diagram;

class DiagramPageRetriever
{
    public void RetrieveSpecificPage()
    {
        // Load an existing Visio document (replace with your file path)
        Diagram diagram = new Diagram("input.vsdx");

        // Numeric ID of the page you want to work with
        int pageId = 3;

        // Retrieve the page by its ID from the Pages collection
        Page targetPage = diagram.Pages.GetPage(pageId);

        // The 'targetPage' reference can now be used for further modifications
        // Example modification (optional):
        // targetPage.Name = "Updated Page Name";
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramPageRetriever();
            obj.RetrieveSpecificPage();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
