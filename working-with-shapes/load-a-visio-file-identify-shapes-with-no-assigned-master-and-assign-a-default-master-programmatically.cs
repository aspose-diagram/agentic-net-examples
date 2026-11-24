using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and stencil file containing the default master.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <defaultMasterStencilPath>");
            return;
        }

        string inputVisioPath = args[0];
        string stencilPath = args[1];

        // Guard: ensure the input Visio file exists.
        if (!File.Exists(inputVisioPath))
        {
            Console.Error.WriteLine($"Error: Visio file not found at '{inputVisioPath}'.");
            return;
        }

        // Guard: ensure the stencil file exists.
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Error: Stencil file not found at '{stencilPath}'.");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputVisioPath))
            {
                // Name of the master to import from the stencil.
                const string defaultMasterName = "Rectangle";

                // Import the master from the stencil. The AddMaster method returns the master ID (int).
                int masterId = diagram.AddMaster(stencilPath, defaultMasterName);

                // Retrieve the Master object by name (or by ID) after import.
                Master defaultMaster = diagram.Masters.GetMasterByName(defaultMasterName);
                if (defaultMaster == null)
                {
                    Console.Error.WriteLine($"Error: Could not retrieve master '{defaultMasterName}' after import.");
                    return;
                }

                // Iterate through all pages and shapes to find shapes without an assigned master.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape has no master, assign the default master.
                        if (shape.Master == null)
                        {
                            shape.Master = defaultMaster;
                        }
                    }
                }

                // Prepare output file path with a suffix.
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(inputVisioPath) ?? string.Empty,
                    Path.GetFileNameWithoutExtension(inputVisioPath) + "_updated.vsdx");

                // Save the modified diagram using the correct overload.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}