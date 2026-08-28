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

                bool triangleFound = false;

                // Iterate through all pages and shapes to locate a triangle shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify the triangle by its master name (e.g., "Triangle")
                        if (shape.Master != null && shape.Master.Name == "Triangle")
                        {
                            // Apply a solid fill pattern
                            shape.Fill.FillPattern.Value = 1; // 1 = solid
                            // Set the foreground fill color to solid red
                            shape.Fill.FillForegnd.Value = "#FF0000";

                            triangleFound = true;
                            // If multiple triangles need to be colored, remove the break statement
                            break;
                        }
                    }

                    if (triangleFound)
                        break;
                }

                if (!triangleFound)
                    throw new Exception("Triangle shape not found in the diagram.");

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