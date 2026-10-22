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
                    // Iterate through pages by index (assumes matching page order).
                    int pageCount = Math.Min(sourceDiagram.Pages.Count, targetDiagram.Pages.Count);
                    for (int i = 0; i < pageCount; i++)
                    {
                        Page srcPage = sourceDiagram.Pages[i];
                        Page tgtPage = targetDiagram.Pages[i];

                        // Iterate all shapes on the source page.
                        foreach (Shape srcShape in srcPage.Shapes)
                        {
                            // Attempt to find a shape on the target page with the same NameU.
                            Shape tgtShape = null;
                            foreach (Shape candidate in tgtPage.Shapes)
                            {
                                if (candidate.NameU == srcShape.NameU)
                                {
                                    tgtShape = candidate;
                                    break;
                                }
                            }

                            // If no matching shape is found, skip synchronization for this shape.
                            if (tgtShape == null)
                                continue;

                            // Synchronize each user‑defined cell from source shape to target shape.
                            foreach (User srcUser in srcShape.Users)
                            {
                                // Look for an existing user‑defined cell with the same name.
                                User matchingUser = null;
                                foreach (User tgtUser in tgtShape.Users)
                                {
                                    if (tgtUser.Name == srcUser.Name)
                                    {
                                        matchingUser = tgtUser;
                                        break;
                                    }
                                }

                                if (matchingUser != null)
                                {
                                    // Update the existing cell's value (and prompt, if needed).
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
                                    tgtShape.Users.Add(newUser);
                                }
                            }
                        }
                    }

                    // Refresh the target diagram to apply all changes.
                    targetDiagram.Refresh();

                    // Save the synchronized diagram to the specified output path.
                    targetDiagram.Save(outputPath, SaveFileFormat.Vsdx);
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}