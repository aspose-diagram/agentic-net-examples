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

                // Iterate through all pages and shapes to find the triangle shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a master and that the master name is "Triangle"
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            // Apply a solid fill pattern (value 1) and set the foreground color to solid red
                            shape.Fill.FillPattern.Value = 1;               // Solid fill
                            shape.Fill.FillForegnd.Value = "#FF0000";       // Red color in HEX

                            // Optional: you can break after the first match if only one triangle is expected
                            // break;
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