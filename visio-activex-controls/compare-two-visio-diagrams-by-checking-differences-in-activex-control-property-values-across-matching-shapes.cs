using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two Visio files to compare
                string filePath1 = @"C:\Diagrams\DiagramA.vsdx";
                string filePath2 = @"C:\Diagrams\DiagramB.vsdx";

                // Load the diagrams using the built‑in constructors (lifecycle rule)
                Diagram diagram1 = new Diagram(filePath1);
                Diagram diagram2 = new Diagram(filePath2);

                // Build a lookup of shapes from diagram2 by universal name (NameU)
                var shapesLookup = new Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
                foreach (Page page in diagram2.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (!string.IsNullOrEmpty(shape.NameU))
                        {
                            // If duplicate names exist, keep the first occurrence
                            if (!shapesLookup.ContainsKey(shape.NameU))
                                shapesLookup.Add(shape.NameU, shape);
                        }
                    }
                }

                // Iterate through shapes of diagram1 and compare ActiveX control properties
                foreach (Page page in diagram1.Pages)
                {
                    foreach (Shape shape1 in page.Shapes)
                    {
                        // Find matching shape in the second diagram
                        if (string.IsNullOrEmpty(shape1.NameU) || !shapesLookup.TryGetValue(shape1.NameU, out Shape shape2))
                            continue; // No matching shape

                        // Both shapes must contain an ActiveX control
                        if (shape1.ActiveXControl == null || shape2.ActiveXControl == null)
                            continue;

                        // Compare all public properties of the ActiveXControl objects via reflection
                        var type = shape1.ActiveXControl.GetType();
                        var properties = type.GetProperties();

                        foreach (var prop in properties)
                        {
                            object val1 = prop.GetValue(shape1.ActiveXControl);
                            object val2 = prop.GetValue(shape2.ActiveXControl);

                            // Simple equality check (handles nulls)
                            bool areEqual = (val1 == null && val2 == null) ||
                                            (val1 != null && val1.Equals(val2));

                            if (!areEqual)
                            {
                                Console.WriteLine($"Difference found in shape '{shape1.NameU}' (Page: {page.Name}):");
                                Console.WriteLine($"  Property '{prop.Name}': DiagramA = '{val1 ?? "null"}', DiagramB = '{val2 ?? "null"}'");
                            }
                        }
                    }
                }

                // Cleanup
                diagram1.Dispose();
                diagram2.Dispose();

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }