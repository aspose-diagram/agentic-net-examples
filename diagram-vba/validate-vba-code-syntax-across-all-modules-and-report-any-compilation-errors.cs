using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project contained in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Iterate through all VBA modules in the project
            foreach (VbaModule module in vbaProject.Modules)
            {
                Console.WriteLine($"Module Name: {module.Name}");
                string code = module.Codes;

                // Validate the VBA code syntax.
                // Aspose.Diagram does not provide a direct compile method,
                // so this placeholder represents where validation logic would be invoked.
                List<string> errors = ValidateVbaCode(code);

                if (errors.Count == 0)
                {
                    Console.WriteLine("  No compilation errors.");
                }
                else
                {
                    foreach (string error in errors)
                    {
                        Console.WriteLine($"  Error: {error}");
                    }
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Placeholder method for VBA syntax validation.
    // Replace with actual validation implementation if available.
    static List<string> ValidateVbaCode(string code)
    {
        // Example stub: always returns an empty error list (no errors).
        // Implement real parsing/compilation checks as needed.
        return new List<string>();
    }
}
