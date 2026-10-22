using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // ------------------------------------------------------------
            // 1. Add a custom property named "Category" to the first shape.
            // ------------------------------------------------------------
            // Find the first non‑connector shape on the first page.
            Shape targetShape = null;
            if (diagram.Pages.Count > 0)
            {
                Page firstPage = diagram.Pages[0];
                foreach (Shape shp in firstPage.Shapes)
                {
                    // Skip connector shapes (OneD == true).
                    if (!shp.OneD)
                    {
                        targetShape = shp;
                        break;
                    }
                }
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found to add the custom property.");
                return;
            }

            // Create the custom property.
            Prop categoryProp = new Prop();
            categoryProp.Name = "Category";                     // Property internal name.
            categoryProp.Label.Value = "Category";              // Display label.
            categoryProp.Value.Val = "Finance";                 // Example value.
            categoryProp.Type.Value = TypePropValue.String;    // Data type.

            // Add the property to the shape.
            targetShape.Props.Add(categoryProp);

            // ------------------------------------------------------------
            // 2. Save the modified diagram.
            // ------------------------------------------------------------
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved with custom property to: {outputPath}");

            // ------------------------------------------------------------
            // 3. Query all shapes that have a "Category" custom property.
            // ------------------------------------------------------------
            Console.WriteLine("Shapes containing the 'Category' custom property:");
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shp in page.Shapes)
                {
                    foreach (Prop prop in shp.Props)
                    {
                        if (prop.Name == "Category")
                        {
                            // Output shape ID and the property value.
                            Console.WriteLine($"Page: {page.Name}, Shape ID: {shp.ID}, Category: {prop.Value.Val}");
                            // Break inner loop as we found the property for this shape.
                            break;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}