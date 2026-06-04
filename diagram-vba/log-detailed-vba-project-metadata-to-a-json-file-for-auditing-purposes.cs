using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

namespace VbaProjectAuditor
{
    // DTO classes for JSON serialization
    public class VbaProjectInfo
    {
        public string Name { get; set; }
        public bool IsSigned { get; set; }
        public List<VbaModuleInfo> Modules { get; set; }
        public List<VbaReferenceInfo> References { get; set; }
    }

    public class VbaModuleInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
    }

    public class VbaReferenceInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Libid { get; set; }
        public string ExtendedLibid { get; set; }
        public string RelativeLibid { get; set; }
        public string TwiddledLibid { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string visioFilePath = "input.vsdx";

                // Output JSON file path
                string jsonOutputPath = "VbaProjectMetadata.json";

                // Load the Visio diagram using Aspose.Diagram (load rule)
                Diagram diagram = new Diagram(visioFilePath);

                // Access the VBA project
                VbaProject vbaProject = diagram.VbaProject;

                // Prepare DTO for serialization
                VbaProjectInfo projectInfo = new VbaProjectInfo
                {
                    Name = vbaProject.Name,
                    IsSigned = vbaProject.IsSigned,
                    Modules = new List<VbaModuleInfo>(),
                    References = new List<VbaReferenceInfo>()
                };

                // Extract modules information
                foreach (VbaModule module in vbaProject.Modules)
                {
                    projectInfo.Modules.Add(new VbaModuleInfo
                    {
                        Name = module.Name,
                        Type = module.Type.ToString(),
                        Code = module.Codes
                    });
                }

                // Extract references information
                foreach (VbaProjectReference reference in vbaProject.References)
                {
                    projectInfo.References.Add(new VbaReferenceInfo
                    {
                        Name = reference.Name,
                        Type = reference.Type.ToString(),
                        Libid = reference.Libid,
                        ExtendedLibid = reference.ExtendedLibid,
                        RelativeLibid = reference.RelativeLibid,
                        TwiddledLibid = reference.Twiddledlibid
                    });
                }

                // Serialize to JSON with indentation for readability
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(projectInfo, jsonOptions);

                // Write JSON to file (save rule for JSON, not diagram)
                File.WriteAllText(jsonOutputPath, jsonString);

                Console.WriteLine($"VBA project metadata has been saved to '{jsonOutputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}