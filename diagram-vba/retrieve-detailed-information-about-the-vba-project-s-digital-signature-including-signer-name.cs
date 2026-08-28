using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Determine the input Visio file path (expects a macro-enabled .vsdm file)
            string filePath = args.Length > 0 ? args[0] : "input.vsdm";

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Access the VBA project associated with the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Retrieve signature information
            bool isSigned = vbaProject.IsSigned;
            string projectName = vbaProject.Name; // Project name may contain author information

            // Output the retrieved details
            Console.WriteLine($"VBA Project Name: {projectName}");
            Console.WriteLine($"Is Signed: {isSigned}");

            // Detailed signer information is not exposed by the Aspose.Diagram API.
            if (isSigned)
            {
                Console.WriteLine("Signature is present, but signer name cannot be retrieved via the current API.");
            }
            else
            {
                Console.WriteLine("The VBA project is not digitally signed.");
            }

        }
        catch (Aspose.Diagram.DiagramException ex)
        {
            Console.Error.WriteLine($"[DiagramException] {ex.Message}");
        }
    }
}
