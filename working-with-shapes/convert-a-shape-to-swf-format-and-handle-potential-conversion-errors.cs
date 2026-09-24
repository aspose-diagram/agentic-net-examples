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

            // Retrieve the first shape on the first page
            Shape targetShape = null;
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                targetShape = shape;
                break;
            }

            if (targetShape == null)
            {
                Console.WriteLine("No shape found in the diagram.");
                return;
            }

            // Define the output path for the SWF file
            string outputPath = "shape_output.swf";

            // Convert the shape to SWF format with error handling
            try
            {
                ImageSaveOptions options = new ImageSaveOptions(SaveFileFormat.Swf);
                targetShape.ToImage(outputPath, options);
                Console.WriteLine($"Shape successfully exported to SWF: {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during shape conversion: {ex.Message}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
