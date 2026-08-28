using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram to validate
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Perform validation
                ValidateEventFormulas(diagram);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Validates that all shape IDs referenced in event formulas exist in the diagram.
        /// </summary>
        /// <param name="diagram">The loaded Aspose.Diagram instance.</param>
        private static void ValidateEventFormulas(Diagram diagram)
        {
            // Collect all existing shape IDs across all pages
            HashSet<long> existingShapeIds = new HashSet<long>();
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    existingShapeIds.Add(shape.ID);
                }
            }

            // Regular expression to capture shape IDs in formulas (e.g., Sheet.5)
            Regex sheetIdRegex = new Regex(@"Sheet\.(\d+)", RegexOptions.Compiled);

            // Iterate through each shape and inspect its event formulas
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // List of event cells to check
                    var eventFormulas = new List<string>();

                    // Guard against null Event section
                    if (shape.Event != null)
                    {
                        // Known event cells (add more if needed)
                        if (!string.IsNullOrEmpty(shape.Event.EventXFMod?.Ufe?.F))
                            eventFormulas.Add(shape.Event.EventXFMod.Ufe.F);
                        if (!string.IsNullOrEmpty(shape.Event.EventDblClick?.Ufe?.F))
                            eventFormulas.Add(shape.Event.EventDblClick.Ufe.F);
                        if (!string.IsNullOrEmpty(shape.Event.EventDrop?.Ufe?.F))
                            eventFormulas.Add(shape.Event.EventDrop.Ufe.F);
                        if (!string.IsNullOrEmpty(shape.Event.EventMultiDrop?.Ufe?.F))
                            eventFormulas.Add(shape.Event.EventMultiDrop.Ufe.F);
                        if (!string.IsNullOrEmpty(shape.Event.TheText?.Ufe?.F))
                            eventFormulas.Add(shape.Event.TheText.Ufe.F);
                        if (!string.IsNullOrEmpty(shape.Event.TheData?.Ufe?.F))
                            eventFormulas.Add(shape.Event.TheData.Ufe.F);
                    }

                    // Validate each formula
                    foreach (string formula in eventFormulas)
                    {
                        foreach (Match match in sheetIdRegex.Matches(formula))
                        {
                            if (long.TryParse(match.Groups[1].Value, out long referencedId))
                            {
                                if (!existingShapeIds.Contains(referencedId))
                                {
                                    Console.WriteLine($"Error: Shape ID {shape.ID} on page '{page.Name}' references non‑existent shape ID {referencedId} in formula \"{formula}\".");
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine("Event formula validation completed.");
        }
    }