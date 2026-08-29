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

                // Access the first page
                Page page = diagram.Pages[0];

                // Find the first shape on the page
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    targetShape = shape;
                    break;
                }

                if (targetShape == null)
                {
                    throw new Exception("No shapes found on the first page.");
                }

                // Rotate the shape (example: 45 degrees)
                double angleDegrees = 45.0;
                double angleRadians = Math.PI * angleDegrees / 180.0;
                targetShape.SetAngle(angleRadians);

                // Disable KeepTextFlat to allow text rotation
                // KeepTextFlat is a BOOL cell; set it to FALSE
                targetShape.ThreeDFormat.KeepTextFlat.Value = BOOL.False;

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