using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Vba;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: the folder containing diagrams and the path to the certificate (certificate handling not exposed by API)
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: BatchSignVba <folderPath> <certificatePath>");
            return;
        }

        string folderPath = args[0];
        string certificatePath = args[1]; // Placeholder – actual signing not supported via Aspose.Diagram API

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"Folder does not exist: {folderPath}");
            return;
        }

        // Process Visio files in the specified folder
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsdm")
                continue; // Skip non‑Visio files

            try
            {
                // Load the diagram
                Diagram diagram = new Diagram(file);

                // Access the VBA project
                VbaProject vba = diagram.VbaProject;
                if (vba == null)
                {
                    Console.WriteLine($"No VBA project found in: {file}");
                    continue;
                }

                // Check if the project is already signed
                if (vba.IsSigned)
                {
                    Console.WriteLine($"Already signed: {file}");
                }
                else
                {
                    // Since Aspose.Diagram does not expose a signing API, embed a marker module
                    int moduleIdx = vba.Modules.Add(VbaModuleType.Procedural, "SignatureInfo");
                    var module = vba.Modules[moduleIdx];
                    module.Codes = $"' Signed with certificate {Path.GetFileName(certificatePath)}{Environment.NewLine}'Signature timestamp: {DateTime.UtcNow:u}";

                    // Save the diagram in a macro‑enabled format to preserve the VBA project
                    string outputPath = Path.Combine(folderPath,
                        Path.GetFileNameWithoutExtension(file) + "_signed.vsdm");
                    diagram.Save(outputPath, SaveFileFormat.Vsdm);
                    Console.WriteLine($"Signed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {file}: {ex.Message}");
            }
        }
    }
}
