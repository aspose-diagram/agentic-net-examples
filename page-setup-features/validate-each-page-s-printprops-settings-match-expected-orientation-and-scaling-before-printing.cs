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

            // Path to the Visio file to validate
            string filePath = "input.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(filePath))
            {
                // Define expected print settings
                PrintPageOrientationValue expectedOrientation = PrintPageOrientationValue.Landscape;
                double expectedScaleX = 1.0;
                double expectedScaleY = 1.0;

                // Iterate through each page and validate its PrintProps
                foreach (Page page in diagram.Pages)
                {
                    PrintProps printProps = page.PageSheet.PrintProps;

                    // Validate orientation
                    if (printProps.PrintPageOrientation.Value != expectedOrientation)
                    {
                        throw new Exception($"Page '{page.Name}' orientation mismatch. Expected: {expectedOrientation}, Actual: {printProps.PrintPageOrientation.Value}");
                    }

                    // Validate scaling factors
                    double actualScaleX = printProps.ScaleX.Value;
                    double actualScaleY = printProps.ScaleY.Value;

                    if (Math.Abs(actualScaleX - expectedScaleX) > 0.0001 || Math.Abs(actualScaleY - expectedScaleY) > 0.0001)
                    {
                        throw new Exception($"Page '{page.Name}' scaling mismatch. Expected: {expectedScaleX}/{expectedScaleY}, Actual: {actualScaleX}/{actualScaleY}");
                    }

                    Console.WriteLine($"Page '{page.Name}' passed PrintProps validation.");
                }

                // At this point all pages have the expected print settings and can be printed.
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
