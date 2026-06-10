using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Path to the stencil (or diagram) that contains the branding masters
            string brandingStencilPath = @"C:\Branding\BrandingMasters.vssx";

            // Folder containing Visio files to synchronize
            string targetFolder = @"C:\VisioFiles";

            // Load the source diagram that holds the master definitions
            Diagram sourceDiagram;
            try
            {
                sourceDiagram = new Diagram(brandingStencilPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load branding stencil: {ex.Message}");
                return;
            }

            // Collect master names from the source diagram
            var sourceMasterNames = new System.Collections.Generic.List<string>();
            foreach (Master master in sourceDiagram.Masters)
            {
                if (!string.IsNullOrEmpty(master.Name))
                {
                    sourceMasterNames.Add(master.Name);
                }
            }

            // Process each Visio file in the target folder
            string[] visioFiles = Directory.GetFiles(targetFolder, "*.vsdx", SearchOption.AllDirectories);
            foreach (string filePath in visioFiles)
            {
                Console.WriteLine($"Processing file: {filePath}");
                Diagram targetDiagram;
                try
                {
                    targetDiagram = new Diagram(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Unable to load file: {ex.Message}");
                    continue;
                }

                bool changesMade = false;

                // Ensure each branding master exists in the target diagram
                foreach (string masterName in sourceMasterNames)
                {
                    // Check existence by name
                    if (!targetDiagram.Masters.IsExist(masterName))
                    {
                        // Add the master from the source diagram
                        int addedMasterId = targetDiagram.AddMaster(sourceDiagram, masterName);
                        if (addedMasterId > 0)
                        {
                            Console.WriteLine($"  Added master '{masterName}' (ID {addedMasterId})");
                            changesMade = true;
                        }
                        else
                        {
                            Console.WriteLine($"  Failed to add master '{masterName}'");
                        }
                    }
                }

                // Save the diagram only if modifications were made
                if (changesMade)
                {
                    try
                    {
                        targetDiagram.Save(filePath, SaveFileFormat.Vsdx);
                        Console.WriteLine("  Saved updated file.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"  Error saving file: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("  No changes required.");
                }

                // Dispose the diagram (optional, as Diagram implements IDisposable)
                targetDiagram.Dispose();
            }

            // Dispose the source diagram
            sourceDiagram.Dispose();

            Console.WriteLine("Master synchronization completed.");
        }
    }