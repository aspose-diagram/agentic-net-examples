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

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vdx");

            // Set layout options to enlarge the page so it fits all drawing content
            LayoutOptions layoutOptions = new LayoutOptions
            {
                EnlargePage = true
            };

            // Apply the layout (auto‑fit) to all pages in the diagram
            diagram.Layout(layoutOptions);

            // Prepare save options that also enforce auto‑fit when saving
            DiagramSaveOptions saveOptions = new DiagramSaveOptions
            {
                AutoFitPageToDrawingContent = true
            };

            // Save the diagram with the auto‑fit layout applied
            diagram.Save("output.vdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
