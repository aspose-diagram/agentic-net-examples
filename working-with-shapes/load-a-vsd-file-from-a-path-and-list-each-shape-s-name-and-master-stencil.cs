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
            string filePath = @"C:\Path\To\Your\Diagram.vsd";

            // Load the diagram using the constructor that accepts a file path
            using (Diagram diagram = new Diagram(filePath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Get the shape's name
                        string shapeName = shape.Name;

                        // Get the name of the master (stencil) the shape is based on, if any
                        string masterName = shape.Master != null ? shape.Master.Name : "N/A";

                        // Output the information
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
