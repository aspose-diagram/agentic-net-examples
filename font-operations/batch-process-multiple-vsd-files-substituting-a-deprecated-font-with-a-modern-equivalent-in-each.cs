using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Folder containing the VSD files
        string folderPath = @"C:\VisioFiles";

        // Deprecated font name and its modern substitutes
        string deprecatedFont = "OldFont";
        string[] modernFonts = new string[] { "NewFont1", "NewFont2" };

        // Register font substitutes globally
        FontConfigs.SetFontSubstitutes(deprecatedFont, modernFonts);

        // Process each VSD file in the folder
        foreach (string filePath in Directory.GetFiles(folderPath, "*.vsd"))
        {
            // Load the diagram
            using (Diagram diagram = new Diagram(filePath))
            {
                // Save the diagram back, overwriting the original file
                diagram.Save(filePath, SaveFileFormat.Vsd);
            }
        }
    }
}
