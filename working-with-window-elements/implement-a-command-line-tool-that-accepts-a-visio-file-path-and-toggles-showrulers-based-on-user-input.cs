using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: ToggleRulers <VisioFilePath> [on|off]");
            return;
        }

        string filePath = args[0];
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File not found: {filePath}");
            return;
        }

        // Determine desired state
        BOOL desiredState;
        if (args.Length >= 2)
        {
            desiredState = ParseState(args[1]);
        }
        else
        {
            Console.Write("Enable ShowRulers? (y/n): ");
            string input = Console.ReadLine()?.Trim().ToLowerInvariant();
            desiredState = (input == "y" || input == "yes" || input == "true") ? BOOL.True : BOOL.False;
        }

        // Load the Visio diagram
        Diagram diagram = new Diagram(filePath);

        // Ensure there is at least one window to modify
        if (diagram.Windows.Count == 0)
        {
            Window newWindow = new Window
            {
                WindowType = WindowTypeValue.Drawing,
                WindowState = WindowStateValue.Maximized
            };
            diagram.Windows.Add(newWindow);
        }

        // Toggle ShowRulers on the first window
        Window window = diagram.Windows[0];
        window.ShowRulers = desiredState;

        // Determine save format based on file extension
        SaveFileFormat saveFormat = GetSaveFormat(Path.GetExtension(filePath));

        // Save the diagram back to the same file
        diagram.Save(filePath, saveFormat);

        Console.WriteLine($"ShowRulers set to {(desiredState == BOOL.True ? "ON" : "OFF")} and diagram saved.");
    }

    // Parses a string like "on"/"off" into BOOL
    private static BOOL ParseState(string value)
    {
        string lowered = value.Trim().ToLowerInvariant();
        return (lowered == "on" || lowered == "true" || lowered == "yes") ? BOOL.True : BOOL.False;
    }

    // Maps file extensions to SaveFileFormat enum values
    private static SaveFileFormat GetSaveFormat(string extension)
    {
        switch (extension.ToLowerInvariant())
        {
            case ".vsdx": return SaveFileFormat.Vsdx;
            case ".vsd":  return SaveFileFormat.Vsd;
            case ".vdx":  return SaveFileFormat.Vdx;
            case ".vsx":  return SaveFileFormat.Vsx;
            case ".vtx":  return SaveFileFormat.Vtx;
            case ".vssx": return SaveFileFormat.Vssx;
            case ".vstx": return SaveFileFormat.Vstx;
            case ".vsdm": return SaveFileFormat.Vsdm;
            case ".vssm": return SaveFileFormat.Vssm;
            case ".vstm": return SaveFileFormat.Vstm;
            case ".vss":  return SaveFileFormat.Vss;
            case ".vst":  return SaveFileFormat.Vst;
            default:      return SaveFileFormat.Vsdx; // fallback
        }
    }
}
