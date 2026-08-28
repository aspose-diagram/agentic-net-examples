using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the CSV file containing Unicode text (UTF‑8 encoded)
                string csvPath = "data.csv";

                // Path to the output Visio diagram
                string outputPath = "output.vsdx";

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Get the first (default) page
                Page page = diagram.ActivePage;

                // Read all lines from the CSV using UTF‑8 encoding to preserve Unicode characters
                string[] lines = File.ReadAllLines(csvPath, Encoding.UTF8);

                // Simple layout: start at (1,1) inches and move down 1.5 inches per line
                double startX = 1.0;
                double startY = 1.0;
                double lineHeight = 1.5;
                double textBoxWidth = 4.0;
                double textBoxHeight = 0.5;

                for (int i = 0; i < lines.Length; i++)
                {
                    string text = lines[i];

                    // Add a text shape for each line; AddText returns a Shape object
                    Shape textShape = page.AddText(
                        startX,
                        startY + i * lineHeight,
                        textBoxWidth,
                        textBoxHeight,
                        text);

                    // Optional: set a font that supports Unicode characters
                    // This ensures characters are rendered correctly when the diagram is saved
                    textShape.Text.Value.Clear();
                    textShape.Text.Value.Add(new Txt(text));
                    textShape.TextStyle = null; // use default style
                }

                // Configure save options to use a Unicode‑capable default font
                DiagramSaveOptions saveOptions = new DiagramSaveOptions();
                saveOptions.DefaultFont = "Arial Unicode MS";

                // Save the diagram in VSDX format
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }