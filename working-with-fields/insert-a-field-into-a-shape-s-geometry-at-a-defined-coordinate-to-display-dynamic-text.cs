using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (mandatory pattern).
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Define the page where the new shape will be added (use the first page).
                Page page = diagram.Pages[0];

                // Define coordinates and size for the new text shape.
                double pinX = 5.0;   // X position (in inches)
                double pinY = 5.0;   // Y position (in inches)
                double width = 2.0;  // Width of the text shape (in inches)
                double height = 0.5; // Height of the text shape (in inches)

                // Insert a plain text shape. The initial text can be empty because we will add a field.
                Shape textShape = page.AddText(pinX, pinY, width, height, string.Empty);

                // Create a new Field object to display dynamic text.
                // Here we use a simple formula that shows the current page name.
                Field field = new Field();

                // Set the field type to undefined (default). Type is an IntValue.
                field.Type.Value = 0; // 0 corresponds to undefined / custom formula.

                // Set the formula that provides dynamic content.
                // Example: display the name of the current page.
                field.Value.Ufev.F = "PageName";

                // Ensure the unit is undefined (no specific measurement unit).
                field.Value.Ufev.Unit = MeasureConst.Undefined;

                // No special formatting; leave Format empty.
                field.Format.Ufev.F = string.Empty;
                field.Format.Ufev.Unit = MeasureConst.Undefined;

                // Do not delete the field (Del = FALSE).
                field.Del = BOOL.False;

                // Add the field to the shape's Fields collection.
                textShape.Fields.Add(field);

                // Refresh the shape to apply changes.
                textShape.RefreshData();

                // Save the modified diagram to the specified output path as VSDX.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Field inserted and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}