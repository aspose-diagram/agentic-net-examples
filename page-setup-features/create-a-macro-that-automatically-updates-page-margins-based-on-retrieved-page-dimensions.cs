using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file to be processed
                string inputPath = "input.vsdx";
                // Path for the updated Visio file
                string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve page width and height (in inches)
                        double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                        double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                        // Calculate margins as 5% of the respective dimensions
                        double leftMargin = pageWidth * 0.05;
                        double rightMargin = pageWidth * 0.05;
                        double topMargin = pageHeight * 0.05;
                        double bottomMargin = pageHeight * 0.05;

                        // Update the page margins via PrintProps
                        page.PageSheet.PrintProps.PageLeftMargin.Value = leftMargin;
                        page.PageSheet.PrintProps.PageRightMargin.Value = rightMargin;
                        page.PageSheet.PrintProps.PageTopMargin.Value = topMargin;
                        page.PageSheet.PrintProps.PageBottomMargin.Value = bottomMargin;
                    }

                    // Save the updated diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Page margins have been updated and saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }