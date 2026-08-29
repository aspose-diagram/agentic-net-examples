using System;
using System.IO;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        // Get folder containing Visio files
        Console.WriteLine("Enter folder path containing Visio files:");
        string folderPath = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(folderPath) || !Directory.Exists(folderPath))
        {
            Console.WriteLine("Invalid folder path.");
            return;
        }

        // Get target user-defined cell name
        Console.WriteLine("Enter the name of the user-defined cell to update:");
        string cellName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(cellName))
        {
            Console.WriteLine("Cell name cannot be empty.");
            return;
        }

        // Get new value for the cell
        Console.WriteLine("Enter the new value for the cell:");
        string newValue = Console.ReadLine();

        // Process each Visio file in the folder
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string filePath in files)
        {
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (!IsVisioExtension(ext))
                continue; // Skip non‑Visio files

            try
            {
                // Load diagram
                Diagram diagram = new Diagram(filePath);
                bool diagramModified = false;

                // Iterate pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        bool cellFound = false;

                        // Search existing user‑defined cells
                        foreach (User user in shape.Users)
                        {
                            if (string.Equals(user.Name, cellName, StringComparison.OrdinalIgnoreCase) ||
                                string.Equals(user.NameU, cellName, StringComparison.OrdinalIgnoreCase))
                            {
                                user.Value.Val = newValue;
                                cellFound = true;
                                diagramModified = true;
                                break;
                            }
                        }

                        // If not found, create a new user‑defined cell
                        if (!cellFound)
                        {
                            User newUser = new User();
                            newUser.Name = cellName;
                            newUser.Value.Val = newValue;
                            shape.Users.Add(newUser);
                            diagramModified = true;
                        }
                    }
                }

                // Save only if changes were made
                if (diagramModified)
                {
                    SaveFileFormat format = GetSaveFormat(ext);
                    diagram.Save(filePath, format);
                    Console.WriteLine($"Updated file: {Path.GetFileName(filePath)}");
                }
                else
                {
                    Console.WriteLine($"No changes needed for file: {Path.GetFileName(filePath)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {Path.GetFileName(filePath)}: {ex.Message}");
            }
        }

        Console.WriteLine("Batch update completed.");
    }

    // Determines whether a file extension corresponds to a supported Visio format
    private static bool IsVisioExtension(string ext)
    {
        return ext == ".vsdx" || ext == ".vsd" || ext == ".vdx" ||
               ext == ".vsdm" || ext == ".vsx" || ext == ".vtx" ||
               ext == ".vssx" || ext == ".vstx" || ext == ".vssm" ||
               ext == ".vstm";
    }

    // Maps file extension to the appropriate SaveFileFormat enum value
    private static SaveFileFormat GetSaveFormat(string ext)
    {
        return ext switch
        {
            ".vsdx" => SaveFileFormat.Vsdx,
            ".vsd"  => SaveFileFormat.Vsd,
            ".vdx"  => SaveFileFormat.Vdx,
            ".vsdm" => SaveFileFormat.Vsdm,
            ".vsx"  => SaveFileFormat.Vsx,
            ".vtx"  => SaveFileFormat.Vtx,
            ".vssx" => SaveFileFormat.Vssx,
            ".vstx" => SaveFileFormat.Vstx,
            ".vssm" => SaveFileFormat.Vssm,
            ".vstm" => SaveFileFormat.Vstm,
            _       => SaveFileFormat.Vsdx // Default fallback
        };
    }
}
