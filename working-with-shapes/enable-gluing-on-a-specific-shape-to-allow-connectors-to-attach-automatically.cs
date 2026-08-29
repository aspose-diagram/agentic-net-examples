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

                // Get the first page (you can change the index as needed)
                Page page = diagram.Pages[0];

                // Retrieve a shape by its ID (example uses ID = 1)
                // In a real scenario, locate the shape by name or other criteria
                Shape shape = page.Shapes.GetShape(1);

                // Enable dynamic glue on the shape so connectors can attach automatically
                // GlueTypeValue.AllowDynamicGlue enables glue; other option is NoAllowDynamicGlue
                shape.Misc.GlueType.Value = GlueTypeValue.AllowDynamicGlue;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Glue enabled on shape ID 1 and diagram saved to " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }