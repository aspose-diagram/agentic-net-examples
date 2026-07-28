using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Name of the master shape to be added (e.g., a rectangle)
            string masterName = "Rectangle";

            // Add a shape to every page in the diagram
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                // AddShape(pinX, pinY, masterName, pageNumber)
                // Position (1,1) is arbitrary; size defaults are taken from the master
                diagram.AddShape(1.0, 1.0, masterName, pageIndex);
            }

            // Validate that each page now contains at least one shape
            for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
            {
                Page page = diagram.Pages[pageIndex];
                if (page.Shapes.Count == 0)
                {
                    // Throw an exception if any page is empty
                    throw new InvalidOperationException($"Page {pageIndex} contains no shapes after batch addition.");
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
