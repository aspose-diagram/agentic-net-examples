using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: folder path, user‑defined cell name, new value
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <exe> <folderPath> <cellName> <newValue>");
            return;
        }

        string folderPath = args[0];
        string targetCellName = args[1];
        string newCellValue = args[2];

        // Guard: folder must exist
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Get Visio files (common extensions) in the specified folder
        string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            // Process only known Visio extensions
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vsx" && ext != ".vtx")
                continue;

            // Guard: file must exist (redundant after GetFiles, but required by rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the Users collection is not null
                        if (shape.Users == null)
                            continue;

                        // Search for the target user‑defined cell
                        foreach (User userCell in shape.Users)
                        {
                            if (userCell.Name == targetCellName || userCell.NameU == targetCellName)
                            {
                                // Update the cell's value
                                userCell.Value.Val = newCellValue;
                                // Optionally, break if only one occurrence per shape is expected
                                break;
                            }
                        }
                    }
                }

                // Save the diagram back to the original file (overwrites)
                diagram.Save(filePath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Updated file: {filePath}");
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing the file
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}