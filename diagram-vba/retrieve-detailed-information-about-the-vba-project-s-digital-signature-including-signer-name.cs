using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram that contains a VBA project
            Diagram diagram = new Diagram("input.vsdx");

            // Access the VBA project associated with the diagram
            var vbaProject = diagram.VbaProject;

            // Determine whether the VBA project is digitally signed
            bool isSigned = vbaProject.IsSigned;
            Console.WriteLine($"VBA Project Signed: {isSigned}");

            // Retrieve the name of the VBA project (often reflects the signer or project owner)
            string projectName = vbaProject.Name;
            Console.WriteLine($"VBA Project Name: {projectName}");

            // If the project is signed, the raw VBA project data (MIME‑encoded) is available.
            // This data can be inspected or saved for further analysis of the signature.
            if (isSigned && diagram.VbProjectData != null && diagram.VbProjectData.Length > 0)
            {
                // Example: display the VBA project data as a Base64 string
                string vbProjectBase64 = Convert.ToBase64String(diagram.VbProjectData);
                Console.WriteLine($"VbProjectData (Base64): {vbProjectBase64}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
