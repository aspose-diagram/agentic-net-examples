using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramSolutionXmlVersionControl
{
    // Simple version control for Diagram SolutionXML elements.
    class VersionControl
    {
        // Path to the file that stores the latest snapshot of SolutionXMLs.
        private const string SnapshotFileName = "latest_snapshot.xml";

        // Saves a new version and creates a diff file based on changes since the last snapshot.
        public void SaveVersion(string diagramPath, string versionFolder)
        {
            // Ensure the version folder exists.
            Directory.CreateDirectory(versionFolder);

            // Load the diagram inside a try/catch to capture Aspose errors.
            Diagram diagram;
            try
            {
                diagram = new Diagram(diagramPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
                return;
            }

            // Build a dictionary of current SolutionXML entries (Name -> XmlValue).
            Dictionary<string, string> current = diagram.SolutionXMLs
                .Cast<SolutionXML>()
                .ToDictionary(s => s.Name, s => s.XmlValue ?? string.Empty);

            // Load previous snapshot if it exists.
            string snapshotPath = Path.Combine(versionFolder, SnapshotFileName);
            Dictionary<string, string> previous = File.Exists(snapshotPath)
                ? LoadSnapshot(snapshotPath)
                : new Dictionary<string, string>();

            // Determine added entries.
            var added = current.Keys.Except(previous.Keys)
                .Select(name => new { Name = name, NewValue = current[name] })
                .ToList();

            // Determine removed entries.
            var removed = previous.Keys.Except(current.Keys)
                .Select(name => new { Name = name, OldValue = previous[name] })
                .ToList();

            // Determine modified entries.
            var modified = current.Keys.Intersect(previous.Keys)
                .Where(name => current[name] != previous[name])
                .Select(name => new { Name = name, OldValue = previous[name], NewValue = current[name] })
                .ToList();

            // If there are changes, write a diff file.
            if (added.Any() || removed.Any() || modified.Any())
            {
                string diffFileName = $"diff_{DateTime.UtcNow:yyyyMMdd_HHmmss}.xml";
                string diffFilePath = Path.Combine(versionFolder, diffFileName);
                // Cast anonymous type lists to List<dynamic> to match method signature.
                WriteDiffFile(
                    diffFilePath,
                    added.Cast<dynamic>().ToList(),
                    removed.Cast<dynamic>().ToList(),
                    modified.Cast<dynamic>().ToList());
                Console.WriteLine($"Diff file created: {diffFilePath}");
            }
            else
            {
                Console.WriteLine("No changes detected; no diff file created.");
            }

            // Update the latest snapshot.
            WriteSnapshotFile(snapshotPath, current);
        }

        // Loads a snapshot XML file into a dictionary.
        private Dictionary<string, string> LoadSnapshot(string path)
        {
            XDocument doc = XDocument.Load(path);
            return doc.Root
                .Elements("SolutionXML")
                .ToDictionary(
                    e => (string)e.Attribute("Name"),
                    e => (string)e.Attribute("XmlValue") ?? string.Empty);
        }

        // Writes the current state as a snapshot XML file.
        private void WriteSnapshotFile(string path, Dictionary<string, string> data)
        {
            XDocument doc = new XDocument(
                new XElement("SolutionXMLs",
                    data.Select(kv =>
                        new XElement("SolutionXML",
                            new XAttribute("Name", kv.Key),
                            new XAttribute("XmlValue", kv.Value)))));

            doc.Save(path);
        }

        // Writes a diff XML file describing added, removed, and modified entries.
        private void WriteDiffFile(string path,
            List<dynamic> added,
            List<dynamic> removed,
            List<dynamic> modified)
        {
            XDocument diffDoc = new XDocument(
                new XElement("Diff",
                    new XElement("Added",
                        added.Select(a =>
                            new XElement("SolutionXML",
                                new XAttribute("Name", a.Name),
                                new XAttribute("XmlValue", a.NewValue)))),
                    new XElement("Removed",
                        removed.Select(r =>
                            new XElement("SolutionXML",
                                new XAttribute("Name", r.Name),
                                new XAttribute("XmlValue", r.OldValue)))),
                    new XElement("Modified",
                        modified.Select(m =>
                            new XElement("SolutionXML",
                                new XAttribute("Name", m.Name),
                                new XAttribute("OldValue", m.OldValue),
                                new XAttribute("NewValue", m.NewValue))))));

            diffDoc.Save(path);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: diagram file path and version folder path.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramSolutionXmlVersionControl <diagramPath> <versionFolder>");
                return;
            }

            string diagramPath = args[0];
            string versionFolder = args[1];

            // Guard against missing diagram file.
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
                return;
            }

            try
            {
                VersionControl vc = new VersionControl();
                vc.SaveVersion(diagramPath, versionFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
    }
}