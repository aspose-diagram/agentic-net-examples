using System;
using System.IO;
using Aspose.Diagram;

public class DiagramPageHandler
{
    // Loads a diagram, retrieves a page by its numeric ID, and stores the reference for later use.
    public void RetrieveSpecificPage()
    {
        // Load the Visio diagram from a file (replace with your actual file path)
        Diagram diagram = new Diagram("input.vsdx");

        // Specify the numeric ID of the page you want to retrieve
        int targetPageId = 2; // Example ID; set to the desired page ID

        // Retrieve the page using the GetPage method that accepts an integer ID
        Page targetPage = diagram.Pages.GetPage(targetPageId);

        // The 'targetPage' variable now holds a reference to the requested page
        // and can be used for further modifications later in the code.
    }
}

class Program
{
    static void Main(string[] args)
    {
        try
        {

            var obj = new DiagramPageHandler();
            obj.RetrieveSpecificPage();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
