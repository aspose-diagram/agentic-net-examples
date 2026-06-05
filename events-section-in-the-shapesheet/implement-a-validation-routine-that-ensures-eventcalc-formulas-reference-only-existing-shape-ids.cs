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

                // Path to the Visio file to validate
                string inputPath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

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

                // Regular expression to find shape ID references like "Sheet.123"
                Regex sheetIdRegex = new Regex(@"Sheet\.(\d+)", RegexOptions.IgnoreCase);

                // Iterate through all shapes and inspect their event formulas
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Helper local function to process a formula string
                        void ProcessFormula(string formula, string eventName)
                        {
                            if (string.IsNullOrWhiteSpace(formula))
                                return;

                            foreach (Match match in sheetIdRegex.Matches(formula))
                            {
                                if (long.TryParse(match.Groups[1].Value, out long referencedId))
                                {
                                    if (!existingShapeIds.Contains(referencedId))
                                    {
                                        string message = $"Shape ID {shape.ID} on page '{page.Name}' has {eventName} formula referencing non‑existent shape ID {referencedId}.";
                                        validationErrors.Add(message);
                                    }
                                }
                            }
                        }

                        // Check each supported event cell
                        ProcessFormula(shape.Event.EventXFMod?.Ufe?.F, "EventXFMod");
                        ProcessFormula(shape.Event.EventDblClick?.Ufe?.F, "EventDblClick");
                        ProcessFormula(shape.Event.EventDrop?.Ufe?.F, "EventDrop");
                        ProcessFormula(shape.Event.EventMultiDrop?.Ufe?.F, "EventMultiDrop");
                        ProcessFormula(shape.Event.TheText?.Ufe?.F, "TheText");
                        ProcessFormula(shape.Event.TheData?.Ufe?.F, "TheData");
                    }
                }

                // Report results
                if (validationErrors.Count == 0)
                {
                    Console.WriteLine("Validation passed: all EventCalc formulas reference existing shape IDs.");
                }
                else
                {
                    Console.WriteLine("Validation failed: found references to non‑existent shape IDs.");
                    foreach (string error in validationErrors)
                    {
                        Console.WriteLine(error);
                    }

                    // Optionally, throw an exception to halt execution
                    throw new Exception("EventCalc validation errors detected.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }