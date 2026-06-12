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
            string outputPath = "output_protected.vsdx";

            // Load the Visio diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Prevent the shape from being deleted by locking the Delete property
                    shape.Protection.LockDelete.Value = BOOL.True;
                }
            }

            // Save the modified diagram with protection applied
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
