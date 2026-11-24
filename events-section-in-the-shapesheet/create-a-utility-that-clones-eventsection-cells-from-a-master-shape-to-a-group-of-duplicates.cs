using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input diagram, output diagram, number of duplicates
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath> <duplicateCount>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string duplicateCountStr = args[2];

        // Guard for input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse duplicate count
        if (!int.TryParse(duplicateCountStr, out int duplicateCount) || duplicateCount <= 0)
        {
            Console.Error.WriteLine("Duplicate count must be a positive integer.");
            return;
        }

        try
        {
            // Load the source diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page
            Page page = diagram.Pages[0];

            // Ensure there is at least one shape to serve as the master source
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("The first page contains no shapes to clone from.");
                return;
            }

            // Retrieve the source shape (assumed to be based on a master)
            Shape masterShape = page.Shapes[0];

            // Verify the shape has an associated master
            if (masterShape.Master == null)
            {
                Console.Error.WriteLine("The selected shape does not have an associated master.");
                return;
            }

            // Prepare a list to hold the duplicated shapes
            List<Shape> duplicatedShapes = new List<Shape>();

            // Starting coordinates for duplication (adjust as needed)
            double startX = 1.0;
            double startY = 1.0;
            double offsetX = 2.0; // horizontal offset between duplicates
            double offsetY = 0.0; // vertical offset (can be changed)

            // Clone the shape multiple times
            for (int i = 0; i < duplicateCount; i++)
            {
                double pinX = startX + i * offsetX;
                double pinY = startY + i * offsetY;

                // Add a new shape based on the same master as the source shape
                long newShapeId = page.AddShape(pinX, pinY, masterShape.Master.Name, false);

                // Retrieve the newly added shape object
                Shape newShape = page.Shapes.GetShape((int)newShapeId);

                // Copy EventSection formulas from the master shape to the new shape
                // EventDrop
                newShape.Event.EventDrop.Ufe.F = masterShape.Event.EventDrop.Ufe.F;
                // EventDblClick
                newShape.Event.EventDblClick.Ufe.F = masterShape.Event.EventDblClick.Ufe.F;
                // EventXFMod
                newShape.Event.EventXFMod.Ufe.F = masterShape.Event.EventXFMod.Ufe.F;
                // EventMultiDrop
                newShape.Event.EventMultiDrop.Ufe.F = masterShape.Event.EventMultiDrop.Ufe.F;
                // TheText
                newShape.Event.TheText.Ufe.F = masterShape.Event.TheText.Ufe.F;
                // TheData
                newShape.Event.TheData.Ufe.F = masterShape.Event.TheData.Ufe.F;

                // Add to collection for later grouping
                duplicatedShapes.Add(newShape);
            }

            // Group all duplicated shapes into a single group shape
            if (duplicatedShapes.Count > 0)
            {
                Shape groupShape = page.Shapes.Group(duplicatedShapes.ToArray());

                // Optional: you can set a name for the group (if needed)
                // groupShape.NameU = "ClonedEventGroup";
            }

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}