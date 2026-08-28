using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_locked.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (adjust index if needed)
                Page page = diagram.Pages[0];

                // Find the first diamond shape on the page
                Shape diamondShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has a master and compare its name
                    if (shape.Master != null && shape.Master.Name == "Diamond")
                    {
                        diamondShape = shape;
                        break;
                    }
                }

                if (diamondShape == null)
                {
                    throw new Exception("Diamond shape not found in the diagram.");
                }

                // Lock the aspect ratio of the diamond shape
                diamondShape.Protection.LockAspect.Value = BOOL.True;

                // Desired new width (in inches)
                double newWidth = 2.0;

                // Preserve the original aspect ratio
                double originalWidth = diamondShape.XForm.Width.Value;
                double originalHeight = diamondShape.XForm.Height.Value;

                if (originalWidth == 0)
                {
                    throw new Exception("Original width of the shape is zero, cannot compute aspect ratio.");
                }

                double aspectRatio = originalHeight / originalWidth;
                double newHeight = newWidth * aspectRatio;

                // Apply the new dimensions
                diamondShape.XForm.Width.Value = newWidth;
                diamondShape.XForm.Height.Value = newHeight;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }