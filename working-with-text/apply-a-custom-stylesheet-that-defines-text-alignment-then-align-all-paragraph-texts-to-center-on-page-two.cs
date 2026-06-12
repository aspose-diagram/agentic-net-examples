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

                // -------------------------------------------------
                // 1. Create a custom StyleSheet (placeholder for text alignment settings)
                // -------------------------------------------------
                StyleSheet customStyle = new StyleSheet
                {
                    // Assign a unique ID (next available)
                    ID = diagram.StyleSheets.Count + 1,
                    // Optionally give it a name for identification
                    Name = "CenterAlignmentStyle"
                };
                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(customStyle);

                // -------------------------------------------------
                // 2. Apply the stylesheet to the entire diagram (optional but fulfills the requirement)
                // -------------------------------------------------
                // ApplyStyle takes three IDs: CharStyleID, LineStyleID, FillStyleID.
                // Using the same ID for all three applies the same stylesheet uniformly.
                foreach (Page pg in diagram.Pages)
                {
                    pg.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);
                }

                // -------------------------------------------------
                // 3. Align all paragraph texts to center on page two (index 1)
                // -------------------------------------------------
                if (diagram.Pages.Count > 1)
                {
                    Page pageTwo = diagram.Pages[1]; // Zero‑based index, so 1 is the second page

                    foreach (Shape shape in pageTwo.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Iterate through all paragraphs of the shape
                        for (int i = 0; i < shape.Paras.Count; i++)
                        {
                            // Set horizontal alignment to center
                            shape.Paras[i].HorzAlign.Value = HorzAlignValue.Center;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("The diagram does not contain a second page.");
                }

                // -------------------------------------------------
                // 4. Save the modified diagram
                // -------------------------------------------------
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Diagram processing completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }