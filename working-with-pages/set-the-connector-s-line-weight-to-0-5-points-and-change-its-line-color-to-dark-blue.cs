using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find connector shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify dynamic connectors (1‑D shapes with a master named "Dynamic connector")
                    if (shape.OneD && shape.Master != null && shape.Master.Name == "Dynamic connector")
                    {
                        // Set line weight to 0.5 points (value in inches; Visio treats the value as points)
                        shape.Line.LineWeight.Value = 0.5;

                        // Set line color to dark blue using a HEX color string
                        shape.Line.LineColor.Value = "#00008B";
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
