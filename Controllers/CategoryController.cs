using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository;
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
    public async Task<IActionResult> Add(string model)
    {
        try
        {
            //if (ModelState.IsValid)
            //{
                if (model != null)
                {
                    Console.WriteLine("FROM BODY RETURN!!!");
                    Console.WriteLine("--------------------");
                    Console.WriteLine(model);
                    Console.WriteLine("--------------------");
                    //await _categoryRepository.Add(model);
                    return Ok(new { message = "Categoria adicionada com sucesso!" });
                }
            //}
            Console.WriteLine("ModelState nula!!!");
            return BadRequest(new { message = "Não foi possível adicionar categoria. ModelState inválida." });
        }
        catch (Exception ex)
        {
            
            Console.WriteLine(ex);
            return BadRequest(new { message = $"Não foi possível adicionar a categoria. Erro: {ex}" });
        }
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] CategoryModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
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
    
    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete([FromBody] CategoryModel model)
    {
        try
        {
            await _categoryRepository.Delete(model);
            return Ok(new { message = "Categoria removida com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível remover a categoria. Erro: {ex.Message}" });
        }
    }
    
}