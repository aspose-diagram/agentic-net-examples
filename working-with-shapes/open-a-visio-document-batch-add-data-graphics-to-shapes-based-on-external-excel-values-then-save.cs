using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input Visio file path and input CSV (Excel) file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <CsvFilePath>");
            return;
        }

        string visioPath = args[0];
        string csvPath = args[1];
        string outputPath = Path.Combine(Path.GetDirectoryName(visioPath) ?? "", "output.vsdx");

        // Guard against missing files
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Load external data from CSV into a dictionary (key: shape NameU, value: data value)
        Dictionary<string, string> shapeDataMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        try
        {
            foreach (string line in File.ReadAllLines(csvPath))
            {
                // Skip empty lines
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                // Expect format: ShapeName,Value
                string[] parts = line.Split(new[] { ',' }, 2);
                if (parts.Length != 2)
                    continue; // malformed line, ignore

                string shapeName = parts[0].Trim();
                string value = parts[1].Trim();
                if (!shapeDataMap.ContainsKey(shapeName))
                    shapeDataMap.Add(shapeName, value);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading CSV file: {ex.Message}");
            return;
        }

        // Open the Visio document and apply data graphics
        try
        {
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Iterate over all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Match shape by its NameU property
                        if (!string.IsNullOrEmpty(shape.NameU) && shapeDataMap.TryGetValue(shape.NameU, out string dataValue))
                        {
                            // Add a custom property named "DataValue" with the external value
                            Prop prop = new Prop();
                            prop.Name = "DataValue";
                            prop.Label.Value = "Data Value";
                            prop.Value.Val = dataValue;
                            prop.Type.Value = TypePropValue.String;
                            shape.Props.Add(prop);

                            // Example: set a red fill if the value contains "Critical"
                            if (dataValue.IndexOf("Critical", StringComparison.OrdinalIgnoreCase) >= 0)
                            {
                                // FillForegnd is a cell that accepts a color string like "#FF0000"
                                shape.Fill.FillForegnd.Value = "#FF0000";
                            }
                        }
                    }
                }

                // Save the modified diagram to a new file (VSDX format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing Visio document: {ex.Message}");
        }
    }
}