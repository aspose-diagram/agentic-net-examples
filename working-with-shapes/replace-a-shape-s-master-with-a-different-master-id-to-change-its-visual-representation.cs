using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input Visio file path
        string inputPath;
        if (args.Length > 0)
        {
            inputPath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio file: ");
            inputPath = Console.ReadLine();
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Prompt for the shape ID whose master will be replaced
        Console.Write("Enter the Shape ID (numeric) to modify: ");
        string shapeIdInput = Console.ReadLine();
        if (!long.TryParse(shapeIdInput, out long targetShapeId))
        {
            Console.Error.WriteLine("Error: Invalid Shape ID.");
            return;
        }

        // Prompt for the new master name to apply
        Console.Write("Enter the new Master name to assign to the shape: ");
        string newMasterName = Console.ReadLine();

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Locate the shape with the specified ID across all pages
                Shape targetShape = null;
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.ID == targetShapeId)
                        {
                            targetShape = shape;
                            break;
                        }
                    }
                    if (targetShape != null)
                        break;
                }

                if (targetShape == null)
                {
                    Console.Error.WriteLine($"Error: Shape with ID {targetShapeId} not found.");
                    return;
                }

                // Retrieve the new master by name
                Master newMaster = diagram.Masters.GetMasterByName(newMasterName);
                if (newMaster == null)
                {
                    Console.Error.WriteLine($"Error: Master named \"{newMasterName}\" does not exist in the diagram.");
                    return;
                }

                // Replace the shape's master with the new master
                // Ensure the shape currently has a master before replacement
                if (targetShape.Master != null)
                {
                    targetShape.Master = newMaster;
                    Console.WriteLine($"Shape ID {targetShapeId} master replaced with \"{newMasterName}\".");
                }
                else
                {
                    Console.Error.WriteLine("Error: Target shape does not have an existing master to replace.");
                    return;
                }

                // Prepare output file path
                string directory = Path.GetDirectoryName(inputPath);
                string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
                string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_modified.vsdx");

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Modified diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or I/O errors
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}