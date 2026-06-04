using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaModuleReport
{
    static void Main(string[] args)
    {
        try
        {

            // List of diagram files to process. Replace with actual paths or obtain from args.
            string[] diagramFiles = new string[]
            {
                "Diagram1.vsdx",
                "Diagram2.vsdx"
                // Add more diagram file paths as needed.
            };

            // Path for the consolidated CSV report.
            string csvPath = "VbaModuleReport.csv";

            // Create a StreamWriter for the CSV file.
            using (var writer = new StreamWriter(csvPath))
            {
                // Write CSV header.
                writer.WriteLine("DiagramFile,TotalModules,Procedural,Document,Class,Designer");

                // Process each diagram.
                foreach (var filePath in diagramFiles)
                {
                    // Load the diagram using the Diagram(string) constructor.
                    Diagram diagram = new Diagram(filePath);

                    // Initialize module counters.
                    int totalModules = 0;
                    int proceduralCount = 0;
                    int documentCount = 0;
                    int classCount = 0;
                    int designerCount = 0;

                    // Ensure the diagram contains a VBA project.
                    if (diagram.VbaProject != null && diagram.VbaProject.Modules != null)
                    {
                        // Iterate through all VBA modules.
                        foreach (VbaModule module in diagram.VbaProject.Modules)
                        {
                            totalModules++;

                            // Increment the counter based on the module type.
                            switch (module.Type)
                            {
                                case VbaModuleType.Procedural:
                                    proceduralCount++;
                                    break;
                                case VbaModuleType.Document:
                                    documentCount++;
                                    break;
                                case VbaModuleType.Class:
                                    classCount++;
                                    break;
                                case VbaModuleType.Designer:
                                    designerCount++;
                                    break;
                            }
                        }
                    }

                    // Write the counts for the current diagram to the CSV.
                    writer.WriteLine($"{Path.GetFileName(filePath)},{totalModules},{proceduralCount},{documentCount},{classCount},{designerCount}");
                }
            }

            Console.WriteLine($"CSV report generated at: {Path.GetFullPath(csvPath)}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
