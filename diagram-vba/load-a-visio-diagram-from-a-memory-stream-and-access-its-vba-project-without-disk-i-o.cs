using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Obtain the Visio file bytes from any source (e.g., embedded resource, network stream)
        byte[] visioData = GetVisioFileBytes();

        if (visioData == null || visioData.Length == 0)
        {
            Console.Error.WriteLine("Visio data is empty.");
            return;
        }

        try
        {
            using (MemoryStream stream = new MemoryStream(visioData))
            {
                Diagram diagram = new Diagram(stream);

                // Access the VBA project
                var vbaProject = diagram.VbaProject;
                Console.WriteLine($"VBA project signed: {vbaProject.IsSigned}");

                // List existing VBA modules
                for (int i = 0; i < vbaProject.Modules.Count; i++)
                {
                    var module = vbaProject.Modules[i];
                    Console.WriteLine($"Module {i}: Name = {module.Name}, Type = {module.Type}");
                }

                // Add a new procedural VBA module
                int newModuleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "MyModule");
                var newModule = vbaProject.Modules[newModuleIndex];
                newModule.Codes = "Attribute VB_Name = \"MyModule\"\nSub Test()\n    MsgBox \"Hello from VBA\"\nEnd Sub";

                Console.WriteLine($"Added module '{newModule.Name}' with code length {newModule.Codes.Length}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    // Placeholder method to provide Visio file bytes.
    static byte[] GetVisioFileBytes()
    {
        // Replace this with actual byte retrieval logic.
        return new byte[0];
    }
}