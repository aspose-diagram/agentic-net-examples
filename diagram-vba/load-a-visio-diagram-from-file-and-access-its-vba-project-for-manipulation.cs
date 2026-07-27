using System;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (must be a format that supports VBA, e.g., .vsdx)
                string inputPath = "input.vsdx";

                // Output Visio file path (macro‑enabled format)
                string outputPath = "output.vsdm";

                try
                {
                    // Load the diagram from file
                    Diagram diagram = new Diagram(inputPath);

                    // Access the VBA project (read‑only property)
                    VbaProject vbaProject = diagram.VbaProject;

                    // Add a new procedural module named "MyModule"
                    int moduleIndex = vbaProject.Modules.Add(VbaModuleType.Procedural, "MyModule");

                    // Retrieve the newly added module
                    VbaModule vbaModule = vbaProject.Modules[moduleIndex];

                    // Set VBA code for the module
                    vbaModule.Codes = @"
                Attribute VB_Name = ""MyModule""
                Sub HelloWorld()
                MsgBox ""Hello from VBA!""
                End Sub
                ";

                    // Save the diagram in a macro‑enabled format to preserve the VBA project
                    diagram.Save(outputPath, SaveFileFormat.Vsdm);

                    Console.WriteLine("Diagram loaded, VBA module added, and saved successfully.");
                }
                catch (Exception ex)
                {
                    // Simple error handling
                    Console.WriteLine($"Error: {ex.Message}");
                    throw;
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }