using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
         try
         {

             // Load an existing Visio diagram
             Diagram diagram = new Diagram("input.vsdx");

             // Specify the numeric ID of the page you want to work with
             int pageId = 2; // replace with the desired page ID

             // Retrieve the page by its ID and keep a reference for later modifications
             Page targetPage = diagram.Pages.GetPage(pageId);

             // Example modification: change the page name (optional)
             // targetPage.Name = "Modified Page";

             // Save the diagram after any modifications (if needed)
             diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

         }
         catch (System.IO.FileNotFoundException ex)
         {
             Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
         }
    }
}
