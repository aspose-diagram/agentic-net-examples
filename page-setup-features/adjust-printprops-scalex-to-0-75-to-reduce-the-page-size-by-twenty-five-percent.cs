using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and set the horizontal print scaling to 75%
                foreach (Page page in diagram.Pages)
                {
                    // Access the PrintProps via the PageSheet and assign the new scale value
                    page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                }

                // Save the modified diagram back to a Visio file (replace with your desired output path)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up resources
                diagram.Dispose();

                Console.WriteLine("Print scaling applied and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }