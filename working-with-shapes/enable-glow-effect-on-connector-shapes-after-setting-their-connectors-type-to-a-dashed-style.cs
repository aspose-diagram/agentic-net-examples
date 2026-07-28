using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Set the connector line style to dashed
                            shape.Line.LinePattern.Value = LinePatternValue.Dash;

                            // Optional: adjust line weight for better visibility
                            shape.Line.LineWeight.Value = 0.02; // inches

                            // Simulate a glow effect by increasing the line weight slightly
                            // and applying a bright color. Real glow effects are not directly
                            // exposed in the Aspose.Diagram API, so this is a common workaround.
                            shape.Line.LineWeight.Value = 0.04; // thicker line for "glow"
                            shape.Line.LineColor.Value = "#FFAA00"; // bright orange glow color

                            // Reduce line transparency to make the glow more pronounced
                            shape.Line.LineColorTrans.Value = 30; // 30% transparency
                        }
                    }
                }

                // Save the modified diagram to a new file
                string outputPath = "output_with_glow.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }