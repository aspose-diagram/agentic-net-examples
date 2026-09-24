using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source VDX file
                string inputPath = "input.vdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Assume the shape is on the first page
                Page page = diagram.Pages[0];

                // Retrieve the shape with ID 5
                Shape shape = page.Shapes.GetShape(5);

                // Prepare the new dynamic text
                string newText = $"Current time: {DateTime.Now}";

                // Replace the existing text
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt(newText));

                // Optional: save the modified diagram to a new file
                string outputPath = "output.vdx";
                diagram.Save(outputPath, SaveFileFormat.Vdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }