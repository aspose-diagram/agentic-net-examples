using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // External data source: field name -> value
                Dictionary<string, string> externalData = new Dictionary<string, string>
                {
                    { "CustomerName", "Acme Corp" },
                    { "OrderId", "12345" },
                    { "Amount", "$1,000" }
                };

                // Mapping: external field name -> shape data field (Data1, Data2, Data3)
                Dictionary<string, string> fieldToShapeData = new Dictionary<string, string>
                {
                    { "CustomerName", "Data1" },
                    { "OrderId", "Data2" },
                    { "Amount", "Data3" }
                };

                // Apply the mapping to every shape on every page
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate over each mapping entry
                        foreach (KeyValuePair<string, string> mapEntry in fieldToShapeData)
                        {
                            string externalField = mapEntry.Key;
                            string shapeDataField = mapEntry.Value;

                            // Ensure the external data contains the field
                            if (!externalData.TryGetValue(externalField, out string externalValue))
                                continue; // No value to assign

                            // Assign the value to the appropriate DataX property
                            switch (shapeDataField)
                            {
                                case "Data1":
                                    shape.Data1 = externalValue;
                                    break;
                                case "Data2":
                                    shape.Data2 = externalValue;
                                    break;
                                case "Data3":
                                    shape.Data3 = externalValue;
                                    break;
                                default:
                                    // Unknown Data field – ignore or handle as needed
                                    break;
                            }
                        }
                    }
                }

                // Save the updated diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }