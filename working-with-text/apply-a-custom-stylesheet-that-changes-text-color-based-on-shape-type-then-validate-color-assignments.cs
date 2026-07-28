using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths (replace with actual file locations as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Create style for Rectangle shapes (red text)
                StyleSheet rectStyle = new StyleSheet();
                rectStyle.ID = diagram.StyleSheets.Count + 1;
                rectStyle.Name = "RectangleStyle";
                Aspose.Diagram.Char rectChar = new Aspose.Diagram.Char();
                rectChar.IX = 0;
                rectChar.Color.Value = "#FF0000"; // Red
                rectStyle.Chars.Add(rectChar);
                diagram.StyleSheets.Add(rectStyle);

                // Create style for Process shapes (green text)
                StyleSheet procStyle = new StyleSheet();
                procStyle.ID = diagram.StyleSheets.Count + 1;
                procStyle.Name = "ProcessStyle";
                Aspose.Diagram.Char procChar = new Aspose.Diagram.Char();
                procChar.IX = 0;
                procChar.Color.Value = "#00FF00"; // Green
                procStyle.Chars.Add(procChar);
                diagram.StyleSheets.Add(procStyle);

                // Create style for Decision shapes (blue text)
                StyleSheet decStyle = new StyleSheet();
                decStyle.ID = diagram.StyleSheets.Count + 1;
                decStyle.Name = "DecisionStyle";
                Aspose.Diagram.Char decChar = new Aspose.Diagram.Char();
                decChar.IX = 0;
                decChar.Color.Value = "#0000FF"; // Blue
                decStyle.Chars.Add(decChar);
                diagram.StyleSheets.Add(decStyle);

                // Apply styles based on master shape name
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master == null) continue;

                        string masterName = shape.Master.Name;
                        if (masterName == "Rectangle")
                        {
                            page.ApplyStyle(rectStyle.ID, rectStyle.ID, rectStyle.ID);
                        }
                        else if (masterName == "Process")
                        {
                            page.ApplyStyle(procStyle.ID, procStyle.ID, procStyle.ID);
                        }
                        else if (masterName == "Decision")
                        {
                            page.ApplyStyle(decStyle.ID, decStyle.ID, decStyle.ID);
                        }
                    }
                }

                // Validation of text color assignments
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master == null) continue;

                        string expectedColor = null;
                        string masterName = shape.Master.Name;
                        if (masterName == "Rectangle")
                            expectedColor = "#FF0000";
                        else if (masterName == "Process")
                            expectedColor = "#00FF00";
                        else if (masterName == "Decision")
                            expectedColor = "#0000FF";

                        if (expectedColor != null)
                        {
                            // Retrieve the inherited character formatting (first char)
                            Aspose.Diagram.Char inheritedChar = shape.InheritChars.GetChar(0);
                            string actualColor = inheritedChar.Color.Value;

                            if (!string.Equals(actualColor, expectedColor, StringComparison.OrdinalIgnoreCase))
                            {
                                throw new Exception($"Color mismatch on shape ID {shape.ID}. Expected {expectedColor}, found {actualColor}.");
                            }
                            else
                            {
                                Console.WriteLine($"Shape ID {shape.ID} ({masterName}) correctly has color {actualColor}.");
                            }
                        }
                    }
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