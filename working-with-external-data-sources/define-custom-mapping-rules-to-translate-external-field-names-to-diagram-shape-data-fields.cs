using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Define mapping: external field name -> shape data field (Data1, Data2, Data3)
                var fieldMapping = new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
                {
                    { "CustomerID", "Data1" },
                    { "OrderNumber", "Data2" },
                    { "Status", "Data3" }
                };

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example: assume external data is stored in shape.NameU as "CustomerID=123;OrderNumber=456;Status=Open"
                        // Parse the name string into key/value pairs
                        if (string.IsNullOrWhiteSpace(shape.NameU))
                            continue;

                        var pairs = shape.NameU.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var pair in pairs)
                        {
                            var kv = pair.Split(new[] { '=' }, 2);
                            if (kv.Length != 2)
                                continue;

                            string externalName = kv[0].Trim();
                            string externalValue = kv[1].Trim();

                            if (fieldMapping.TryGetValue(externalName, out string targetDataField))
                            {
                                // Assign the value to the appropriate Data field
                                switch (targetDataField)
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
                                        // If an unsupported field is specified, ignore it
                                        break;
                                }
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }