using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Reset each page to Portrait orientation and ScaleX = 1.0
                foreach (Page page in diagram.Pages)
                {
                    // Set page orientation to Portrait
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                    // Reset scaling factors
                    page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                    page.PageSheet.PrintProps.ScaleY.Value = 1.0;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
