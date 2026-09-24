using System;
using Aspose.Diagram;
using Aspose.Diagram.Manipulation;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram.
                // Replace the path with the actual file location.
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // The universal name of the shape to enable gluing on.
                string targetShapeNameU = "MyShape";

                bool shapeFound = false;

                // Iterate through all pages and shapes to locate the target shape.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU == targetShapeNameU)
                        {
                            // Enable dynamic gluing for the shape.
                            // This allows connectors to automatically attach to the shape.
                            shape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

                            shapeFound = true;
                            break;
                        }
                    }

                    if (shapeFound)
                        break;
                }

                if (!shapeFound)
                    throw new Exception($"Shape with NameU '{targetShapeNameU}' was not found in the diagram.");

                // Save the modified diagram.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }