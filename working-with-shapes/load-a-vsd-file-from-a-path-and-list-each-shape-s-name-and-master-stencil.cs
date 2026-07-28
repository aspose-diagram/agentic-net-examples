using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSD file
            string filePath = @"C:\Path\To\YourFile.vsd";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Shape name (may be empty)
                        string shapeName = shape.Name ?? "<no name>";

                        // Master stencil name (if the shape is based on a master)
                        string masterName = shape.Master != null ? shape.Master.Name ?? "<no master name>" : "<no master>";

                        Console.WriteLine($"Shape: {shapeName}, Master Stencil: {masterName}");
                    }
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
