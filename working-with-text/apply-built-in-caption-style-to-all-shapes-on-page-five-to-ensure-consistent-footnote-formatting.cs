using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";
                // Path to the output Visio file
                const string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Ensure the diagram has at least five pages
                if (diagram.Pages.Count < 5)
                {
                    throw new Exception("The diagram does not contain a fifth page.");
                }

                // Retrieve page five (zero‑based index 4)
                Page pageFive = diagram.Pages[4];

                // Locate the built‑in 'Caption' style sheet
                StyleSheet captionStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "Caption")
                    {
                        captionStyle = ss;
                        break;
                    }
                }

                if (captionStyle == null)
                {
                    throw new Exception("The 'Caption' style sheet was not found in the diagram.");
                }

                // Apply the 'Caption' style to every shape on page five
                foreach (Shape shape in pageFive.Shapes)
                {
                    // Assign the style to the shape's text formatting
                    shape.TextStyle = captionStyle;
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }