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

            // Load the diagram using the VSD format
            Diagram diagram = new Diagram(filePath, LoadFileFormat.Vsd);

            // Iterate through all pages in the document
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Shape's own name
                    string shapeName = shape.Name;

                    // Name of the master (stencil) the shape is based on, if any
                    string masterName = shape.Master != null ? shape.Master.Name : "None";

                    Console.WriteLine($"Shape: {shapeName}, Master Stencil: {masterName}");
                }
            }

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
