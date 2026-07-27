using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            // Initialize a new empty diagram inside a using block for proper disposal
            using (Diagram diagram = new Diagram())
            {
                // Convert 20 points to inches (Visio uses inches for margins)
                double marginInInches = 20.0 / 72.0;

                // Apply the same margins to every existing page (none at creation, but loop is safe)
                foreach (Page page in diagram.Pages)
                {
                    // Set top, bottom, left, and right margins via the PrintProps collection
                    page.PageSheet.PrintProps.PageTopMargin.Value = marginInInches;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = marginInInches;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = marginInInches;
                    page.PageSheet.PrintProps.PageRightMargin.Value = marginInInches;
                }

                // Add a rectangle shape to the first page; fourth argument is a bool (isCalculate)
                long shapeId = diagram.Pages[0].AddShape(2.0, 2.0, "Rectangle", false);
                // Retrieve the shape object using the returned ID
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);
                // Clear any existing text and add new sample text
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Sample Shape"));

                // Save the diagram to a VSDX file
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Output any errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}