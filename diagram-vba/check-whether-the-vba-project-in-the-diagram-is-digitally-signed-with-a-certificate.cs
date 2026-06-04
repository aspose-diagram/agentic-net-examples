using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from file
            Diagram diagram = new Diagram("input.vsdx");

            // Determine whether the VBA project is digitally signed
            bool isSigned = false;
            if (diagram.VbaProject != null)
            {
                isSigned = diagram.VbaProject.IsSigned;
            }

            // Output the result
            Console.WriteLine($"VBA project signed: {isSigned}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
