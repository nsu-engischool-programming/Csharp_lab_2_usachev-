using Calculator;
class Program
{
    static void Main()
    {
        Calculator.Calculator calculator = new Calculator.Calculator();
        while (true)
        {
            try
            {
                Console.Write("Enter command: ");
                string input = Console.ReadLine();
                if (input == null) continue;
                var parts = input.Split(' ');

                switch (parts[0])
                {
                    case "push":
                        if (parts[1] == "operator" && parts.Length == 3)
                        {
                            calculator.PushOperator(parts[2]);
                        }
                        else if (parts[1] == "value" && parts.Length == 5)
                        {
                            double value = double.Parse(parts[2]);
                            string valueType = parts[3];
                            string id = parts[4];
                            calculator.PushValue(value, valueType, id);
                        }
                        else
                        {
                            Console.WriteLine("Invalid command format for push.");
                        }
                        break;

                    case "change":
                        if (parts[1] == "value" && parts.Length == 5)
                        {
                            double value = double.Parse(parts[2]);
                            string valueType = parts[3];
                            string id = parts[4];
                            calculator.ChangeValue(value, valueType, id);
                        }
                        else
                        {
                            Console.WriteLine("Invalid command format for change.");
                        }
                        break;

                    case "remove":
                        if (parts[1] == "value" && parts.Length == 3)
                        {
                            string id = parts[2];
                            calculator.RemoveValue(id);
                        }
                        else
                        {
                            Console.WriteLine("Invalid command format for remove.");
                        }
                        break;

                    case "compute":
                        double result = calculator.Compute();
                        Console.WriteLine($"Result: {result}");
                        break;

                    default:
                        Console.WriteLine("Unknown command.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}