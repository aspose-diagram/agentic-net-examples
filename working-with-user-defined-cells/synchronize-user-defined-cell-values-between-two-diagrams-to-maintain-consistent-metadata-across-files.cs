using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <sourceDiagramPath> <targetDiagramPath> <outputDiagramPath>");
            return;
        }

        string sourcePath = args[0];
        string targetPath = args[1];
        string outputPath = args[2];

        // Guard against missing files.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(targetPath))
        {
            Console.Error.WriteLine($"Target file not found: {targetPath}");
            return;
        }

        try
        {
            // Load source diagram inside a using block.
            using (Diagram sourceDiagram = new Diagram(sourcePath))
            {
                // Load target diagram inside a using block.
                using (Diagram targetDiagram = new Diagram(targetPath))
                {
                    // Iterate all pages in the source diagram.
                    foreach (Page srcPage in sourceDiagram.Pages)
                    {
                        // Iterate all shapes on the current source page.
                        foreach (Shape srcShape in srcPage.Shapes)
                        {
                            // Attempt to find a matching shape in the target diagram by NameU.
                            Shape targetShape = FindShapeByNameU(targetDiagram, srcShape.NameU);
                            if (targetShape == null)
                            {
                                // No matching shape; skip synchronization for this shape.
                                continue;
                            }

                            // Synchronize each user‑defined cell from source shape to target shape.
                            foreach (User srcUser in srcShape.Users)
                            {
                                // Look for an existing user‑defined cell with the same NameU in the target shape.
                                User matchingUser = null;
                                foreach (User tgtUser in targetShape.Users)
                                {
                                    if (tgtUser.NameU == srcUser.NameU)
                                    {
                                        matchingUser = tgtUser;
                                        break;
                                    }
                                }

                                if (matchingUser != null)
                                {
                                    // Update existing cell's value and prompt.
                                    matchingUser.Value.Val = srcUser.Value.Val;
                                    matchingUser.Prompt.Value = srcUser.Prompt.Value;
                                }
                                else
                                {
                                    // Create a new user‑defined cell and add it to the target shape.
                                    User newUser = new User();
                                    newUser.Name = srcUser.Name;
                                    newUser.NameU = srcUser.NameU;
                                    newUser.Value.Val = srcUser.Value.Val;
                                    newUser.Prompt.Value = srcUser.Prompt.Value;
                                    targetShape.Users.Add(newUser);
                                }
                            }
                        }
                    }

                    // Refresh the target diagram to apply changes.
                    targetDiagram.Refresh();

                    // Save the synchronized diagram to the specified output path.
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }

            Console.WriteLine("Synchronization completed successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Helper method to locate a shape in any page of a diagram by its NameU.
    private static Shape FindShapeByNameU(Diagram diagram, string nameU)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (shape.NameU == nameU)
                {
                    return shape;
                }
            }
        }
        return null;
    }
}