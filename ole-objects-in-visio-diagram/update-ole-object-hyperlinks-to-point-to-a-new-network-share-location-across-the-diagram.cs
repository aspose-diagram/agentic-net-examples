using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input Visio file path
            string inputPath = "input.vsdx";
            // Output Visio file path
            string outputPath = "output.vsdx";
            // New network share location to set for OLE object hyperlinks
            string newShare = @"\\newserver\share\";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Verify the shape is an OLE object
                    if (shape.Type == TypeValue.Foreign &&
                        shape.ForeignData != null &&
                        shape.ForeignData.ForeignType == ForeignType.Object)
                    {
                        // Ensure the shape has a Hyperlinks collection
                        if (shape.Hyperlinks != null)
                        {
                            // Update each hyperlink address to point to the new network share
                            foreach (Hyperlink link in shape.Hyperlinks)
                            {
                                // Replace the existing address with the new share location.
                                // If you need to preserve part of the original path, adjust accordingly.
                                link.Address.Value = newShare;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
