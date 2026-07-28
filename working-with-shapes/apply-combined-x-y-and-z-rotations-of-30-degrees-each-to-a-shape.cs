using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Ensure there is at least one page and one shape
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The diagram contains no pages.");
                }

                Page page = diagram.Pages[0];

                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    targetShape = shape;
                    break; // take the first shape found
                }

                if (targetShape == null)
                {
                    throw new Exception("No shape found on the first page.");
                }

                // Apply combined rotations of 30 degrees around X, Y, and Z axes
                targetShape.ThreeDFormat.RotationXAngle.Value = 30.0;
                targetShape.ThreeDFormat.RotationYAngle.Value = 30.0;
                targetShape.ThreeDFormat.RotationZAngle.Value = 30.0;

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }