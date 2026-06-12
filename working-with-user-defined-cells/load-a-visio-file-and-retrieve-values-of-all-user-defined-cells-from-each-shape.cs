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
            string visioPath = @"C:\Path\To\Your\Diagram.vsdx";

            // Load the Visio document (uses the Diagram(string) constructor)
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // User‑defined cells are stored in the Users collection of a shape
                    foreach (User userCell in shape.Users)
                    {
                        // Output the page name, shape name, user cell name and its value
                        Console.WriteLine(
                            $"Page: {page.Name}, Shape: {shape.Name}, User Cell: {userCell.NameU} = {userCell.Value}");
                    }
                }
            }

            // Dispose the diagram when done
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
