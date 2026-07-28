using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // If the shape is based on a master, retrieve its master information
                    if (shape.Master != null)
                    {
                        // Log the shape ID and the name of its master
                        Console.WriteLine($"Shape ID: {shape.ID}, Master Name: {shape.Master.Name}");
                    }
                }
            }

            // Save the diagram if any changes were made (optional)
            // diagram.Save("output.vsdx", SaveFileFormat.Vdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
