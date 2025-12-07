// Feras Akileh 
// Matrix Programming Assignment 
// Braeden Jeske
// C# Programming
using System;

public class Matrix
{
    // data elements are stored as a private array of floats
    private float[] data;
    
    // property for the shape of the matrix
    public (int Rows, int Cols) Shape { get; private set; }

    // constructor that takes arguments for the shape of the matrix
    public Matrix(int rows, int cols)
    {
        // check for positive matrix dimensions
        if (rows <= 0 || cols <= 0)
            throw new ArgumentException("Matrix dimensions must be positive integers.");

        Shape = (rows, cols);
        data = new float[rows * cols];
    }

    // indexer for setting the elements (i, j)
    public float this[int i, int j]
    {
        get
        {
            ValidateIndices(i, j);
            return data[i * Shape.Cols + j];
        }
        set
        {
            ValidateIndices(i, j);
            data[i * Shape.Cols + j] = value;
        }
    }

    // private method that ensures the indices are in range
    private void ValidateIndices(int i, int j)
    {
        if (i < 0 || i >= Shape.Rows || j < 0 || j >= Shape.Cols)
            throw new IndexOutOfRangeException("Matrix index is out of range.");
    }

    // this is where the operators are overloaded

    // matrix addition
    public static Matrix operator +(Matrix a, Matrix b)
    {
        if (a.Shape != b.Shape)
            throw new ArgumentException("Matrix addition requires the matrices to be the same shape.");

        // create a new matrix with the values of a 
        Matrix result = new Matrix(a.Shape.Rows, a.Shape.Cols);
        // loop through and do the element wise addition
        for (int i = 0; i < a.data.Length; i++)
            result.data[i] = a.data[i] + b.data[i];
        return result;
    }

    // matrix subtraction
    public static Matrix operator -(Matrix a, Matrix b)
    {
        
        if (a.Shape != b.Shape)
            throw new ArgumentException("Matrix subtraction requires the matrices to be the same shape.");

        // create a new matrix with the values of a 
        Matrix result = new Matrix(a.Shape.Rows, a.Shape.Cols);
        // loop through and do the element wise subtraction
        for (int i = 0; i < a.data.Length; i++)
            result.data[i] = a.data[i] - b.data[i];
        return result;
    }

    // matrix multiplication with a float
    public static Matrix operator *(Matrix a, float scalar)
    {
        // create a new matrix with the values of a
        Matrix result = new Matrix(a.Shape.Rows, a.Shape.Cols);
        // loop through and multiply the corresponding left hand side
        // input by the scalar
        for (int i = 0; i < a.data.Length; i++)
            result.data[i] = a.data[i] * scalar;
        return result;
    }

    // matrix multiplication with another matrix
    public static Matrix operator *(Matrix a, Matrix b)
    {
        // validate dimensions
        if (a.Shape.Cols != b.Shape.Rows)
            throw new ArgumentException("Matrix Multiplication requires that the number of columns in the first matrix equal the number of rows in the second matrix.");

        int rows = a.Shape.Rows;
        int cols = b.Shape.Cols;
        int inner = a.Shape.Cols; // same as b.Shape.Rows

        // create result matrix of size rows x cols
        Matrix result = new Matrix(rows, cols);

        // triple nested loop
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                float sum = 0;

                for (int k = 0; k < inner; k++)
                {
                    sum += a[i, k] * b[k, j];
                }

                result[i, j] = sum;
            }
        }

        return result;
    }

    
    
    // matrix / scalar
    public static Matrix operator /(Matrix a, float scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException("Cannot divide by zero.");

        Matrix result = new Matrix(a.Shape.Rows, a.Shape.Cols);
        for (int i = 0; i < a.data.Length; i++)
            result.data[i] = a.data[i] / scalar;
        return result;
    }

    //  reshape 
    public void Reshape(int newRows, int newCols)
    {
        if (newRows * newCols != data.Length)
            throw new ArgumentException("Invalid reshape: new dimensions must contain the same number of elements.");

        Shape = (newRows, newCols);
    }

    // transpose 
    public void Transpose()
    {
        float[] newData = new float[data.Length];
        for (int i = 0; i < Shape.Rows; i++)
        {
            for (int j = 0; j < Shape.Cols; j++)
            {
                newData[j * Shape.Rows + i] = this[i, j];
            }
        }

        data = newData;
        Shape = (Shape.Cols, Shape.Rows);
    }

    //  ToString 
    public override string ToString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.AppendLine($"Matrix ({Shape.Rows}x{Shape.Cols}):");
        for (int i = 0; i < Shape.Rows; i++)
        {
            sb.Append("[ ");
            for (int j = 0; j < Shape.Cols; j++)
            {
                sb.Append($"{this[i, j],6:F2} ");
            }
            sb.AppendLine("]");
        }
        return sb.ToString();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("-- Matrix Assignment Test --\n");

        //  Test constructor 
        Matrix A = new Matrix(2, 3);
        Matrix B = new Matrix(2, 3);

        // Fill A
        A[0, 0] = 1; A[0, 1] = 2; A[0, 2] = 3;
        A[1, 0] = 4; A[1, 1] = 5; A[1, 2] = 6;

        // Fill B
        B[0, 0] = 6; B[0, 1] = 5; B[0, 2] = 4;
        B[1, 0] = 3; B[1, 1] = 2; B[1, 2] = 1;

        Console.WriteLine("Matrix A:");
        Console.WriteLine(A);

        Console.WriteLine("Matrix B:");
        Console.WriteLine(B);

        //  Addition 
        Console.WriteLine("A + B:");
        Matrix C = A + B;
        Console.WriteLine(C);

        //  Subtraction 
        Console.WriteLine("A - B:");
        Matrix D = A - B;
        Console.WriteLine(D);

        //  Scalar multiplication 
        Console.WriteLine("A * 2:");
        Matrix E = A * 2;
        Console.WriteLine(E);

        //  Scalar division 
        Console.WriteLine("A / 2:");
        Matrix F = A / 2;
        Console.WriteLine(F);

        //  Matrix multiplication 
        Console.WriteLine("Matrix Multiplication Test");
        Matrix X = new Matrix(2, 3);
        Matrix Y = new Matrix(3, 2);

        // Fill X
        X[0, 0] = 1; X[0, 1] = 2; X[0, 2] = 3;
        X[1, 0] = 4; X[1, 1] = 5; X[1, 2] = 6;

        // Fill Y
        Y[0, 0] = 7;  Y[0, 1] = 8;
        Y[1, 0] = 9;  Y[1, 1] = 10;
        Y[2, 0] = 11; Y[2, 1] = 12;

        Console.WriteLine("X:");
        Console.WriteLine(X);

        Console.WriteLine("Y:");
        Console.WriteLine(Y);

        Console.WriteLine("X * Y:");
        Matrix Z = X * Y;
        Console.WriteLine(Z);

        //  Transpose 
        Console.WriteLine("Transpose of A:");
        A.Transpose();
        Console.WriteLine(A);

        //  Reshape 
        Console.WriteLine("Reshape test:");
        Matrix R = new Matrix(4, 2);
        int count = 1;

        // Fill R with 1–8
        for (int i = 0; i < 4; i++)
            for (int j = 0; j < 2; j++)
                R[i, j] = count++;

        Console.WriteLine("Original R:");
        Console.WriteLine(R);

        R.Reshape(2, 4);
        Console.WriteLine("Reshaped R (2x4):");
        Console.WriteLine(R);

        Console.WriteLine("\n-- End of Tests --");
    }
}
