using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Create a text file to hold the summary report
            using (StreamWriter writer = new StreamWriter("ShapeCountReport.txt"))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Count the shapes on the current page
                    int shapeCount = page.Shapes.Count;

                    // Write the page information and shape count to the report
                    writer.WriteLine($"Page {page.ID} (Name: {page.Name}): {shapeCount} shapes");
                }
            }

            // Dispose the diagram object
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
