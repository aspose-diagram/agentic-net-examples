using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments:
        // 0 - input Visio file path
        // 1 - name of the master to clone
        // 2 - comma‑separated list of shape IDs to which the cloned master will be applied
        // 3 - output Visio file path
        if (args.Length != 4)
        {
            Console.Error.WriteLine("Usage: <program> <inputPath> <masterName> <shapeIdsCsv> <outputPath>");
            return;
        }

        string inputPath = args[0];
        string masterName = args[1];
        string shapeIdsCsv = args[2];
        string outputPath = args[3];

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse shape IDs
        string[] idTokens = shapeIdsCsv.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        long[] shapeIds = new long[idTokens.Length];
        for (int i = 0; i < idTokens.Length; i++)
        {
            if (!long.TryParse(idTokens[i].Trim(), out shapeIds[i]))
            {
                Console.Error.WriteLine($"Invalid shape ID: '{idTokens[i]}'");
                return;
            }
        }

        try
        {
            // Load the diagram inside a using block (mandatory pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Verify that the requested master exists
                if (!diagram.Masters.IsExist(masterName))
                {
                    Console.Error.WriteLine($"Master with name '{masterName}' does not exist in the diagram.");
                    return;
                }

                // Retrieve the original master
                Master originalMaster = diagram.Masters.GetMasterByName(masterName);

                // Clone the master (explicit cast required)
                Master clonedMaster = (Master)originalMaster.Clone();

                // Modify the text block of the cloned master.
                // A master can contain multiple shapes; we modify the first shape for simplicity.
                if (clonedMaster.Shapes.Count > 0)
                {
                    Shape masterShape = clonedMaster.Shapes[0];
                    // Clear existing text
                    masterShape.Text.Value.Clear();
                    // Add new text content
                    masterShape.Text.Value.Add(new Txt("Cloned Master Text"));
                }
                else
                {
                    Console.Error.WriteLine("Cloned master contains no shapes to modify text.");
                }

                // Add the cloned master to the diagram's master collection
                diagram.Masters.Add(clonedMaster);

                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page (index 0)
                Page page = diagram.Pages[0];

                // Apply the cloned master to each selected shape
                foreach (long shapeId in shapeIds)
                {
                    // Retrieve the shape by its ID
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape == null)
                    {
                        Console.Error.WriteLine($"Shape with ID {shapeId} not found on page 0.");
                        continue;
                    }

                    // Assign the cloned master to the shape
                    shape.Master = clonedMaster;
                }

                // Save the modified diagram to the specified output path
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}