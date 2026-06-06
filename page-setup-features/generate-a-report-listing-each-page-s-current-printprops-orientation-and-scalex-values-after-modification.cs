using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with actual file path)
                const string inputPath = "input.vsdx";

                // Path to the output Visio file after modifications (replace with desired file path)
                const string outputPath = "output.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate over each page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Modify the print orientation to Landscape
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                        // Set the horizontal scaling factor (ScaleX) to 0.75 (75%)
                        page.PageSheet.PrintProps.ScaleX.Value = 0.75;

                        // Report the current orientation and ScaleX after modification
                        Console.WriteLine($"Page ID {page.ID}: Orientation = {page.PageSheet.PrintProps.PrintPageOrientation.Value}, ScaleX = {page.PageSheet.PrintProps.ScaleX.Value}");
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }