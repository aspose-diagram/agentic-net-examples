using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the CSV file containing Unicode text (one entry per line)
        string csvPath = "data.csv";

        // Verify the CSV file exists
        if (!File.Exists(csvPath))
        {
            Console.WriteLine($"CSV file not found: {csvPath}");
            return;
        }

        // Read all lines using UTF-8 encoding to preserve Unicode characters
        string[] lines = File.ReadAllLines(csvPath, Encoding.UTF8);

        // Create a new empty diagram
        Diagram diagram = new Diagram();

        // Use the first page (created by default)
        Page page = diagram.Pages[0];

        // Layout parameters for placing shapes
        double startX = 1.0;   // inches from left
        double startY = 1.0;   // inches from top
        double offsetX = 2.5;  // horizontal spacing between shapes
        double offsetY = 1.5;  // vertical spacing between rows
        int shapesPerRow = 5;  // number of shapes before moving to next row

        int index = 0;
        foreach (string line in lines)
        {
            // Calculate position for the current shape
            double pinX = startX + (index % shapesPerRow) * offsetX;
            double pinY = startY + (index / shapesPerRow) * offsetY;

            // Add a rectangle shape; the fourth parameter isCalculate must be false
            long shapeId = page.AddShape(pinX, pinY, "Rectangle", false);

            // Retrieve the shape object using its ID
            Shape shape = page.Shapes.GetShape(shapeId);

            // Clear any existing text (should be empty for a new shape)
            shape.Text.Value.Clear();

            // Add the Unicode text from the CSV line
            shape.Text.Value.Add(new Txt(line));

            index++;
        }

        // Save the diagram preserving Unicode text
        string outputPath = "output.vsdx";
        diagram.Save(outputPath, SaveFileFormat.Csv);

        Console.WriteLine($"Diagram saved to {outputPath}");
    }
}
