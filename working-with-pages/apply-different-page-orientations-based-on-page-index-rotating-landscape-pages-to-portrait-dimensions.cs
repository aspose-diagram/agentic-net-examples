using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page
                    foreach (Page page in diagram.Pages)
                    {
                        // If the page is in Landscape orientation, convert it to Portrait
                        if (page.PageSheet.PrintProps.PrintPageOrientation.Value == PrintPageOrientationValue.Landscape)
                        {
                            // Swap width and height to achieve portrait dimensions
                            double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                            double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                            page.PageSheet.PageProps.PageWidth.Value = originalHeight;
                            page.PageSheet.PageProps.PageHeight.Value = originalWidth;

                            // Update the orientation flag to Portrait
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                        }
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