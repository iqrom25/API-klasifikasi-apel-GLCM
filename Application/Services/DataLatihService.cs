using System.Diagnostics;
using Application.DTOs;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Utils;
using Domain.Enums;
using Domain.Models;

namespace Application.Services;

public class DataLatihService : IDataLatihService
{
    private readonly IGenericRepository<DataLatih> _repository;

    public DataLatihService(IGenericRepository<DataLatih> repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<DataLatih>> GetAll()
    {
        try
        {
            return await _repository.FindAll();
        }
        catch (Exception e)
        {
            Debug.Print(e.Message);
            throw;
        }
    }

    public async Task<DataLatih> Create(DataLatih data)
    {
        try
        {
            return await _repository.Save(data);
        }
        catch (Exception e)
        {
            Debug.Print(e.Message);
            throw;
        }
    }

    public async Task DeleteAll()
    {
        try
        {
            await _repository.Truncate("m_data_latih");
        }
        catch (Exception e)
        {
            Debug.Print(e.Message);
            throw;
        }
    }

    public async Task<HasilKFoldDTO> GetKFold(int fold)
    {
        var data = await GetAll();

        //cari jumlah data per dataset
        var totalDataset = data.Count() / fold;
        

        //pisahin dataset apel baik dan buruk
        var apelBaik = data.Where(dataLatih => dataLatih.Kelas == Kelas.Baik).ToList();
        var apelBuruk = data.Where(dataLatih => dataLatih.Kelas == Kelas.Buruk).ToList();
        
        // random data
        apelBaik = apelBaik.OrderBy(o => Guid.NewGuid()).ToList();
        apelBuruk = apelBuruk.OrderBy(o => Guid.NewGuid()).ToList();

        var listFold = new List<DataLatih>[fold];
        for (int i = 0; i < fold; i++)
        {
            var tmpListFold = new List<DataLatih>();
            var totalDataApelBaik =  totalDataset / 2;
            var totalDataApelBuruk = totalDataset / 2;
            
            // jaga2 kalau total datasetnya ganjil
            if (totalDataset % 2 != 0)
            {
                if (i % 2 == 0) totalDataApelBaik = totalDataset / 2 + 1;
                else totalDataApelBuruk = totalDataset / 2 + 1;
            }

            // masukkan data apel baik dan buruk ke dalam data fold
            tmpListFold.AddRange(apelBaik.GetRange(0,totalDataApelBaik));
            tmpListFold.AddRange(apelBuruk.GetRange(0,totalDataApelBuruk));
            
            // hapus data yang sudah dimasukkan agar tidak ada duplikat
            apelBaik.RemoveRange(0,totalDataApelBaik);
            apelBuruk.RemoveRange(0,totalDataApelBuruk);

            listFold[i] = tmpListFold;
        }

        var tes = listFold;
        var detail = new List<HasilKFold>();
        for (int i = 0; i < fold; i++)
        {
            var hasilFold = new List<KFold>();

            foreach (var ctx in listFold[i])
            {
                // masukkan sisa data fold yang tidak terambil sebagai data latih
                var sisaList = listFold.Where((obj, idx) => idx != i).ToList();
                var dataLatih = sisaList.SelectMany(x => x).ToList();
                
                var tmpResult = ctx.HitungJarakKFold(dataLatih).OrderBy(m => m.jarak).Take(3).ToList();
                var result = tmpResult[0];
                var dataResult = dataLatih.Find(m => m.Id == result.id);
                var kFold = new KFold
                {
                    Id = ctx.Id,
                    Aktual = ctx.Kelas,
                    Hasil = dataResult.Kelas,
                    Jarak = result.jarak,
                    Keterangan = ctx.Kelas == dataResult.Kelas
                };
                
                hasilFold.Add(kFold);

            }

            var jumlahBenarApelBaik = hasilFold.Count(m => m is { Aktual: Kelas.Baik, Keterangan: true });
            var jumlahBenarApelBuruk = hasilFold.Count(m => m is { Aktual: Kelas.Buruk, Keterangan: true });
            var jumlahBenar = hasilFold.Count(m => m.Keterangan);

            var jumlahApelBaik = listFold[i].Count(obj => obj.Kelas == Kelas.Baik);
            var akurasiApelBaik = (double) jumlahBenarApelBaik / jumlahApelBaik * 100;
            
            var jumlahApelBuruk = listFold[i].Count(obj => obj.Kelas == Kelas.Buruk);
            var akurasiApelBuruk = (double) jumlahBenarApelBuruk / jumlahApelBuruk * 100;
            
            var akurasiTotal = (double) jumlahBenar / totalDataset * 100;
            
            detail.Add(new HasilKFold
            {
                Detail = hasilFold,
                AkurasiApelBaik = akurasiApelBaik,
                AkurasiApelBuruk = akurasiApelBuruk,
                AkurasiTotal = akurasiTotal
            });

        }
        
        var rataRataAKurasiApelBaik = detail.Sum(ctx => ctx.AkurasiApelBaik) / fold;
        var rataRataAKurasiApelBuruk = detail.Sum(ctx => ctx.AkurasiApelBuruk) / fold;
        var rataRataAKurasiTotal = detail.Sum(ctx => ctx.AkurasiTotal) / fold;


        return new HasilKFoldDTO
        {
            HasilKFold = detail,
            RataRataAKurasiApelBaik = rataRataAKurasiApelBaik,
            RataRataAKurasiApelBuruk = rataRataAKurasiApelBuruk,
            RataRataAKurasiTotal = rataRataAKurasiTotal
        };



    }
    
}