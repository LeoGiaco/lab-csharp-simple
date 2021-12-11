using ComplexAlgebra;

namespace Calculus
{
    /// <summary>
    /// A calculator for <see cref="Complex"/> numbers, supporting 2 operations ('+', '-').
    /// The calculator visualizes a single value at a time.
    /// Users may change the currently shown value as many times as they like.
    /// Whenever an operation button is chosen, the calculator memorises the currently shown value and resets it.
    /// Before resetting, it performs any pending operation.
    /// Whenever the final result is requested, all pending operations are performed and the final result is shown.
    /// The calculator also supports resetting.
    /// </summary>
    ///
    /// HINT: model operations as constants
    /// HINT: model the following _public_ properties methods
    /// HINT: - a property/method for the currently shown value
    /// HINT: - a property/method to let the user request the final result
    /// HINT: - a property/method to let the user reset the calculator
    /// HINT: - a property/method to let the user request an operation
    /// HINT: - the usual ToString() method, which is helpful for debugging
    /// HINT: - you may exploit as many _private_ fields/methods/properties as you like
    ///
    /// TODO: implement the calculator class in such a way that the Program below works as expected
    class Calculator
    {
        public const char OperationPlus = '+';
        public const char OperationMinus = '-';
        private Complex Total = new Complex(0, 0);
        private Complex _value = null;
        public Complex Value 
        {
            get => _value;
            set
            {
                _value = value;
                if(Value != null)
                {
                    if (Operation == null)
                        Total = Value;
                    else 
                        switch(Operation)
                        {
                            case OperationPlus:
                                Total += Value;
                                break;
                            case OperationMinus:
                                Total -= Value;
                                break;
                            default:
                                Total = null;
                                break;
                        }
                }
            }
        }
        private char? _op = null;
        public char? Operation
        {
            get => _op;
            set
            {
                _op = value;
                if(Operation != null)
                    Value = null;   // Reset value to prepare for next operation.
            }
        }

        public void ComputeResult()
        {
            Value = Total;
            Operation = null;
        }

        public void Reset()
        {
            Total = 0;
            Operation = null;
            Value = null;
        }

        public override string ToString() => $"{(Value != null ? Value.ToString() : "null")}, {(Operation != null ? Operation.ToString() : "null")}";
    }
}