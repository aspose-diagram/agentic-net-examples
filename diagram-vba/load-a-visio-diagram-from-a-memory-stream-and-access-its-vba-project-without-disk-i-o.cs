using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Retrieve Visio file bytes (replace with actual data source).
        byte[] visioData = GetVisioFileBytes();

        // Guard against empty or null byte array to prevent stream read errors.
        if (visioData == null || visioData.Length == 0)
        {
            Console.Error.WriteLine("Visio data is empty or null.");
            return;
        }

        // Load diagram from memory stream inside a try/catch to handle Aspose exceptions.
        try
        {
            using (MemoryStream stream = new MemoryStream(visioData))
            {
                // Construct Diagram from the memory stream.
                Diagram diagram = new Diagram(stream);

                // Access the read‑only VBA project.
                VbaProject vba = diagram.VbaProject;
                Console.WriteLine($"VBA Project Signed: {vba.IsSigned}");

                // List existing VBA modules.
                for (int i = 0; i < vba.Modules.Count; i++)
                {
                    VbaModule module = vba.Modules[i];
                    Console.WriteLine($"Module {i}: Name = {module.Name}");
                    Console.WriteLine("Code:");
                    Console.WriteLine(module.Codes);
                }

                // Add a new procedural VBA module.
                int newIndex = vba.Modules.Add(VbaModuleType.Procedural, "NewModule");
                VbaModule newModule = vba.Modules[newIndex];
                newModule.Codes = "Sub HelloWorld()\n    MsgBox \"Hello from Aspose.Diagram!\"\nEnd Sub";

                Console.WriteLine($"Added module '{newModule.Name}' with code:");
                Console.WriteLine(newModule.Codes);
            }
        }
        catch (Exception ex)
        {
            // Output any errors encountered during loading or VBA manipulation.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Placeholder method to obtain Visio file bytes.
    // Replace this implementation with actual byte retrieval logic (e.g., reading from a file, network, etc.).
    static byte[] GetVisioFileBytes()
    {
        // For demonstration, return an empty array.
        // In a real scenario, provide valid Visio file content.
        return new byte[0];
    }
}