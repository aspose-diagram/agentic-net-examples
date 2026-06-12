using System;
using System.IO;
using Aspose.Diagram;

class VisioBatchRenameAndExport
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = @"C:\Visio\SampleDiagram.vsdx";

            // Derive a prefix from the file name (without extension) and add an underscore
            string prefix = Path.GetFileNameWithoutExtension(inputPath) + "_";

            // Destination PDF file (same folder, same base name)
            string outputPdf = Path.ChangeExtension(inputPath, ".pdf");

            // Load the Visio diagram using the provided constructor
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and their shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Rename each shape using the derived prefix and the shape's ID
                        // (ID is unique within the document)
                        shape.Name = prefix + shape.ID;
                    }
                }

                // Export the modified diagram to PDF using the provided Save method
                diagram.Save(outputPdf, SaveFileFormat.Pdf);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
