using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;
using Aspose.Diagram.Manipulation; // for ConnectionPointPlace if needed in future

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        try
        {
            // Load the existing Visio diagram (Diagram does not implement IDisposable)
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Auto‑space shapes to create uniform gaps
                AutoSpaceOptions autoSpace = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 0.5, // inches
                    DistanceInVertical = 0.5    // inches
                };
                page.AutoSpaceShapes(page.Shapes, autoSpace);

                // Re‑calculate connector routing after spacing
                LayoutOptions layoutOpts = new LayoutOptions
                {
                    LayoutStyle = LayoutStyle.FlowChart,
                    Direction = LayoutDirection.TopToBottom,
                    EnlargePage = false // EnlargePage expects a bool, not BOOL
                };
                page.Layout(layoutOpts);
            }

            // Save the updated diagram using a valid overload (second argument is SaveFileFormat)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}