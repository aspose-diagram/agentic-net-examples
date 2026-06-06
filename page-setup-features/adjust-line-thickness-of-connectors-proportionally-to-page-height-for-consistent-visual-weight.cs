using System.IO;
using System;
using Aspose.Diagram;

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
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Retrieve the page height (in inches)
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define a proportion factor for line thickness (adjust as needed)
                    double factor = 0.001; // 0.1% of page height

                    // Calculate the desired line weight
                    double desiredWeight = pageHeight * factor;

                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Process only connector shapes (1‑D shapes)
                        if (shape.OneD)
                        {
                            // Set the connector's line thickness proportionally to the page height
                            shape.Line.LineWeight.Value = desiredWeight;
                        }
                    }
                }

                // Save the modified diagram back to a Visio file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Connector line thickness adjusted and saved to '{outputPath}'.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
