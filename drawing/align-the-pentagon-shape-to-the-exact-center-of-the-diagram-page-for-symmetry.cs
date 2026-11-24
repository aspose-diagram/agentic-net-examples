using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                // Work with the first page (index 0).
                Page page = diagram.Pages[0];

                // Retrieve page dimensions (in inches) from the PageProps section.
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Locate the first shape whose master name is "Pentagon" and that is not deleted.
                Shape pentagonShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False && shape.Master != null && shape.Master.Name == "Pentagon")
                    {
                        pentagonShape = shape;
                        break;
                    }
                }

                if (pentagonShape == null)
                {
                    Console.Error.WriteLine("Error: No pentagon shape found on the first page.");
                    return;
                }

                // Center the pentagon shape on the page.
                // PinX and PinY represent the shape's center coordinates.
                pentagonShape.XForm.PinX.Value = pageWidth / 2.0;
                pentagonShape.XForm.PinY.Value = pageHeight / 2.0;

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Pentagon shape centered and diagram saved to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}