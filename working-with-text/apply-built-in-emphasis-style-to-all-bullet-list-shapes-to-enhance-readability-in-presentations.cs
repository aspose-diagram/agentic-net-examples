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
                var diagram = new Diagram("input.vsdx");

                // Locate the built‑in "Emphasis" style sheet (if it exists)
                StyleSheet emphasisStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "Emphasis")
                    {
                        emphasisStyle = ss;
                        break;
                    }
                }

                // If the style sheet is not found, inform the user and exit
                if (emphasisStyle == null)
                {
                    Console.WriteLine("Emphasis style sheet not found in the document.");
                    return;
                }

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Determine whether the shape contains any bullet‑formatted paragraphs
                        bool hasBullet = false;
                        foreach (var para in shape.Paras)
                        {
                            // Bullet cell may be null for some shapes; guard against it
                            if (para.Bullet != null && para.Bullet.Value != BulletValue.None)
                            {
                                hasBullet = true;
                                break;
                            }
                        }

                        // Apply the Emphasis style to shapes that contain bullet lists
                        if (hasBullet)
                        {
                            shape.TextStyle = emphasisStyle;
                            shape.FillStyle = emphasisStyle;
                            shape.LineStyle = emphasisStyle;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Emphasis style applied and diagram saved as output.vsdx");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }