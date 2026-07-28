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

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Count the shapes on the current page
                int shapeCount = page.Shapes.Count;

                // Output the summary for the page
                Console.WriteLine($"Page \"{page.Name}\" (ID: {page.ID}) contains {shapeCount} shape(s).");
            }

            // Save the diagram (optional, demonstrates save usage)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
