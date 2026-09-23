using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the current page width (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

                    // Prepare layout options
                    LayoutOptions layoutOptions = new LayoutOptions
                    {
                        // Do not automatically enlarge the page during layout
                        EnlargePage = false
                    };

                    // Conditional layout based on page width
                    if (pageWidth > 11.0)
                    {
                        // For wide pages, use a compact tree layout
                        layoutOptions.LayoutStyle = LayoutStyle.CompactTree;
                        layoutOptions.Direction = LayoutDirection.DownThenRight;
                    }
                    else
                    {
                        // For narrower pages, use a flowchart layout
                        layoutOptions.LayoutStyle = LayoutStyle.FlowChart;
                        layoutOptions.Direction = LayoutDirection.TopToBottom;
                    }

                    // Apply the layout to the current page
                    page.Layout(layoutOptions);
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
