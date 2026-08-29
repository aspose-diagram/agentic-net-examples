using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the target shape name (universal name)
                string targetShapeNameU = "MyShape";

                // Iterate through all pages to find the shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU == targetShapeNameU)
                        {
                            // Disable line inheritance by explicitly setting line properties
                            // Set a new line weight (in inches)
                            shape.Line.LineWeight.Value = 0.05; // Example weight

                            // Optionally set a line color to ensure the line is not inherited
                            shape.Line.LineColor.Value = "#FF0000";

                            // Break after modifying the first matching shape
                            break;
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