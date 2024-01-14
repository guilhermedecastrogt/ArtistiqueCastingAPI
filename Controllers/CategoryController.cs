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
    public async Task<IActionResult> Add([FromBody] CategoryModel model, [FromForm]CategoryModel? model2)
    {
        try
        {
            if(model2 != null) Console.WriteLine("FROM FORM RETURN!!!");
            //if (ModelState.IsValid)
            //{
                Console.WriteLine("--------------------");
                Console.WriteLine(model2.Slug);
                Console.WriteLine(model2.Name);
                Console.WriteLine("--------------------");
                if (model != null)
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine(model.Slug);
                    Console.WriteLine(model.Name);
                    Console.WriteLine("--------------------");
                }
                await _categoryRepository.Add(model2);
                return Ok(new { message = "Categoria adicionada com sucesso!" });
            //}
            Console.WriteLine("ModelState inválida");
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