using System.Drawing;
using Application.Parameters;
using Domain;

namespace Application.DTOs;

public class HasilPengujianDTO
{
    public DataGambarDTO Hasil { get; init; }
    
    public string Grayscale { get; set; }

    public virtual List<TetanggaTerdekatDTO> TetanggaTerdekat{ get; set; }
}