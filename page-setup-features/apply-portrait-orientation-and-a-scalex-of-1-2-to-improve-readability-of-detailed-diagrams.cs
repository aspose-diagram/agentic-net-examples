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

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Apply portrait orientation and ScaleX = 1.2 to every page
                foreach (Page page in diagram.Pages)
                {
                    // Access the print properties of the page
                    var printProps = page.PageSheet.PrintProps;

                    // Set orientation to Portrait
                    printProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;

                    // Set horizontal scaling factor to 1.2 (120%)
                    printProps.ScaleX.Value = 1.2;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }