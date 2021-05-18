using System;

namespace ConsoleApp
{
    internal interface ISort
    {
        void Sort(int[,] matrix, Matrix.Operation sortBy, Func<int[,], int, int, int[,]> swapRows);
    }
}
