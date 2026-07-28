using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file (replace with an actual file path)
                string inputPath = "input.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Access the first page
                    Page page = diagram.Pages[0];

                    // Store original page dimensions (in inches)
                    double originalWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double originalHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Apply a scaling factor (e.g., 50%)
                    page.PageSheet.PrintProps.ScaleX.Value = 0.5;
                    page.PageSheet.PrintProps.ScaleY.Value = 0.5;

                    // Save the diagram temporarily (optional, demonstrates save workflow)
                    diagram.Save("temp_output.vsdx", SaveFileFormat.Vsdx);

                    // Reset scaling back to original (100%)
                    page.PageSheet.PrintProps.ScaleX.Value = 1.0;
                    page.PageSheet.PrintProps.ScaleY.Value = 1.0;

                    // Validation: ScaleX should be 1.0
                    if (page.PageSheet.PrintProps.ScaleX.Value != 1.0)
                    {
                        throw new Exception("ScaleX was not reset to 1.0.");
                    }

                    // Validation: Page dimensions should remain unchanged
                    double currentWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double currentHeight = page.PageSheet.PageProps.PageHeight.Value;

                    const double tolerance = 0.0001; // tolerance for floating‑point comparison

                    if (Math.Abs(currentWidth - originalWidth) > tolerance)
                    {
                        throw new Exception("Page width changed after resetting ScaleX.");
                    }

                    if (Math.Abs(currentHeight - originalHeight) > tolerance)
                    {
                        throw new Exception("Page height changed after resetting ScaleX.");
                    }

                    Console.WriteLine("ScaleX reset validation succeeded. Page dimensions are unchanged.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }