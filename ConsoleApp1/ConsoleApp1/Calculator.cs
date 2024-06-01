using Calculator;

namespace Calculator
{
    public class Calculator
    {
        private HashSet<string> operators = ["+", "-", "*", "/", "(", ")"];
        private Dictionary<string, ValueContainer> values = [];
        private List<string> expression = [];

        public void PushOperator(string operatorToken)
        {
            if (operators.Contains(operatorToken))
            {
                expression.Add(operatorToken);
            }
            else
            {
                throw new ArgumentException($"Invalid operator: {operatorToken}. Valid operators are {string.Join(", ", operators)}");
            }
        }

        public void PushValue(double value, string valueType, string id)
        {
            if (values.ContainsKey(id))
            {
                throw new ArgumentException($"Value with id {id} already exists.");
            }
            values[id] = new ValueContainer(value, valueType);
            expression.Add(id);
        }

        public void ChangeValue(double value, string valueType, string id)
        {
            if (!values.ContainsKey(id))
            {
                throw new ArgumentException($"Value with id {id} does not exist.");
            }
            values[id].UpdateValue(value, valueType);
        }

        public void RemoveValue(string id)
        {
            if (!expression.Contains(id))
            {
                throw new ArgumentException($"Value with id {id} does not exist in expression.");
            }
            for (int i = 0; i < expression.Count; i++)
            {
                if (expression[i] == id)
                {
                    string neutralElement = GetNeutralElement(i);
                    expression[i] = neutralElement;
                }
            }
        }

        public double Compute()
        {
            try
            {
                return EvaluateExpression();
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Error computing expression: {e.Message}");
            }
        }

        private string GetNeutralElement(int index)
        {
            if (index == 0 || operators.Contains(expression[index - 1]))
            {
                return "0";
            }
            else if (expression[index - 1] == "*" || expression[index - 1] == "/")
            {
                return "1";
            }
            else
            {
                throw new InvalidOperationException("Invalid expression format.");
            }
        }
        private double EvaluateExpression()
        {
            Stack<double> valuesStack = new Stack<double>();
            Stack<string> operatorsStack = new Stack<string>();
            Stack<string> typesStack = new Stack<string>();

            foreach (var token in expression)
            {
                if (operators.Contains(token))
                {
                    operatorsStack.Push(token);
                }
                else
                {
                    var valueContainer = values[token];
                    valuesStack.Push(valueContainer.Value);
                    typesStack.Push(valueContainer.ValueType);

                    if (valuesStack.Count >= 2 && operatorsStack.Count >= 1)
                    {
                        string rightType = typesStack.Pop();
                        double rightValue = valuesStack.Pop();
                        string leftType = typesStack.Pop();
                        double leftValue = valuesStack.Pop();
                        string operatorToken = operatorsStack.Pop();

                        if (leftType != rightType)
                        {
                            throw new InvalidOperationException($"Cannot apply operator {operatorToken} to different value types: {leftType} and {rightType}.");
                        }

                        double result;
                        switch (operatorToken)
                        {
                            case "+":
                                result = leftValue + rightValue;
                                break;
                            case "-":
                                result = leftValue - rightValue;
                                break;
                            case "*":
                                result = leftValue * rightValue;
                                break;
                            case "/":
                                if (rightValue == 0)
                                    throw new DivideByZeroException("Cannot divide by zero.");
                                result = leftValue / rightValue;
                                break;
                            default:
                                throw new InvalidOperationException($"Invalid operator: {operatorToken}");
                        }

                        valuesStack.Push(result);
                        typesStack.Push(leftType);
                    }
                }
            }

            if (valuesStack.Count == 1)
            {
                return valuesStack.Pop();
            }

            throw new InvalidOperationException("Invalid expression.");
        }

        public ValueContainer GetValue(string id)
        {
            if (values.ContainsKey(id))
            {
                return values[id];
            }
            throw new ArgumentException($"Value with id {id} does not exist.");
        }
    }
}