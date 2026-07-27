using System;
using System.Linq;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Filter custom properties whose names start with "Dept"
                var deptProperties = diagram.DocumentProps.CustomProps
                    .Where(prop => !string.IsNullOrEmpty(prop.Name) && prop.Name.StartsWith("Dept"));

                // List the names and values of the filtered properties
                foreach (var prop in deptProperties)
                {
                    // Access the value via CustomValue.ValueString as per Aspose.Diagram API
                    string value = prop.CustomValue?.ValueString ?? string.Empty;
                    Console.WriteLine($"{prop.Name}: {value}");
                }

                // Save the diagram (optional, demonstrates lifecycle usage)
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }