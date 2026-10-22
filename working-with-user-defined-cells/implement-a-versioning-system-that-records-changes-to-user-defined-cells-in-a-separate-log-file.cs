using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Log file path for versioning changes
        string logPath = "cell_changes.log";

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Build a lookup of shapes by their ID for quick access
            Dictionary<long, Shape> shapeLookup = new Dictionary<long, Shape>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True) continue;
                    shapeLookup[shape.ID] = shape;
                }
            }

            // List all shapes with user-defined cells
            Console.WriteLine("Shapes with user-defined cells:");
            foreach (var kvp in shapeLookup)
            {
                Shape shape = kvp.Value;
                if (shape.Users.Count > 0)
                {
                    Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.NameU}");
                    foreach (User user in shape.Users)
                    {
                        Console.WriteLine($"  Cell: {user.NameU}, Value: {user.Value.Val}");
                    }
                }
            }

            // Prompt user to select a shape ID
            Console.Write("Enter Shape ID to modify: ");
            if (!long.TryParse(Console.ReadLine(), out long shapeId) || !shapeLookup.ContainsKey(shapeId))
            {
                Console.Error.WriteLine("Invalid Shape ID.");
                return;
            }

            Shape targetShape = shapeLookup[shapeId];

            // Prompt user to select a user-defined cell name
            Console.Write("Enter User-defined Cell NameU to modify: ");
            string cellNameU = Console.ReadLine();
            User targetUser = null;
            foreach (User user in targetShape.Users)
            {
                if (string.Equals(user.NameU, cellNameU, StringComparison.OrdinalIgnoreCase))
                {
                    targetUser = user;
                    break;
                }
            }

            if (targetUser == null)
            {
                Console.Error.WriteLine("User-defined cell not found on the selected shape.");
                return;
            }

            // Show current value and ask for new value
            string oldValue = targetUser.Value.Val;
            Console.WriteLine($"Current value of '{cellNameU}': {oldValue}");
            Console.Write("Enter new value: ");
            string newValue = Console.ReadLine();

            // Update the cell value
            targetUser.Value.Val = newValue;

            // Save the diagram (overwrite original or save as new)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

            // Append change to log file
            string logEntry = $"{DateTime.Now:u} | ShapeID: {shapeId} | Cell: {cellNameU} | Old: {oldValue} | New: {newValue}";
            File.AppendAllText(logPath, logEntry + Environment.NewLine);
            Console.WriteLine($"Change logged to {logPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}