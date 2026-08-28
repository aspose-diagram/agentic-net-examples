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

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Proportional factor for line thickness (e.g., 0.5% of page height)
                const double weightFactor = 0.005;

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page height in inches
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Calculate the line weight based on the page height
                    double lineWeight = pageHeight * weightFactor;

                    // Adjust line thickness for all connector shapes (1‑D shapes) on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.OneD) // Connector detection
                        {
                            shape.Line.LineWeight.Value = lineWeight;
                        }
                    }
                }

                // Save the updated diagram back to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
