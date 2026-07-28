using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two Visio files to compare
                string filePath1 = @"C:\Diagrams\Diagram1.vsdx";
                string filePath2 = @"C:\Diagrams\Diagram2.vsdx";

                // Load the diagrams using the Aspose.Diagram constructors
                Diagram diagram1 = new Diagram(filePath1);
                Diagram diagram2 = new Diagram(filePath2);

                // Build a lookup dictionary for shapes in the second diagram keyed by universal name (NameU)
                var shapeLookup2 = new Dictionary<string, Shape>(StringComparer.OrdinalIgnoreCase);
                foreach (Page page in diagram2.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (!string.IsNullOrEmpty(shape.NameU))
                        {
                            // If duplicate names exist, the last one wins – adjust as needed for your scenario
                            shapeLookup2[shape.NameU] = shape;
                        }
                    }
                }

                // Iterate through shapes in the first diagram and compare ActiveX control properties
                foreach (Page page1 in diagram1.Pages)
                {
                    foreach (Shape shape1 in page1.Shapes)
                    {
                        if (string.IsNullOrEmpty(shape1.NameU) || !shapeLookup2.TryGetValue(shape1.NameU, out Shape shape2))
                        {
                            // No matching shape in diagram2
                            continue;
                        }

                        // Both shapes exist – check for ActiveX controls
                        var ax1 = shape1.ActiveXControl;
                        var ax2 = shape2.ActiveXControl;

                        if (ax1 == null && ax2 == null)
                        {
                            // Neither shape contains an ActiveX control – nothing to compare
                            continue;
                        }

                        if (ax1 == null || ax2 == null)
                        {
                            Console.WriteLine($"Shape '{shape1.NameU}' ActiveX presence differs between diagrams.");
                            continue;
                        }

                        // Extract property dictionaries for both controls
                        var props1 = GetActiveXProperties(ax1);
                        var props2 = GetActiveXProperties(ax2);

                        // Compare property sets
                        foreach (var kvp in props1)
                        {
                            string propName = kvp.Key;
                            string value1 = kvp.Value;
                            props2.TryGetValue(propName, out string value2);

                            if (!string.Equals(value1, value2, StringComparison.Ordinal))
                            {
                                Console.WriteLine($"Shape '{shape1.NameU}' – Property '{propName}' differs:");
                                Console.WriteLine($"   Diagram1: {value1 ?? "<null>"}");
                                Console.WriteLine($"   Diagram2: {value2 ?? "<null>"}");
                            }
                        }

                        // Detect properties present only in diagram2
                        foreach (var kvp in props2)
                        {
                            if (!props1.ContainsKey(kvp.Key))
                            {
                                Console.WriteLine($"Shape '{shape1.NameU}' – Property '{kvp.Key}' only present in Diagram2 with value: {kvp.Value}");
                            }
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Retrieves all readable public properties of an ActiveXControl instance as a dictionary.
        /// </summary>
        private static Dictionary<string, string> GetActiveXProperties(object activeXControl)
        {
            var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (activeXControl == null)
                return dict;

            // Use reflection to enumerate public instance properties
            PropertyInfo[] props = activeXControl.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pi in props)
            {
                // Skip indexers
                if (pi.GetIndexParameters().Length > 0)
                    continue;

                // Only consider properties that can be read
                if (!pi.CanRead)
                    continue;

                try
                {
                    object val = pi.GetValue(activeXControl);
                    dict[pi.Name] = val?.ToString();
                }
                catch
                {
                    // If a property throws, ignore it for comparison purposes
                    dict[pi.Name] = "<unreadable>";
                }
            }

            return dict;
        }
    }