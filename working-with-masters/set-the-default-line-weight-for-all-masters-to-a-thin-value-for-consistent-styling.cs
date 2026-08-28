using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio file (create/load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Define a thin line weight (e.g., 0.028 inches)
            DoubleValue thinWeight = new DoubleValue { Value = 0.028 };

            // Set the line weight for every shape in each master
            foreach (Master master in diagram.Masters)
            {
                foreach (Shape shape in master.Shapes)
                {
                    shape.Line.LineWeight = thinWeight;
                }
            }

            // Save the updated diagram (save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
