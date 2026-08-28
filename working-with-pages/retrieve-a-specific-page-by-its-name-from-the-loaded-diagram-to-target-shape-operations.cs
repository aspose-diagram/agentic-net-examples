using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to load
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Name of the page you want to retrieve
            string targetPageName = "MyPage";

            // Retrieve the page by its name
            Page page = diagram.Pages.GetPage(targetPageName);

            // Validate that the page exists
            if (page == null)
            {
                throw new Exception($"Page '{targetPageName}' not found in the diagram.");
            }

            // Output basic page information
            Console.WriteLine($"Page found: ID = {page.ID}, Name = {page.Name}, NameU = {page.NameU}");

            // Example: iterate over shapes on the retrieved page
            foreach (Shape shape in page.Shapes)
            {
                Console.WriteLine($"Shape ID = {shape.ID}, Name = {shape.Name}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
