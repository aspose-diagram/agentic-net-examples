using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (provide via command‑line or use defaults)
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the names of the source shape (contains the hyperlink to copy)
                // and the target shape (where the cloned hyperlink will be attached)
                string sourceShapeName = "SourceShape";
                string targetShapeName = "TargetShape";

                // Locate the source shape
                Shape sourceShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU == sourceShapeName)
                        {
                            sourceShape = shape;
                            break;
                        }
                    }
                    if (sourceShape != null) break;
                }

                if (sourceShape == null)
                {
                    throw new Exception($"Source shape \"{sourceShapeName}\" not found.");
                }

                // Ensure the source shape has at least one hyperlink
                if (sourceShape.Hyperlinks == null || sourceShape.Hyperlinks.Count == 0)
                {
                    throw new Exception($"Source shape \"{sourceShapeName}\" does not contain any hyperlinks.");
                }

                // Clone the first hyperlink from the source shape
                Hyperlink originalLink = sourceShape.Hyperlinks[0];
                Hyperlink clonedLink = new Hyperlink();

                // Copy address and sub‑address (if any)
                clonedLink.Address.Value = originalLink.Address.Value;
                clonedLink.SubAddress.Value = originalLink.SubAddress.Value;

                // Modify the description as required
                clonedLink.Description.Value = "Cloned hyperlink with updated description";

                // Optionally copy the name (identifier) of the hyperlink
                clonedLink.Name = originalLink.Name;

                // Locate the target shape
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU == targetShapeName)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null) break;
                }

                if (targetShape == null)
                {
                    throw new Exception($"Target shape \"{targetShapeName}\" not found.");
                }

                // Ensure the target shape's Hyperlinks collection is initialized
                if (targetShape.Hyperlinks == null)
                {
                    // The Hyperlinks collection is always instantiated by Aspose.Diagram,
                    // but this check guards against unexpected null references.
                    throw new Exception("Target shape's Hyperlinks collection is null.");
                }

                // Add the cloned hyperlink to the target shape
                targetShape.Hyperlinks.Add(clonedLink);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Hyperlink cloned from \"{sourceShapeName}\" and attached to \"{targetShapeName}\".");
                Console.WriteLine($"Diagram saved to \"{outputPath}\".");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }