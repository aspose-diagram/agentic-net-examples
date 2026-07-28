using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramDataMapper
{
    // Simple data model representing the expected schema.
    public class Person
    {
        // Name may be null after deserialization; validation will enforce non‑null.
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input JSON file path and output Visio file path.
            if (args.Length != 2)
            {
                Console.Error.WriteLine("Usage: DiagramDataMapper <inputJsonPath> <outputVisioPath>");
                return;
            }

            string jsonPath = args[0];
            string outputPath = args[1];

            // Guard: ensure the input JSON file exists.
            if (!File.Exists(jsonPath))
            {
                Console.Error.WriteLine($"Input file not found: {jsonPath}");
                return;
            }

            // Read and deserialize JSON.
            string jsonContent = File.ReadAllText(jsonPath);
            Person[]? persons;
            try
            {
                persons = JsonSerializer.Deserialize<Person[]>(jsonContent);
                if (persons == null)
                {
                    Console.Error.WriteLine("JSON deserialization returned null.");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to parse JSON: {ex.Message}");
                return;
            }

            // Validate each record against the schema.
            foreach (var person in persons)
            {
                if (string.IsNullOrWhiteSpace(person.Name))
                {
                    Console.Error.WriteLine("Validation error: Name is required.");
                    return;
                }

                if (person.Age < 0)
                {
                    Console.Error.WriteLine($"Validation error: Age cannot be negative (Name: {person.Name}).");
                    return;
                }
            }

            // Wrap Aspose operations in a try/catch block.
            try
            {
                // Create a new empty diagram.
                Diagram diagram = new Diagram();

                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    diagram.Pages.Add(new Page());
                }

                Page page = diagram.Pages[0];

                // Layout parameters.
                double startX = 2.0;
                double startY = 2.0;
                double offsetY = 2.0;

                // Add a shape for each person.
                for (int i = 0; i < persons.Length; i++)
                {
                    double pinX = startX;
                    double pinY = startY + i * offsetY;

                    // Add a rectangle shape (master name "Rectangle").
                    long shapeId = page.AddShape(pinX, pinY, "Rectangle");
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Clear any existing text and add new text.
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt($"{persons[i].Name}, Age {persons[i].Age}"));
                }

                // Save the diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Aspose operation failed: {ex.Message}");
            }
        }
    }
}