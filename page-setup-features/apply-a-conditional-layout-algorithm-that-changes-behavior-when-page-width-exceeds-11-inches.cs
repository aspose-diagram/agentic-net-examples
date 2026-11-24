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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page width in inches.
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

                    // Prepare layout options based on the page width.
                    LayoutOptions layoutOptions = new LayoutOptions();

                    if (pageWidth > 11.0)
                    {
                        // For wide pages (> 11 inches) use a FlowChart layout and enlarge the page.
                        layoutOptions.LayoutStyle = LayoutStyle.FlowChart;
                        layoutOptions.Direction = LayoutDirection.TopToBottom;
                        layoutOptions.EnlargePage = true; // EnlargePage is a bool, not BOOL.
                    }
                    else
                    {
                        // For narrower pages use a CompactTree layout without enlarging the page.
                        layoutOptions.LayoutStyle = LayoutStyle.CompactTree;
                        layoutOptions.Direction = LayoutDirection.DownThenRight;
                        layoutOptions.EnlargePage = false;
                    }

                    // Apply the layout to the current page.
                    page.Layout(layoutOptions);
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}