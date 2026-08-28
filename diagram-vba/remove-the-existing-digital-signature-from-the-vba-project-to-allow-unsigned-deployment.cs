using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // If the VBA project exists and is signed, remove the macro (which also clears the signature)
            if (diagram.VbaProject != null && diagram.VbaProject.IsSigned)
            {
                diagram.RemoveMacro();
            }

            // Save the diagram without the digital signature
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
