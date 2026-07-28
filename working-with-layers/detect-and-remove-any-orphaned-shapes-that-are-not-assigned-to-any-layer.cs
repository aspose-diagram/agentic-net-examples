using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path after orphan removal
                string outputPath = "output_cleaned.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes already marked as deleted
                        if (shape.Del == BOOL.False)
                        {
                            // Retrieve the layer membership string (may be null)
                            string layerMember = shape.LayerMem?.LayerMember?.Value;

                            // If the shape is not assigned to any layer, mark it as deleted
                            if (string.IsNullOrEmpty(layerMember))
                            {
                                shape.Del = BOOL.True;
                                Console.WriteLine($"Removed orphan shape ID {shape.ID} on page \"{page.Name}\".");
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to \"{outputPath}\".");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }