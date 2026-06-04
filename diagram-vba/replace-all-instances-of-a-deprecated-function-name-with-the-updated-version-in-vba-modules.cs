using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class ReplaceDeprecatedVbaFunction
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project contained in the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Define the deprecated function name and its replacement
            const string deprecatedFunction = "OldFunc";
            const string updatedFunction = "NewFunc";

            // Iterate through all VBA modules in the project
            foreach (VbaModule module in vbaProject.Modules)
            {
                // Get the source code of the current module
                string code = module.Codes;

                // If the code contains the deprecated function, replace it
                if (!string.IsNullOrEmpty(code) && code.Contains(deprecatedFunction))
                {
                    string updatedCode = code.Replace(deprecatedFunction, updatedFunction);
                    module.Codes = updatedCode; // Update the module with the new code
                }
            }

            // Save the modified diagram (replace with your desired output path)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
