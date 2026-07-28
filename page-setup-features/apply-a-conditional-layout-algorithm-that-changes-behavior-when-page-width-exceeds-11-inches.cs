using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the current page width (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

                        // Prepare layout options based on page width
                        LayoutOptions layoutOpts = new LayoutOptions();

                        if (pageWidth > 11.0)
                        {
                            // For wide pages (>11 inches), use a compact tree layout
                            layoutOpts.LayoutStyle = LayoutStyle.CompactTree;
                            layoutOpts.Direction = LayoutDirection.DownThenRight;
                            layoutOpts.EnlargePage = false; // Keep original page size
                        }
                        else
                        {
                            // For narrower pages, use a flowchart layout
                            layoutOpts.LayoutStyle = LayoutStyle.FlowChart;
                            layoutOpts.Direction = LayoutDirection.TopToBottom;
                            layoutOpts.EnlargePage = true; // Allow page to grow if needed
                        }

                        // Apply the layout to the current page
                        page.Layout(layoutOpts);
                    }

                    // Save the modified diagram back to Visio format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Conditional layout applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }