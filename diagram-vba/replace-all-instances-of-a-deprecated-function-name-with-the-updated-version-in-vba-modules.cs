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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Define the deprecated function name and its replacement
            const string deprecatedFunction = "OldFunction";
            const string updatedFunction = "NewFunction";

            // Iterate through all VBA modules in the project
            foreach (VbaModule vbaModule in diagram.VbaProject.Modules)
            {
                // Ensure the module contains code before attempting replacement
                if (!string.IsNullOrEmpty(vbaModule.Codes))
                {
                    // Replace all occurrences of the deprecated function name
                    vbaModule.Codes = vbaModule.Codes.Replace(deprecatedFunction, updatedFunction);
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
