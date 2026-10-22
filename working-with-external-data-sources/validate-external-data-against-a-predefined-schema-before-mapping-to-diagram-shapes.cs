using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    // Simple data model matching the expected external schema
    class DataItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
    }

    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // Validate command‑line arguments
        // ------------------------------------------------------------
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramPath> <DataJsonPath>");
            return;
        }

        string diagramPath = args[0];
        string dataPath = args[1];

        // ------------------------------------------------------------
        // Guard input files existence
        // ------------------------------------------------------------
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(dataPath))
        {
            Console.Error.WriteLine($"Data file not found: {dataPath}");
            return;
        }

        // ------------------------------------------------------------
        // Load external data and validate against the schema
        // ------------------------------------------------------------
        List<DataItem> dataItems;
        try
        {
            string json = File.ReadAllText(dataPath);
            dataItems = JsonSerializer.Deserialize<List<DataItem>>(json);
            if (dataItems == null)
            {
                Console.Error.WriteLine("Failed to deserialize data file.");
                return;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading or parsing data file: {ex.Message}");
            return;
        }

        // ------------------------------------------------------------
        // Prepare a list of valid items
        // ------------------------------------------------------------
        List<DataItem> validItems = new List<DataItem>();
        foreach (DataItem item in dataItems)
        {
            bool isValid = true;

            // Id must be positive
            if (item.Id <= 0)
            {
                Console.Error.WriteLine($"Invalid Id ({item.Id}) for item with Name '{item.Name}'.");
                isValid = false;
            }

            // Name must be non‑empty
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                Console.Error.WriteLine($"Name is required for item with Id {item.Id}.");
                isValid = false;
            }

            // X and Y must be finite numbers
            if (double.IsNaN(item.X) || double.IsInfinity(item.X))
            {
                Console.Error.WriteLine($"Invalid X coordinate for item Id {item.Id}.");
                isValid = false;
            }

            if (double.IsNaN(item.Y) || double.IsInfinity(item.Y))
            {
                Console.Error.WriteLine($"Invalid Y coordinate for item Id {item.Id}.");
                isValid = false;
            }

            if (isValid)
                validItems.Add(item);
        }

        if (validItems.Count == 0)
        {
            Console.Error.WriteLine("No valid data items found. Exiting.");
            return;
        }

        // ------------------------------------------------------------
        // Load the Visio diagram inside a using block
        // ------------------------------------------------------------
        try
        {
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Use the first page for shape insertion
                Page page = diagram.Pages[0];

                // --------------------------------------------------------
                // Map each valid data item to a shape
                // --------------------------------------------------------
                foreach (DataItem item in validItems)
                {
                    // Add a rectangle shape at the specified coordinates.
                    // The fourth argument is a bool indicating whether to calculate geometry; set to false.
                    long shapeId = page.AddShape(item.X, item.Y, "Rectangle", false);

                    // Retrieve the newly added shape.
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Store the Name in the shape's Data1 field.
                    shape.Data1 = item.Name;

                    // Set a default size for visibility.
                    shape.XForm.Width.Value = 1.0;
                    shape.XForm.Height.Value = 1.0;

                    // Ensure the shape's position matches the data (redundant but explicit).
                    shape.XForm.PinX.Value = item.X;
                    shape.XForm.PinY.Value = item.Y;
                }

                // --------------------------------------------------------
                // Save the updated diagram
                // --------------------------------------------------------
                string outputPath = Path.Combine(
                    Path.GetDirectoryName(diagramPath) ?? "",
                    Path.GetFileNameWithoutExtension(diagramPath) + "_mapped.vsdx");

                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Aspose.Diagram operation failed: {ex.Message}");
            return;
        }
    }
}