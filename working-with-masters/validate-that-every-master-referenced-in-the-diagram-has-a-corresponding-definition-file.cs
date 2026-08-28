using System;
using System.IO;
using Aspose.Diagram;

class MasterValidator
{
    static void Main()
    {
        try
        {

            // Load the diagram (lifecycle rule)
            Diagram diagram = new Diagram("input.vdx");

            // Folder containing master definition files (e.g., .vss or .vst)
            string mastersFolder = @"C:\Masters";

            // Iterate through each master referenced in the diagram
            foreach (Master master in diagram.Masters)
            {
                // Assume definition file name matches the master name with .vss extension
                string definitionPath = Path.Combine(mastersFolder, master.Name + ".vss");

                // Validate existence of the definition file
                if (!File.Exists(definitionPath))
                {
                    Console.WriteLine($"Missing master definition file for master: {master.Name}");
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
