using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments.
        if (args.Length != 3)
        {
            Console.Error.WriteLine("Usage: <save|apply> <diagramPath> <diffFolder>");
            return;
        }

        string command = args[0];
        string diagramPath = args[1];
        string diffFolder = args[2];

        // Guard: ensure diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure diff folder exists (create if missing).
        if (!Directory.Exists(diffFolder))
        {
            try
            {
                Directory.CreateDirectory(diffFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create diff folder: {ex.Message}");
                return;
            }
        }

        if (command.Equals("save", StringComparison.OrdinalIgnoreCase))
        {
            SaveSolutionXmlDiff(diagramPath, diffFolder);
        }
        else if (command.Equals("apply", StringComparison.OrdinalIgnoreCase))
        {
            ApplySolutionXmlDiffs(diagramPath, diffFolder);
        }
        else
        {
            Console.Error.WriteLine("Invalid command. Use 'save' or 'apply'.");
        }
    }

    // Saves the current SolutionXML collection as a diff file.
    private static void SaveSolutionXmlDiff(string diagramPath, string diffFolder)
    {
        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Build a simple line‑based representation: Name|XmlValue
            var lines = new System.Text.StringBuilder();
            foreach (SolutionXML solXml in diagram.SolutionXMLs)
            {
                // Escape any pipe characters in the XML value to avoid split issues.
                string escapedXml = solXml.XmlValue?.Replace("|", "&#124;") ?? string.Empty;
                lines.AppendLine($"{solXml.Name}|{escapedXml}");
            }

            // Generate a timestamped diff file name.
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
            string diffFilePath = Path.Combine(diffFolder, $"diff_{timestamp}.txt");

            // Write the diff content to the file.
            File.WriteAllText(diffFilePath, lines.ToString());
            Console.WriteLine($"SolutionXML diff saved to: {diffFilePath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while saving SolutionXML diff: {ex.Message}");
        }
    }

    // Applies all diff files (in chronological order) to the diagram and saves a new copy.
    private static void ApplySolutionXmlDiffs(string diagramPath, string diffFolder)
    {
        try
        {
            // Load the original diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Get all diff files sorted by name (timestamp order).
            string[] diffFiles = Directory.GetFiles(diffFolder, "diff_*.txt");
            Array.Sort(diffFiles);

            // Clear existing SolutionXML collection.
            diagram.SolutionXMLs.Clear();

            // Apply each diff by adding its XML content.
            foreach (string diffFile in diffFiles)
            {
                string[] lines = File.ReadAllLines(diffFile);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Split at the first pipe character.
                    int pipeIndex = line.IndexOf('|');
                    if (pipeIndex <= 0)
                        continue; // Invalid line format; skip.

                    string name = line.Substring(0, pipeIndex);
                    string xmlValueEscaped = line.Substring(pipeIndex + 1);
                    // Restore any escaped pipe characters.
                    string xmlValue = xmlValueEscaped.Replace("&#124;", "|");

                    // Create a new SolutionXML instance and add it.
                    SolutionXML solXml = new SolutionXML
                    {
                        Name = name,
                        XmlValue = xmlValue
                    };
                    diagram.SolutionXMLs.Add(solXml);
                }
            }

            // Save the updated diagram to a new file.
            string outputPath = Path.Combine(
                Path.GetDirectoryName(diagramPath) ?? string.Empty,
                Path.GetFileNameWithoutExtension(diagramPath) + "_updated.vsdx");

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Updated diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error while applying SolutionXML diffs: {ex.Message}");
        }
    }
}