using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find a triangle shape
                bool triangleFound = false;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify triangle by its master name (e.g., "Triangle")
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            // Apply solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // 1 = solid
                            // Set foreground (fill) color to solid red
                            shape.Fill.FillForegnd.Value = "#FF0000";

                            triangleFound = true;
                            // If only one triangle is needed, break out of loops
                            break;
                        }
                    }
                    if (triangleFound) break;
                }

                if (!triangleFound)
                {
                    throw new Exception("Triangle shape not found in the diagram.");
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