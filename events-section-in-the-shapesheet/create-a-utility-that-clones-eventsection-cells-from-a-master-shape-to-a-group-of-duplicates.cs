using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile> [duplicateCount]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        int duplicateCount = 3; // default number of duplicates

        // Optional duplicate count argument
        if (args.Length >= 3 && !int.TryParse(args[2], out duplicateCount))
        {
            Console.Error.WriteLine("Invalid duplicate count. Using default value of 3.");
            duplicateCount = 3;
        }

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Use the first page as the working canvas
            Page page = diagram.Pages[0];

            // Find a source shape that has an associated master (the "master shape")
            Shape sourceShape = null;
            foreach (Shape shp in page.Shapes)
            {
                // Skip deleted shapes
                if (shp.Del == BOOL.True)
                    continue;

                if (shp.Master != null)
                {
                    sourceShape = shp;
                    break;
                }
            }

            if (sourceShape == null)
            {
                Console.Error.WriteLine("No shape with a master was found on the first page.");
                return;
            }

            // Retrieve the master name for creating duplicates
            string masterName = sourceShape.Master.Name;

            // Store created duplicate shapes for later grouping
            List<Shape> duplicateShapes = new List<Shape>();

            // Base position for the first duplicate (offset from source shape)
            double basePinX = sourceShape.XForm.PinX.Value;
            double basePinY = sourceShape.XForm.PinY.Value;
            double offsetX = 2.0; // inches offset between duplicates

            // Clone event formulas from the source shape
            string eventDropFormula = sourceShape.Event.EventDrop.Ufe.F;
            string eventDblClickFormula = sourceShape.Event.EventDblClick.Ufe.F;
            string eventXFModFormula = sourceShape.Event.EventXFMod.Ufe.F;
            string eventMultiDropFormula = sourceShape.Event.EventMultiDrop.Ufe.F;
            string theTextFormula = sourceShape.Event.TheText.Ufe.F;
            string theDataFormula = sourceShape.Event.TheData.Ufe.F;

            // Create the requested number of duplicate shapes
            for (int i = 0; i < duplicateCount; i++)
            {
                // Calculate position for this duplicate
                double pinX = basePinX + (i + 1) * offsetX;
                double pinY = basePinY;

                // Add a new shape based on the same master; isCalculate set to false
                long newShapeId = page.AddShape(pinX, pinY, masterName, false);

                // Retrieve the Shape object from the ID
                Shape newShape = page.Shapes.GetShape((int)newShapeId);
                if (newShape == null)
                {
                    Console.Error.WriteLine($"Failed to retrieve newly added shape with ID {newShapeId}.");
                    continue;
                }

                // Copy event formulas to the new shape
                newShape.Event.EventDrop.Ufe.F = eventDropFormula;
                newShape.Event.EventDblClick.Ufe.F = eventDblClickFormula;
                newShape.Event.EventXFMod.Ufe.F = eventXFModFormula;
                newShape.Event.EventMultiDrop.Ufe.F = eventMultiDropFormula;
                newShape.Event.TheText.Ufe.F = theTextFormula;
                newShape.Event.TheData.Ufe.F = theDataFormula;

                // Add to collection for grouping
                duplicateShapes.Add(newShape);
            }

            // Include the original source shape in the group as well
            duplicateShapes.Insert(0, sourceShape);

            // Group all shapes together
            Shape groupShape = page.Shapes.Group(duplicateShapes.ToArray());

            // Optional: you could set a name for the group (if needed)
            // groupShape.NameU = "ClonedEventGroup";

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}