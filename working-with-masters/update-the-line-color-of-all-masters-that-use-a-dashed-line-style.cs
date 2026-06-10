using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (use defaults if not provided)
            string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
            string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Define the new line color (hex string)
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
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
