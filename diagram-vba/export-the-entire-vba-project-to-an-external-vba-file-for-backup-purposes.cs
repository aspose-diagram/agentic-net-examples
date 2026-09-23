using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio file containing the VBA project
                string visioPath = "input.vsdm";

                // Path where the exported VBA code will be saved
                string vbaExportPath = "backup.vba";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Access the VBA project
                var vbaProject = diagram.VbaProject;

                // Export all VBA modules to a single .vba file
                using (StreamWriter writer = new StreamWriter(vbaExportPath, false, Encoding.UTF8))
                {
                    foreach (VbaModule module in vbaProject.Modules)
                    {
                        writer.WriteLine($"'--- Module: {module.Name} ---");
                        writer.WriteLine(module.Codes);
                        writer.WriteLine(); // Add an empty line between modules
                    }
                }

                Console.WriteLine($"VBA project exported successfully to '{vbaExportPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }