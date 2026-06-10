using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file
                string diagramPath = "input.vsdx";

                // Load the diagram from file
                Diagram diagram = new Diagram(diagramPath);

                // Name of the page you want to retrieve
                string targetPageName = "MyPage";

                // Retrieve the page by its name
                Page page = diagram.Pages.GetPage(targetPageName);

                if (page != null)
                {
                    Console.WriteLine($"Page '{targetPageName}' found. ID: {page.ID}");

                    // Example: iterate shapes on the retrieved page
                    foreach (Shape shape in page.Shapes)
                    {
                        Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.Name}");
                    }
                }
                else
                {
                    Console.WriteLine($"Page '{targetPageName}' not found in the diagram.");
                }

                // No explicit disposal needed; Diagram implements IDisposable but will be cleaned up when out of scope

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }