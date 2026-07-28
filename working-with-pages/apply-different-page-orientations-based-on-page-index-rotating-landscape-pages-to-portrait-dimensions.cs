using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
                    for (int i = 0; i < diagram.Pages.Count; i++)
                    {
                        Page page = diagram.Pages[i];

                        // Retrieve current page dimensions (in inches)
                        double width = page.PageSheet.PageProps.PageWidth.Value;
                        double height = page.PageSheet.PageProps.PageHeight.Value;

                        // Determine if the page is landscape (width greater than height)
                        if (width > height)
                        {
                            // Set print orientation to Portrait
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                            // Swap width and height to rotate the page dimensions
                            page.PageSheet.PageProps.PageWidth.Value = height;
                            page.PageSheet.PageProps.PageHeight.Value = width;
                        }
                        else
                        {
                            // Ensure portrait pages retain Portrait orientation
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Page orientation adjustment completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }