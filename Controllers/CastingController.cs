﻿using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ArtistiqueCastingAPI.Controllers;

[Route("api/v1/casting")]
public class CastingController : Controller
{
    private readonly ICastingRepository _castingRepository;

    public CastingController()
    {
        _castingRepository = new CastingRepository();
    }
    
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> List()
    {
        try
        {
            List<CastingModel> list = await _castingRepository.List();
            return Ok(list);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar os castings. Erro: {ex.Message}" });
        }
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] CastingModel model)
    {
        try
        {
            await _castingRepository.Add(model);
            return Ok(new { message = "Casting adicionado com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível adicionar o casting. Erro: {ex.Message}" });
        }
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] CastingModel model)
    {
        try
        {
            await _castingRepository.Update(model);
            return Ok(new { message = "Casting atualizado com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível atualizar o casting. Erro: {ex.Message}" });
        }
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete([FromBody] CastingModel model)
    {
        try
        {
            await _castingRepository.Delete(model);
            return Ok(new { message = "Casting deletado com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível deletar o casting. Erro: {ex.Message}" });
        }
    }
}
    