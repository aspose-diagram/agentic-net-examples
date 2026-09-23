using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find the pentagon shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a master and check its name
                        if (shape.Master != null && shape.Master.Name == "Pentagon")
                        {
                            // Set line pattern to dashed
                            shape.Line.LinePattern.Value = LinePatternValue.Dash;

                            // Set line thickness (weight) in inches, e.g., 0.03 inches
                            shape.Line.LineWeight.Value = 0.03;
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