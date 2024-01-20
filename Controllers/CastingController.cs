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
        return Ok(new {message = "Casting API"});
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
    
    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> Filter([FromBody] GetListCastingModel model)
    {
        try
        {
            List<CastingModel> listCasting = await _castingRepository
                .FilterByCategoryAndSubCategory(model.CategorySlug, model.SubCategorySlug);
            
            return Ok(listCasting);
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
    
    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> GetBySearch([FromBody] string stringSearch)
    {
        try
        {
            if (ModelState.IsValid)
            {
                List<CastingModel> listCasting = await _castingRepository.SearchCastingByName(stringSearch);
                return Ok(listCasting);
            }
            return BadRequest(new {message = "Não foi possível buscar o casting. Erro: ModelState inválido"});
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível buscar o casting. Erro: {ex.Message}" });
        }
    }
    
    [HttpGet]
    [Route("list/exclusives")]
    public async Task<IActionResult> GetExclusives()
    {
        try
        {
            return Ok(await _castingRepository.GetExclusives());
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar os castings. Erro: {ex.Message}" });
        }
    }
}
    