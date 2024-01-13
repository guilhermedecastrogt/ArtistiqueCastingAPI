using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ArtistiqueCastingAPI.Controllers;

[Route("api/v1/subcategory")]
public class SubCategoryController : Controller
{
    private readonly ISubCategoryRepository _subCategoryRepository;

    public SubCategoryController()
    {
        _subCategoryRepository = new SubCategoryRepository();
    }
    
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> List()
    {
        try
        {
            List<SubCategoryModel> list = await _subCategoryRepository.List();
            return Ok(list);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar as subcategorias. Erro: {ex.Message}" });
        }
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] SubCategoryModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _subCategoryRepository.Add(model);
                return Ok(new { message = "Subcategoria adicionada com sucesso!" });
            }
            return BadRequest(new{message = "Erro ao adicionar sebcategorias. ModelState inválido."});
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível adicionar a subcategoria. Erro: {ex.Message}" });
        }
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] SubCategoryModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _subCategoryRepository.Update(model);
                return Ok(new { message = "Subcategoria atualizada com sucesso!" });
            }
            return BadRequest(new { message = "Erro ao atualizar subcategoria. ModelState inválido." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível atualizar a subcategoria. Erro: {ex.Message}" });
        }
    }
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete([FromBody] SubCategoryModel model)
    {
        try
        {
            await _subCategoryRepository.Delete(model);
            return Ok(new { message = "Subcategoria removida com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível remover a subcategoria. Erro: {ex.Message}" });
        }
    }
    
}