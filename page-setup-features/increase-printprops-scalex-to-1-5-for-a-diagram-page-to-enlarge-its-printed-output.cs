using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Printing;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_scaled.vsdx";

                try
                {
                    // Load the Visio diagram
                    Diagram diagram = new Diagram(inputPath);

                    // Iterate through all pages (or target a specific page)
                    foreach (Page page in diagram.Pages)
                    {
                        // Set the horizontal print scaling factor to 1.5 (150%)
                        page.PageSheet.PrintProps.ScaleX.Value = 1.5;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Diagram saved with updated PrintProps.ScaleX.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }