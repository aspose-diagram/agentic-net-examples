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

                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Find the shape by its universal name (NameU)
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "MyShape")
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                {
                    throw new Exception("Shape with NameU 'MyShape' not found.");
                }

                // Set glue type to allow only incoming connectors (disable outgoing dynamic glue)
                targetShape.Misc.GlueType.Value = GlueTypeValue.NoAllowDynamicGlue;

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