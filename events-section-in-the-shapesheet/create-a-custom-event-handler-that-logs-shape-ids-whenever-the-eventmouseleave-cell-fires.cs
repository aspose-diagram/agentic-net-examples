using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Set the EventDrop cell (closest substitute for EventMouseLeave)
                    // The formula calls a custom Visio macro/function "LogShapeLeave"
                    // that should be defined in the Visio document's VBA project.
                    // The macro can retrieve the shape ID via the built‑in variable "ThisShape".
                    shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"LogShapeLeave\")";

                    // Optionally, store the shape ID in a user‑defined cell for the macro to read
                    // Create or update a user‑defined cell named "ShapeID"
                    bool userCellExists = false;
                    foreach (User user in shape.Users)
                    {
                        if (user.Name == "ShapeID")
                        {
                            user.Value.Val = shape.ID.ToString();
                            userCellExists = true;
                            break;
                        }
                    }
                    if (!userCellExists)
                    {
                        User userCell = new User();
                        userCell.Name = "ShapeID";
                        userCell.Value.Val = shape.ID.ToString();
                        shape.Users.Add(userCell);
                    }
                }
            }

            // Save the modified diagram to the output path
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}