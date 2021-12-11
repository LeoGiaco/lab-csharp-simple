using System;
using System.Text;

namespace ComplexAlgebra
{
    /// <summary>
    /// A type for representing Complex numbers.
    /// </summary>
    public class Complex
    {
        /// <summary>
        /// The real part of the complex number.
        /// </summary>
        public double Real { get; private set; }
        /// <summary>
        /// The imaginary part of the complex number.
        /// </summary>
        public double Imaginary { get; private set; }
        /// <summary>
        /// The modulus, or magnitude, of the complex number.
        /// </summary>
        public double Modulus => Math.Sqrt(Math.Pow(Real, 2) + Math.Pow(Imaginary, 2));
        /// <summary>
        /// The phase, or argument, of the complex number.
        /// </summary>
        public double Phase => Math.Atan2(Imaginary, Real);

        /// <summary>
        /// Creates a new Complex number.
        /// </summary>
        /// <param name="real"> The real part of the number. </param>
        /// <param name="img"> The imaginary part of the number. </param>
        public Complex(double real, double img)
        {
            Real = real;
            Imaginary = img;
        }

        /// <summary>
        /// The complex conjugate of the complex number.
        /// </summary>
        public Complex Complement() => new Complex(Real, -Imaginary);

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Complex))
                return false;
            else
            {
                Complex c = (Complex)obj;
                return Real == c.Real && Imaginary == c.Imaginary;
            }    
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            if (Real != 0 || Imaginary == 0)
            {
                sb.Append(Real);
                if (Imaginary < 0)
                    sb.Append(" - ");
                else if (Imaginary > 0)
                    sb.Append(" + ");
            }
            else
            {
                if (Imaginary < 0)
                    sb.Append("-");
            }
            if (Imaginary != 0 && Math.Abs(Imaginary) != 1)
                sb.Append(Math.Abs(Imaginary));
            if (Imaginary != 0)
                sb.Append("i");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the sum of two complex numbers.
        /// </summary>
        /// <param name="c1"> The first complex number. </param>
        /// <param name="c2"> The second complex number. </param>
        /// <returns> The sum of the two complex numbers. </returns>
        public static Complex operator +(Complex c1, Complex c2) => new Complex(c1.Real + c2.Real, c1.Imaginary + c2.Imaginary);
        /// <summary>
        /// Returns the difference of two complex numbers.
        /// </summary>
        /// <param name="c1"> The first complex number. </param>
        /// <param name="c2"> The second complex number. </param>
        /// <returns> The difference of the two complex numbers. </returns>
        public static Complex operator -(Complex c1, Complex c2) => new Complex(c1.Real - c2.Real, c1.Imaginary - c2.Imaginary);
        /// <summary>
        /// Returns a boolean indicating whether the two Complex numbers are equal.
        /// </summary>
        /// <param name="c1"> The first complex number. </param>
        /// <param name="c2"> The second complex number. </param>
        /// <returns> True if they are equal, false otherwise. </returns>
        public static bool operator ==(Complex c1, Complex c2) => c1.Equals(c2);
        /// <summary>
        /// Returns a boolean indicating whether the two Complex numbers are different.
        /// </summary>
        /// <param name="c1"> The first complex number. </param>
        /// <param name="c2"> The second complex number. </param>
        /// <returns> True if they are different, false otherwise. </returns>
        public static bool operator !=(Complex c1, Complex c2) => !(c1 == c2);
    }
}