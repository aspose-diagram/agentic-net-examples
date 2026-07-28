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

            // Load the existing Visio diagram
            Diagram diagram = new Diagram("input.vdx");

            // Configure layout options to enlarge the page so that all shapes fit
            LayoutOptions layoutOptions = new LayoutOptions
            {
                EnlargePage = true
            };

            // Apply the layout (auto‑fit) to all pages of the diagram
            diagram.Layout(layoutOptions);

            // Prepare save options that also enforce auto‑fit when saving
            DiagramSaveOptions saveOptions = new DiagramSaveOptions
            {
                AutoFitPageToDrawingContent = true
            };

            // Save the diagram with the specified options
            diagram.Save("output.vdx", saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
