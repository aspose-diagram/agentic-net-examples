using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the sample Visio file (ensure this file exists in the execution directory)
                string samplePath = "sample.vsdx";

                // Expected page height in inches for the known sample file
                double expectedHeight = 11.0; // adjust this value to match the actual sample

                // Load the diagram using the Aspose.Diagram constructor
                using (Diagram diagram = new Diagram(samplePath))
                {
                    // Retrieve the first page (index 0)
                    Page page = diagram.Pages[0];

                    // Access the page height value (in inches)
                    double actualHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Output the retrieved height for diagnostic purposes
                    Console.WriteLine($"Actual page height: {actualHeight} inches");

                    // Verify that the actual height matches the expected height
                    // Use a tolerance to account for floating‑point precision
                    double tolerance = 0.0001;
                    if (Math.Abs(actualHeight - expectedHeight) > tolerance)
                    {
                        throw new Exception($"Page height verification failed. Expected: {expectedHeight}, Actual: {actualHeight}");
                    }
                    else
                    {
                        Console.WriteLine("Page height verification passed.");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }