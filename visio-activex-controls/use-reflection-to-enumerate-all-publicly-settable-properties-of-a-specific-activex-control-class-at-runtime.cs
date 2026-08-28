using System;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // Add a CommandButton ActiveX control to the active page
                // Parameters: control type, pinX, pinY, width, height (in inches)
                long shapeId = diagram.ActivePage.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);

                // Retrieve the shape that contains the ActiveX control
                Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                // Get the ActiveX control instance and cast to its specific type
                CommandButtonActiveXControl commandButton = shape.ActiveXControl as CommandButtonActiveXControl;
                if (commandButton == null)
                {
                    Console.WriteLine("Failed to retrieve the CommandButton ActiveX control.");
                    return;
                }

                // Use reflection to enumerate all publicly settable properties
                Type controlType = commandButton.GetType();
                PropertyInfo[] properties = controlType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

                Console.WriteLine($"Publicly settable properties of {controlType.Name}:");
                foreach (PropertyInfo prop in properties)
                {
                    // Consider a property settable if it has a public setter
                    MethodInfo setMethod = prop.GetSetMethod();
                    if (setMethod != null && setMethod.IsPublic)
                    {
                        Console.WriteLine($"- {prop.Name} ({prop.PropertyType.Name})");
                    }
                }

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }