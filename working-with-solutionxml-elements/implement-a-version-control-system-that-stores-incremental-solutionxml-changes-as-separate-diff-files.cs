using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

namespace DiagramVersionControl
{
    // Simple version control for Diagram SolutionXML changes.
    public class SolutionXmlVersionControl
    {
        private Diagram _diagram;                     // Loaded diagram instance
        private readonly string _baseDiagramPath;     // Path to the original diagram file
        private readonly string _diffFolderPath;      // Folder where diff files are stored
        private int _currentVersion;                  // Incremental version number

        // In‑memory list of diffs applied in the current session (for reconstruction if needed)
        private readonly List<SolutionXML> _pendingDiffs = new List<SolutionXML>();

        public SolutionXmlVersionControl(string baseDiagramPath, string diffFolderPath)
        {
            _baseDiagramPath = baseDiagramPath;
            _diffFolderPath = diffFolderPath;
            _currentVersion = 0;

            // Ensure diff folder exists
            Directory.CreateDirectory(_diffFolderPath);
        }

        // Load the base diagram from file
        public void Load()
        {
            // Aspose.Diagram loads the diagram; no custom create/load rule is required here
            _diagram = new Diagram(_baseDiagramPath);
        }

        // Save the current diagram state back to the original file
        public void Save()
        {
            if (_diagram == null)
                throw new InvalidOperationException("Diagram not loaded.");

            // Save using VDX format (Visio 2003-2007). Adjust format as needed.
            _diagram.Save(_baseDiagramPath, SaveFileFormat.Vdx);
        }

        // Add a new SolutionXML entry to the diagram
        public void AddSolutionXml(string name, string xmlValue)
        {
            if (_diagram == null)
                throw new InvalidOperationException("Diagram not loaded.");

            var solutionXml = new SolutionXML(name, xmlValue);
            _diagram.SolutionXMLs.Add(solutionXml);
            _pendingDiffs.Add(solutionXml); // Track for diff file creation
        }

        // Commit pending SolutionXML changes as a diff file
        public void Commit()
        {
            if (_pendingDiffs.Count == 0)
                return; // Nothing to commit

            _currentVersion++;

            // Create a diff file that contains only the newly added SolutionXML entries
            string diffFilePath = Path.Combine(_diffFolderPath, $"diff_{_currentVersion}.xml");

            using (var writer = new StreamWriter(diffFilePath))
            {
                writer.WriteLine("<SolutionXMLDiffs>");
                foreach (var xml in _pendingDiffs)
                {
                    writer.WriteLine("  <SolutionXML>");
                    writer.WriteLine($"    <Name>{System.Security.SecurityElement.Escape(xml.Name)}</Name>");
                    writer.WriteLine($"    <XmlValue>{System.Security.SecurityElement.Escape(xml.XmlValue)}</XmlValue>");
                    writer.WriteLine("  </SolutionXML>");
                }
                writer.WriteLine("</SolutionXMLDiffs>");
            }

            // Clear pending diffs after they have been persisted
            _pendingDiffs.Clear();
        }

        // Reconstruct a diagram at a specific version by applying diffs sequentially
        public Diagram ReconstructVersion(int targetVersion)
        {
            if (targetVersion < 0)
                throw new ArgumentOutOfRangeException(nameof(targetVersion));

            // Load a fresh copy of the base diagram
            var reconstructed = new Diagram(_baseDiagramPath);

            // Apply diffs up to the requested version
            for (int v = 1; v <= targetVersion; v++)
            {
                string diffFilePath = Path.Combine(_diffFolderPath, $"diff_{v}.xml");
                if (!File.Exists(diffFilePath))
                    break; // No further diffs

                var diffXml = System.Xml.Linq.XDocument.Load(diffFilePath);
                foreach (var elem in diffXml.Root.Elements("SolutionXML"))
                {
                    string name = elem.Element("Name")?.Value ?? string.Empty;
                    string xmlValue = elem.Element("XmlValue")?.Value ?? string.Empty;
                    var solutionXml = new SolutionXML(name, xmlValue);
                    reconstructed.SolutionXMLs.Add(solutionXml);
                }
            }

            return reconstructed;
        }

        // Get the current version number (number of committed diffs)
        public int CurrentVersion => _currentVersion;
    }

    // Example usage
    class Program
    {
        static void Main()
        {
            try
            {

                string diagramPath = @"C:\Diagrams\sample.vdx";
                string diffFolder = @"C:\Diagrams\Diffs";

                var vcs = new SolutionXmlVersionControl(diagramPath, diffFolder);
                vcs.Load();

                // Add a new SolutionXML entry
                vcs.AddSolutionXml("CustomData", "<custom><value>123</value></custom>");

                // Commit the change as a diff file
                vcs.Commit();

                // Save the updated diagram
                vcs.Save();

                // Reconstruct diagram at version 1
                Diagram version1Diagram = vcs.ReconstructVersion(1);
                // version1Diagram can now be saved or inspected as needed

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}