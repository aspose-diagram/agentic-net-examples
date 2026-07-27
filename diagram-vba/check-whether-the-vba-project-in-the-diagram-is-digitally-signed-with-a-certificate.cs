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

            // Load the Visio diagram (provide the correct file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Get the VBA project associated with the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Determine whether the VBA project is digitally signed
            bool isSigned = vbaProject.IsSigned;

            // Output the result
            Console.WriteLine($"VBA project signed: {isSigned}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
