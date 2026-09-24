using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSDX file from the specified path
            Diagram diagram = new Diagram(@"C:\Path\To\Your\File.vsdx");

            // Enumerate all shapes in all pages and output their IDs
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    Console.WriteLine($"Shape ID: {shape.ID}");
                }
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
