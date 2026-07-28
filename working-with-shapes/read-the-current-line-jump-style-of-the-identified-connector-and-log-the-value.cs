using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file (adjust as needed)
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes: 1-D and using the "Dynamic connector" master
                    if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                    {
                        // Retrieve the line jump style from the shape's layout
                        var jumpStyle = shape.Layout.ConLineJumpStyle.Value;

                        // Log the connector ID and its line jump style
                        Console.WriteLine($"Connector ID {shape.ID} has line jump style: {jumpStyle}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
