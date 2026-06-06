using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Path to the Visio file to be validated
            string inputPath = "input.vsdx";

            // Expected print settings
            PrintPageOrientationValue expectedOrientation = PrintPageOrientationValue.Landscape;
            double expectedScaleX = 1.0; // 100%
            double expectedScaleY = 1.0; // 100%

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and validate PrintProps
                foreach (Page page in diagram.Pages)
                {
                    var printProps = page.PageSheet.PrintProps;

                    // Validate orientation
                    if (printProps.PrintPageOrientation.Value != expectedOrientation)
                    {
                        throw new Exception(
                            $"Page '{page.Name}' orientation mismatch. Expected: {expectedOrientation}, Actual: {printProps.PrintPageOrientation.Value}");
                    }

                    // Validate scaling factors
                    if (Math.Abs(printProps.ScaleX.Value - expectedScaleX) > 0.0001 ||
                        Math.Abs(printProps.ScaleY.Value - expectedScaleY) > 0.0001)
                    {
                        throw new Exception(
                            $"Page '{page.Name}' scaling mismatch. Expected ScaleX/ScaleY: {expectedScaleX}/{expectedScaleY}, " +
                            $"Actual ScaleX/ScaleY: {printProps.ScaleX.Value}/{printProps.ScaleY.Value}");
                    }

                    Console.WriteLine($"Page '{page.Name}' passed validation.");
                }

                // Optional: proceed to print if all pages are valid
                // diagram.Print(); // Uncomment to print after successful validation
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
