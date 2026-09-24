using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            // Prompt user for the Visio file path
            Console.Write("Enter the path to the Visio file: ");
            string filePath = Console.ReadLine();

            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve required properties
                    long shapeId = shape.ID;
                    string shapeName = shape.NameU; // Universal name of the shape
                    double width = shape.XForm.Width.Value;
                    double height = shape.XForm.Height.Value;
                    double pinX = shape.XForm.PinX.Value;
                    double pinY = shape.XForm.PinY.Value;

                    // Output the shape summary
                    Console.WriteLine(
                        $"Page: {page.Name}, Shape ID: {shapeId}, Name: {shapeName}, " +
                        $"Width: {width}, Height: {height}, PinX: {pinX}, PinY: {pinY}");
                }
            }

            // Optional: Save a copy of the diagram (demonstrates save usage)
            string outputPath = "output_copy.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
    }