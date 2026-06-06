using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (replace with actual paths as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through all pages and set print orientation to Portrait
                    foreach (Page page in diagram.Pages)
                    {
                        // Access the PrintProps of the page sheet and set orientation
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Print orientation set to Portrait for all pages and saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }