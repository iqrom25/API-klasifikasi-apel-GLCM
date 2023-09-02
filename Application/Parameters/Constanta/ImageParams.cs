namespace Application.Parameters;

public class ImageParams
{
    public  const int IMAGE_WIDTH = 100;
    
    public  const int IMAGE_HEIGHT = 100;

    public static readonly IReadOnlyList<double> GRAYSCALE_COOFICIENT = Array.AsReadOnly(new[] { 0.2125, 0.7154, 0.0721 });

    public static int GRADATION(int value)
    {
        return value switch
        {
            <= 31 => 0,
            <= 63 => 1,
            <= 95 => 2,
            <= 127 => 3,
            <= 159 => 4,
            <= 191 => 5,
            <= 223 => 6,
            <= 255 => 7,
            _ => -1
        };
    }
}