using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, new master name, output diagram path.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <newMasterName> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string newMasterName = args[1];
        string outputPath = args[2];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Retrieve the active page.
                Page page = diagram.ActivePage;

                // Find the first group shape on the page.
                Shape? groupShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    // A group shape can be identified by its Type being Group.
                    if (shape.Type == TypeValue.Group)
                    {
                        groupShape = shape;
                        break;
                    }
                }

                if (groupShape == null)
                {
                    Console.Error.WriteLine("No group shape found in the diagram.");
                    return;
                }

                // Verify that the requested master exists in the diagram's masters collection.
                Master? newMaster = diagram.Masters.GetMasterByName(newMasterName);
                if (newMaster == null)
                {
                    Console.Error.WriteLine($"Master \"{newMasterName}\" not found in the diagram.");
                    return;
                }

                // Change the master of the group shape.
                // This operation preserves the layout of sub‑shapes automatically.
                groupShape.Master = newMaster;

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}