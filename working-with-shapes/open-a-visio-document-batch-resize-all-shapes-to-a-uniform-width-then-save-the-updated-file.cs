using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class VisioBatchResize
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string sourceFile = @"C:\Path\To\InputDiagram.vsdx";

            // Path where the updated Visio file will be saved
            string outputFile = @"C:\Path\To\ResizedDiagram.vsdx";

            // Desired uniform width for all shapes (in inches)
            double uniformWidth = 2.0;

            // Load the Visio document using the Diagram constructor (lifecycle rule)
            using (Diagram diagram = new Diagram(sourceFile))
            {
                // Iterate through each page in the document
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Set the shape's width to the uniform value
                        shape.SetWidth(uniformWidth);
                    }
                }

                // Save the modified document using the Save method (lifecycle rule)
                diagram.Save(outputFile, SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
