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
            string sourcePath = @"C:\Input\diagram.vsdx";

            // Path to the output Visio file
            string outputPath = @"C:\Output\diagram_resized.vsdx";

            // Desired uniform width (in inches)
            double uniformWidth = 2.0;

            // Load the Visio document using the Diagram(string) constructor
            using (Diagram diagram = new Diagram(sourcePath))
            {
                // Iterate through all pages in the document
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Set the shape's width to the uniform value
                        shape.SetWidth(uniformWidth);
                    }
                }

                // Save the modified diagram back to a file
                // Using SaveFileFormat.Vsdx to keep the same format as the source
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
