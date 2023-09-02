using System.Drawing;
using Application.Parameters;
using System.Linq;
using Domain.Enums;

namespace Application.Utils;

public static class Glcm
{
    public static int[,] Kuantitasi(this Bitmap bitmap)
    {
        var matrix = new int[bitmap.Height, bitmap.Width];
        for (int i = 0; i < bitmap.Height; i++)
        {
            for (int j = 0; j < bitmap.Width; j++)
            {
                var pixelValue = bitmap.GetPixel(i, j).R;
                matrix[i, j] = ImageParams.GRADATION(pixelValue);
            }
        }

        return matrix;
    }
    
    public static int[,] CoOccurence(this int[,] source, Sudut sudut)
    {
        var matrix = new int[8,8];

        switch (sudut)
        {
            case Sudut.Sudut_1:
                for (int i = 0; i < source.GetLength(0); i++)
                {
                    for (int j = 0; j < source.GetLength(1)-1; j++)
                    {
                        var row = source[i, j];
                        var col = source[i, j + 1];
                        matrix[row, col]++;
                    }
                }
                break;
            case Sudut.Sudut_2:
                for (int i = 0; i < source.GetLength(0); i++)
                {
                    for (int j = 0; j < source.GetLength(1) - 1; j++)
                    {
                        var row = source[j, i];
                        var col = source[j, i + 1];
                        matrix[row, col]++;
                    }
                }
                break;
            case Sudut.Sudut_3:
                for (int i = 0; i < source.GetLength(0) - 1; i++)
                {
                    for (int j = source.GetLength(1) - 1; j > 0; j--)
                    {
                        var row = source[i,j];
                        var col = source[i+1,j-1];
                        matrix[row, col]++;
                    }
                }
                break;
            case Sudut.Sudut_4:
                for (int i = 0; i < source.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < source.GetLength(1) - 1; j++)
                    {
                        var row = source[i,j];
                        var col = source[i+1,j+1];
                        matrix[row, col]++;
                    }
                }
                break;
            default:
                break;
        }
        
        

        return matrix;
    }
    
    public static int[,] Simetris(this int[,] source)
    {
        var transpose = source.SquareTranspose();

        var matrix = source.Addition(transpose);   
        
        return matrix;
    }
    
    public static double[,] Normalisasi(this int[,] source)
    {
        var sum = source.SumElement();
        
        var matrix = new double[source.GetLength(0),source.GetLength(1)];
        
        for (int i = 0; i < source.GetLength(0); i++)
        {
            for (int j = 0; j < source.GetLength(1); j++)
            {
                matrix[i, j] = (double) source[i, j] / sum;
            }
        }

        return matrix;
    }

    public static double Kontras(this double[,] matrix)
    {
        double kontras = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                kontras += matrix[i, j] * Math.Pow((i - j), 2);
            }
        }

        return kontras;
    }
    
    public static double Homogenitas(this double[,] matrix)
    {
        double homogenitas = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                homogenitas += matrix[i, j] /(1 + Math.Pow((i - j), 2));
            }
        }

        return homogenitas;
    }

    public static double Energi(this double[,] matrix)
    {
        return matrix.Cast<double>().Sum(element => Math.Pow(element, 2));
    }

    public static double Korelasi(this double[,] matrix)
    {
        double ui = 0;
        double uj = 0;
        double ai = 0;
        double aj = 0;
        double temp1 = 0;
        double temp2 = 0;
        double korelasi;
        
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                ui += i * matrix[i, j];
                uj += j * matrix[i, j];
            }
        }
        
        
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                ai += Math.Pow((i - ui),2)*matrix[i,j];
                aj += Math.Pow((j - uj),2)*matrix[i,j];
            }
        }

        ai = Math.Sqrt(ai);
        aj = Math.Sqrt(aj);

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                temp1 += (i - ui) * matrix[i, j];
                temp2 += (j - uj) * matrix[i, j];
            }
        }

        korelasi = (temp1 * temp2) / (ai * aj);
        

        return korelasi;

    }
    
    
    
   
}