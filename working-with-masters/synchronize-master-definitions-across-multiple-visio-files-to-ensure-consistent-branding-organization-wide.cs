using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Diagram;

public static class MasterSynchronizer
    {
        /// <summary>
        /// Synchronizes master definitions from a template Visio file to a collection of target Visio files.
        /// Masters that already exist in a target file (matched by universal name) are skipped.
        /// </summary>
        /// <param name="masterTemplatePath">Full path to the Visio file that contains the authoritative masters.</param>
        /// <param name="targetFilePaths">List of full paths to Visio files that need to be updated.</param>
        public static void SyncMasters(string masterTemplatePath, IEnumerable<string> targetFilePaths)
        {
            // Load the source diagram that holds the master definitions (using the constructor that loads from file)
            using (var sourceDiagram = new Diagram(masterTemplatePath))
            {
                // Iterate over each target file
                foreach (var targetPath in targetFilePaths)
                {
                    // Load the target diagram
                    using (var targetDiagram = new Diagram(targetPath))
                    {
                        // For each master in the source diagram, ensure it exists in the target diagram
                        foreach (var sourceMaster in sourceDiagram.Masters)
                        {
                            // Check if a master with the same universal name already exists in the target
                            bool exists = targetDiagram.Masters.Any(m => string.Equals(m.NameU, sourceMaster.NameU, StringComparison.OrdinalIgnoreCase));

                            if (!exists)
                            {
                                // Add the missing master to the target diagram using the AddMaster overload that takes a source diagram and master name
                                targetDiagram.AddMaster(sourceDiagram, sourceMaster.NameU);
                            }
                        }

                        // Save the updated target diagram back to its original file (using the Save method that takes a file path)
                        targetDiagram.Save(targetPath, SaveFileFormat.Vdx);
                    }
                }
            }
        }

        // Example usage
        public static void Main()
        {
            try
            {

                // Path to the master template containing the branding masters
                string masterTemplate = @"C:\Visio\BrandingTemplate.vssx";

                // List of Visio files that need to be synchronized
                var targets = new List<string>
                {
                    @"C:\Visio\DeptA\Diagram1.vsdx",
                    @"C:\Visio\DeptB\Diagram2.vsdx",
                    @"C:\Visio\DeptC\Diagram3.vsdx"
                };

                // Perform synchronization
                SyncMasters(masterTemplate, targets);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }