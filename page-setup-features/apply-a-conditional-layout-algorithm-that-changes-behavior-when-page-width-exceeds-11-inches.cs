using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the current page width (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

                    // Prepare layout options based on the page width
                    LayoutOptions layoutOpts = new LayoutOptions();

                    if (pageWidth > 11.0)
                    {
                        // For wide pages (> 11 inches), use a compact tree layout without enlarging the page
                        layoutOpts.LayoutStyle = LayoutStyle.CompactTree;
                        layoutOpts.Direction = LayoutDirection.DownThenRight;
                        layoutOpts.EnlargePage = false;
                    }
                    else
                    {
                        // For narrower pages (<= 11 inches), use a flowchart layout and allow page enlargement
                        layoutOpts.LayoutStyle = LayoutStyle.FlowChart;
                        layoutOpts.Direction = LayoutDirection.TopToBottom;
                        layoutOpts.EnlargePage = true;
                    }

                    // Apply the layout to the current page
                    page.Layout(layoutOpts);
                }

                // Save the modified diagram back to a Visio file
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up resources
                diagram.Dispose();

                Console.WriteLine("Conditional layout applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }