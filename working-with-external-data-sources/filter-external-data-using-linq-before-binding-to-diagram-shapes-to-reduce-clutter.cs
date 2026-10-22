using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Simulated external data source.
                // In a real scenario this could be read from a database or file.
                // ------------------------------------------------------------
                List<Person> externalData = new List<Person>
                {
                    new Person { Name = "Alice Johnson", Department = "Sales" },
                    new Person { Name = "Bob Smith", Department = "Engineering" },
                    new Person { Name = "Carol White", Department = "Sales" },
                    new Person { Name = "David Brown", Department = "HR" }
                };

                // ------------------------------------------------------------
                // Filter the external data using LINQ to keep only Sales staff.
                // This reduces the amount of data bound to the diagram.
                // ------------------------------------------------------------
                List<Person> filteredData = externalData
                    .Where(p => string.Equals(p.Department, "Sales", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // ------------------------------------------------------------
                // Bind filtered data to shapes whose name starts with "DataPlaceholder".
                // Each matching shape receives the next person's name as its text.
                // ------------------------------------------------------------
                int dataIndex = 0;
                foreach (Aspose.Diagram.Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Ensure the shape has a name to evaluate.
                        if (shape.Name == null)
                            continue;

                        // Identify placeholder shapes by naming convention.
                        if (shape.Name.StartsWith("DataPlaceholder", StringComparison.OrdinalIgnoreCase))
                        {
                            // If we have run out of filtered data, stop binding.
                            if (dataIndex >= filteredData.Count)
                                break;

                            // Clear any existing text fragments.
                            if (shape.Text != null && shape.Text.Value != null)
                            {
                                shape.Text.Value.Clear();

                                // Add new text containing the person's name.
                                shape.Text.Value.Add(new Txt(filteredData[dataIndex].Name));
                            }

                            dataIndex++;
                        }
                    }

                    // If all filtered items have been bound, exit the outer loop as well.
                    if (dataIndex >= filteredData.Count)
                        break;
                }

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path.
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Simple POCO representing external data records.
    private class Person
    {
        public string Name { get; set; }
        public string Department { get; set; }
    }
}