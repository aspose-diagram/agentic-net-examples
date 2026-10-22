using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file \"{inputPath}\" does not exist.");
            return;
        }

        try
        {
            // Load the source Visio diagram inside a using block (required pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // -----------------------------------------------------------------
                // Locate a shape that already contains at least one hyperlink.
                // -----------------------------------------------------------------
                Shape sourceShape = null;
                Hyperlink originalLink = null;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Hyperlinks != null && shape.Hyperlinks.Count > 0)
                        {
                            sourceShape = shape;
                            // Take the first hyperlink as the one to clone.
                            originalLink = shape.Hyperlinks[0];
                            break;
                        }
                    }
                    if (sourceShape != null) break;
                }

                if (sourceShape == null || originalLink == null)
                {
                    Console.Error.WriteLine("Error: No shape with a hyperlink was found in the diagram.");
                    return;
                }

                // -----------------------------------------------------------------
                // Clone the found hyperlink and modify its description.
                // -----------------------------------------------------------------
                Hyperlink clonedLink = new Hyperlink();

                // Copy mutable properties.
                clonedLink.Name = originalLink.Name;
                clonedLink.Address.Value = originalLink.Address.Value;
                clonedLink.SubAddress.Value = originalLink.SubAddress.Value;

                // Description: append a note about modification.
                string originalDesc = originalLink.Description != null ? originalLink.Description.Value : string.Empty;
                clonedLink.Description.Value = string.IsNullOrEmpty(originalDesc)
                    ? "Cloned hyperlink (modified description)"
                    : originalDesc + " (modified description)";

                // -----------------------------------------------------------------
                // Locate a different shape to attach the cloned hyperlink.
                // -----------------------------------------------------------------
                Shape targetShape = null;

                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip the source shape itself.
                        if (shape.ID == sourceShape.ID) continue;

                        // Choose the first other shape as the target.
                        targetShape = shape;
                        break;
                    }
                    if (targetShape != null) break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine("Error: No suitable target shape found to attach the cloned hyperlink.");
                    return;
                }

                // Ensure the target shape's Hyperlinks collection is available.
                if (targetShape.Hyperlinks == null)
                {
                    Console.Error.WriteLine("Error: Target shape's Hyperlinks collection is null.");
                    return;
                }

                // Attach the cloned hyperlink to the target shape.
                targetShape.Hyperlinks.Add(clonedLink);

                // -----------------------------------------------------------------
                // Save the modified diagram.
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}