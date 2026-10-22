using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Apply an automatic layout to all shapes in the diagram.
                var layoutOptions = new LayoutOptions
                {
                    LayoutStyle = LayoutStyle.CompactTree,   // compact tree layout
                    Direction = LayoutDirection.DownThenRight, // direction of layout
                    EnlargePage = false                     // do not enlarge the page automatically
                };
                diagram.Layout(layoutOptions);

                // Create save options and enable auto‑fit of the page to the drawing content.
                DiagramSaveOptions saveOptions = new DiagramSaveOptions();
                saveOptions.AutoFitPageToDrawingContent = true; // use native bool, not BOOL enum

                // Save the modified diagram with the auto‑fit option applied.
                diagram.Save(outputPath, saveOptions);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
