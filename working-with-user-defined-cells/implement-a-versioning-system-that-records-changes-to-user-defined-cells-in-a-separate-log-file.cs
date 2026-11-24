using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (first argument or hardcoded)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Log file path for recording cell changes
        string logPath = "cell_changes.log";

        Diagram diagram;
        try
        {
            // Load the diagram
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        // Store original values of all user-defined cells for change detection
        var originalValues = diagram.Pages
            .Cast<Page>()
            .SelectMany(page => page.Shapes.Cast<Shape>()
                .Where(shape => shape.Del == BOOL.False) // ignore deleted shapes
                .SelectMany(shape => shape.Users.Cast<User>()
                    .Select(user => new
                    {
                        ShapeId = shape.ID,
                        UserName = user.Name,
                        OriginalValue = user.Value?.Val ?? string.Empty,
                        Shape = shape,
                        User = user
                    })))
            .ToDictionary(
                x => (x.ShapeId, x.UserName),
                x => x.OriginalValue);

        // Interactive loop for editing user-defined cells
        while (true)
        {
            Console.WriteLine("\n--- User-Defined Cells ---");
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True) continue; // skip deleted shapes
                    foreach (User user in shape.Users)
                    {
                        Console.WriteLine($"Shape ID: {shape.ID}, Cell: {user.Name}, Value: {user.Value?.Val}");
                    }
                }
            }

            Console.Write("\nEnter Shape ID to edit (or 'exit' to finish): ");
            string shapeInput = Console.ReadLine();
            if (string.Equals(shapeInput, "exit", StringComparison.OrdinalIgnoreCase))
                break;

            if (!int.TryParse(shapeInput, out int targetShapeId))
            {
                Console.WriteLine("Invalid Shape ID.");
                continue;
            }

            Console.Write("Enter User-Defined Cell Name: ");
            string cellName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(cellName))
            {
                Console.WriteLine("Cell name cannot be empty.");
                continue;
            }

            // Locate the shape
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                targetShape = page.Shapes.GetShape(targetShapeId);
                if (targetShape != null && targetShape.Del == BOOL.False)
                    break;
            }

            if (targetShape == null)
            {
                Console.WriteLine($"Shape with ID {targetShapeId} not found.");
                continue;
            }

            // Locate the user-defined cell
            User targetUser = targetShape.Users
                .Cast<User>()
                .FirstOrDefault(u => string.Equals(u.Name, cellName, StringComparison.OrdinalIgnoreCase) ||
                                     string.Equals(u.NameU, cellName, StringComparison.OrdinalIgnoreCase));

            if (targetUser == null)
            {
                Console.WriteLine($"User-defined cell '{cellName}' not found in shape {targetShapeId}.");
                continue;
            }

            string oldValue = targetUser.Value?.Val ?? string.Empty;
            Console.Write($"Current Value: '{oldValue}'. Enter New Value: ");
            string newValue = Console.ReadLine() ?? string.Empty;

            // Update the cell value
            try
            {
                targetUser.Value.Val = newValue;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to set new value: {ex.Message}");
                continue;
            }

            // Log the change
            string logEntry = $"{DateTime.UtcNow:O}\tShapeID:{targetShapeId}\tCell:{cellName}\tOldValue:{oldValue}\tNewValue:{newValue}{Environment.NewLine}";
            try
            {
                File.AppendAllText(logPath, logEntry);
                Console.WriteLine("Change logged.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to write log: {ex.Message}");
            }

            // Update original values dictionary
            originalValues[(targetShapeId, cellName)] = newValue;
        }

        // Save the modified diagram
        string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "output.vsdx");
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
        }
    }
}