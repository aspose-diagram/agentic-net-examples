using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Adjust each page so its size tightly encloses all shapes
            foreach (Page page in diagram.Pages)
            {
                // LayoutOptions with EnlargePage = true tells Aspose to resize the page
                // to the bounding box of its drawing content.
                LayoutOptions layoutOptions = new LayoutOptions
                {
                    EnlargePage = true
                };

                // Apply the layout; this updates the page width/height as needed.
                page.Layout(layoutOptions);
            }

            // Configure save options to ensure the page size matches the drawing content.
            DiagramSaveOptions saveOptions = new DiagramSaveOptions
            {
                AutoFitPageToDrawingContent = true
            };

            // Save the modified diagram (uses the provided save rule)
            diagram.Save("output.vsdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
