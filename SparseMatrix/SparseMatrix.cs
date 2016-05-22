using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SparseMatrix
{
    public class SparseMatrix
    {
        List<int> column = new List<int>();
        List<int> value = new List<int>();
        List<int> row = new List<int>();
        public List<int> result = new List<int>();


        public SparseMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                bool flag = false;
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        value.Add(matrix[i, j]);
                        if (flag == false)
                        {
                            row.Add(value.Count - 1);
                            flag = true;
                        }
                        column.Add(j);
                    }
                }
            }
        }

        public void Multiply(int slice, int noOfThreads, int[] vertex)
        {
            Stopwatch sw = Stopwatch.StartNew();
            
            for (int i = slice / noOfThreads; i < row.Count / noOfThreads; i++)
            {
                result.Add(0);
                int j = row[i];
                if (j < row[(row.Count) - 1])
                    for (j = row[i];
                         j < row[i + 1]; j++)
                    {
                        result[i] += value[j] * vertex[column[j]];
                    }
                else
                    for (j = row[i];
                             j < value.Count; j++)
                    {
                        result[i] += value[j] * vertex[column[j]];
                    }
            }

            sw.Stop();
            Console.WriteLine("Vrijeme izvršavanja " + (slice + 1) + ". threada: " + sw.Elapsed);
        }

    }
}
