using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Create layout options and enable page enlargement to fit drawing content
                LayoutOptions layoutOpts = new LayoutOptions
                {
                    EnlargePage = true   // enlarge page to enclose all shapes
                };

                // Apply layout with the specified options; this adjusts the page size
                page.Layout(layoutOpts);
            }

            // Prepare save options to ensure the page size matches the drawing content
            DiagramSaveOptions saveOpts = new DiagramSaveOptions
            {
                AutoFitPageToDrawingContent = true
            };

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", saveOpts);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
