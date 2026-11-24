using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments count
        if (args.Length < 5)
        {
            Console.Error.WriteLine("Usage: <inputDiagramPath> <masterNameToClone> <newText> <outputDiagramPath> <shapeNamesCommaSeparated>");
            return;
        }

        string inputPath = args[0];
        string masterName = args[1];
        string newText = args[2];
        string outputPath = args[3];
        string[] targetShapeNames = args[4].Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        // Guard for input file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify the master exists
                if (!diagram.Masters.IsExist(masterName))
                {
                    Console.Error.WriteLine($"Master with name '{masterName}' does not exist in the diagram.");
                    return;
                }

                // Retrieve the original master
                Master originalMaster = diagram.Masters.GetMasterByName(masterName);

                // Clone the master (explicit cast required)
                Master clonedMaster = (Master)originalMaster.Clone();

                // Optionally give the cloned master a distinct name
                clonedMaster.NameU = originalMaster.NameU + "_Clone";

                // Add the cloned master to the diagram's master collection
                diagram.Masters.Add(clonedMaster);

                // Iterate through all pages and shapes to find target shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Match shape by its NameU (case-insensitive)
                        foreach (string targetName in targetShapeNames)
                        {
                            if (string.Equals(shape.NameU, targetName.Trim(), StringComparison.OrdinalIgnoreCase))
                            {
                                // Apply the cloned master to the shape
                                shape.Master = clonedMaster;

                                // Clear existing text
                                shape.Text.Value.Clear();

                                // Add new text to the shape's text block
                                shape.Text.Value.Add(new Txt(newText));

                                // Break inner loop once matched
                                break;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}