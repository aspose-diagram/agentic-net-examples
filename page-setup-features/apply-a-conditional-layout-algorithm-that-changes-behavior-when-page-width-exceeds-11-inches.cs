using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page and apply a conditional layout based on page width.
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the page width in inches.
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

                // Create layout options for the current page.
                LayoutOptions layoutOptions = new LayoutOptions();

                if (pageWidth > 11.0)
                {
                    // Wide pages (>11 inches): use FlowChart style, top‑to‑bottom direction,
                    // and allow the page to enlarge automatically.
                    layoutOptions.LayoutStyle = LayoutStyle.FlowChart;
                    layoutOptions.Direction = LayoutDirection.TopToBottom;
                    layoutOptions.EnlargePage = true; // EnlargePage expects a bool, not BOOL.
                }
                else
                {
                    // Narrow pages (≤11 inches): use CompactTree style, down‑then‑right direction,
                    // and keep the page size unchanged.
                    layoutOptions.LayoutStyle = LayoutStyle.CompactTree;
                    layoutOptions.Direction = LayoutDirection.DownThenRight;
                    layoutOptions.EnlargePage = false; // EnlargePage expects a bool.
                }

                // Apply the layout to the current page.
                page.Layout(layoutOptions);
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}