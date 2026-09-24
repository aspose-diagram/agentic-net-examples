using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file
        string inputPath = "input.vsdx";

        // Guard: ensure the file exists before loading
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only group shapes (containers of sub‑shapes)
                    if (shape.Type == TypeValue.Group)
                    {
                        // The group's own PinX/Y values (center of the group)
                        double groupPinX = shape.XForm.PinX.Value;
                        double groupPinY = shape.XForm.PinY.Value;

                        // Iterate through the sub‑shapes that belong to the group
                        foreach (Shape subShape in shape.Shapes)
                        {
                            // Sub‑shape coordinates are stored relative to the group.
                            // Approximate absolute coordinates by adding the group's PinX/Y.
                            // For a precise calculation you would need to apply the group's
                            // scaling and rotation matrix, but this simple addition works for
                            // many typical Visio diagrams.
                            double absolutePinX = groupPinX + subShape.XForm.PinX.Value;
                            double absolutePinY = groupPinY + subShape.XForm.PinY.Value;

                            // Log the page name, group ID, sub‑shape ID and the calculated absolute positions
                            Console.WriteLine(
                                $"Page: {page.NameU}, Group ID: {shape.ID}, Sub‑Shape ID: {subShape.ID}, " +
                                $"AbsPinX: {absolutePinX}, AbsPinY: {absolutePinY}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose‑Diagram errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}