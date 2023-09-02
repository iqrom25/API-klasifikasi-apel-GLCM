using System.Drawing;
using Application.DTOs;
using Application.Interfaces.Services;
using Domain;
using Domain.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers.v1;

[ApiController]
[Route("/api/v1/[controller]")]
public class CitraDigital : ControllerBase
{
    private readonly IPelatihanService _pelatihan;
    private readonly IPengujianService _pengujian;
    private readonly IDataLatihService _dataLatih;

    public CitraDigital(
        IPelatihanService pelatihan
        , IPengujianService pengujian
        ,IDataLatihService dataLatih
        )
    {
        _pelatihan = pelatihan;
        _pengujian = pengujian;
        _dataLatih = dataLatih;
    }

    [EnableCors]
    [HttpGet("/")]
    public Response<string> Index()
    {
        var response = new Response<string>()
        {
            Status = true,
            Message = "Succesfully",
            Data = "yey"
        };
        
        return response;
    }

    [EnableCors("AllowAll")]
    [HttpGet("[action]")]
    public async Task<IActionResult> ListDataLatih()
    {
        var result = await _dataLatih.GetAll();
        var response = new Response<IEnumerable<DataLatih>>
        {
            Status = true,
            Message = "Success",
            Data = result
        };
        
        return Ok(response);
    }

    [EnableCors("AllowAll")]
    [HttpPost("[action]")]
    public async Task<IActionResult> Pelatihan([FromForm] PelatihanDTO requestPelatihan)
    {
        var result = await _pelatihan.Training(requestPelatihan);
        var response = new Response<IEnumerable<DataLatih>>
        {
            Status = true,
            Message = "Success",
            Data = result
        };

        return Created("/api/v1/ListDataLatih", response);
    }

    [EnableCors("AllowAll")]
    [HttpPost("[action]")]
    public async Task<IActionResult> Pengujian([FromForm] PengujianDTO requestPengujian)
    {
        var result = await _pengujian.HasilPengujian(requestPengujian);
        
        
        var response = new Response<HasilPengujianDTO>
        {
            Status = true,
            Message = "Success",
            Data = result
        };

        return Ok(response);
    }
    
    
    [EnableCors("AllowAll")]
    [HttpDelete("[action]")]
    public async Task<IActionResult> ClearDatabase()
    {
        await _dataLatih.DeleteAll();
        
        return NoContent();
    }
    
    [EnableCors("AllowAll")]
    [HttpGet("[action]")]
    public async Task<IActionResult> KFold([FromQuery]int fold)
    {
        var result = await _dataLatih.GetKFold(fold);
        
        var response = new Response<HasilKFoldDTO>
        {
            Status = true,
            Message = "Success",
            Data = result
        };

        return Ok(response);
    }

}