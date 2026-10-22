using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram path, output diagram path, optional source page index, optional target page index
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> [sourcePageIndex] [targetPageIndex]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Default page indices
        int sourcePageIndex = 0; // first page
        int targetPageIndex = 1; // second page

        if (args.Length >= 3 && !int.TryParse(args[2], out sourcePageIndex))
        {
            Console.Error.WriteLine("Invalid source page index. It must be an integer.");
            return;
        }

        if (args.Length >= 4 && !int.TryParse(args[3], out targetPageIndex))
        {
            Console.Error.WriteLine("Invalid target page index. It must be an integer.");
            return;
        }

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the source diagram inside a using block for proper disposal
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Validate source page index
                if (sourcePageIndex < 0 || sourcePageIndex >= diagram.Pages.Count)
                {
                    Console.Error.WriteLine($"Source page index {sourcePageIndex} is out of range. Diagram has {diagram.Pages.Count} page(s).");
                    return;
                }

                // Retrieve the source page
                Page sourcePage = diagram.Pages[sourcePageIndex];

                // Ensure the source page contains at least one shape
                if (sourcePage.Shapes.Count == 0)
                {
                    Console.Error.WriteLine($"Source page (index {sourcePageIndex}) contains no shapes to clone.");
                    return;
                }

                // Retrieve the first shape on the source page
                Shape sourceShape = sourcePage.Shapes[0];

                // Ensure the shape has an associated master (required for cloning via master name)
                if (sourceShape.Master == null)
                {
                    Console.Error.WriteLine("The selected shape does not have a master and cannot be cloned.");
                    return;
                }

                // Extract geometry and master information from the source shape
                string masterName = sourceShape.Master.Name;
                double pinX = sourceShape.XForm.PinX.Value;
                double pinY = sourceShape.XForm.PinY.Value;
                double width = sourceShape.XForm.Width.Value;
                double height = sourceShape.XForm.Height.Value;

                // Determine or create the target page
                Page targetPage;
                if (targetPageIndex < diagram.Pages.Count)
                {
                    targetPage = diagram.Pages[targetPageIndex];
                }
                else
                {
                    // Add blank pages until the desired index exists
                    while (diagram.Pages.Count <= targetPageIndex)
                    {
                        diagram.Pages.Add(new Page());
                    }
                    targetPage = diagram.Pages[targetPageIndex];
                }

                // Add a new shape on the target page using the same master and geometry
                // The fourth parameter (isCalculate) must be a bool; false means no automatic size calculation
                long newShapeId = targetPage.AddShape(pinX, pinY, width, height, masterName, false);
                Shape clonedShape = targetPage.Shapes.GetShape(newShapeId);

                // Modify the width of the cloned shape (example: increase by 20%)
                double newWidth = width * 1.20;
                clonedShape.XForm.Width.Value = newWidth;

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}