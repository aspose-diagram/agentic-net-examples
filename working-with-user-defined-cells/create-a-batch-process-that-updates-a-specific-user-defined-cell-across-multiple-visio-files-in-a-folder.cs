using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Prompt for the folder containing Visio files.
        Console.Write("Enter the full path to the folder with Visio files: ");
        string folderPath = Console.ReadLine();

        // Guard: ensure the folder exists.
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.Error.WriteLine("Folder does not exist: " + folderPath);
            return;
        }

        // Prompt for the name of the user-defined cell to update.
        Console.Write("Enter the user-defined cell name (exact Name or NameU): ");
        string targetCellName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(targetCellName))
        {
            Console.Error.WriteLine("User-defined cell name cannot be empty.");
            return;
        }

        // Prompt for the new value to assign to the cell.
        Console.Write("Enter the new value for the cell: ");
        string newValue = Console.ReadLine();

        // Get all Visio files in the folder (common extensions).
        string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in visioFiles)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            // Process only known Visio extensions.
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" && ext != ".vsdm" && ext != ".vsdm")
            {
                continue;
            }

            // Guard: ensure the file exists (should be true, but added per rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine("File not found: " + filePath);
                continue;
            }

            try
            {
                // Load the Visio diagram.
                Diagram diagram = new Diagram(filePath);

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through user-defined cells of the shape.
                        foreach (User userCell in shape.Users)
                        {
                            // Match by Name or NameU (case-sensitive as per API).
                            if (userCell.Name == targetCellName || userCell.NameU == targetCellName)
                            {
                                // Update the cell's value.
                                userCell.Value.Val = newValue;
                                // Optionally break if only one instance per shape is expected.
                                // break;
                            }
                        }
                    }
                }

                // Determine appropriate SaveFileFormat based on original extension.
                SaveFileFormat saveFormat = SaveFileFormat.Vsdx; // default
                if (ext == ".vsd")
                    saveFormat = SaveFileFormat.Vsd;
                else if (ext == ".vdx")
                    saveFormat = SaveFileFormat.Vdx;
                else if (ext == ".vsdm")
                    saveFormat = SaveFileFormat.Vsdm;

                // Save the updated diagram, overwriting the original file.
                diagram.Save(filePath, saveFormat);
                Console.WriteLine($"Updated file: {filePath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing of this file.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }

        Console.WriteLine("Batch update completed.");
    }
}