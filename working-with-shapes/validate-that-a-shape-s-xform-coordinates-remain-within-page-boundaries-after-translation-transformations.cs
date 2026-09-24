using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file to be processed
                string diagramPath = "input.vsdx";

                // Translation offsets (in inches) to apply to each shape
                double deltaX = 0.5; // move right by 0.5 inch
                double deltaY = 0.2; // move up by 0.2 inch

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Flag to track if any shape exceeds page boundaries after translation
                bool anyOutOfBounds = false;

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Apply translation to the shape's position
                        shape.XForm.PinX.Value += deltaX;
                        shape.XForm.PinY.Value += deltaY;

                        // Calculate shape boundaries based on its center (PinX/PinY) and size
                        double halfWidth = shape.XForm.Width.Value / 2.0;
                        double halfHeight = shape.XForm.Height.Value / 2.0;

                        double left   = shape.XForm.PinX.Value - halfWidth;
                        double right  = shape.XForm.PinX.Value + halfWidth;
                        double bottom = shape.XForm.PinY.Value - halfHeight;
                        double top    = shape.XForm.PinY.Value + halfHeight;

                        // Validate that the shape stays within the page limits
                        if (left < 0 || right > pageWidth || bottom < 0 || top > pageHeight)
                        {
                            anyOutOfBounds = true;
                            Console.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" is out of bounds after translation:");
                            Console.WriteLine($"  Left: {left:F3} (min 0), Right: {right:F3} (max {pageWidth:F3})");
                            Console.WriteLine($"  Bottom: {bottom:F3} (min 0), Top: {top:F3} (max {pageHeight:F3})");
                        }
                    }
                }

                if (anyOutOfBounds)
                {
                    throw new Exception("One or more shapes exceed page boundaries after translation.");
                }
                else
                {
                    Console.WriteLine("All shapes remain within page boundaries after translation.");
                }

                // Optional: save the modified diagram (uncomment if needed)
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }