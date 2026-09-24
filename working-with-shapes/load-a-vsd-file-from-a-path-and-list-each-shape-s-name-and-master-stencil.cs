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

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through each page and its shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the shape's name
                    string shapeName = shape.Name;

                    // Get the master stencil name (if the shape is based on a master)
                    string masterStencil = shape.Master != null ? shape.Master.NameU : "N/A";

                    Console.WriteLine($"Shape Name: {shapeName}, Master Stencil: {masterStencil}");
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
