using System;
using System.IO;
using System.Diagnostics;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Initialize a stopwatch to measure the duration of adding shapes.
        Stopwatch stopwatch = new Stopwatch();

        try
        {
            // Create a new empty diagram inside a using block as required.
            using (Diagram diagram = new Diagram())
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    // Add a blank page if none exist.
                    diagram.Pages.Add(new Page());
                }

                // Reference the first page where shapes will be added.
                Page page = diagram.Pages[0];

                // Start timing before adding shapes.
                stopwatch.Start();

                // Add 100 rectangle shapes to the page.
                for (int i = 0; i < 100; i++)
                {
                    // Add a rectangle shape at arbitrary coordinates (e.g., i*1.0, i*1.0).
                    // The AddShape method returns the shape ID (long).
                    long shapeId = diagram.AddShape(i * 1.0, i * 1.0, "Rectangle", 0);

                    // Retrieve the shape object using the returned ID.
                    Shape shape = page.Shapes.GetShape(shapeId);

                    // Optional: set shape properties here if needed.
                    // Example: shape.Text.Value.Clear();
                    // shape.Text.Value.Add(new Txt($"Rect {i + 1}"));
                }

                // Stop timing after all shapes have been added.
                stopwatch.Stop();

                // Log the elapsed time in milliseconds.
                Console.WriteLine($"Time taken to add 100 rectangle shapes: {stopwatch.Elapsed.TotalMilliseconds} ms");

                // (Optional) Save the diagram to verify the shapes were added.
                // string outputPath = "output.vsdx";
                // diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}