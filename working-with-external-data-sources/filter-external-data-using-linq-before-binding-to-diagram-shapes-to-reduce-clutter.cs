using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    // Simple data model representing external data that will be bound to diagram shapes.
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }

    static void Main(string[] args)
    {
        // Paths to the input Visio file and the output file.
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        // --------------------------------------------------------------------
        // Simulate external data source (e.g., a database or CSV file).
        // In a real scenario this data would be loaded from an external system.
        // --------------------------------------------------------------------
        List<Employee> externalData = new List<Employee>
        {
            new Employee { Id = 1, Name = "Alice", Department = "HR" },
            new Employee { Id = 2, Name = "Bob", Department = "Finance" },
            new Employee { Id = 3, Name = "Charlie", Department = "HR" },
            new Employee { Id = 4, Name = "Diana", Department = "IT" }
        };

        // --------------------------------------------------------------------
        // Filter the external data using LINQ to keep only the HR department.
        // This reduces the amount of data that will be bound to the diagram.
        // --------------------------------------------------------------------
        List<Employee> filteredData = externalData
            .Where(emp => string.Equals(emp.Department, "HR", StringComparison.OrdinalIgnoreCase))
            .ToList();

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Work with the first page of the diagram.
                Page page = diagram.Pages[0];

                // Iterate over all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked for deletion.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has a non‑empty name before attempting to match.
                    if (string.IsNullOrEmpty(shape.Name))
                        continue;

                    // Find a matching employee based on the shape's name.
                    Employee match = filteredData
                        .FirstOrDefault(emp => string.Equals(emp.Name, shape.Name, StringComparison.OrdinalIgnoreCase));

                    if (match != null)
                    {
                        // Clear any existing text.
                        shape.Text.Value.Clear();

                        // Add new text containing the employee's name and department.
                        shape.Text.Value.Add(new Txt($"{match.Name} ({match.Department})"));
                    }
                }

                // Save the modified diagram to the output file.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}