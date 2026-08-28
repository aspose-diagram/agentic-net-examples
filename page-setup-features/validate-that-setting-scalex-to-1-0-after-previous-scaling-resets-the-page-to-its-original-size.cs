using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with an actual file path)
                const string inputPath = "sample.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Retrieve the PrintProps cell collection
                var printProps = page.PageSheet.PrintProps;

                // Store the original ScaleX value (should be 1.0 for a new diagram)
                double originalScaleX = printProps.ScaleX.Value;

                // Apply a scaling factor (e.g., 75%)
                printProps.ScaleX.Value = 0.75;
                printProps.ScaleY.Value = 0.75;

                // Verify that scaling was applied
                if (Math.Abs(printProps.ScaleX.Value - 0.75) > 0.0001 ||
                    Math.Abs(printProps.ScaleY.Value - 0.75) > 0.0001)
                {
                    throw new Exception("Initial scaling to 0.75 failed.");
                }

                // Reset ScaleX (and ScaleY) back to 1.0
                printProps.ScaleX.Value = 1.0;
                printProps.ScaleY.Value = 1.0;

                // Validate that ScaleX has returned to its original value
                if (Math.Abs(printProps.ScaleX.Value - originalScaleX) > 0.0001)
                {
                    throw new Exception("ScaleX reset did not return to the original value.");
                }

                // Additional check: ensure ScaleY is also reset to 1.0
                if (Math.Abs(printProps.ScaleY.Value - 1.0) > 0.0001)
                {
                    throw new Exception("ScaleY reset did not return to 1.0.");
                }

                Console.WriteLine("ScaleX reset validation succeeded. Page is back to its original size.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }