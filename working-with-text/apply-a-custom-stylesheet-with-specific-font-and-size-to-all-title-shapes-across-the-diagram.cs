using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Create a custom stylesheet for title shapes
                StyleSheet titleStyle = new StyleSheet();
                // Assign a unique ID (must be greater than existing IDs)
                titleStyle.ID = diagram.StyleSheets.Count + 1;

                // Define character formatting: font name and size (12 pt)
                Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
                charFormat.IX = 0; // first character run
                charFormat.FontName.Value = "Arial";
                charFormat.Size.Value = 12.0 / 72.0; // size in inches (points / 72)

                // Add the character definition to the stylesheet
                titleStyle.Chars.Add(charFormat);

                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(titleStyle);

                // Apply the stylesheet to every shape whose universal name is "Title"
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.NameU != null && shape.NameU.Equals("Title", StringComparison.OrdinalIgnoreCase))
                        {
                            // Assign the custom stylesheet to the shape's text style
                            shape.TextStyle = titleStyle;
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }