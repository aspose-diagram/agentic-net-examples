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
            // Path to the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page height (in inches)
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define a base line weight (in inches) for a reference page height (e.g., 11 inches)
                    const double referenceHeight = 11.0; // typical A4 height in inches
                    const double baseLineWeight = 0.02; // 0.02 inches for the reference height

                    // Compute the proportional line weight for the current page
                    double proportionalWeight = baseLineWeight * (pageHeight / referenceHeight);

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Apply the calculated line weight
                            shape.Line.LineWeight.Value = proportionalWeight;
                        }
                    }
                }

                // Save the modified diagram back to a Visio file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
