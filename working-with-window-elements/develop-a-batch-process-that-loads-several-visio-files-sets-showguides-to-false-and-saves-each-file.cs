using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Folder containing Visio files (modify as needed)
            string folderPath = @"C:\VisioFiles";

            // Get all supported Visio files in the folder
            string[] visioFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);
            foreach (string filePath in visioFiles)
            {
                string extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (!IsSupportedExtension(extension))
                {
                    continue; // Skip unsupported files
                }

                // Load the diagram
                Diagram diagram = new Diagram(filePath);

                // Set ShowGuides to false for all windows in the diagram
                foreach (Window window in diagram.Windows)
                {
                    window.ShowGuides = BOOL.False;
                }

                // Determine the appropriate SaveFileFormat based on the file extension
                SaveFileFormat format = GetSaveFileFormat(extension);

                // Save the diagram back to the same file
                diagram.Save(filePath, format);
            }
        }

        // Checks if the file extension is a supported Visio format
        private static bool IsSupportedExtension(string ext)
        {
            return ext == ".vsdx" || ext == ".vsd" || ext == ".vdx" ||
                   ext == ".vsx" || ext == ".vtx" || ext == ".vssx" ||
                   ext == ".vstx" || ext == ".vsdm" || ext == ".vssm" ||
                   ext == ".vstm";
        }

        // Maps file extensions to the corresponding SaveFileFormat enum values
        private static SaveFileFormat GetSaveFileFormat(string ext)
        {
            return ext switch
            {
                ".vsdx" => SaveFileFormat.Vsdx,
                ".vsd"  => SaveFileFormat.Vsd,
                ".vdx"  => SaveFileFormat.Vdx,
                ".vsx"  => SaveFileFormat.Vsx,
                ".vtx"  => SaveFileFormat.Vtx,
                ".vssx" => SaveFileFormat.Vssx,
                ".vstx" => SaveFileFormat.Vstx,
                ".vsdm" => SaveFileFormat.Vsdm,
                ".vssm" => SaveFileFormat.Vssm,
                ".vstm" => SaveFileFormat.Vstm,
                _       => SaveFileFormat.Vsdx // Default fallback
            };
        }
    }