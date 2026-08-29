using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (replace with actual paths as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_with_watermark.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page dimensions (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Retrieve printable margins (in inches)
                        double leftMargin = page.PageSheet.PrintProps.PageLeftMargin.Value;
                        double rightMargin = page.PageSheet.PrintProps.PageRightMargin.Value;
                        double topMargin = page.PageSheet.PrintProps.PageTopMargin.Value;
                        double bottomMargin = page.PageSheet.PrintProps.PageBottomMargin.Value;

                        // Calculate printable area dimensions
                        double printableWidth = pageWidth - leftMargin - rightMargin;
                        double printableHeight = pageHeight - topMargin - bottomMargin;

                        // Determine the center point of the printable area
                        double pinX = leftMargin + printableWidth / 2.0;
                        double pinY = bottomMargin + printableHeight / 2.0;

                        // Add watermark text that fits within the printable area
                        // Font size is specified in inches (e.g., 0.5 inches ≈ 36 points)
                        Shape watermark = page.AddText(
                            pinX,
                            pinY,
                            printableWidth,
                            printableHeight,
                            "CONFIDENTIAL",
                            "Calibri",
                            "#a5a5a5",
                            0.5);

                        // Optional: rotate the watermark 45 degrees (angle in radians)
                        // watermark.TextXForm.TxtAngle.Value = Math.PI / 4;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Watermark applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }