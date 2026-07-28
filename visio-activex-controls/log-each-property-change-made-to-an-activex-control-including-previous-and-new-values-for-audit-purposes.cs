using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes to locate ActiveX controls
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Each shape may contain an ActiveX control; null if none
                    ActiveXControl control = shape.ActiveXControl;
                    if (control == null) continue;

                    // Use the shape name as a simple identifier for logging
                    string controlId = shape.Name;

                    // Example: change BackOleColor and log the change
                    LogPropertyChange(controlId, "BackOleColor", control.BackOleColor, 0x00FF00);
                    control.BackOleColor = 0x00FF00;

                    // Example: change IsEnabled and log the change
                    LogPropertyChange(controlId, "IsEnabled", control.IsEnabled, false);
                    control.IsEnabled = false;

                    // Example: change Width and log the change
                    LogPropertyChange(controlId, "Width", control.Width, 2.5);
                    control.Width = 2.5;
                }
            }

            // Save the modified diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Logs a property change with previous and new values
    static void LogPropertyChange(string controlId, string propertyName, object oldValue, object newValue)
    {
        // Only log when the value actually changes
        if (!Equals(oldValue, newValue))
        {
            Console.WriteLine($"Control '{controlId}': Property '{propertyName}' changed from '{oldValue}' to '{newValue}'.");
        }
    }
}
