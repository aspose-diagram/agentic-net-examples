using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Assume we work with the first page
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Example translation values (in inches)
                double deltaX = 1.0; // move right by 1 inch
                double deltaY = 0.5; // move up by 0.5 inch

                // Iterate through shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Store original coordinates
                    double originalPinX = shape.XForm.PinX.Value;
                    double originalPinY = shape.XForm.PinY.Value;
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;

                    // Apply translation
                    shape.Move(deltaX, deltaY);

                    // Calculate new bounding box edges
                    double left   = shape.XForm.PinX.Value - (width / 2.0);
                    double right  = shape.XForm.PinX.Value + (width / 2.0);
                    double top    = shape.XForm.PinY.Value - (height / 2.0);
                    double bottom = shape.XForm.PinY.Value + (height / 2.0);

                    // Validate against page boundaries
                    bool withinHorizontal = left >= 0 && right <= pageWidth;
                    bool withinVertical   = top  >= 0 && bottom <= pageHeight;

                    if (!withinHorizontal || !withinVertical)
                    {
                        // Restore original position before throwing
                        shape.Move(-deltaX, -deltaY);
                        throw new Exception(
                            $"Shape ID {shape.ID} moved out of page bounds. " +
                            $"Horizontal OK: {withinHorizontal}, Vertical OK: {withinVertical}.");
                    }
                    else
                    {
                        Console.WriteLine(
                            $"Shape ID {shape.ID} successfully translated and remains within page boundaries.");
                    }
                }

                // Optionally save the modified diagram
                // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }