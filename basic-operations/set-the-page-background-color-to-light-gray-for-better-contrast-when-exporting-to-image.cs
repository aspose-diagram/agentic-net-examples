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

            // Load an existing Visio diagram
            using (Diagram diagram = new Diagram("input.vsdx"))
            {
                // Create a background page
                Page backgroundPage = new Page();
                backgroundPage.Name = "BackgroundPage";
                backgroundPage.Background = BOOL.True; // Mark as background page

                // Retrieve page dimensions from the first foreground page (assumed to exist)
                Page firstPage = diagram.Pages[0];
                double pageWidth = firstPage.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = firstPage.PageSheet.PageProps.PageHeight.Value;

                // Add a rectangle shape that covers the entire page
                // Parameters: pinX, pinY (center), width, height, master name, isCalculate
                long bgShapeId = backgroundPage.AddShape(pageWidth / 2, pageHeight / 2, pageWidth, pageHeight, "Rectangle", false);
                Shape bgShape = backgroundPage.Shapes.GetShape(bgShapeId);

                // Set solid fill pattern and light gray color (#D3D3D3)
                bgShape.Fill.FillPattern.Value = 1;               // Solid fill
                bgShape.Fill.FillForegnd.Value = "#D3D3D3";       // Light gray
                // Remove outline
                bgShape.Line.LinePattern.Value = 0;               // No line

                // Send the shape to back and lock it from selection
                bgShape.SendToBack();
                bgShape.Protection.LockSelect.Value = BOOL.True;

                // Add the background page to the diagram
                diagram.Pages.Add(backgroundPage);

                // Link each foreground page to the new background page
                foreach (Page page in diagram.Pages)
                {
                    if (page.Background == BOOL.False) // Skip the background page itself
                    {
                        page.BackPage = backgroundPage;
                    }
                }

                // Export the first page to PNG with the background applied
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.PageIndex = 0; // Export the first page
                diagram.Save("output.png", saveOptions);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
