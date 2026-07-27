using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class VbaProjectAuditor
{
    static void Main(string[] args)
    {
        try
        {

            // Paths for input Visio file and output JSON audit file
            string visioPath = "input.vsdx";
            string jsonPath = "vba_audit.json";

            // Load the Visio diagram (uses Aspose.Diagram's load rule)
            Diagram diagram = new Diagram(visioPath);

            // Access the VBA project within the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Build an anonymous object containing all required metadata
            var auditData = new
            {
                ProjectName = vbaProject.Name,
                IsSigned = vbaProject.IsSigned,
                Modules = ExtractModules(vbaProject),
                References = ExtractReferences(vbaProject)
            };

            // Serialize the metadata to formatted JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(auditData, jsonOptions);

            // Write the JSON to the specified file (uses standard .NET file I/O)
            File.WriteAllText(jsonPath, json);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper to collect module information
    static List<object> ExtractModules(VbaProject project)
    {
        var modules = new List<object>();
        foreach (VbaModule module in project.Modules)
        {
            modules.Add(new
            {
                Name = module.Name,
                Type = module.Type.ToString(),
                Code = module.Codes
            });
        }
        return modules;
    }

    // Helper to collect reference information
    static List<object> ExtractReferences(VbaProject project)
    {
        var references = new List<object>();
        foreach (VbaProjectReference reference in project.References)
        {
            references.Add(new
            {
                Name = reference.Name,
                Type = reference.Type.ToString(),
                Libid = reference.Libid,
                ExtendedLibid = reference.ExtendedLibid,
                RelativeLibid = reference.RelativeLibid,
                TwiddledLibid = reference.Twiddledlibid
            });
        }
        return references;
    }
}
