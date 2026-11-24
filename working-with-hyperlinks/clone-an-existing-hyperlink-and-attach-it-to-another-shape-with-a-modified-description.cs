using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio file
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Path to the output Visio file
        string outputPath = "output.vsdx";

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape sourceShape = null;
                Shape targetShape = null;

                // Locate the first shape that contains at least one hyperlink (source)
                // and the next distinct shape to receive the cloned hyperlink (target)
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify source shape
                        if (sourceShape == null && shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            sourceShape = shape;
                            continue;
                        }

                        // Identify target shape (must be different from source)
                        if (sourceShape != null && targetShape == null && shape != sourceShape)
                        {
                            targetShape = shape;
                            break;
                        }
                    }

                    if (sourceShape != null && targetShape != null)
                        break;
                }

                // Validate that both shapes were found
                if (sourceShape == null)
                {
                    Console.Error.WriteLine("No shape with a hyperlink was found in the diagram.");
                    return;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("No suitable target shape was found to attach the cloned hyperlink.");
                    return;
                }

                // Retrieve the first hyperlink from the source shape
                Hyperlink originalLink = null;
                foreach (Hyperlink hl in sourceShape.Hyperlinks)
                {
                    originalLink = hl;
                    break; // take the first hyperlink only
                }

                if (originalLink == null)
                {
                    Console.Error.WriteLine("Source shape does not contain a hyperlink to clone.");
                    return;
                }

                // Ensure the target shape's Hyperlinks collection exists
                if (targetShape.Hyperlinks == null)
                {
                    Console.Error.WriteLine("Target shape's Hyperlinks collection is null; cannot add hyperlink.");
                    return;
                }

                // Create a new hyperlink instance and copy relevant properties
                Hyperlink clonedLink = new Hyperlink();

                // Copy the name (optional, can be empty)
                clonedLink.Name = originalLink.Name;

                // Copy address, sub-address and description (cell‑based values)
                clonedLink.Address.Value = originalLink.Address.Value;
                clonedLink.SubAddress.Value = originalLink.SubAddress.Value;

                // Modify the description as required
                clonedLink.Description.Value = "Cloned hyperlink - modified description";

                // NOTE: The NewWindow property is read‑only in this API version,
                // so it is not copied. If needed, it can be set via its underlying cell.

                // Add the cloned hyperlink to the target shape
                targetShape.Hyperlinks.Add(clonedLink);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}