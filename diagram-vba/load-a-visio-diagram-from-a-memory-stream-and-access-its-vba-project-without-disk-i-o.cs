using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Obtain Visio file bytes (replace with actual data in real scenario)
        byte[] visioBytes = GetSampleVisioBytes();

        // Guard against empty or null byte array to avoid stream read errors
        if (visioBytes == null || visioBytes.Length == 0)
        {
            Console.Error.WriteLine("Visio byte array is empty or null.");
            return;
        }

        // Load the diagram from a memory stream (no disk I/O)
        using (MemoryStream inputStream = new MemoryStream(visioBytes))
        {
            Diagram diagram;
            try
            {
                // Attempt to construct Diagram from the stream
                diagram = new Diagram(inputStream);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Access the VBA project (read‑only property)
            VbaProject vbaProject = diagram.VbaProject;

            // Output basic VBA project information
            Console.WriteLine($"VBA Project Name: {vbaProject.Name}");
            Console.WriteLine($"Is Signed: {vbaProject.IsSigned}");
            Console.WriteLine($"Number of Modules: {vbaProject.Modules.Count}");

            // List existing modules
            for (int i = 0; i < vbaProject.Modules.Count; i++)
            {
                VbaModule module = vbaProject.Modules[i];
                Console.WriteLine($"Module {i}: Name = {module.Name}");
                Console.WriteLine($"Code:\n{module.Codes}");
            }

            // Add a new procedural module
            int newModuleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "MyNewModule");
            VbaModule newModule = vbaProject.Modules[newModuleIndex];
            newModule.Codes = @"
Attribute VB_Name = ""MyNewModule""
Public Sub HelloWorld()
    MsgBox ""Hello from VBA!""
End Sub
";

            Console.WriteLine($"Added new module: {newModule.Name}");

            // (Optional) Save the modified diagram to a memory stream in macro‑enabled format
            using (MemoryStream outputStream = new MemoryStream())
            {
                try
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdm);
                    Console.WriteLine($"Diagram saved to memory stream. Size: {outputStream.Length} bytes");
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
                }
            }
        }
    }

    // Placeholder method to provide sample Visio file bytes.
    // In practice, replace this with actual byte content.
    static byte[] GetSampleVisioBytes()
    {
        // Return an empty array for demonstration; real implementation should supply valid Visio data.
        return new byte[0];
    }
}