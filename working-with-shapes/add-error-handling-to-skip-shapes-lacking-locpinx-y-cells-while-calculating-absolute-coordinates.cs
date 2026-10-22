using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFilePath>");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Load the diagram inside a using block as required
        try
        {
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // If the shape is a group, process its child shapes recursively
                        if (shape.Type == TypeValue.Group)
                        {
                            foreach (Shape child in shape.Shapes)
                            {
                                ProcessShape(child, shape);
                            }
                        }
                        else
                        {
                            // Regular (non‑group) shape
                            ProcessShape(shape, null);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading or processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Calculates and prints the absolute coordinates of a shape.
    /// Skips the shape if required LocPinX/Y cells are missing.
    /// </summary>
    /// <param name="shape">The shape to process.</param>
    /// <param name="parentGroup">The parent group shape, if any.</param>
    private static void ProcessShape(Shape shape, Shape parentGroup)
    {
        try
        {
            // Verify that the shape's XForm and its LocPin cells are present
            if (shape.XForm == null ||
                shape.XForm.LocPinX == null ||
                shape.XForm.LocPinY == null ||
                shape.XForm.PinX == null ||
                shape.XForm.PinY == null)
            {
                Console.Error.WriteLine($"Warning: Shape ID {shape.ID} lacks required XForm cells. Skipping.");
                return;
            }

            // If there is a parent group, also verify its required cells
            if (parentGroup != null)
            {
                if (parentGroup.XForm == null ||
                    parentGroup.XForm.LocPinX == null ||
                    parentGroup.XForm.LocPinY == null ||
                    parentGroup.XForm.PinX == null ||
                    parentGroup.XForm.PinY == null)
                {
                    Console.Error.WriteLine($"Warning: Parent group ID {parentGroup.ID} lacks required XForm cells. Skipping child shape ID {shape.ID}.");
                    return;
                }
            }

            double absoluteX;
            double absoluteY;

            if (parentGroup != null)
            {
                // Absolute position for a sub‑shape within a group
                absoluteX = parentGroup.XForm.PinX.Value
                            + shape.XForm.PinX.Value
                            - parentGroup.XForm.LocPinX.Value;

                absoluteY = parentGroup.XForm.PinY.Value
                            + shape.XForm.PinY.Value
                            - parentGroup.XForm.LocPinY.Value;
            }
            else
            {
                // Absolute position for a regular shape
                absoluteX = shape.XForm.PinX.Value;
                absoluteY = shape.XForm.PinY.Value;
            }

            Console.WriteLine($"Shape ID {shape.ID}: Absolute X = {absoluteX}, Absolute Y = {absoluteY}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing shape ID {shape.ID}: {ex.Message}");
        }
    }
}