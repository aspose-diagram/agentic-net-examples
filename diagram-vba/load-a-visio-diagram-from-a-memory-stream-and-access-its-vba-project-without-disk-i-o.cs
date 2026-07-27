using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Retrieve Visio file bytes (replace with actual source, e.g., embedded resource)
        byte[] visioData = GetVisioFileBytes();

        // Guard against empty or null data to prevent stream read errors
        if (visioData == null || visioData.Length == 0)
        {
            Console.Error.WriteLine("Visio data is empty or null.");
            return;
        }

        // Load the diagram from a memory stream (no disk I/O for the diagram itself)
        using (MemoryStream ms = new MemoryStream(visioData))
        {
            try
            {
                // Diagram constructor reads the stream; wrap in try/catch for safety
                using (Diagram diagram = new Diagram(ms))
                {
                    // Access the read‑only VBA project attached to the diagram
                    VbaProject vba = diagram.VbaProject;
                    Console.WriteLine($"VBA Project Name: {vba.Name}");
                    Console.WriteLine($"Is Signed: {vba.IsSigned}");

                    // Enumerate existing VBA modules
                    for (int i = 0; i < vba.Modules.Count; i++)
                    {
                        VbaModule module = vba.Modules[i];
                        Console.WriteLine($"Module {i}: {module.Name}");
                        Console.WriteLine("Code:");
                        Console.WriteLine(module.Codes);
                    }

                    // Add a new procedural VBA module
                    int newIndex = vba.Modules.Add(VbaModuleType.Procedural, "NewModule");
                    VbaModule newModule = vba.Modules[newIndex];
                    newModule.Codes =
                        "Attribute VB_Name = \"NewModule\"\n" +
                        "Sub HelloWorld()\n" +
                        "    MsgBox \"Hello from Aspose.Diagram\"\n" +
                        "End Sub";
                    Console.WriteLine("Added new module 'NewModule' with sample code.");
                }
            }
            catch (Exception ex)
            {
                // Log any Aspose or I/O errors
                Console.Error.WriteLine($"Error loading diagram or processing VBA: {ex.Message}");
            }
        }
    }

    // Placeholder: replace with actual logic to retrieve Visio file bytes
    static byte[] GetVisioFileBytes()
    {
        // Example returns an empty array; in practice, provide the .vsdx/.vsdm content.
        return new byte[0];
    }
}