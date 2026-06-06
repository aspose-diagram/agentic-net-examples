using System;
using Aspose.Diagram;

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
                    // Iterate through all pages and reset orientation and scale
                    foreach (Page page in diagram.Pages)
                    {
                        // Access the print properties of the page
                        var printProps = page.PageSheet.PrintProps;

                        // Set orientation to Portrait
                        printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                        // Reset horizontal scaling factor to 1.0 (100%)
                        printProps.ScaleX.Value = 1.0;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("All pages have been reset to Portrait orientation with ScaleX = 1.0.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }