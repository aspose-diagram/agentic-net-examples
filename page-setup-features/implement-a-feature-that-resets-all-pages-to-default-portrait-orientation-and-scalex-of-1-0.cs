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

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages and reset orientation and scale
                    foreach (Page page in diagram.Pages)
                    {
                        // Set page orientation to Portrait
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                        // Reset horizontal scaling factor to 1.0 (100%)
                        page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved to '{outputPath}' with all pages set to Portrait orientation and ScaleX = 1.0.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }