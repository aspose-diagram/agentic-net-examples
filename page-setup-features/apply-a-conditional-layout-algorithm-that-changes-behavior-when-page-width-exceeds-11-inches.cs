using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (replace with actual paths as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the current page width (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;

                        // Prepare layout options based on the page width condition
                        LayoutOptions layoutOptions = new LayoutOptions();

                        if (pageWidth > 11.0)
                        {
                            // For wide pages (> 11 inches) use a compact tree layout without enlarging the page
                            layoutOptions.LayoutStyle = LayoutStyle.CompactTree;
                            layoutOptions.Direction = LayoutDirection.DownThenRight;
                            layoutOptions.EnlargePage = false;
                        }
                        else
                        {
                            // For narrower pages use a flowchart layout and allow the page to enlarge if needed
                            layoutOptions.LayoutStyle = LayoutStyle.FlowChart;
                            layoutOptions.Direction = LayoutDirection.TopToBottom;
                            layoutOptions.EnlargePage = true;
                        }

                        // Apply the layout to the current page
                        page.Layout(layoutOptions);
                    }

                    // Save the modified diagram back to Visio format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Layout processing completed and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }