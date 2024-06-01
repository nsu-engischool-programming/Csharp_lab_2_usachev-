using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ValueContainer
    {
        public double Value { get; private set; }
        public string ValueType { get; private set; }

        public ValueContainer(double value, string valueType)
        {
            if (!IsValidValueType(valueType))
            {
                throw new ArgumentException($"Invalid valueType: {valueType}. Valid valueTypes are 'Speed', 'Time', 'Length', 'Scalar'.");
            }
            Value = value;
            ValueType = valueType;
        }

        private bool IsValidValueType(string valueType)
        {
            return valueType == "Speed" || valueType == "Time" || valueType == "Length" || valueType == "Scalar";
        }

        public void UpdateValue(double value, string valueType)
        {
            if (!IsValidValueType(valueType))
            {
                throw new ArgumentException($"Invalid valueType: {valueType}. Valid valueTypes are 'Speed', 'Time', 'Length', 'Scalar'.");
            }
            Value = value;
            ValueType = valueType;
        }
    }
}
