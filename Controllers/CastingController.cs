using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository;
using ArtistiqueCastingAPI.Repository.MapsRepository;
using Microsoft.AspNetCore.Mvc;

namespace ArtistiqueCastingAPI.Controllers;

[Route("api/v1/casting")]
public class CastingController : Controller
{
    private readonly ICastingRepository _castingRepository;
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICastingSubCategoryRepository _castingSubCategoryRepository;
    
    public CastingController()
    {
        _categoryRepository = new CategoryRespository();
        _castingRepository = new CastingRepository();
        _subCategoryRepository = new SubCategoryRepository();
        _castingSubCategoryRepository = new CastingSubCategoryRepository();
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        /*string? connectionString = Environment.GetEnvironmentVariable("ConnectionStringName");
        if (connectionString == null)
        {
            return Ok(new {message = "Casting API"});
        }
        return Ok(new {message = $"Connection String Recebida: {connectionString}"});*/
        return Ok(new {message = "Casting API"});
    }
    
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> List([FromBody] GetListCastingModel? model)
    {
        try
        {
            if ((model.CategorySlug == null &&
                 model.SubCategorySlug == null &&
                 model.SearchByName == null) ||
                model == null)
            {
                List<CastingModel> list = await _castingRepository.List();
                return Ok(list);
            }
            CategoryModel category = await _categoryRepository.GetBySlug(model.CategorySlug);
            if (category != null)
            {
                List<CastingModel> listCasting = await _castingRepository
                    .FilterByCategoryAndSubCategory(category.Slug, model.SubCategorySlug);
                return Ok(listCasting);
            }
            return Ok();
            //else if (model.SearchByName != null)
            //{
                //List<CastingModel> listCasting = await _castingRepository.SearchCastingByName(model.SearchByName);
            //}
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar os castings. Erro: {ex.Message}" });
        }
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] AddCastingModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _castingRepository.Add(model.Casting);
                _castingSubCategoryRepository.Add(model.Casting.Id, model.SubCategorySlug);
                return Ok(new { message = "Casting adicionado com sucesso!" });
            }

            return BadRequest(new { message = "Não foi possível adicionar o casting. Erro: ModelState inválido" });

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível adicionar o casting. Erro: {ex}" });
        }
    }
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] CastingModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _castingRepository.Update(model);
                return Ok(new { message = "Casting atualizado com sucesso!" });
            }
            return BadRequest(new {message = "Não foi possível atualizar o casting. Erro: ModelState inválido"});
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
            if(ModelState.IsValid)
            {
                await _castingRepository.Delete(model);
                return Ok(new { message = "Casting deletado com sucesso!" });
            }
            return BadRequest(new {message = "Não foi possível deletar o casting. Erro: ModelState inválido"});
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível deletar o casting. Erro: {ex.Message}" });
        }
    }
    
}
    