using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (replace with your file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Retrieve the first shape on the page (ensure at least one shape exists)
                if (page.Shapes.Count > 0)
                {
                    // Get the shape by its ID
                    Shape shape = page.Shapes.GetShape(page.Shapes[0].ID);

                    // Set the default tab stop to 0.5 inches (distance between tab stops)
                    shape.TextBlock.DefaultTabStop.Value = 0.5;

                    // Optional: add a text run containing a tab character to demonstrate the effect
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("First\tSecond\tThird"));
                }
                else
                {
                    Console.WriteLine("No shapes found on the first page.");
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram saved with default tab stop set to 0.5 inches.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }