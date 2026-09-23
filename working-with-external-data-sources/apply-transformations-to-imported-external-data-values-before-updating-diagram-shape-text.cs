using System;
using System.Data;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramDataTransformation <inputPath> <outputPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Simulate importing external data (e.g., from a database)
            DataTable externalData = new DataTable();
            externalData.Columns.Add("Value", typeof(double));

            // Sample data rows
            externalData.Rows.Add(10.5);
            externalData.Rows.Add(23.0);
            externalData.Rows.Add(7.75);

            // Transform the imported values (example: multiply by 2)
            List<double> transformedValues = new List<double>();
            foreach (DataRow row in externalData.Rows)
            {
                double original = Convert.ToDouble(row["Value"]);
                double transformed = original * 2.0;
                transformedValues.Add(transformed);
            }

            // Prepare the new text to be placed into the target shape
            string newText = string.Join(", ", transformedValues);

            // Locate the shape that should display the transformed data
            // This example looks for a shape with the universal name "DataShape"
            bool shapeFound = false;
            foreach (Page page in diagram.Pages)
            {
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals("DataShape", StringComparison.OrdinalIgnoreCase))
                    {
                        // Clear existing text
                        shape.Text.Value.Clear();

                        // Add the new transformed data as a single text run
                        shape.Text.Value.Add(new Txt(newText));

                        shapeFound = true;
                        break;
                    }
                }
                if (shapeFound) break;
            }

            if (!shapeFound)
            {
                Console.WriteLine("Target shape with NameU 'DataShape' not found. No text was updated.");
            }

            // Save the updated diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }