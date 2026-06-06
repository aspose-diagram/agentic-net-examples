using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – replace with actual file locations as needed
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Store original page dimensions
                double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Store original scaling factor (should be 1.0 for a new document)
                double originalScaleX = page.PageSheet.PrintProps.ScaleX.Value;

                // Apply a different scaling factor
                page.PageSheet.PrintProps.ScaleX.Value = 0.5;

                // Reset scaling back to 1.0
                page.PageSheet.PrintProps.ScaleX.Value = 1.0;

                // Validation: ScaleX must be exactly 1.0
                if (page.PageSheet.PrintProps.ScaleX.Value != 1.0)
                    throw new Exception($"ScaleX reset failed. Current value: {page.PageSheet.PrintProps.ScaleX.Value}");

                // Validation: Page dimensions must remain unchanged
                if (page.PageSheet.PageProps.PageWidth.Value != originalWidth ||
                    page.PageSheet.PageProps.PageHeight.Value != originalHeight)
                    throw new Exception("Page dimensions changed after resetting ScaleX.");

                // Optional: Verify that the original scaling factor (if it was not 1.0) is restored
                if (originalScaleX != 1.0 && page.PageSheet.PrintProps.ScaleX.Value != originalScaleX)
                    throw new Exception("ScaleX did not return to its original value.");

                // Save the diagram to confirm no errors during save
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("ScaleX reset validation completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }