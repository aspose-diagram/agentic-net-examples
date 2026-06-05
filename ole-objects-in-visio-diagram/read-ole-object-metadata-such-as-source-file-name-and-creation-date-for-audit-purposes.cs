using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file to be audited
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Audit information header
            Console.WriteLine("=== OLE Object Metadata Audit ===");
            Console.WriteLine($"Diagram file: {Path.GetFileName(inputPath)}");
            Console.WriteLine($"Diagram created on: {diagram.DocumentProps.TimeCreated}");
            Console.WriteLine();

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has foreign data
                    if (shape.ForeignData == null)
                        continue;

                    // Verify the shape is an OLE object
                    if (shape.Type != TypeValue.Foreign)
                        continue;

                    // Verify the foreign type is an embedded OLE object
                    if (shape.ForeignData.ForeignType != ForeignType.Object)
                        continue;

                    // Retrieve source file name of the OLE object
                    string sourceFileName = shape.ForeignData.ObjectSourceFullName ?? "N/A";

                    // OLE objects do not expose a creation date directly.
                    // Use the diagram's creation date as a fallback for audit purposes.
                    DateTime oleCreationDate = diagram.DocumentProps.TimeCreated;

                    Console.WriteLine($"Page: {page.Name}");
                    Console.WriteLine($"Shape ID: {shape.ID}");
                    Console.WriteLine($"OLE Source File: {sourceFileName}");
                    Console.WriteLine($"Assumed Creation Date: {oleCreationDate}");
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.WriteLine("Audit completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}