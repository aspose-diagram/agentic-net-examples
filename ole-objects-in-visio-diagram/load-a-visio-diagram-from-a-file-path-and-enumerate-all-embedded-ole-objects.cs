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
            string filePath = @"C:\Path\To\Your\Diagram.vsdx";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page: {page.Name}");

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // An embedded OLE object is represented by a shape that has ForeignData
                    if (shape.ForeignData != null && shape.ForeignData.ObjectData != null && shape.ForeignData.ObjectData.Length > 0)
                    {
                        Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.Name}");
                        Console.WriteLine($"    OLE Object Type: {shape.ForeignData.ObjectType}");
                        Console.WriteLine($"    Width: {shape.ForeignData.ObjectWidth}, Height: {shape.ForeignData.ObjectHeight}");
                        Console.WriteLine($"    Show As Icon: {shape.ForeignData.ShowAsIcon}");
                    }
                }
            }

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
