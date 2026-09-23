using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two file paths as command‑line arguments.
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: DiagramCustomPropertyComparer <DiagramPath1> <DiagramPath2>");
                return;
            }

            string diagramPath1 = args[0];
            string diagramPath2 = args[1];

            // Load the two diagrams.
            Diagram diagram1 = new Diagram(diagramPath1);
            Diagram diagram2 = new Diagram(diagramPath2);

            // Build dictionaries of custom property name -> value for each diagram.
            var props1 = GetCustomProperties(diagram1);
            var props2 = GetCustomProperties(diagram2);

            // Identify added, removed, and changed properties.
            var added = new List<string>();
            var removed = new List<string>();
            var changed = new List<string>();

            foreach (var kvp in props1)
            {
                if (!props2.ContainsKey(kvp.Key))
                {
                    removed.Add(kvp.Key);
                }
                else if (!string.Equals(kvp.Value, props2[kvp.Key], StringComparison.Ordinal))
                {
                    changed.Add(kvp.Key);
                }
            }

            foreach (var kvp in props2)
            {
                if (!props1.ContainsKey(kvp.Key))
                {
                    added.Add(kvp.Key);
                }
            }

            // Output the differences.
            Console.WriteLine("Custom Property Differences:");
            Console.WriteLine();

            if (added.Count > 0)
            {
                Console.WriteLine("Added:");
                foreach (var name in added)
                {
                    Console.WriteLine($"  {name} = \"{props2[name]}\"");
                }
                Console.WriteLine();
            }

            if (removed.Count > 0)
            {
                Console.WriteLine("Removed:");
                foreach (var name in removed)
                {
                    Console.WriteLine($"  {name} = \"{props1[name]}\"");
                }
                Console.WriteLine();
            }

            if (changed.Count > 0)
            {
                Console.WriteLine("Changed:");
                foreach (var name in changed)
                {
                    Console.WriteLine($"  {name}: \"{props1[name]}\" => \"{props2[name]}\"");
                }
                Console.WriteLine();
            }

            if (added.Count == 0 && removed.Count == 0 && changed.Count == 0)
            {
                Console.WriteLine("No differences found in custom properties.");
            }
        }

        /// <summary>
        /// Extracts custom properties from a diagram into a dictionary.
        /// </summary>
        /// <param name="diagram">The diagram to inspect.</param>
        /// <returns>Dictionary where key = property name, value = property value as string.</returns>
        private static Dictionary<string, string> GetCustomProperties(Diagram diagram)
        {
            var dict = new Dictionary<string, string>(StringComparer.Ordinal);
            var customProps = diagram.DocumentProps.CustomProps;

            for (int i = 0; i < customProps.Count; i++)
            {
                CustomProp prop = customProps[i];
                string name = prop.Name;
                string value = prop.CustomValue.ValueString ?? string.Empty;
                dict[name] = value;
            }

            return dict;
        }
    }