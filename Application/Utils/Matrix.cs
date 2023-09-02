namespace Application.Utils;

public static class Matrix
{
    public static int[,] SquareTranspose(this int[,] matrix)
    {
        var w = matrix.GetLength(0);
        var h = matrix.GetLength(1);

        var result = new int[h, w];

        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                result[j, i] = matrix[i, j];
            }
        }

        return result;
    }

    public static int[,] Addition(this int[,] matrix, int[,] addMatrix)
    {
        var w = matrix.GetLength(0);
        var h = matrix.GetLength(1);

        var result = new int[h, w];

        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                result[i, j] = matrix[i, j] + addMatrix[i,j];
            }
        }

        return result;
    }

    public static int SumElement(this int[,] matrix)
    {
        return matrix.Cast<int>().Sum();
    }
    
    
}