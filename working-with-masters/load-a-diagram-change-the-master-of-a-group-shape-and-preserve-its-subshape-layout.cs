using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, and the target master name.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputPath> <outputPath> <newMasterName>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];
        string newMasterName = args[2];

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Locate the first group shape on the first page.
            Page page = diagram.Pages[0];
            Shape? groupShape = null;
            foreach (Shape shape in page.Shapes)
            {
                // Identify a group shape by its TypeValue.
                if (shape.Type == TypeValue.Group)
                {
                    groupShape = shape;
                    break;
                }
            }

            // Guard: ensure a group shape was found.
            if (groupShape == null)
            {
                Console.Error.WriteLine("No group shape found in the diagram.");
                return;
            }

            // Verify that the target master exists in the diagram's master collection.
            Master? targetMaster = diagram.Masters.GetMasterByName(newMasterName);
            if (targetMaster == null)
            {
                Console.Error.WriteLine($"Master \"{newMasterName}\" not found in the diagram.");
                return;
            }

            // Preserve the current master name for logging.
            string oldMasterName = groupShape.Master?.Name ?? "(none)";
            Console.WriteLine($"Changing master of group shape (ID={groupShape.ID}) from \"{oldMasterName}\" to \"{newMasterName}\".");

            // Assign the new master to the group shape. This operation keeps the sub‑shape layout intact.
            groupShape.Master = targetMaster;

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to \"{outputPath}\".");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}