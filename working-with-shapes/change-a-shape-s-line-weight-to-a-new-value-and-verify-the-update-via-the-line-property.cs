using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram (replace with actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Define the new line weight (in inches)
                double newLineWeight = 0.05; // Example: 0.05 inches

                // Retrieve the first shape on the first page
                Page page = diagram.Pages[0];
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    targetShape = shape;
                    break;
                }

                if (targetShape == null)
                {
                    throw new Exception("No shape found on the first page to modify.");
                }

                // Update the shape's line weight
                targetShape.Line.LineWeight.Value = newLineWeight;

                // Verify the update
                if (Math.Abs(targetShape.Line.LineWeight.Value - newLineWeight) < 1e-9)
                {
                    Console.WriteLine($"Line weight successfully updated to {newLineWeight} inches.");
                }
                else
                {
                    throw new Exception("Line weight update verification failed.");
                }

                // Save the modified diagram (optional)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }