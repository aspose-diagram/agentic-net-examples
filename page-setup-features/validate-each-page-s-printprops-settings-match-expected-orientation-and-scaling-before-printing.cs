using System;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram file
                string diagramPath = "input.vsdx";

                // Expected print settings
                PrintPageOrientationValue expectedOrientation = PrintPageOrientationValue.Landscape;
                double expectedScaleX = 0.75; // 75% scaling
                double expectedScaleY = 0.75; // 75% scaling

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through each page and validate its PrintProps
                foreach (Page page in diagram.Pages)
                {
                    var printProps = page.PageSheet.PrintProps;

                    // Validate orientation
                    if (printProps.PrintPageOrientation.Value != expectedOrientation)
                    {
                        throw new Exception(
                            $"Page '{page.Name}' orientation mismatch. Expected: {expectedOrientation}, Actual: {printProps.PrintPageOrientation.Value}");
                    }

                    // Validate scaling (allow a tiny tolerance for floating‑point comparison)
                    if (Math.Abs(printProps.ScaleX.Value - expectedScaleX) > 0.0001 ||
                        Math.Abs(printProps.ScaleY.Value - expectedScaleY) > 0.0001)
                    {
                        throw new Exception(
                            $"Page '{page.Name}' scaling mismatch. Expected ScaleX/Y: {expectedScaleX}, Actual ScaleX: {printProps.ScaleX.Value}, ScaleY: {printProps.ScaleY.Value}");
                    }

                    Console.WriteLine($"Page '{page.Name}' passed print settings validation.");
                }

                // Clean up
                diagram.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }