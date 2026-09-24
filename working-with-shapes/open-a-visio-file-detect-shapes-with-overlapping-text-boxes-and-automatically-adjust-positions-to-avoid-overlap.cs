using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output Visio file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Process each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Collect non-deleted, non-connector shapes for overlap checking
                List<Shape> shapes = new List<Shape>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False && shape.OneD == false)
                    {
                        shapes.Add(shape);
                    }
                }

                // Margin to keep between shapes (in inches)
                double margin = 0.2;

                // Compare each pair of shapes for overlap
                for (int i = 0; i < shapes.Count; i++)
                {
                    Shape s1 = shapes[i];
                    double s1Left   = s1.XForm.PinX.Value - s1.XForm.Width.Value  / 2.0;
                    double s1Right  = s1.XForm.PinX.Value + s1.XForm.Width.Value  / 2.0;
                    double s1Top    = s1.XForm.PinY.Value + s1.XForm.Height.Value / 2.0;
                    double s1Bottom = s1.XForm.PinY.Value - s1.XForm.Height.Value / 2.0;

                    for (int j = i + 1; j < shapes.Count; j++)
                    {
                        Shape s2 = shapes[j];
                        double s2Left   = s2.XForm.PinX.Value - s2.XForm.Width.Value  / 2.0;
                        double s2Right  = s2.XForm.PinX.Value + s2.XForm.Width.Value  / 2.0;
                        double s2Top    = s2.XForm.PinY.Value + s2.XForm.Height.Value / 2.0;
                        double s2Bottom = s2.XForm.PinY.Value - s2.XForm.Height.Value / 2.0;

                        // Determine if the two rectangles intersect (including margin)
                        bool overlap = !(s1Right + margin < s2Left ||
                                         s2Right + margin < s1Left ||
                                         s1Top   + margin < s2Bottom ||
                                         s2Top   + margin < s1Bottom);

                        if (overlap)
                        {
                            // Shift the second shape to the right to resolve overlap
                            double shift = (s1.XForm.Width.Value + s2.XForm.Width.Value) / 2.0 + margin;
                            s2.XForm.PinX.Value += shift;

                            // Update the cached rectangle for further comparisons
                            s2Left   = s2.XForm.PinX.Value - s2.XForm.Width.Value  / 2.0;
                            s2Right  = s2.XForm.PinX.Value + s2.XForm.Width.Value  / 2.0;
                        }
                    }
                }
            }

            // Save the adjusted diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
