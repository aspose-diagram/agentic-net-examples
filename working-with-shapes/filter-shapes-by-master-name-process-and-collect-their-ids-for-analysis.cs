using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // List to hold IDs of shapes whose master name is "Process"
            List<long> processShapeIds = new List<long>();

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an associated master and compare its universal name
                    if (shape.Master != null && 
                        string.Equals(shape.Master.NameU, "Process", StringComparison.OrdinalIgnoreCase))
                    {
                        processShapeIds.Add(shape.ID);
                    }
                }
            }

            // Output the collected IDs (or use them for further analysis)
            Console.WriteLine("Shape IDs with master name \"Process\":");
            foreach (long id in processShapeIds)
            {
                Console.WriteLine(id);
            }

            // Save the diagram if any modifications were made (optional)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
