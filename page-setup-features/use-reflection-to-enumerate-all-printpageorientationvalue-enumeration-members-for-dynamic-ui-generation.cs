using System;
using System.Reflection;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Get the Type object for the enum
            Type enumType = typeof(PrintPageOrientationValue);

            // Ensure the type is an enum
            if (!enumType.IsEnum)
            {
                Console.WriteLine($"{enumType.FullName} is not an enumeration.");
                return;
            }

            // Retrieve all enum names and their corresponding values
            string[] names = Enum.GetNames(enumType);
            Array values = Enum.GetValues(enumType);

            Console.WriteLine("PrintPageOrientationValue members:");
            for (int i = 0; i < names.Length; i++)
            {
                // Cast the value to its underlying integral type for display
                object rawValue = Convert.ChangeType(values.GetValue(i), Enum.GetUnderlyingType(enumType));
                Console.WriteLine($"- {names[i]} = {rawValue}");
            }

            // Alternative using reflection to get fields (including the underlying value field)
            Console.WriteLine("\nUsing reflection (FieldInfo):");
            FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                object fieldValue = field.GetValue(null);
                Console.WriteLine($"- {field.Name} = {fieldValue}");
            }
        }
    }