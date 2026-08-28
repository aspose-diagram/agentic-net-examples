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

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Retrieve the page height (in inches)
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Define a simple scaling factor based on page height.
                // For demonstration, we set the rotation angle to (pageHeight * 5) degrees.
                double rotationAngle = pageHeight * 5.0;

                // Adjust rotation for each non-deleted shape on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set the shape's rotation angle (degrees)
                    shape.XForm.Angle.Value = rotationAngle;
                }
            }

            // Save the modified diagram back to a Visio file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
