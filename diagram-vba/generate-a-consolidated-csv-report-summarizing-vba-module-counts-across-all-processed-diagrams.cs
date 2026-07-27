using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
    {
        static void Main(string[] args)
        {
            // Expect a folder path as the first argument; if not provided, use current directory.
            string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            // Collect all Visio files (VSD, VSDX, VSDM, VDX, VDXM, VSS, VST, VTX, VTXM, VDW, VDX, etc.).
            // For simplicity, consider common extensions.
            string[] extensions = new[] { "*.vsd", "*.vsdx", "*.vsdm", "*.vdx", "*.vdw" };
            var diagramFiles = new List<string>();
            foreach (var ext in extensions)
            {
                diagramFiles.AddRange(Directory.GetFiles(folderPath, ext, SearchOption.AllDirectories));
            }

            // Prepare CSV output.
            var csvBuilder = new StringBuilder();
            csvBuilder.AppendLine("DiagramFile,TotalModules,ProceduralCount,DocumentCount,ClassCount,DesignerCount");

            foreach (var filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram using Aspose.Diagram.
                    using (var diagram = new Diagram(filePath))
                    {
                        // Access the VBA project; it may be null if no VBA is present.
                        var vbaProject = diagram.VbaProject;
                        int total = 0;
                        int procedural = 0;
                        int document = 0;
                        int @class = 0;
                        int designer = 0;

                        if (vbaProject != null && vbaProject.Modules != null)
                        {
                            foreach (VbaModule module in vbaProject.Modules)
                            {
                                total++;
                                switch (module.Type)
                                {
                                    case VbaModuleType.Procedural:
                                        procedural++;
                                        break;
                                    case VbaModuleType.Document:
                                        document++;
                                        break;
                                    case VbaModuleType.Class:
                                        @class++;
                                        break;
                                    case VbaModuleType.Designer:
                                        designer++;
                                        break;
                                }
                            }
                        }

                        // Escape commas in file path if any.
                        string escapedPath = $"\"{filePath.Replace("\"", "\"\"")}\"";
                        csvBuilder.AppendLine($"{escapedPath},{total},{procedural},{document},{@class},{designer}");
                    }
                }
                catch (Exception ex)
                {
                    // If a file cannot be processed, write a line with zeros and the error message.
                    string escapedPath = $"\"{filePath.Replace("\"", "\"\"")}\"";
                    csvBuilder.AppendLine($"{escapedPath},0,0,0,0,0");
                    Console.Error.WriteLine($"Failed to process '{filePath}': {ex.Message}");
                }
            }

            // Write CSV to output file named VbaModuleReport.csv in the same folder.
            string outputCsv = Path.Combine(folderPath, "VbaModuleReport.csv");
            File.WriteAllText(outputCsv, csvBuilder.ToString(), Encoding.UTF8);
            Console.WriteLine($"VBA module report generated: {outputCsv}");
        }
    }