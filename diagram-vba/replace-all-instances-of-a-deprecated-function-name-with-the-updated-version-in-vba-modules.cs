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

            // Load the Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Get the VBA project associated with the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Define the deprecated function name and its replacement
            string deprecatedFunction = "OldFunction";   // <-- deprecated name
            string updatedFunction = "NewFunction";     // <-- new name

            // Iterate through each VBA module in the project
            foreach (VbaModule module in vbaProject.Modules)
            {
                // Ensure the module contains code before attempting replacement
                if (!string.IsNullOrEmpty(module.Codes))
                {
                    // Replace all occurrences of the deprecated function name
                    module.Codes = module.Codes.Replace(deprecatedFunction, updatedFunction);
                }
            }

            // Save the modified diagram (replace with desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
