using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two file paths: first diagram and second diagram
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: DiagramCustomPropertyComparer <DiagramPath1> <DiagramPath2>");
                return;
            }

            string path1 = args[0];
            string path2 = args[1];

            try
            {
                // Load the two diagrams
                using (Diagram diagram1 = new Diagram(path1))
                using (Diagram diagram2 = new Diagram(path2))
                {
                    // Extract custom properties into dictionaries for easy lookup
                    var props1 = BuildCustomPropertyDictionary(diagram1);
                    var props2 = BuildCustomPropertyDictionary(diagram2);

                    // Compare properties present in the first diagram
                    foreach (var kvp in props1)
                    {
                        string name = kvp.Key;
                        string value1 = kvp.Value;

                        if (props2.TryGetValue(name, out string value2))
                        {
                            if (!string.Equals(value1, value2, StringComparison.Ordinal))
                            {
                                Console.WriteLine($"[Modified] Property '{name}': '{value1}' => '{value2}'");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"[Removed] Property '{name}' exists only in the first diagram with value '{value1}'.");
                        }
                    }

                    // Find properties that exist only in the second diagram
                    foreach (var kvp in props2)
                    {
                        string name = kvp.Key;
                        if (!props1.ContainsKey(name))
                        {
                            Console.WriteLine($"[Added] Property '{name}' exists only in the second diagram with value '{kvp.Value}'.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Any unexpected error is reported via exception
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // Helper method to build a dictionary of custom property name -> value string
        private static Dictionary<string, string> BuildCustomPropertyDictionary(Diagram diagram)
        {
            var dict = new Dictionary<string, string>(StringComparer.Ordinal);
            var customProps = diagram.DocumentProps.CustomProps;

            for (int i = 0; i < customProps.Count; i++)
            {
                var prop = customProps[i];
                // Use the property name as the key and its string value as the value
                string name = prop.Name;
                string value = prop.CustomValue?.ValueString ?? string.Empty;
                dict[name] = value;
            }

            return dict;
        }
    }