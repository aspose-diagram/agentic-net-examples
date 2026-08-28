using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Threshold for deciding orientation based on text length
                const int textLengthThreshold = 100;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    bool needsLandscape = false;

                    // Examine each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain text from the shape
                        string plainText = shape.Text.Value.Text ?? string.Empty;

                        // If any shape contains long text, mark the page for landscape orientation
                        if (plainText.Length > textLengthThreshold)
                        {
                            needsLandscape = true;
                            break; // No need to check further shapes on this page
                        }
                    }

                    // Set page orientation based on analysis
                    if (needsLandscape)
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Landscape;
                    }
                    else
                    {
                        page.PageSheet.PrintProps.PrintPageOrientation.Value = PrintPageOrientationValue.Portrait;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram processing completed. Saved to: " + outputPath);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
