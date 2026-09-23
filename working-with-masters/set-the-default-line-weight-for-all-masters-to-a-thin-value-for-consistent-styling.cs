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

            // Define a thin line weight (in inches)
            double thinLineWeight = 0.02; // approx 0.5 mm

            // Iterate through all masters in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Iterate through each shape that belongs to the master
                foreach (Shape shape in master.Shapes)
                {
                    // Set the line weight for the shape
                    shape.Line.LineWeight.Value = thinLineWeight;
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
