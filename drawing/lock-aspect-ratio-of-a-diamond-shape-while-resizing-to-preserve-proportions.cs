using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume the diamond shape is on the first page
                Page page = diagram.Pages[0];

                // Find the first shape whose master name is "Diamond"
                Shape diamondShape = null;
                foreach (Shape shape in page.Shapes)
                {
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
                double aspectRatio = originalHeight / originalWidth;

                // Apply new dimensions while keeping the proportion
                diamondShape.XForm.Width.Value = newWidth;
                diamondShape.XForm.Height.Value = newWidth * aspectRatio;

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }