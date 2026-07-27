using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file (can be .vsdx, .vsdm, etc.)
                string filePath = "input.vsdm";

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Access the VBA project
                VbaProject vbaProject = diagram.VbaProject;

                // Retrieve signature status
                bool isSigned = vbaProject.IsSigned;

                // Retrieve the VBA project name (there is no dedicated signer name property)
                string projectName = vbaProject.Name;

                // Output the information
                Console.WriteLine("VBA Project Information:");
                Console.WriteLine($"Project Name : {projectName}");
                Console.WriteLine($"Is Signed    : {isSigned}");

                // If the project is signed, additional signer details are not exposed via the API.
                if (isSigned)
                {
                    Console.WriteLine("Signer name information is not available through Aspose.Diagram API.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }