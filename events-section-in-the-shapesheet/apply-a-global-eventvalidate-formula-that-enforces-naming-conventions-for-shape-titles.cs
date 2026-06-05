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

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Apply a global event formula that will be called to validate the shape's title
                        // The formula uses CALLTHIS to invoke a custom macro (to be defined in the Visio file)
                        shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"ValidateShapeTitle\")";

                        // Optionally, also trigger validation on shape drop
                        shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"ValidateShapeTitle\")";
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