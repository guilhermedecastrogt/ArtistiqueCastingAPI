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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add([FromBody] CategoryModel model)
    {
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateCategoryRequestModel model)
    {
        try
        {
            CategoryModel category = await _categoryRepository.GetBySlug(model.beforeSlug);
            if (category != null)
            {
                if (model.slug != model.beforeSlug)
                {
                    await _categoryRepository.Delete(await _categoryRepository.GetBySlug(model.beforeSlug));
                    
                    category.Slug = model.slug;
                    category.Name = model.name;
                    
                    await _categoryRepository.Add(category);
                    return Ok(new { message = "Categoria atualizada com sucesso!" });
                }
                await _categoryRepository.Update(category);
                return Ok(new { message = "Categoria atualizada com sucesso!" });
            }

            return BadRequest(new { message = "Não foi possível atualizar a categoria. Model não encontrada." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível atualizar a categoria. Erro: {ex.Message}" });
        }
    }
    
    [HttpPost]
    [Route("delete")]
    [Authorize(Roles = "Admin")]
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
    
    [HttpGet]
    [Route("get-by-slug/{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        if(slug == null) return BadRequest(new { message = "Slug não informado." });
        try
        {
            CategoryModel model = await _categoryRepository.GetBySlug(slug);
            if (model == null)
            {
                return BadRequest(new { message = "Categoria não encontrada." });
            }
            return Ok(model);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível encontrar a categoria. Erro: {ex}" });
        }
    }
}