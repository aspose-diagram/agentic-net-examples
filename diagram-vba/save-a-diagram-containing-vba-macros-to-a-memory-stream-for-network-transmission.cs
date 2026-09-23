using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.Vba;

class Program
{
    static void Main()
    {
        // Create a new diagram (empty Visio document)
        Diagram diagram = new Diagram();

        // Add a VBA procedural module to the diagram
        int moduleIndex = diagram.VbaProject.Modules.Add(VbaModuleType.Procedural, "MyModule");
        var vbaModule = diagram.VbaProject.Modules[moduleIndex];
        vbaModule.Codes = @"
Attribute VB_Name = ""MyModule""
Sub HelloWorld()
    MsgBox ""Hello from VBA!""
End Sub
";

        // Save the diagram (with macros) to a memory stream in macro-enabled format
        using (MemoryStream memoryStream = new MemoryStream())
        {
            diagram.Save(memoryStream, SaveFileFormat.Vsdm);
            memoryStream.Position = 0; // Reset stream position for reading/transmission

            // Example usage: output the size of the generated stream
            Console.WriteLine($"Saved diagram to memory stream. Size: {memoryStream.Length} bytes.");
        }
    }
}
