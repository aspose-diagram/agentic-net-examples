using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the VSDX file
            string filePath = @"C:\Path\To\YourDiagram.vsdx";

            // Load the diagram specifying the VSDX format explicitly
            using (Diagram diagram = new Diagram(filePath, LoadFileFormat.Vsdx))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Output the unique identifier of each shape
                        Console.WriteLine($"Shape ID: {shape.ID}");
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
