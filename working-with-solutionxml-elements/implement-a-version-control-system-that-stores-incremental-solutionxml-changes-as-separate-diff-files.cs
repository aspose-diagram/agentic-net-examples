using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: path to Visio file and path to diff folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath> <DiffFolderPath>");
            return;
        }

        string visioPath = args[0];
        string diffFolder = args[1];

        // Guard for input Visio file existence
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Visio file not found: {visioPath}");
            return;
        }

        // Ensure diff folder exists
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

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(visioPath);

            // Iterate over all SolutionXML elements in the document
            for (int i = 0; i < diagram.SolutionXMLs.Count; i++)
            {
                SolutionXML solXml = diagram.SolutionXMLs[i];

                // Retrieve the XML content; if null, treat as empty string
                string xmlContent = solXml.XmlValue ?? string.Empty;

                // Prepare file names with timestamps for versioning
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string versionFileName = $"SolutionXML_{i}_{timestamp}.xml";
                string versionFilePath = Path.Combine(diffFolder, versionFileName);

                // Write the full XML content to the version file
                File.WriteAllText(versionFilePath, xmlContent);

                // Find the most recent previous version file for this index (if any)
                var previousFiles = Directory.GetFiles(diffFolder, $"SolutionXML_{i}_*.xml")
                                             .Where(f => !f.Equals(versionFilePath, StringComparison.OrdinalIgnoreCase))
                                             .OrderByDescending(f => f)
                                             .ToList();

                if (previousFiles.Count > 0)
                {
                    string previousFilePath = previousFiles[0];
                    string previousContent = File.ReadAllText(previousFilePath);

                    // Compute a simple line-by-line diff
                    var currentLines = xmlContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
                    var previousLines = previousContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

                    List<string> diffLines = new List<string>();
                    int maxLines = Math.Max(currentLines.Length, previousLines.Length);
                    for (int lineIdx = 0; lineIdx < maxLines; lineIdx++)
                    {
                        string curLine = lineIdx < currentLines.Length ? currentLines[lineIdx] : string.Empty;
                        string prevLine = lineIdx < previousLines.Length ? previousLines[lineIdx] : string.Empty;

                        if (curLine != prevLine)
                        {
                            if (!string.IsNullOrEmpty(prevLine))
                                diffLines.Add($"- {prevLine}");
                            if (!string.IsNullOrEmpty(curLine))
                                diffLines.Add($"+ {curLine}");
                        }
                    }

                    // Write diff to a .txt file if there are differences
                    if (diffLines.Count > 0)
                    {
                        string diffFileName = $"SolutionXML_{i}_diff_{timestamp}.txt";
                        string diffFilePath = Path.Combine(diffFolder, diffFileName);
                        File.WriteAllLines(diffFilePath, diffLines);
                    }
                }
            }

            Console.WriteLine("SolutionXML versioning completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}