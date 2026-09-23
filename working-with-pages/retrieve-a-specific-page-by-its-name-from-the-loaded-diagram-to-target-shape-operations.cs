using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file
            string inputPath = "example.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Name of the page you want to work with
            string pageName = "TargetPage";

            // Retrieve the page by its name
            Page page = diagram.Pages.GetPage(pageName);

            if (page == null)
            {
                Console.WriteLine($"Page '{pageName}' was not found in the diagram.");
                return;
            }

            Console.WriteLine($"Page '{pageName}' retrieved successfully.");
            Console.WriteLine($"Number of shapes on the page: {page.Shapes.Count}");

            // Example: iterate through shapes on the retrieved page
            foreach (Shape shape in page.Shapes)
            {
                Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
