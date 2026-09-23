using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram file (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Verify that a VBA project exists and check its digital signature status
            bool isSigned = diagram.VbaProject != null && diagram.VbaProject.IsSigned;

            // Output the result
            if (isSigned)
            {
                Console.WriteLine("The VBA project is digitally signed.");
            }
            else
            {
                Console.WriteLine("The VBA project is not digitally signed.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
