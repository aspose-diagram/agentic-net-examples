using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input file path (first argument) and expected orientation (second argument)
                string filePath = args.Length > 0 ? args[0] : "input.vsdx";
                string expectedStr = args.Length > 1 ? args[1] : "Landscape";

                // Parse the expected orientation string to the enum value
                if (!Enum.TryParse(expectedStr, out PrintPageOrientationValue expectedOrientation))
                {
                    Console.WriteLine($"Invalid expected orientation '{expectedStr}'. Use Landscape, Portrait, or SameAsPrinter.");
                    return;
                }

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all pages in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Retrieve the actual orientation from the page's PrintProps
                        PrintPageOrientationValue actualOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;

                        // Compare with the expected value
                        if (actualOrientation != expectedOrientation)
                        {
                            // Throw an exception on mismatch
                            throw new Exception($"Page '{page.Name}' orientation mismatch. Expected: {expectedOrientation}, Actual: {actualOrientation}.");
                        }
                        else
                        {
                            // Log success for each page
                            Console.WriteLine($"Page '{page.Name}' orientation matches expected value: {expectedOrientation}.");
                        }
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }