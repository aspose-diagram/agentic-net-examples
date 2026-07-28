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

            // Input Visio file path (modify as needed)
            string inputPath = "input.vsdx";
            // Output Visio file path
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    bool setLandscape = false;

                    // Analyze shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve shape dimensions
                        double width = shape.XForm.Width.Value;
                        double height = shape.XForm.Height.Value;

                        // If any shape is wider than it is tall, consider the page landscape
                        if (width > height)
                        {
                            setLandscape = true;
                            break;
                        }
                    }

                    // Set page orientation based on analysis
                    if (setLandscape)
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

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
