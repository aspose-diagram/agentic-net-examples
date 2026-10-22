using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and audit log file path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <auditLogPath>");
            return;
        }

        string diagramPath = args[0];
        string auditLogPath = args[1];

        // Guard: ensure the input diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Ensure the directory for the audit log exists
        try
        {
            string auditDir = Path.GetDirectoryName(auditLogPath);
            if (!string.IsNullOrEmpty(auditDir) && !Directory.Exists(auditDir))
            {
                Directory.CreateDirectory(auditDir);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating audit log directory: {ex.Message}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate over all pages and shapes to configure the deletion event
            foreach (Page page in diagram.Pages)
            {
                // Collect shape IDs to avoid modifying collection while iterating
                var shapeIds = new System.Collections.Generic.List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    shapeIds.Add(shape.ID);
                }

                foreach (long shapeId in shapeIds)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape == null) continue;

                    // Add a user-defined cell to store deletion timestamp (if not already present)
                    bool hasUserCell = false;
                    foreach (User userCell in shape.Users)
                    {
                        if (userCell.Name == "DeletedTimestamp")
                        {
                            hasUserCell = true;
                            break;
                        }
                    }

                    if (!hasUserCell)
                    {
                        User user = new User();
                        user.Name = "DeletedTimestamp";
                        user.Value.Val = ""; // initialize empty
                        shape.Users.Add(user);
                    }

                    // Set the EventDrop formula to capture deletion timestamp
                    // The formula uses SETF to assign NOW() to the user cell when the shape is deleted
                    shape.Event.EventDrop.Ufe.F = "SETF(@User.DeletedTimestamp, NOW())";
                }
            }

            // Save the modified diagram (overwrites the original file)
            diagram.Save(diagramPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
            return;
        }

        // After saving, scan for shapes marked as deleted and log their timestamps
        try
        {
            // Reload the diagram to reflect any deletions that may have occurred
            Diagram diagram = new Diagram(diagramPath);
            using (StreamWriter logWriter = new StreamWriter(auditLogPath, append: true))
            {
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape is marked as deleted
                        if (shape.Del == BOOL.True)
                        {
                            // Retrieve the timestamp from the user-defined cell
                            string timestamp = "";
                            foreach (User userCell in shape.Users)
                            {
                                if (userCell.Name == "DeletedTimestamp")
                                {
                                    timestamp = userCell.Value.Val;
                                    break;
                                }
                            }

                            // Write an audit entry
                            string entry = $"ShapeID={shape.ID}, PageID={page.ID}, DeletedAt={timestamp}";
                            logWriter.WriteLine(entry);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error writing audit log: {ex.Message}");
        }
    }
}