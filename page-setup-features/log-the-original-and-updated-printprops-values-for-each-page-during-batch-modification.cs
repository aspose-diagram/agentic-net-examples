using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the modified output file
            string outputPath = "ModifiedDiagram.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Capture original PrintProps values
                    PrintPageOrientationValue originalOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;
                    double originalScaleX = page.PageSheet.PrintProps.ScaleX.Value;
                    double originalScaleY = page.PageSheet.PrintProps.ScaleY.Value;
                    double originalTopMargin = page.PageSheet.PrintProps.PageTopMargin.Value;
                    double originalBottomMargin = page.PageSheet.PrintProps.PageBottomMargin.Value;
                    double originalLeftMargin = page.PageSheet.PrintProps.PageLeftMargin.Value;
                    double originalRightMargin = page.PageSheet.PrintProps.PageRightMargin.Value;

                    // Log original values
                    Console.WriteLine($"Page ID {page.ID} - Original PrintProps:");
                    Console.WriteLine($"  Orientation: {originalOrientation}");
                    Console.WriteLine($"  ScaleX: {originalScaleX}");
                    Console.WriteLine($"  ScaleY: {originalScaleY}");
                    Console.WriteLine($"  TopMargin: {originalTopMargin}");
                    Console.WriteLine($"  BottomMargin: {originalBottomMargin}");
                    Console.WriteLine($"  LeftMargin: {originalLeftMargin}");
                    Console.WriteLine($"  RightMargin: {originalRightMargin}");

                    // Modify PrintProps
                    page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    page.PageSheet.PrintProps.ScaleX.Value = 0.75;
                    page.PageSheet.PrintProps.ScaleY.Value = 0.75;
                    page.PageSheet.PrintProps.PageTopMargin.Value = 0.5;
                    page.PageSheet.PrintProps.PageBottomMargin.Value = 0.5;
                    page.PageSheet.PrintProps.PageLeftMargin.Value = 0.5;
                    page.PageSheet.PrintProps.PageRightMargin.Value = 0.5;
                    page.PageSheet.PrintProps.OnPage.Value = BOOL.True; // Ensure page is set to print

                    // Capture updated PrintProps values
                    PrintPageOrientationValue updatedOrientation = page.PageSheet.PrintProps.PrintPageOrientation.Value;
                    double updatedScaleX = page.PageSheet.PrintProps.ScaleX.Value;
                    double updatedScaleY = page.PageSheet.PrintProps.ScaleY.Value;
                    double updatedTopMargin = page.PageSheet.PrintProps.PageTopMargin.Value;
                    double updatedBottomMargin = page.PageSheet.PrintProps.PageBottomMargin.Value;
                    double updatedLeftMargin = page.PageSheet.PrintProps.PageLeftMargin.Value;
                    double updatedRightMargin = page.PageSheet.PrintProps.PageRightMargin.Value;

                    // Log updated values
                    Console.WriteLine($"Page ID {page.ID} - Updated PrintProps:");
                    Console.WriteLine($"  Orientation: {updatedOrientation}");
                    Console.WriteLine($"  ScaleX: {updatedScaleX}");
                    Console.WriteLine($"  ScaleY: {updatedScaleY}");
                    Console.WriteLine($"  TopMargin: {updatedTopMargin}");
                    Console.WriteLine($"  BottomMargin: {updatedBottomMargin}");
                    Console.WriteLine($"  LeftMargin: {updatedLeftMargin}");
                    Console.WriteLine($"  RightMargin: {updatedRightMargin}");
                    Console.WriteLine(); // Blank line for readability
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
