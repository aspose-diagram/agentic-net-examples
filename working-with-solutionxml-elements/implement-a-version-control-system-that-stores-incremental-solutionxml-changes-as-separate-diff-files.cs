using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

namespace SolutionXmlVersionControlDemo
{
    // Simple version control for Diagram.SolutionXMLs.
    // Each commit creates a diff file that contains only the changed SolutionXML entries.
    public class SolutionXmlVersionControl
    {
        private readonly Diagram _diagram;
        // Keeps the last committed state of each SolutionXML by its Name.
        private readonly Dictionary<string, string> _lastCommittedXml = new Dictionary<string, string>();

        public SolutionXmlVersionControl(string diagramPath)
        {
            // Load the diagram from file.
            _diagram = new Diagram(diagramPath);
        }

        // Adds or updates a SolutionXML entry in the diagram.
        public void AddOrUpdateSolutionXml(string name, string xmlContent)
        {
            // Try to find an existing entry with the same name.
            SolutionXML existing = null;
            foreach (SolutionXML sx in _diagram.SolutionXMLs)
            {
                if (sx.Name == name)
                {
                    existing = sx;
                    break;
                }
            }

            if (existing != null)
            {
                // Update existing entry.
                existing.XmlValue = xmlContent;
            }
            else
            {
                // Add new entry.
                var newSolutionXml = new SolutionXML(name, xmlContent);
                _diagram.SolutionXMLs.Add(newSolutionXml);
            }
        }

        // Commits the current state of SolutionXMLs to a diff file.
        // Only entries that have changed since the last commit are written.
        public void Commit(string diffFolder)
        {
            if (!Directory.Exists(diffFolder))
                Directory.CreateDirectory(diffFolder);

            // Build diff content.
            var diffLines = new List<string>();
            foreach (SolutionXML sx in _diagram.SolutionXMLs)
            {
                string currentXml = sx.XmlValue ?? string.Empty;
                if (_lastCommittedXml.TryGetValue(sx.Name, out string previousXml))
                {
                    if (previousXml != currentXml)
                    {
                        diffLines.Add($"--- {sx.Name}");
                        diffLines.Add(currentXml);
                    }
                }
                else
                {
                    // New entry.
                    diffLines.Add($"+++ {sx.Name}");
                    diffLines.Add(currentXml);
                }
            }

            // If there are changes, write them to a new diff file.
            if (diffLines.Count > 0)
            {
                string timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmssfff");
                string diffFilePath = Path.Combine(diffFolder, $"SolutionXmlDiff_{timestamp}.txt");
                File.WriteAllLines(diffFilePath, diffLines);
            }

            // Update the last committed snapshot.
            _lastCommittedXml.Clear();
            foreach (SolutionXML sx in _diagram.SolutionXMLs)
            {
                _lastCommittedXml[sx.Name] = sx.XmlValue ?? string.Empty;
            }
        }

        // Saves the diagram back to a file.
        public void SaveDiagram(string outputPath)
        {
            _diagram.Save(outputPath, SaveFileFormat.Vdx);
        }
    }

    // Example usage.
    class Program
    {
        static void Main()
        {
            try
            {

                string diagramPath = @"C:\Diagrams\sample.vdx";
                string diffFolder = @"C:\Diagrams\Diffs";
                string outputDiagramPath = @"C:\Diagrams\sample_updated.vdx";

                var vc = new SolutionXmlVersionControl(diagramPath);

                // Add or modify SolutionXML entries.
                vc.AddOrUpdateSolutionXml("CustomData", "<root><value>123</value></root>");
                vc.AddOrUpdateSolutionXml("Metadata", "<meta><author>John Doe</author></meta>");

                // Commit changes – creates a diff file with the modifications.
                vc.Commit(diffFolder);

                // Save the updated diagram.
                vc.SaveDiagram(outputDiagramPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}