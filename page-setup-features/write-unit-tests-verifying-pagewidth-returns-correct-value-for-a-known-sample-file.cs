using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the sample Visio file (ensure the file exists at this location)
                const string filePath = "sample.vsdx";

                // Expected page width in inches for the known sample file
                const double expectedWidth = 8.5; // adjust to the actual expected value

                // Load the diagram inside a using block to ensure proper disposal
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Retrieve the first page (index 0)
                    Page page = diagram.Pages[0];

                    // Access the page width value (in inches)
                    double actualWidth = page.PageSheet.PageProps.PageWidth.Value;

                    // Verify the width matches the expected value
                    // Use a tolerance to account for floating‑point precision
                    const double tolerance = 0.001;
                    if (Math.Abs(actualWidth - expectedWidth) > tolerance)
                    {
                        throw new Exception($"Page width verification failed. Expected: {expectedWidth} inches, Actual: {actualWidth} inches.");
                    }
                    else
                    {
                        Console.WriteLine($"Page width verification succeeded. Width: {actualWidth} inches.");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }