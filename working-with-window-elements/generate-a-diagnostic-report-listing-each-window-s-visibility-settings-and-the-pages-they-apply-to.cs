using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <visio-file-path>");
            return;
        }

        string filePath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"Error: File not found - {filePath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(filePath);

            // Use reflection to obtain the WindowCollection (property name may vary).
            PropertyInfo windowsProp = diagram.GetType().GetProperty("Windows");
            if (windowsProp == null)
            {
                Console.Error.WriteLine("Error: Diagram does not contain a 'Windows' property.");
                return;
            }

            object windowsObj = windowsProp.GetValue(diagram);
            if (windowsObj == null)
            {
                Console.WriteLine("No windows found in the diagram.");
                return;
            }

            // The WindowCollection is expected to be enumerable.
            var windowsEnumerable = windowsObj as System.Collections.IEnumerable;
            if (windowsEnumerable == null)
            {
                Console.Error.WriteLine("Error: Unable to enumerate windows.");
                return;
            }

            Console.WriteLine("Diagnostic Report: Window Visibility Settings");
            Console.WriteLine("------------------------------------------------");

            foreach (object window in windowsEnumerable)
            {
                // Retrieve window identifier (if available) for reporting.
                string windowId = GetPropertyString(window, "ID") ?? "N/A";

                // Retrieve visibility settings using reflection.
                string showGrid = GetBoolPropertyString(window, "ShowGrid");
                string showGuides = GetBoolPropertyString(window, "ShowGuides");
                string showRulers = GetBoolPropertyString(window, "ShowRulers");
                string showPageBreaks = GetBoolPropertyString(window, "ShowPageBreaks");

                // Determine the page(s) the window applies to.
                // Attempt to get a PageID or PageIndex property.
                string pageInfo = GetAssociatedPageInfo(diagram, window);

                // Output the collected information.
                Console.WriteLine($"Window ID: {windowId}");
                Console.WriteLine($"  ShowGrid       : {showGrid}");
                Console.WriteLine($"  ShowGuides     : {showGuides}");
                Console.WriteLine($"  ShowRulers     : {showRulers}");
                Console.WriteLine($"  ShowPageBreaks : {showPageBreaks}");
                Console.WriteLine($"  Applies to Page: {pageInfo}");
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    // Helper to retrieve a string representation of a BOOL property.
    private static string GetBoolPropertyString(object obj, string propertyName)
    {
        PropertyInfo prop = obj.GetType().GetProperty(propertyName);
        if (prop != null)
        {
            object value = prop.GetValue(obj);
            // Aspose BOOL values are of type BOOL (enum). Use ToString().
            return value?.ToString() ?? "Undefined";
        }
        return "PropertyNotFound";
    }

    // Helper to retrieve a string property (e.g., ID).
    private static string GetPropertyString(object obj, string propertyName)
    {
        PropertyInfo prop = obj.GetType().GetProperty(propertyName);
        if (prop != null)
        {
            object value = prop.GetValue(obj);
            return value?.ToString();
        }
        return null;
    }

    // Helper to determine which page a window is associated with.
    private static string GetAssociatedPageInfo(Diagram diagram, object window)
    {
        // Try common property names that may hold a page reference.
        string[] possiblePageProps = { "PageID", "PageIndex", "Page" };

        foreach (string propName in possiblePageProps)
        {
            PropertyInfo prop = window.GetType().GetProperty(propName);
            if (prop != null)
            {
                object pageRef = prop.GetValue(window);
                if (pageRef != null)
                {
                    // If the property is a numeric ID, attempt to locate the page.
                    if (pageRef is int pageIndex)
                    {
                        // Validate index range.
                        if (pageIndex >= 0 && pageIndex < diagram.Pages.Count)
                        {
                            var page = diagram.Pages[pageIndex];
                            string name = GetPropertyString(page, "NameU") ?? $"Page_{pageIndex}";
                            return $"{name} (Index {pageIndex})";
                        }
                        return $"Invalid Page Index {pageIndex}";
                    }
                    else if (pageRef is long pageId)
                    {
                        // Attempt to retrieve by ID using GetPage (if available).
                        MethodInfo getPageMethod = diagram.GetType().GetMethod("GetPage", new Type[] { typeof(long) });
                        if (getPageMethod != null)
                        {
                            var page = getPageMethod.Invoke(diagram, new object[] { pageId });
                            if (page != null)
                            {
                                string name = GetPropertyString(page, "NameU") ?? $"Page_{pageId}";
                                return $"{name} (ID {pageId})";
                            }
                        }
                        return $"Page ID {pageId}";
                    }
                    else
                    {
                        // If the property itself is a Page object, extract its name.
                        string name = GetPropertyString(pageRef, "NameU") ?? pageRef.ToString();
                        return name;
                    }
                }
            }
        }

        // Fallback: no specific page association found.
        return "All Pages (global)";
    }
}