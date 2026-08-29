using System;
using Aspose.Diagram;

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

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape is a connector (1‑D shape)
                        if (shape.OneD)
                        {
                            // Enable line inheritance for connectors by copying inherited line values
                            shape.Line.LineColor.Value = shape.InheritLine.LineColor.Value;
                            shape.Line.LineWeight.Value = shape.InheritLine.LineWeight.Value;
                            shape.Line.LinePattern.Value = shape.InheritLine.LinePattern.Value;
                            shape.Line.BeginArrow.Value = shape.InheritLine.BeginArrow.Value;
                            shape.Line.EndArrow.Value = shape.InheritLine.EndArrow.Value;
                        }
                        else
                        {
                            // Disable line inheritance for non‑connectors by setting explicit line values
                            shape.Line.LineColor.Value = "#000000";               // Black line color
                            shape.Line.LineWeight.Value = 0.02;                  // 0.02 inches line weight
                            shape.Line.LinePattern.Value = LinePatternValue.Solid; // Solid line pattern
                            shape.Line.BeginArrow.Value = 0;                     // No begin arrow
                            shape.Line.EndArrow.Value = 0;                       // No end arrow
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