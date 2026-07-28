using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (replace with actual paths as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram from the specified file
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    // Iterate through each shape within the master
                    foreach (Shape shape in master.Shapes)
                    {
                        // Set the line weight to a thin value (e.g., 0.02 inches)
                        shape.Line.LineWeight.Value = 0.02;
                    }
                }

                // Save the modified diagram to the output file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }