using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the modified Visio file
                string outputPath = "output_landscape.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Retrieve the first page (index 0)
                    Page page = diagram.Pages[0];

                    // Set page orientation to Landscape
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // Adjust page dimensions (example: 11 inches width, 8.5 inches height for landscape)
                    page.PageSheet.PageProps.PageWidth.Value = 11.0;   // Width in inches
                    page.PageSheet.PageProps.PageHeight.Value = 8.5;  // Height in inches

                    // Save the modified diagram using a valid SaveFileFormat enum value
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram orientation set to landscape and saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }