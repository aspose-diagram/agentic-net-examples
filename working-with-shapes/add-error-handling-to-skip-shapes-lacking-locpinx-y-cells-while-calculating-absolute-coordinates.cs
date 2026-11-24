using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio file as the first argument.");
            return;
        }

        string inputPath = args[0];

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        try
                        {
                            // Skip shapes that are marked for deletion
                            if (shape.Del == BOOL.True)
                                continue;

                            // Ensure the shape has XForm and its PinX/PinY cells (required for any position)
                            if (shape.XForm == null ||
                                shape.XForm.PinX == null ||
                                shape.XForm.PinY == null)
                            {
                                // Shape lacks basic position cells; skip it
                                continue;
                            }

                            // Determine if the shape is a group
                            bool isGroup = shape.Type == TypeValue.Group;

                            if (isGroup)
                            {
                                // For group shapes, ensure the group itself has LocPin cells
                                if (shape.XForm.LocPinX == null || shape.XForm.LocPinY == null)
                                {
                                    // Group missing location pins; cannot compute child positions
                                    continue;
                                }

                                // Iterate child shapes within the group
                                foreach (Shape child in shape.Shapes)
                                {
                                    try
                                    {
                                        // Skip child shapes marked for deletion
                                        if (child.Del == BOOL.True)
                                            continue;

                                        // Guard: child must have XForm and LocPin cells
                                        if (child.XForm == null ||
                                            child.XForm.LocPinX == null ||
                                            child.XForm.LocPinY == null)
                                        {
                                            // Child lacks required location cells; skip it
                                            continue;
                                        }

                                        // Calculate absolute coordinates for the child shape
                                        double absoluteX = shape.XForm.PinX.Value
                                                          + child.XForm.LocPinX.Value
                                                          - shape.XForm.LocPinX.Value;

                                        double absoluteY = shape.XForm.PinY.Value
                                                          + child.XForm.LocPinY.Value
                                                          - shape.XForm.LocPinY.Value;

                                        Console.WriteLine($"Group child shape ID {child.ID} absolute position: ({absoluteX}, {absoluteY})");
                                    }
                                    catch (Exception childEx)
                                    {
                                        // Log any errors while processing a child shape and continue
                                        Console.Error.WriteLine($"Error processing child shape ID {child.ID}: {childEx.Message}");
                                    }
                                }
                            }
                            else
                            {
                                // Non-group shape: use its PinX/PinY as absolute coordinates
                                double absoluteX = shape.XForm.PinX.Value;
                                double absoluteY = shape.XForm.PinY.Value;

                                Console.WriteLine($"Shape ID {shape.ID} absolute position: ({absoluteX}, {absoluteY})");
                            }
                        }
                        catch (Exception shapeEx)
                        {
                            // Log any errors while processing the current shape and continue
                            Console.Error.WriteLine($"Error processing shape ID {shape.ID}: {shapeEx.Message}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during diagram loading or overall processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}