using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the stencil that contains the branding masters
                string brandingStencilPath = @"C:\Branding\BrandingMasters.vssx";

                // Verify the stencil exists
                if (!File.Exists(brandingStencilPath))
                    throw new FileNotFoundException("Branding stencil not found.", brandingStencilPath);

                // Load the stencil to obtain its master definitions
                Diagram brandingDiagram = new Diagram(brandingStencilPath);

                // Folder that contains all Visio files to be synchronized
                string targetFolder = @"C:\VisioFiles";

                // Get all Visio drawing files (VSDX) in the folder (non‑recursive)
                string[] targetFiles = Directory.GetFiles(targetFolder, "*.vsdx", SearchOption.TopDirectoryOnly);

                foreach (string targetPath in targetFiles)
                {
                    // Load the target Visio document
                    Diagram targetDiagram = new Diagram(targetPath);

                    // Ensure each branding master exists in the target diagram
                    foreach (Master brandingMaster in brandingDiagram.Masters)
                    {
                        // Check by master name; if missing, import it from the stencil
                        if (!targetDiagram.Masters.IsExist(brandingMaster.Name))
                        {
                            // Import the master from the branding stencil into the target diagram
                            targetDiagram.AddMaster(brandingStencilPath, brandingMaster.Name);
                        }
                    }

                    // Save the updated diagram, overwriting the original file
                    targetDiagram.Save(targetPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Master synchronization completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }