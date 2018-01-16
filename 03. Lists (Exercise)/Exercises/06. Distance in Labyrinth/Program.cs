using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07.Distance_in_Labyrinth
{
    class Program
    {
        class Cell
        {
            public int Row { get; set; }
            public int Col { get; set; }

            public Cell(int row, int col)
            {
                this.Row = row;
                this.Col = col;
            }
        }

        const int StartValue = -2;
        const int NoAccessValue = -1;

        static void Main(string[] args)
        {
            int startRow;
            int startCol;
            int[,] matrix = ReadMatrix(out startRow, out startCol);
            Queue<Cell> queue = new Queue<Cell>();
            queue.Enqueue(new Cell(startRow, startCol));
            while (queue.Count > 0)
            {
                Cell current = queue.Dequeue();
                if (current.Row + 1 < matrix.GetLength(0) && matrix[current.Row + 1, current.Col] == 0)
                {
                    queue.Enqueue(new Cell(current.Row + 1, current.Col));
                    matrix[current.Row + 1, current.Col] = matrix[current.Row, current.Col] + 1;
                }
                if (current.Row - 1 >= 0 && matrix[current.Row - 1, current.Col] == 0)
                {
                    queue.Enqueue(new Cell(current.Row - 1, current.Col));
                    matrix[current.Row - 1, current.Col] = matrix[current.Row, current.Col] + 1;
                }
                if (current.Col + 1 < matrix.GetLength(1) && matrix[current.Row, current.Col + 1] == 0)
                {
                    queue.Enqueue(new Cell(current.Row, current.Col + 1));
                    matrix[current.Row, current.Col + 1] = matrix[current.Row, current.Col] + 1;
                }
                if (current.Col - 1 >= 0 && matrix[current.Row, current.Col - 1] == 0)
                {
                    queue.Enqueue(new Cell(current.Row, current.Col - 1));
                    matrix[current.Row, current.Col - 1] = matrix[current.Row, current.Col] + 1;
                }
            }
            matrix[startRow, startCol] = StartValue;
            PrintMatrix(matrix);
        }

        private static int[,] ReadMatrix(out int startRow, out int startCol)
        {
            startRow = 0;
            startCol = 0;
            int size = int.Parse(Console.ReadLine());
            int[,] matrix = new int[size, size];
            for (int row = 0; row < size; row++)
            {
                char[] inputs = Console.ReadLine().ToCharArray();
                for (int col = 0; col < size; col++)
                {
                    if (inputs[col] == '*')
                    {
                        startRow = row;
                        startCol = col;
                    }
                    if (inputs[col] == 'x')
                    {
                        matrix[row, col] = -1;
                    }
                    else
                    {
                        matrix[row, col] = 0;
                    }
                }
            }
            return matrix;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                StringBuilder rowValues = new StringBuilder();
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    switch (matrix[row, col])
                    {

                        case NoAccessValue:
                            rowValues.Append('x');
                            break;
                        case 0:
                            rowValues.Append('u');
                            break;
                        case StartValue:
                            rowValues.Append('*');
                            break;
                        default:
                            rowValues.Append(matrix[row, col].ToString());
                            break;
                    }
                }
                Console.WriteLine(rowValues);
            }
        }
    }
}
