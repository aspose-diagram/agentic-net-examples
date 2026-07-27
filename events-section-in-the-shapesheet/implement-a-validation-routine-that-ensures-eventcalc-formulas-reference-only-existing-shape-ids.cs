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

                // Path to the Visio diagram file (adjust as needed)
                string diagramPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Collect all existing shape IDs across all pages
                HashSet<long> existingShapeIds = new HashSet<long>();
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        existingShapeIds.Add(shape.ID);
                    }
                }

                // Prepare a list to hold validation error messages
                List<string> validationErrors = new List<string>();

                // Regular expression to find shape ID references in formulas (e.g., Sheet.12!Cell)
                Regex sheetIdRegex = new Regex(@"Sheet\.(\d+)", RegexOptions.Compiled);

                // Iterate through all shapes and examine their event formulas
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the Event section exists
                        if (shape.Event == null)
                            continue;

                        // Helper local function to validate a single formula
                        void ValidateFormula(string formula, string eventName)
                        {
                            if (string.IsNullOrWhiteSpace(formula))
                                return;

                            foreach (Match match in sheetIdRegex.Matches(formula))
                            {
                                if (long.TryParse(match.Groups[1].Value, out long referencedId))
                                {
                                    if (!existingShapeIds.Contains(referencedId))
                                    {
                                        string message = $"Shape ID {shape.ID} has {eventName} formula referencing non‑existent shape ID {referencedId}.";
                                        validationErrors.Add(message);
                                    }
                                }
                            }
                        }

                        // Validate each known event cell
                        ValidateFormula(shape.Event.EventXFMod?.Ufe?.F, "EventXFMod");
                        ValidateFormula(shape.Event.EventDblClick?.Ufe?.F, "EventDblClick");
                        ValidateFormula(shape.Event.EventDrop?.Ufe?.F, "EventDrop");
                        ValidateFormula(shape.Event.EventMultiDrop?.Ufe?.F, "EventMultiDrop");
                        ValidateFormula(shape.Event.TheText?.Ufe?.F, "TheText");
                        ValidateFormula(shape.Event.TheData?.Ufe?.F, "TheData");
                    }
                }

                // Output validation results
                if (validationErrors.Count == 0)
                {
                    Console.WriteLine("All Event formulas reference existing shape IDs.");
                }
                else
                {
                    Console.WriteLine("Validation errors found:");
                    foreach (string error in validationErrors)
                    {
                        Console.WriteLine(error);
                    }

                    // Optionally, throw an exception to indicate failure
                    throw new Exception("Event formula validation failed. See console output for details.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }