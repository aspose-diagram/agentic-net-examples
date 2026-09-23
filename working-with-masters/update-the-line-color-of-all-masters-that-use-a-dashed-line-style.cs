using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Desired line color for masters that use a dashed line style
                const string newLineColor = "#FF0000"; // Red

                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    bool usesDashedLine = false;

                    // Check if any shape within the master uses a dashed line pattern
                    foreach (Shape shape in master.Shapes)
                    {
                        if (shape.Line.LinePattern.Value == LinePatternValue.Dash)
                        {
                            usesDashedLine = true;
                            break;
                        }
                    }

                    // If the master uses a dashed line, update the line color of all its shapes
                    if (usesDashedLine)
                    {
                        foreach (Shape shape in master.Shapes)
                        {
                            shape.Line.LineColor.Value = newLineColor;
                        }
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