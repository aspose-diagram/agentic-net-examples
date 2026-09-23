using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (modify as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Minimum page height in inches
                    const double minHeightInches = 5.0;

                    // Iterate through all pages and enforce the minimum height
                    foreach (Page page in diagram.Pages)
                    {
                        double currentHeight = page.PageSheet.PageProps.PageHeight.Value;
                        if (currentHeight < minHeightInches)
                        {
                            page.PageSheet.PageProps.PageHeight.Value = minHeightInches;
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Page height constraint applied and diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }