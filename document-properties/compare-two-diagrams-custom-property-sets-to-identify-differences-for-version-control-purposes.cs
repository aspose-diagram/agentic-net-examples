using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two file paths as arguments: diagram1 and diagram2
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramCustomPropertyComparer <DiagramPath1> <DiagramPath2>");
                return;
            }

            string diagramPath1 = args[0];
            string diagramPath2 = args[1];

            // Load the two diagrams
            Diagram diagram1 = new Diagram(diagramPath1);
            Diagram diagram2 = new Diagram(diagramPath2);

            // Retrieve custom property collections
            var customProps1 = diagram1.DocumentProps.CustomProps;
            var customProps2 = diagram2.DocumentProps.CustomProps;

            // Build dictionaries for easy lookup (Name -> ValueString)
            var dict1 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (CustomProp prop in customProps1)
            {
                // Guard against null CustomValue
                string value = prop.CustomValue?.ValueString ?? string.Empty;
                dict1[prop.Name] = value;
            }

            var dict2 = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (CustomProp prop in customProps2)
            {
                string value = prop.CustomValue?.ValueString ?? string.Empty;
                dict2[prop.Name] = value;
            }

            bool differencesFound = false;

            // Compare properties present in diagram1
            foreach (var kvp in dict1)
            {
                string name = kvp.Key;
                string value1 = kvp.Value;

                if (!dict2.ContainsKey(name))
                {
                    differencesFound = true;
                    Console.WriteLine($"Property '{name}' exists in Diagram 1 but not in Diagram 2.");
                }
                else
                {
                    string value2 = dict2[name];
                    if (!string.Equals(value1, value2, StringComparison.Ordinal))
                    {
                        differencesFound = true;
                        Console.WriteLine($"Property '{name}' differs:");
                        Console.WriteLine($"  Diagram 1 value: '{value1}'");
                        Console.WriteLine($"  Diagram 2 value: '{value2}'");
                    }
                }
            }

            // Find properties present only in diagram2
            foreach (var kvp in dict2)
            {
                string name = kvp.Key;
                if (!dict1.ContainsKey(name))
                {
                    differencesFound = true;
                    Console.WriteLine($"Property '{name}' exists in Diagram 2 but not in Diagram 1.");
                }
            }

            if (!differencesFound)
            {
                Console.WriteLine("No differences found in custom property sets between the two diagrams.");
            }
        }
    }