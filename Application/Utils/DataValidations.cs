using System.Drawing;
using Application.Parameters;
using Microsoft.AspNetCore.Http;

namespace Application.Utils;

public static class DataValidations
{
    public static bool UkuranSalah(this Bitmap bitmap)
    {
        return (bitmap.Height < ImageParams.IMAGE_HEIGHT || bitmap.Width < ImageParams.IMAGE_WIDTH);
    }
    
    public static bool FormatSalah(this IFormFile file)
    {
        var format = new[] {"jpg","jpeg","png","heic","heif" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        extension = extension[1..];
        return (!format.Contains(extension));
    }
}