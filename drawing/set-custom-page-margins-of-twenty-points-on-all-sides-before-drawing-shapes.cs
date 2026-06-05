using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new diagram instance
                using (Diagram diagram = new Diagram())
                {
                    // Access the first (default) page
                    Page page = diagram.Pages[0];

                    // Define margin in points and convert to inches (1 point = 1/72 inch)
                    double marginPoints = 20.0;
                    double marginInches = marginPoints / 72.0;

                    // Set all four page margins using PrintProps (values are in inches)
                    page.PageSheet.PrintProps.PageTopMargin.Value = marginInches;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = marginInches;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = marginInches;
                    page.PageSheet.PrintProps.PageRightMargin.Value = marginInches;

                    // Example shape addition after margins are set
                    // Add a rectangle master shape at position (2,2) inches; 'false' means no geometry recalculation needed now
                    long shapeId = page.AddShape(2.0, 2.0, "Rectangle", false);
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Set simple text for the shape
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Sample Shape"));

                    // Save the diagram to a VSDX file
                    diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }