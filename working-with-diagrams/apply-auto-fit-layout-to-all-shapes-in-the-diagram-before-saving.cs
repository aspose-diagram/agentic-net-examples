using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Configure layout options (FlowChart style, top‑to‑bottom direction)
                LayoutOptions layoutOpts = new LayoutOptions
                {
                    LayoutStyle = LayoutStyle.FlowChart,
                    Direction = LayoutDirection.TopToBottom,
                    EnlargePage = false
                };

                // Apply automatic layout to all pages/shapes in the diagram
                diagram.Layout(layoutOpts);

                // Enable auto‑fit of the page to the drawing content when saving
                DiagramSaveOptions saveOpts = new DiagramSaveOptions
                {
                    AutoFitPageToDrawingContent = true
                };

                // Save the updated diagram with the auto‑fit option
                diagram.Save(outputPath, saveOpts);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }