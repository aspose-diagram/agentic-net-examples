using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

public class SolutionXmlDto
{
    public string Name { get; set; } = string.Empty;
    public string XmlValue { get; set; } = string.Empty;
}

public static class SolutionXmlVersionControl
{
    // Load the diagram from a file path
    public static Diagram LoadDiagram(string diagramPath)
    {
        if (!File.Exists(diagramPath))
            throw new FileNotFoundException($"Diagram file not found: {diagramPath}");

        return new Diagram(diagramPath);
    }

    // Convert Aspose.Diagram.SolutionXML collection to DTO list for easier processing/serialization
    public static List<SolutionXmlDto> GetCurrentSolutionXmls(Diagram diagram)
    {
        var list = new List<SolutionXmlDto>();
        foreach (SolutionXML solXml in diagram.SolutionXMLs)
        {
            list.Add(new SolutionXmlDto
            {
                Name = solXml.Name,
                XmlValue = solXml.XmlValue
            });
        }
        return list;
    }

    // Load previously saved snapshot (if any)
    public static List<SolutionXmlDto> LoadSnapshot(string snapshotPath)
    {
        if (!File.Exists(snapshotPath))
            return new List<SolutionXmlDto>();

        string json = File.ReadAllText(snapshotPath);
        return JsonSerializer.Deserialize<List<SolutionXmlDto>>(json) ?? new List<SolutionXmlDto>();
    }

    // Save snapshot for next run
    public static void SaveSnapshot(string snapshotPath, List<SolutionXmlDto> current)
    {
        string json = JsonSerializer.Serialize(current, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(snapshotPath, json);
    }

    // Write diff files for added or modified SolutionXML entries
    public static void WriteDiffs(string diffFolder, List<SolutionXmlDto> previous, List<SolutionXmlDto> current)
    {
        // Ensure diff folder exists
        Directory.CreateDirectory(diffFolder);

        // Build lookup for previous entries
        var prevLookup = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        foreach (var item in previous)
            prevLookup[item.Name] = item.XmlValue;

        foreach (var cur in current)
        {
            bool isNew = !prevLookup.ContainsKey(cur.Name);
            bool isModified = prevLookup.TryGetValue(cur.Name, out string prevValue) && prevValue != cur.XmlValue;

            if (isNew || isModified)
            {
                string timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmssfff");
                string safeName = string.Join("_", cur.Name.Split(Path.GetInvalidFileNameChars()));
                string diffFileName = $"{safeName}_{timestamp}.xml";
                string diffPath = Path.Combine(diffFolder, diffFileName);
                File.WriteAllText(diffPath, cur.XmlValue);
            }
        }
    }
}

public class Program
{
    // Expected arguments:
    // args[0] - path to the Visio diagram file
    // args[1] - folder where diff files will be stored
    // args[2] - path to snapshot file (JSON) that holds previous SolutionXML state
    public static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Usage: <program> <diagramPath> <diffFolder> <snapshotPath>");
            return;
        }

        string diagramPath = args[0];
        string diffFolder = args[1];
        string snapshotPath = args[2];

        try
        {
            // Load diagram
            Diagram diagram = SolutionXmlVersionControl.LoadDiagram(diagramPath);

            // Get current SolutionXML collection
            List<SolutionXmlDto> current = SolutionXmlVersionControl.GetCurrentSolutionXmls(diagram);

            // Load previous snapshot (if any)
            List<SolutionXmlDto> previous = SolutionXmlVersionControl.LoadSnapshot(snapshotPath);

            // Write diffs for added/changed entries
            SolutionXmlVersionControl.WriteDiffs(diffFolder, previous, current);

            // Update snapshot for next execution
            SolutionXmlVersionControl.SaveSnapshot(snapshotPath, current);

            Console.WriteLine("Version control processing completed successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            // In a real scenario you might want to rethrow or handle differently
        }
    }
}