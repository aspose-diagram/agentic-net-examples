using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required.
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page to perform spacing and verification.
                foreach (Page page in diagram.Pages)
                {
                    // ------------------------------------------------------------
                    // Spacing operation: auto‑space shapes on the page.
                    // ------------------------------------------------------------
                    try
                    {
                        AutoSpaceOptions options = new AutoSpaceOptions();
                        options.DistanceInHorizontal = 2; // horizontal spacing (in inches)
                        options.DistanceInVertical = 2;   // vertical spacing (in inches)

                        // AutoSpaceShapes requires the shape collection and options.
                        page.AutoSpaceShapes(page.Shapes, options);
                    }
                    catch (Exception spacingEx)
                    {
                        Console.Error.WriteLine($"Spacing operation failed on page '{page.Name}': {spacingEx.Message}");
                        // Continue with verification even if spacing failed.
                    }

                    // ------------------------------------------------------------
                    // Verify that all connectors remain attached to valid source and target shapes.
                    // ------------------------------------------------------------
                    foreach (Connect connect in page.Connects)
                    {
                        long fromId = connect.FromSheet;
                        long toId = connect.ToSheet;

                        Shape sourceShape = null;
                        Shape targetShape = null;

                        // Attempt to retrieve the source shape.
                        try
                        {
                            sourceShape = page.Shapes.GetShape(fromId);
                        }
                        catch
                        {
                            // Shape may not exist; leave as null.
                        }

                        // Attempt to retrieve the target shape.
                        try
                        {
                            targetShape = page.Shapes.GetShape(toId);
                        }
                        catch
                        {
                            // Shape may not exist; leave as null.
                        }

                        bool sourceValid = sourceShape != null && sourceShape.Del == BOOL.False;
                        bool targetValid = targetShape != null && targetShape.Del == BOOL.False;

                        if (!sourceValid || !targetValid)
                        {
                            Console.WriteLine($"[ERROR] Connector on page '{page.Name}' has invalid attachment. Source ID {fromId} valid: {sourceValid}, Target ID {toId} valid: {targetValid}");
                        }
                        else
                        {
                            Console.WriteLine($"[OK] Connector on page '{page.Name}' correctly attached to source ID {fromId} and target ID {toId}.");
                        }
                    }
                }

                // Save the diagram after spacing.
                try
                {
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
                }
                catch (Exception saveEx)
                {
                    Console.Error.WriteLine($"Failed to save diagram: {saveEx.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
    }
}