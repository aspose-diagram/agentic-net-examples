using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source VDX file
                string inputPath = "input.vdx";

                // Load the diagram from the file
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the shape with ID 5 from the first page
                // Shape IDs are of type long
                long targetShapeId = 5L;
                Shape shape = diagram.Pages[0].Shapes.GetShape(targetShapeId);

                // Prepare the dynamic replacement text
                string newText = $"Updated at {DateTime.Now:yyyy-MM-dd HH:mm:ss}";

                // Replace the existing text
                shape.Text.Value.Clear();                     // Remove any existing text runs
                shape.Text.Value.Add(new Txt(newText));       // Add the new text run

                // Save the modified diagram (optional)
                string outputPath = "output.vdx";
                diagram.Save(outputPath, SaveFileFormat.Vdx);

                Console.WriteLine($"Shape ID {targetShapeId} text replaced and diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }