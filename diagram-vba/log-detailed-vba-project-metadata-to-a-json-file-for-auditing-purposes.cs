using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Vba;
using Aspose.Diagram.Properties;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (must be macro-enabled to contain VBA)
        string inputPath = "input.vsdm";
        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Path for the audit JSON output
        string outputPath = "audit.json";

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Access the VBA project associated with the diagram
            VbaProject vbaProject = diagram.VbaProject;

            // Collect VBA module metadata without using LINQ (VbaModuleCollection is not IEnumerable)
            var vbaModules = new List<object>();
            foreach (VbaModule module in vbaProject.Modules)
            {
                vbaModules.Add(new
                {
                    Name = module.Name,
                    Code = module.Codes
                });
            }

            // Assemble VBA project information
            var vbaInfo = new
            {
                ProjectName = vbaProject.Name,
                IsSigned = vbaProject.IsSigned,
                Modules = vbaModules
            };

            // Access document properties
            DocumentProperties docProps = diagram.DocumentProps;

            // Collect custom document properties without LINQ
            var customProps = new List<object>();
            foreach (CustomProp cp in docProps.CustomProps)
            {
                customProps.Add(new
                {
                    Name = cp.Name,
                    Type = cp.PropType.ToString(),
                    Value = cp.CustomValue?.ValueString
                });
            }

            // Assemble built‑in document property information
            var documentInfo = new
            {
                Title = docProps.Title,
                Subject = docProps.Subject,
                Creator = docProps.Creator,
                TimeCreated = docProps.TimeCreated,
                TimeEdited = docProps.TimeEdited,
                BuildNumberCreated = docProps.BuildNumberCreated,
                BuildNumberEdited = docProps.BuildNumberEdited,
                CustomProperties = customProps
            };

            // Combine all audit information into a single object
            var auditData = new
            {
                DiagramVersion = diagram.Version,
                VbaProject = vbaInfo,
                DocumentProperties = documentInfo
            };

            // Serialize the audit data to formatted JSON
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(auditData, jsonOptions);

            // Write the JSON output to the specified file
            File.WriteAllText(outputPath, json);

            Console.WriteLine($"Audit data written to {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}