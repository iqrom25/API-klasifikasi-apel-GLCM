using System.Drawing;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math;
using Application.Parameters;
using Domain.Enums;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Image = System.Drawing.Image;


namespace Application.Utils;

public static class ImageProcessing
{
    public static async Task<Bitmap> ConvertToBitmap(this IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        extension = extension[1..];

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        
        if (extension != "heic")
            return (Bitmap) Image.FromStream(memoryStream);

        memoryStream.Position = 0;
        using var newImage = new MagickImage(memoryStream);
        newImage.Format = MagickFormat.Jpg;
        await memoryStream.DisposeAsync();

        using var stream = new MemoryStream(newImage.ToByteArray());
        return (Bitmap)Image.FromStream(stream);


    }

    public static Histogram[] GetRgb(this Bitmap bitmap)
    {
        var histogram = new ImageStatistics(bitmap);
        var rgb = new [] { histogram.Red, histogram.Green, histogram.Blue };

        return rgb;
    }

    public static Bitmap ResizeImage(this Bitmap bitmap, int width, int height)
    {
        var filter = new ResizeNearestNeighbor(width,height);

        var newImage = filter.Apply(bitmap);

        return newImage;
    }

    public static Bitmap Grayscale(this Bitmap bitmap, List<double> cooficient)
    {
        var filter = new Grayscale(cooficient[0], cooficient[1], cooficient[2]);

        var newImage = filter.Apply(bitmap);

        return newImage;
    }



    public static GlcmParams Glcm(this Bitmap bitmap, Sudut sudut)
    {
        var matrixKuantitasi = bitmap.Kuantitasi();
        var matrixCoOccurence = matrixKuantitasi.CoOccurence(sudut);
        var matrixSimetris = matrixCoOccurence.Simetris();
        var matrixNormalisasi = matrixSimetris.Normalisasi();
    
        var kontras = matrixNormalisasi.Kontras();
        var homogenitas = matrixNormalisasi.Homogenitas();
        var energi = matrixNormalisasi.Energi();
        var korelasi = matrixNormalisasi.Korelasi();

        return new GlcmParams
        {
            Energi = energi,
            Homogenitas = homogenitas,
            Kontras = kontras,
            Korelasi = korelasi
        };

    }

    public static string ToBase64(this Bitmap image)
    {
        using var stream = new MemoryStream();
        image.Save(stream,ImageFormat.Bmp);
        var byteImage = stream.ToArray();
        return Convert.ToBase64String(byteImage);
    }



    
    
    
}