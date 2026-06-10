using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path to the output Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define a thin line weight (in inches)
                double thinLineWeight = 0.01; // Adjust as needed for "thin"

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    // Iterate through each shape within the master
                    foreach (Shape shape in master.Shapes)
                    {
                        // Set the line weight for the shape
                        shape.Line.LineWeight.Value = thinLineWeight;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }