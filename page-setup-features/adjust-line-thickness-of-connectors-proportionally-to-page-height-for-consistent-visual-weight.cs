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
            // Path for the modified Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the page height (in inches)
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define a proportional factor for line thickness.
                // Example: 0.001 means the line weight will be 0.1% of the page height.
                double thicknessFactor = 0.001;
                double newLineWeight = pageHeight * thicknessFactor;

                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Set the line thickness proportionally to the page height
                        shape.Line.LineWeight.Value = newLineWeight;
                    }
                }
            }

            // Save the modified diagram back to Visio format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
