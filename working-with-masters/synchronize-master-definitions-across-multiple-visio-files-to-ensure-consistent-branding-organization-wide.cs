using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

public static class MasterSynchronizer
    {
        /// <summary>
        /// Synchronizes specified master shapes from a template Visio file into multiple target Visio files.
        /// If a master does not exist in a target file, it is added from the template.
        /// </summary>
        /// <param name="templatePath">Path to the Visio file that contains the authoritative masters.</param>
        /// <param name="targetFilePaths">Collection of Visio file paths that need to be synchronized.</param>
        /// <param name="masterNames">Names (or universal names) of the masters to synchronize.</param>
        public static void SyncMasters(string templatePath, IEnumerable<string> targetFilePaths, IEnumerable<string> masterNames)
        {
            // Load the template diagram once – it holds the source masters.
            using (var templateDiagram = new Diagram(templatePath))
            {
                // Iterate over each target file.
                foreach (var targetPath in targetFilePaths)
                {
                    // Load the target diagram.
                    using (var targetDiagram = new Diagram(targetPath))
                    {
                        // For each master name that should be present.
                        foreach (var masterName in masterNames)
                        {
                            // Check if the master already exists in the target diagram.
                            bool exists = false;
                            foreach (Master existingMaster in targetDiagram.Masters)
                            {
                                if (string.Equals(existingMaster.Name, masterName, StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(existingMaster.NameU, masterName, StringComparison.OrdinalIgnoreCase))
                                {
                                    exists = true;
                                    break;
                                }
                            }

                            // If the master is missing, add it from the template.
                            if (!exists)
                            {
                                // AddMaster returns the new master's ID; we ignore it here.
                                targetDiagram.AddMaster(templateDiagram, masterName);
                            }
                        }

                        // Save the modified target diagram back to its original location.
                        // Using default save options (VDX format) – adjust if needed.
                        targetDiagram.Save(targetPath, SaveFileFormat.Vdx);
                    }
                }
            }
        }

        // Example usage.
        public static void Main()
        {
            try
            {

                // Path to the master template Visio file.
                string templateFile = @"C:\Visio\BrandTemplate.vstx";

                // List of Visio files that need to be updated.
                var targets = new List<string>
                {
                    @"C:\Visio\DeptA\Diagram1.vsdx",
                    @"C:\Visio\DeptB\Diagram2.vsdx",
                    @"C:\Visio\DeptC\Diagram3.vsdx"
                };

                // Masters that represent the branding elements (e.g., logo, header, footer).
                var mastersToSync = new List<string> { "CompanyLogo", "HeaderBox", "FooterBox" };

                // Perform synchronization.
                SyncMasters(templateFile, targets, mastersToSync);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }