using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path after resizing
                string outputPath = "output_resized.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Check if auto‑expand (automatic drawing resize) is enabled for the page
                        if (page.PageSheet.PageProps.DrawingResizeType.Value == DrawingResizeTypeValue.Automatically)
                        {
                            // Retrieve original dimensions (in inches)
                            double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                            double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                            // Log the page information and its original size
                            Console.WriteLine($"Page ID: {page.ID}, Name: {page.Name}");
                            Console.WriteLine($"  Original Width: {originalWidth} in, Original Height: {originalHeight} in");

                            // Example resizing: increase both dimensions by 10%
                            double newWidth = originalWidth * 1.10;
                            double newHeight = originalHeight * 1.10;

                            // Apply the new dimensions
                            page.PageSheet.PageProps.PageWidth.Value = newWidth;
                            page.PageSheet.PageProps.PageHeight.Value = newHeight;

                            Console.WriteLine($"  Resized Width: {newWidth} in, Resized Height: {newHeight} in");
                        }
                    }

                    // Save the modified diagram to a new file
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}'.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }