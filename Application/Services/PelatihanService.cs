using Application.DTOs;
using Application.Exeptions;
using Application.Interfaces.Services;
using Application.Parameters;
using Application.Utils;
using Domain.Models;

namespace Application.Services;

public class PelatihanService : IPelatihanService
{
    private readonly IDataLatihService _dataLatihService;

    public PelatihanService(IDataLatihService dataLatihService)
    {
        _dataLatihService = dataLatihService;
    }
    
    public async Task<IEnumerable<DataLatih>> Training(PelatihanDTO dataLatih)
    {

        var listDataLatih = new List<DataLatih>();
        
        foreach (var gambar in dataLatih.DataLatih)
        {
            if (gambar.FormatSalah())
                throw new BadRequestException("Format gambar tidak sesuai");



            var image = await gambar.ConvertToBitmap();
            
            if (image.UkuranSalah())
                throw new BadRequestException(
                    $"ukuran gambar minimum {ImageParams.IMAGE_WIDTH}px x {ImageParams.IMAGE_HEIGHT}px");

            var rgb = image.GetRgb();

            var resizedImage = image.ResizeImage(ImageParams.IMAGE_WIDTH, ImageParams.IMAGE_HEIGHT);

            var grayImage = resizedImage.Grayscale(ImageParams.GRAYSCALE_COOFICIENT.ToList());

            var glcm = grayImage.Glcm(dataLatih.Sudut);

            var result = new DataLatih
            {
                Red = rgb[0].Mean,
                Green = rgb[1].Mean,
                Blue = rgb[2].Mean,
                Energi = glcm.Energi,
                Homogenitas = glcm.Homogenitas,
                Kontras = glcm.Kontras,
                Korelasi = glcm.Korelasi,
                Kelas = dataLatih.JenisPelatihan,
                Sudut = dataLatih.Sudut
            };

            listDataLatih.Add(result);
        }

        foreach (var result in listDataLatih)
        {
            await _dataLatihService.Create(result);
        }

        return listDataLatih;
    }
}