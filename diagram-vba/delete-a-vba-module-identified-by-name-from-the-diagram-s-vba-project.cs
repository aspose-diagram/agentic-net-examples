using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the output file (must be macro‑enabled to keep VBA)
            string outputPath = "output.vsdm";
            // Name of the VBA module to delete
            string moduleNameToDelete = "ModuleToRemove";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Ensure a VBA project and modules collection exist
            if (diagram.VbaProject != null && diagram.VbaProject.Modules != null)
            {
                int removeIndex = -1;

                // Locate the module by name (case‑insensitive)
                for (int i = 0; i < diagram.VbaProject.Modules.Count; i++)
                {
                    VbaModule module = diagram.VbaProject.Modules[i];
                    if (string.Equals(module.Name, moduleNameToDelete, StringComparison.OrdinalIgnoreCase))
                    {
                        removeIndex = i;
                        break;
                    }
                }

                if (removeIndex >= 0)
                {
                    // Delete the identified module
                    diagram.VbaProject.Modules.RemoveAt(removeIndex);
                    Console.WriteLine($"VBA module '{moduleNameToDelete}' has been removed.");
                }
                else
                {
                    Console.WriteLine($"VBA module '{moduleNameToDelete}' was not found.");
                }
            }
            else
            {
                Console.WriteLine("The diagram does not contain a VBA project or any modules.");
            }

            // Save the modified diagram using a macro‑enabled format
            diagram.Save(outputPath, SaveFileFormat.Vsdm);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
