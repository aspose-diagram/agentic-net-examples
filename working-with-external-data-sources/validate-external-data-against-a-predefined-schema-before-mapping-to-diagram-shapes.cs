using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Aspose.Diagram;

namespace DiagramDataMapper
{
    // Simple data model representing the expected schema
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the external JSON data file
                string jsonPath = args.Length > 0 ? args[0] : "data.json";

                // Path to the Visio template file
                string templatePath = "template.vsdx";

                // Output diagram file
                string outputPath = "output.vsdx";

                // Load and validate external data
                Person person = LoadAndValidateJson(jsonPath);

                // Load the Visio diagram template
                Diagram diagram = new Diagram(templatePath);

                // Find the target shape by its universal name (NameU)
                Shape targetShape = FindShapeByNameU(diagram, "DataShape");
                if (targetShape == null)
                    throw new Exception("Target shape with NameU 'DataShape' not found in the diagram.");

                // Update the shape's text with the validated data
                UpdateShapeText(targetShape, person);

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Loads JSON from file, deserializes into Person, and validates required fields
        private static Person LoadAndValidateJson(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"JSON data file not found: {path}");

            string jsonContent = File.ReadAllText(path);
            Person person;
            try
            {
                person = JsonSerializer.Deserialize<Person>(jsonContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to parse JSON data.", ex);
            }

            // Basic schema validation
            var validationErrors = new List<string>();

            if (person == null)
                validationErrors.Add("JSON does not represent a valid Person object.");
            else
            {
                if (string.IsNullOrWhiteSpace(person.Name))
                    validationErrors.Add("Name is required and cannot be empty.");

                if (person.Age <= 0)
                    validationErrors.Add("Age must be a positive integer.");

                if (string.IsNullOrWhiteSpace(person.Email) || !person.Email.Contains("@"))
                    validationErrors.Add("Email is required and must contain '@'.");
            }

            if (validationErrors.Count > 0)
                throw new Exception("Data validation failed: " + string.Join(" ", validationErrors));

            return person;
        }

        // Searches all pages for a shape with the specified universal name (NameU)
        private static Shape FindShapeByNameU(Diagram diagram, string nameU)
        {
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU != null && shape.NameU.Equals(nameU, StringComparison.OrdinalIgnoreCase))
                        return shape;
                }
            }
            return null;
        }

        // Clears existing text and adds new text based on the Person data
        private static void UpdateShapeText(Shape shape, Person person)
        {
            // Clear any existing text runs
            shape.Text.Value.Clear();

            // Construct the display text
            string displayText = $"{person.Name}, Age: {person.Age}, Email: {person.Email}";

            // Add the new text run
            shape.Text.Value.Add(new Txt(displayText));
        }
    }
}