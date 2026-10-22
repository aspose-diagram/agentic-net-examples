using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine the folder to process: first argument or current directory.
        string folderPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

        // Guard: ensure the folder exists.
        if (!Directory.Exists(folderPath))
        {
            Console.Error.WriteLine($"Folder not found: {folderPath}");
            return;
        }

        // Retrieve all files in the folder.
        string[] allFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly);

        foreach (string filePath in allFiles)
        {
            // Process only supported Visio extensions.
            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".vsdx" && ext != ".vsd" && ext != ".vdx" &&
                ext != ".vssx" && ext != ".vstx" && ext != ".vss" && ext != ".vst")
            {
                continue;
            }

            // Guard: ensure the file exists (redundant after GetFiles, but required by rules).
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required.
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Create a custom stylesheet.
                    StyleSheet customStyle = new StyleSheet();
                    // Assign a unique ID (next available) and a name.
                    customStyle.ID = diagram.StyleSheets.Count + 1;
                    customStyle.Name = "CustomStyle";

                    // Configure a simple text style (white text).
                    Aspose.Diagram.Char txtChar = new Aspose.Diagram.Char();
                    txtChar.IX = 0; // character index
                    txtChar.Color.Value = "#FFFFFF"; // white
                    customStyle.Chars.Add(txtChar);

                    // Configure a line style (red line).
                    customStyle.Line.LineColor.Value = "#FF0000";

                    // Configure a fill style (green fill).
                    customStyle.Fill.FillForegnd.Value = "#00FF00";

                    // Add the stylesheet to the diagram's collection.
                    diagram.StyleSheets.Add(customStyle);

                    // Apply the stylesheet to each page using its ID for text, line, and fill.
                    foreach (Page page in diagram.Pages)
                    {
                        page.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);
                    }

                    // Build the output file path (original name with "_styled" suffix).
                    string directory = Path.GetDirectoryName(filePath);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_styled.vsdx");

                    // Save the updated diagram in VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during processing.
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}