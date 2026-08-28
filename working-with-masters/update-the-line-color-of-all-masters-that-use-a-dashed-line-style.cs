using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the new line color (hex format)
                const string newLineColor = "#FF0000"; // Red

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    // Check each shape within the master
                    foreach (Shape shape in master.Shapes)
                    {
                        // If the shape uses a dashed line pattern, update its line color
                        if (shape.Line.LinePattern.Value == LinePatternValue.Dash)
                        {
                            shape.Line.LineColor.Value = newLineColor;
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