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

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page and attempt to set Landscape orientation
                    foreach (Page page in diagram.Pages)
                    {
                        try
                        {
                            // Attempt to set Landscape orientation
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                        }
                        catch (Exception ex)
                        {
                            // If any exception occurs (e.g., due to file corruption), fallback to Portrait
                            Console.WriteLine($"Failed to set Landscape on page '{page.Name}'. Falling back to Portrait. Error: {ex.Message}");
                            page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                        }
                    }

                    // Save the modified diagram
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