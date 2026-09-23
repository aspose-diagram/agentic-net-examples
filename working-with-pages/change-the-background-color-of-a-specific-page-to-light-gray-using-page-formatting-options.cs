using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Specify the page to modify (e.g., the first page)
            Page targetPage = diagram.Pages[0];

            // Determine the maximum existing page ID to assign a unique ID to the new background page
            int maxPageId = 0;
            foreach (Page p in diagram.Pages)
            {
                if (p.ID > maxPageId)
                    maxPageId = p.ID;
            }

            // Create a new background page
            Page backgroundPage = new Page();
            backgroundPage.ID = maxPageId + 1;
            backgroundPage.Name = "BackgroundPage";
            backgroundPage.Background = BOOL.True; // Mark as a background page

            // Add the background page to the diagram
            diagram.Pages.Add(backgroundPage);

            // Retrieve page dimensions (in inches)
            double pageWidth = targetPage.PageSheet.PageProps.PageWidth.Value;
            double pageHeight = targetPage.PageSheet.PageProps.PageHeight.Value;

            // Add a rectangle shape that spans the entire page on the background page
            // Parameters: pinX, pinY (center of shape), width, height, master name, isCalculate flag
            double pinX = pageWidth / 2.0;
            double pinY = pageHeight / 2.0;
            long shapeId = backgroundPage.AddShape(pinX, pinY, pageWidth, pageHeight, "Rectangle", false);

            // Retrieve the shape object
            Shape bgShape = backgroundPage.Shapes.GetShape(shapeId);

            // Set fill to solid light gray (#D3D3D3)
            bgShape.Fill.FillPattern.Value = 1; // Solid fill
            bgShape.Fill.FillForegnd.Value = "#D3D3D3";

            // Remove border by setting line weight to zero
            bgShape.Line.LineWeight.Value = 0.0;
            bgShape.Line.LinePattern.Value = LinePatternValue.Solid;

            // Send the shape to the back so it appears behind all other content
            bgShape.SendToBack();

            // Link the foreground page to the new background page
            targetPage.BackPage = backgroundPage;

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
