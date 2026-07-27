using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // Path to the input Visio file
    private const string InputPath = "input.vsdx";
    // Path to the output Visio file
    private const string OutputPath = "output.vsdx";
    // Path to the external audit log file
    private const string AuditLogPath = "audit_log.txt";

    static void Main()
    {
        // Verify that the input file exists before attempting to load it
        if (!File.Exists(InputPath))
        {
            Console.Error.WriteLine($"File not found: {InputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(InputPath);

            // Iterate through all pages and shapes in the diagram
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // The EventShapeDeleted cell does not exist in the API.
                    // As an alternative, use the EventDrop cell to trigger a macro when the shape is dropped.
                    // Adjust the macro name as needed; it must be defined in the Visio document.
                    shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"LogDeletion\")";
                }
            }

            // Save the modified diagram to the output path using the Vsdx format
            diagram.Save(OutputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Example method that could be invoked by the Visio macro (CALLTHIS)
    // This method writes a timestamp to the external audit log file.
    // Note: The actual invocation from Visio requires a VBA macro named "LogDeletion".
    public static void LogDeletion()
    {
        string timestamp = DateTime.UtcNow.ToString("o");
        try
        {
            // Append a line with the deletion timestamp to the audit log file
            File.AppendAllText(AuditLogPath, $"Shape deleted at {timestamp}{Environment.NewLine}");
        }
        catch (Exception ex)
        {
            // In a real scenario, handle exceptions appropriately.
            Console.WriteLine($"Failed to write audit log: {ex.Message}");
        }
    }
}