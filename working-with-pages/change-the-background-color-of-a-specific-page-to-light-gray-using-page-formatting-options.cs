using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, target page name
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <input.vsdx> <output.vsdx> <pageName>");
            return;
        }

        // Assign input file path and verify existence
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign output file path (no existence check needed)
        string outputPath = args[1];

        // Assign target page name
        string targetPageName = args[2];

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the page that needs its background changed
            Page targetPage = diagram.Pages.GetPage(targetPageName);
            if (targetPage == null)
            {
                Console.Error.WriteLine($"Page not found: {targetPageName}");
                return;
            }

            // Create a new background page
            Page backgroundPage = new Page();

            // Mark the new page as a background page
            backgroundPage.Background = BOOL.True;

            // Copy dimensions from the target page to the background page
            double pageWidth = targetPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = targetPage.PageSheet.PageProps.PageHeight.Value;
            backgroundPage.PageSheet.PageProps.PageWidth.Value = pageWidth;
            backgroundPage.PageSheet.PageProps.PageHeight.Value = pageHeight;

            // Calculate the center point for the rectangle shape (pinX, pinY)
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;

            // Add a rectangle shape that spans the entire page
            long rectShapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", false);

            // Retrieve the newly added shape to set its formatting
            Shape rectShape = backgroundPage.Shapes.GetShape(rectShapeId);

            // Set fill pattern to solid (1) and fill color to light gray (#D3D3D3)
            rectShape.Fill.FillPattern.Value = 1;
            rectShape.Fill.FillForegnd.Value = "#D3D3D3";

            // Remove any border by setting line pattern to 0 (no line)
            rectShape.Line.LinePattern.Value = 0;

            // Send the rectangle to the back so other shapes appear above it
            rectShape.SendToBack();

            // Add the background page to the diagram's page collection
            diagram.Pages.Add(backgroundPage);

            // Link the target page to the newly created background page
            targetPage.BackPage = backgroundPage;

            // Save the modified diagram to the output file in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Output any unexpected errors to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}