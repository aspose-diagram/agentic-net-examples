using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Select the first page (index 0). Adjust the index or use GetPage(name) as needed.
                    Page page = diagram.Pages[0];

                    // Set the page orientation to Landscape
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Page orientation set to Landscape and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }