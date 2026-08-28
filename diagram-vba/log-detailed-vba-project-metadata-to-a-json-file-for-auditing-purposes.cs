using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

namespace VbaMetadataAuditor
{
    // DTO classes for JSON serialization
    public class VbaMetadata
    {
        public string ProjectName { get; set; }
        public bool IsSigned { get; set; }
        public List<ModuleInfo> Modules { get; set; } = new();
        public List<ReferenceInfo> References { get; set; } = new();
    }

    public class ModuleInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Code { get; set; }
    }

    public class ReferenceInfo
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
            // Validate arguments: input diagram path and output JSON path
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: VbaMetadataAuditor <input-diagram> <output-json>");
                return;
            }

            string diagramPath = args[0];
            string jsonPath = args[1];

            // Load the Visio diagram using Aspose.Diagram (lifecycle rule)
            Diagram diagram = new Diagram(diagramPath);

            // Access the VBA project; may be null if no VBA project exists
            VbaProject vbaProject = diagram.VbaProject;
            if (vbaProject == null)
            {
                Console.WriteLine("No VBA project found in the diagram.");
                return;
            }

            // Build metadata object
            VbaMetadata metadata = new VbaMetadata
            {
                ProjectName = vbaProject.Name,
                IsSigned = vbaProject.IsSigned
            };

            // Extract modules
            foreach (VbaModule module in vbaProject.Modules)
            {
                metadata.Modules.Add(new ModuleInfo
                {
                    Name = module.Name,
                    Type = module.Type.ToString(),
                    Code = module.Codes // full VBA source code
                });
            }

            // Extract references
            foreach (VbaProjectReference reference in vbaProject.References)
            {
                metadata.References.Add(new ReferenceInfo
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
            string json = JsonSerializer.Serialize(metadata, jsonOptions);

            // Write JSON to the specified file
            File.WriteAllText(jsonPath, json);

            Console.WriteLine($"VBA metadata successfully written to '{jsonPath}'.");
        }
    }
}