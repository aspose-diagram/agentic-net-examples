using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Folder containing Visio files
        string folderPath = @"C:\VisioFiles";

        // Retrieve all files in the folder
        string[] files = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);

        foreach (string file in files)
        {
            string ext = Path.GetExtension(file).ToLowerInvariant();

            // Process only supported Visio formats
            if (ext == ".vsdx" || ext == ".vsd" || ext == ".vdx" || ext == ".vsx" || ext == ".vtx")
            {
                // Load the diagram
                Diagram diagram = new Diagram(file);

                // Set the left header to the document title
                diagram.HeaderFooter.HeaderLeft = diagram.DocumentProps.Title;

                // Determine the appropriate SaveFileFormat based on the file extension
                SaveFileFormat format = GetSaveFormat(ext);

                // Save the diagram back to the same file (overwrites the original)
                diagram.Save(file, format);
            }
        }
    }

    // Maps file extensions to the corresponding SaveFileFormat enum values
    static SaveFileFormat GetSaveFormat(string extension)
    {
        return extension switch
        {
            ".vsdx" => SaveFileFormat.Vsdx,
            ".vsd"  => SaveFileFormat.Vsd,
            ".vdx"  => SaveFileFormat.Vdx,
            ".vsx"  => SaveFileFormat.Vsx,
            ".vtx"  => SaveFileFormat.Vtx,
            _       => SaveFileFormat.Vsdx,
        };
    }
}
