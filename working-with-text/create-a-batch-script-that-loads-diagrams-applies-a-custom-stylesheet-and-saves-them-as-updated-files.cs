using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine the folder containing diagrams.
            // If a path is passed as an argument, use it; otherwise use the current directory.
            string inputFolder = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();

            // Get all Visio files (VSDX) in the folder.
            string[] diagramFiles = Directory.GetFiles(inputFolder, "*.vsdx");

            foreach (string filePath in diagramFiles)
            {
                try
                {
                    // Load the diagram from file.
                    Diagram diagram = new Diagram(filePath);

                    // -------------------------------------------------
                    // Create a custom stylesheet.
                    // -------------------------------------------------
                    StyleSheet customStyle = new StyleSheet();
                    // Assign a unique ID.
                    customStyle.ID = diagram.StyleSheets.Count + 1;

                    // Define character (text) formatting.
                    Aspose.Diagram.Char textChar = new Aspose.Diagram.Char();
                    textChar.IX = 0; // first character run
                    textChar.Color.Value = "#FF0000"; // red text
                    customStyle.Chars.Add(textChar);

                    // Define fill formatting.
                    customStyle.Fill.FillForegnd.Value = "#00FF00"; // green fill
                    customStyle.Fill.FillPattern.Value = 1; // solid fill (pattern index)

                    // Define line formatting.
                    customStyle.Line.LineColor.Value = "#0000FF"; // blue line
                    customStyle.Line.LineWeight.Value = 0.02; // line weight in inches

                    // Add the stylesheet to the diagram.
                    diagram.StyleSheets.Add(customStyle);

                    // -------------------------------------------------
                    // Apply the custom stylesheet to all shapes on all pages.
                    // -------------------------------------------------
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Apply the stylesheet to text, fill, and line.
                            shape.TextStyle = customStyle;
                            shape.FillStyle = customStyle;
                            shape.LineStyle = customStyle;
                        }
                    }

                    // -------------------------------------------------
                    // Save the updated diagram with a new file name.
                    // -------------------------------------------------
                    string directory = Path.GetDirectoryName(filePath);
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    string outputPath = Path.Combine(directory, $"{fileNameWithoutExt}_updated.vsdx");

                    // Use the Save overload that takes a file name and a SaveFileFormat.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
                catch (Exception ex)
                {
                    // Log any errors for the current file and continue with the next one.
                    Console.WriteLine($"Error processing file '{filePath}': {ex.Message}");
                }
            }

            Console.WriteLine("Batch processing completed.");
        }
    }