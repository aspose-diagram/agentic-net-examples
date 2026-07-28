using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Dictionary to hold comment counts per shape type
            var commentCounts = new Dictionary<TypeValue, int>();

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all annotations (comments) on the page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // ShapeID links the comment to a shape; 0 means no shape association
                    int shapeId = annotation.ShapeID;
                    if (shapeId != 0)
                    {
                        // Retrieve the shape by its ID
                        Shape shape = page.Shapes.GetShape(shapeId);
                        if (shape != null)
                        {
                            TypeValue shapeType = shape.Type;
                            if (commentCounts.ContainsKey(shapeType))
                                commentCounts[shapeType]++;
                            else
                                commentCounts[shapeType] = 1;
                        }
                    }
                }
            }

            // Output the grouped comment report to the console
            Console.WriteLine("Comments grouped by shape type:");
            foreach (var kvp in commentCounts)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }

            // Save a copy of the diagram (optional)
            string outputPath = "output_copy.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
