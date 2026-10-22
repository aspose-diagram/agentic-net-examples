using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: <inputVisioPath> <shapeName> <hyperlinkName> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string targetShapeName = args[1];
        string targetHyperlinkName = args[2];
        string outputPath = args[3];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Flag to indicate whether the shape was found.
            bool shapeFound = false;
            // Flag to indicate whether a matching hyperlink was removed.
            bool hyperlinkRemoved = false;

            // Iterate through all pages and shapes to locate the shape by its NameU.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    if (shape.NameU == targetShapeName)
                    {
                        shapeFound = true;

                        // Ensure the shape has a Hyperlinks collection.
                        if (shape.Hyperlinks != null)
                        {
                            // Collect hyperlinks that match the target name.
                            // We cannot modify the collection while iterating, so store matches first.
                            var toRemove = new System.Collections.Generic.List<Hyperlink>();

                            foreach (Hyperlink hl in shape.Hyperlinks)
                            {
                                if (hl.Name == targetHyperlinkName)
                                {
                                    toRemove.Add(hl);
                                }
                            }

                            // Remove the matching hyperlinks.
                            foreach (Hyperlink hl in toRemove)
                            {
                                shape.Hyperlinks.Remove(hl);
                                hyperlinkRemoved = true;
                            }
                        }

                        // Shape found; no need to continue searching further.
                        break;
                    }
                }

                if (shapeFound)
                    break;
            }

            if (!shapeFound)
            {
                Console.Error.WriteLine($"Shape with name '{targetShapeName}' was not found.");
                return;
            }

            if (!hyperlinkRemoved)
            {
                Console.Error.WriteLine($"No hyperlink named '{targetHyperlinkName}' was found on shape '{targetShapeName}'.");
                // Still proceed to save the diagram (unchanged) if desired, or exit.
                // Here we choose to continue saving the diagram.
            }

            // Save the updated diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O exceptions.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}