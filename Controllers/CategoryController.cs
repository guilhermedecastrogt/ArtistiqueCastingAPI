using ArtistiqueCastingAPI.ModelBinders;
using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtistiqueCastingAPI.Controllers;

[Route("api/v1/category")]
public class CategoryController : Controller
{
    
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController()
    {
        _categoryRepository = new CategoryRespository();
    }
    
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> List()
    {
        try
        {
            List<CategoryModel> list = await _categoryRepository.List();
            return Ok(list);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar as categorias. Erro: {ex.Message}" });
        }
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] CategoryModel model)
    {
        Console.WriteLine("SLUG:"+model.Slug);
        Console.WriteLine("NAME:"+model.Name);
        try
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.Add(model);
                return Ok(new { message = "Categoria adicionada com sucesso!" });
            }
            
            if(model == null) Console.WriteLine("---------- MODEL RETURN NULL ----------");
            Console.WriteLine($"ModelState inválido {model.Name} --- {model.Slug}");
            return BadRequest(new { message = "Não foi possível adicionar a categoria. ModelState inválida." });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return BadRequest(new { message = $"Não foi possível adicionar a categoria. Erro: {ex.Message}" });
        }
    }
    
    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> Update(
        [ModelBinder(BinderType = typeof(CategoryModelBinder))] CategoryModel model, string beforeSlug)
    {
        try
        {
            if (ModelState.IsValid)
            {
                if (model.Slug != beforeSlug)
                {
                    await _categoryRepository.Delete(await _categoryRepository.GetBySlug(beforeSlug));
                    await _categoryRepository.Add(model);
                    return Ok(new { message = "Categoria atualizada com sucesso!" });
                }
                await _categoryRepository.Update(model);
                return Ok(new { message = "Categoria atualizada com sucesso!" });
            }

            return BadRequest(new { message = "Não foi possível atualizar a categoria. ModelState inválida." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível atualizar a categoria. Erro: {ex.Message}" });
        }
    }
    
    [HttpPost]
    [Route("delete")]
    public async Task<IActionResult> Delete([FromBody] string slug)
    {
        try
        {
            if(slug == null) return BadRequest(new { message = "Slug não informado." });
            CategoryModel model = await _categoryRepository.GetBySlug(slug);
            if (model == null)
            {
                return BadRequest(new { message = "Categoria não encontrada." });
            }
            await _categoryRepository.Delete(model);
            return Ok(new { message = "Categoria removida com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível remover a categoria. Erro: {ex}" });
        }
    }
}