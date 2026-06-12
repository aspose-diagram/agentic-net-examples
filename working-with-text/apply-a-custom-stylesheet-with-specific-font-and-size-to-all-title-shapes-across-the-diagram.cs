using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram from a file.
                // Replace "input.vsdx" with the actual path to your diagram.
                Diagram diagram = new Diagram("input.vsdx");

                // Create a new stylesheet that will be applied to title shapes.
                StyleSheet titleStyle = new StyleSheet();
                // Assign a unique ID for the stylesheet.
                titleStyle.ID = diagram.StyleSheets.Count + 1;
                // Optional: give the stylesheet a name for identification.
                titleStyle.Name = "TitleStyle";

                // Define character formatting: font name and size.
                Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
                charFormat.IX = 0; // Index of the character run.
                charFormat.FontName.Value = "Arial";               // Desired font.
                charFormat.Size.Value = 12.0 / 72.0;               // Font size in inches (12 points).

                // Add the character formatting to the stylesheet.
                titleStyle.Chars.Add(charFormat);

                // Add the stylesheet to the diagram's collection.
                diagram.StyleSheets.Add(titleStyle);

                // Iterate through all pages and shapes to find title shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify title shapes by their universal name (NameU).
                        // Adjust the condition if your titles are identified differently.
                        if (shape.NameU != null && shape.NameU.Equals("Title", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply the custom stylesheet to the shape's text.
                            shape.TextStyle = titleStyle;
                        }
                    }
                }

                // Save the modified diagram to a new file.
                // Replace "output.vsdx" with the desired output path.
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }