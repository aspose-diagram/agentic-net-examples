using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate that an output file path was provided
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <outputFilePath>");
            return;
        }

        string outputPath = args[0];

        try
        {
            // Create a new empty diagram inside a using block as required
            using (Diagram diagram = new Diagram())
            {
                // Ensure the diagram has at least one page (page index 0)
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram does not contain any pages.");
                    return;
                }

                // Access the first page (page zero)
                Page firstPage = diagram.Pages[0];

                // Configure page margins to 0.5 inches on all sides
                firstPage.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                firstPage.PageSheet.PrintProps.PageRightMargin.Value = 0.5;
                firstPage.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                firstPage.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;

                // Add a rectangle shape to the first page using DrawRectangle (pinX, pinY, width, height)
                // Coordinates are in inches
                long rectShapeId = firstPage.DrawRectangle(1.0, 1.0, 2.0, 1.0);
                Shape rectShape = firstPage.Shapes.GetShape(rectShapeId);

                // Set the shape's text by adding a Txt run to the FormatTxtCollection
                rectShape.Text.Value.Clear(); // Clear any existing text runs
                rectShape.Text.Value.Add(new Txt("Sample Rectangle"));

                // Optionally adjust the shape's size (already set by DrawRectangle, but shown for completeness)
                rectShape.XForm.Width.Value = 2.0;
                rectShape.XForm.Height.Value = 1.0;

                // Save the diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}