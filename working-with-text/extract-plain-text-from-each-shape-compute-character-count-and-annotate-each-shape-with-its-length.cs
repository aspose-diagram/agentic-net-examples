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
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain (unformatted) text of the shape
                    string plainText = shape.GetPureText();

                    // Process only shapes that contain text
                    if (!string.IsNullOrEmpty(plainText))
                    {
                        // Compute character count
                        int charCount = plainText.Length;

                        // Prepare annotation text
                        string annotation = $"Length: {charCount}";

                        // Use the shape's position as a reference point
                        double pinX = shape.XForm.PinX.Value;
                        double pinY = shape.XForm.PinY.Value;

                        // Add a new text shape slightly below the original shape
                        // Width and height are arbitrary; adjust as needed
                        double width = 1.0;
                        double height = 0.2;
                        double offsetY = 0.5; // vertical offset for the annotation

                        Shape annotationShape = page.AddText(pinX, pinY - offsetY, width, height, annotation);

                        // Refresh the newly added shape so its geometry is calculated correctly
                        annotationShape.RefreshData();
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
