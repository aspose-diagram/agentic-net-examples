using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the cleaned Visio file
                string outputPath = "output_cleaned.vsdx";

                // Load the diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // HashSet to keep track of unique event comment formulas
                    HashSet<string> seenComments = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // NOTE: Aspose.Diagram does not define a specific "EventComment" cell.
                            // For the purpose of this example we treat the EventDrop cell as the
                            // location where a comment formula might be stored.
                            // Adjust the cell name if your diagram uses a different event cell.
                            string commentFormula = shape.Event.EventDrop.Ufe.F;

                            // If the cell is empty, skip processing
                            if (string.IsNullOrWhiteSpace(commentFormula))
                                continue;

                            // If this comment has already been encountered, clear it to remove the duplicate
                            if (seenComments.Contains(commentFormula))
                            {
                                // Clear the duplicate comment by setting an empty formula
                                shape.Event.EventDrop.Ufe.F = string.Empty;
                            }
                            else
                            {
                                // First occurrence – keep it and record the formula
                                seenComments.Add(commentFormula);
                            }
                        }
                    }

                    // Save the modified diagram
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Duplicate event comments have been removed and the diagram saved.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }