using Application.DTOs;
using Domain.Enums;
using Domain.Models;

namespace Application.Utils;

public static class EuclideanDistance
{
    public static List<(int id, double jarak, Kelas kelas )> HitungJarak(this DataGambarDTO dataUji, List<DataLatih> listDataLatih)
    {

        var result = new List<(int id, double jarak, Kelas kelas )>();
        foreach (var dataLatih in listDataLatih)
        {
            var d2 = Math.Pow(dataLatih.Red - dataUji.Red,2)
                     + Math.Pow(dataLatih.Green - dataUji.Green,2)
                     + Math.Pow(dataLatih.Blue - dataUji.Blue,2)
                     + Math.Pow(dataLatih.Kontras - dataUji.Glcm.Kontras,2)
                     + Math.Pow(dataLatih.Homogenitas - dataUji.Glcm.Homogenitas,2)
                     + Math.Pow(dataLatih.Energi - dataUji.Glcm.Energi,2)
                     + Math.Pow(dataLatih.Korelasi - dataUji.Glcm.Korelasi,2);

            var di = Math.Sqrt(d2);

            var temp = (id: dataLatih.Id, jarak: di, Kelas: dataLatih.Kelas);

            result.Add(temp);
        }

        return result;
    } 
    
    public static List<(int id, double jarak)> HitungJarakKFold(this DataLatih dataUji, List<DataLatih> listDataLatih)
    {

        var result = new List<(int id, double jarak)>();
        foreach (var dataLatih in listDataLatih)
        {
            var d2 = Math.Pow(dataLatih.Red - dataUji.Red,2)
                     + Math.Pow(dataLatih.Green - dataUji.Green,2)
                     + Math.Pow(dataLatih.Blue - dataUji.Blue,2)
                     + Math.Pow(dataLatih.Kontras - dataUji.Kontras,2)
                     + Math.Pow(dataLatih.Homogenitas - dataUji.Homogenitas,2)
                     + Math.Pow(dataLatih.Energi - dataUji.Energi,2)
                     + Math.Pow(dataLatih.Korelasi - dataUji.Korelasi,2);

            var di = Math.Sqrt(d2);

            var temp = (id: dataLatih.Id, jarak: di);

            result.Add(temp);
        }

        return result;
    }
}