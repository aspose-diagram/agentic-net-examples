using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output Visio file with the applied theme
                string outputPath = "output_arial_theme.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Create a new stylesheet that sets the font family to Arial
                    StyleSheet arialStyle = new StyleSheet();
                    // Assign a unique ID (next available)
                    arialStyle.ID = diagram.StyleSheets.Count + 1;
                    // Optional: give the stylesheet a name for reference
                    arialStyle.Name = "ArialTheme";

                    // Define a character formatting entry with FontName = Arial
                    Aspose.Diagram.Char arialChar = new Aspose.Diagram.Char();
                    arialChar.IX = 0; // Index of the character run
                    arialChar.FontName.Value = "Arial";
                    // Add the character entry to the stylesheet
                    arialStyle.Chars.Add(arialChar);

                    // Add the stylesheet to the diagram's collection
                    diagram.StyleSheets.Add(arialStyle);

                    // Apply the stylesheet to every page in the diagram
                    foreach (Page page in diagram.Pages)
                    {
                        // Apply the same style for characters, lines, and fills
                        page.ApplyStyle(arialStyle.ID, arialStyle.ID, arialStyle.ID);
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Theme applied and diagram saved to: " + outputPath);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }