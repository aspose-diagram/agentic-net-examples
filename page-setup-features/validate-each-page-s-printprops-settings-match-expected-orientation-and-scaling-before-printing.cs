using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string inputPath = "input.vsdx";

            // Expected print settings for every page
            PrintPageOrientationValue expectedOrientation = PrintPageOrientationValue.Landscape; // or Portrait
            double expectedScaleX = 1.0; // 100% scaling
            double expectedScaleY = 1.0; // 100% scaling

            // Load the diagram inside a using block to ensure proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the print properties of the current page
                    PrintProps printProps = page.PageSheet.PrintProps;

                    // Validate page orientation
                    if (printProps.PrintPageOrientation.Value != expectedOrientation)
                    {
                        Console.WriteLine($"[Error] Page '{page.Name}' orientation mismatch. Expected: {expectedOrientation}, Actual: {printProps.PrintPageOrientation.Value}");
                        throw new Exception("Print orientation validation failed.");
                    }

                    // Validate page scaling factors
                    double actualScaleX = printProps.ScaleX.Value;
                    double actualScaleY = printProps.ScaleY.Value;

                    if (Math.Abs(actualScaleX - expectedScaleX) > 0.0001 || Math.Abs(actualScaleY - expectedScaleY) > 0.0001)
                    {
                        Console.WriteLine($"[Error] Page '{page.Name}' scaling mismatch. Expected: {expectedScaleX}/{expectedScaleY}, Actual: {actualScaleX}/{actualScaleY}");
                        throw new Exception("Print scaling validation failed.");
                    }

                    // If both checks pass, report success for this page
                    Console.WriteLine($"Page '{page.Name}' passed print settings validation.");
                }

                // After successful validation you may print the diagram.
                // Uncomment the following line if actual printing is required.
                // diagram.Print();
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
