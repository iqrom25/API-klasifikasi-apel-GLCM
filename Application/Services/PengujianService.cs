using Application.DTOs;
using Application.Exeptions;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Parameters;
using Application.Utils;
using Domain.Models;



namespace Application.Services;

public class PengujianService : IPengujianService
{
    private readonly IDataLatihService _dataLatihService;

    public PengujianService(IDataLatihService dataLatihService)
    {
        _dataLatihService = dataLatihService;
    }
    
    public async Task<HasilPengujianDTO> HasilPengujian(PengujianDTO dataUji)
    {
        if (dataUji.Gambar.FormatSalah())
            throw new BadRequestException("Format gambar tidak sesuai");

        var image = await dataUji.Gambar.ConvertToBitmap();

        if (image.UkuranSalah())
            throw new BadRequestException(
                $"ukuran gambar minimum {ImageParams.IMAGE_WIDTH}px x {ImageParams.IMAGE_HEIGHT}px");

        var listDataLatih = await _dataLatihService.GetAll();

        if (!listDataLatih.Any())
            throw new NotFoundException("Data latih tidak ditemukan");
        
        var rgb = image.GetRgb();
        
        var resizedImage = image.ResizeImage(ImageParams.IMAGE_WIDTH, ImageParams.IMAGE_HEIGHT);

        var grayImage = resizedImage.Grayscale(ImageParams.GRAYSCALE_COOFICIENT.ToList());

        var glcm = grayImage.Glcm(dataUji.Sudut);


        var hasilImageProcessing = new DataGambarDTO
        {
            Red = rgb[0].Mean,
            Green = rgb[1].Mean,
            Blue = rgb[2].Mean,
            Glcm = glcm
        };

        var euclideanDistance = hasilImageProcessing.HitungJarak(listDataLatih.ToList());
        var top3 = euclideanDistance.OrderBy(data => data.jarak).Take(3).ToList();

        var tetanggaTerdekat = top3.Select(data =>
            new TetanggaTerdekatDTO
            {
                DataLatih = listDataLatih.SingleOrDefault(dataLatih => dataLatih.Id == data.id),
                Jarak = data.jarak
            }).ToList();

        hasilImageProcessing.Kelas = tetanggaTerdekat[0].DataLatih.Kelas;

        var result = new HasilPengujianDTO
        {
            Hasil = hasilImageProcessing,
            TetanggaTerdekat = tetanggaTerdekat,
            Grayscale = grayImage.ToBase64()
             
        };

        return result;
    }
}