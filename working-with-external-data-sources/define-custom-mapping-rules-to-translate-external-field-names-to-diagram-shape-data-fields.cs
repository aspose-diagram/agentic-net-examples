using System;
using System.IO;
using Aspose.Diagram;

namespace DiagramDataMapping
{
    // Represents a mapping from external field names to shape data fields (Data1, Data2, Data3)
    public class DataFieldMapper
    {
        // Mapping: external field name -> target data field index (1, 2, or 3)
        private readonly System.Collections.Generic.Dictionary<string, int> _fieldToDataIndex;

        public DataFieldMapper(System.Collections.Generic.Dictionary<string, int> fieldToDataIndex)
        {
            if (fieldToDataIndex == null)
                throw new ArgumentNullException(nameof(fieldToDataIndex));

            _fieldToDataIndex = fieldToDataIndex;
        }

        // Apply external data values to a shape according to the mapping
        public void ApplyMapping(Shape shape, System.Collections.Generic.Dictionary<string, string> externalData)
        {
            if (shape == null)
                throw new ArgumentNullException(nameof(shape));
            if (externalData == null)
                throw new ArgumentNullException(nameof(externalData));

            foreach (System.Collections.Generic.KeyValuePair<string, int> mapping in _fieldToDataIndex)
            {
                string externalField = mapping.Key;
                int dataIndex = mapping.Value;

                if (!externalData.TryGetValue(externalField, out string value))
                    continue; // No external value for this field; skip

                // Assign the value to the appropriate Data property
                switch (dataIndex)
                {
                    case 1:
                        shape.Data1 = value;
                        break;
                    case 2:
                        shape.Data2 = value;
                        break;
                    case 3:
                        shape.Data3 = value;
                        break;
                    default:
                        // Invalid data index; ignore or throw
                        throw new Exception($"Invalid data index {dataIndex} for field '{externalField}'.");
                }
            }
        }
    }

    public class Program
    {
        public static void Main()
        {
            // Path to the source Visio diagram
            string inputPath = "input.vsdx";
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                return;
            }

            // Path to the output Visio diagram after mapping
            string outputPath = "output.vsdx";

            // Define how external field names map to shape data fields
            var fieldToDataIndex = new System.Collections.Generic.Dictionary<string, int>
            {
                { "CustomerName", 1 },   // Map to Data1
                { "OrderNumber", 2 },    // Map to Data2
                { "OrderDate",   3 }     // Map to Data3
            };

            // Example external data (in a real scenario this could come from a database, CSV, etc.)
            var externalData = new System.Collections.Generic.Dictionary<string, string>
            {
                { "CustomerName", "Acme Corp" },
                { "OrderNumber",  "12345" },
                { "OrderDate",    "2023-08-15" }
            };

            // Create the mapper instance
            var mapper = new DataFieldMapper(fieldToDataIndex);

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Apply the mapping to each shape
                        mapper.ApplyMapping(shape, externalData);
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Propagate any errors
                throw new Exception("An error occurred while processing the diagram: " + ex.Message, ex);
            }
        }
    }
}