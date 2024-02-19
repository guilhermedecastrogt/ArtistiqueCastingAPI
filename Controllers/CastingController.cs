using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository;
using ArtistiqueCastingAPI.Repository.MapsRepository;
using ArtistiqueCastingAPI.Services;
using Microsoft.AspNetCore.Authorization;
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
            return Ok(await _castingRepository.ListCastingTable());
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar os castings. Erro: {ex}" });
        }
    }
    
    [HttpPost]
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Add([FromBody] AddCastingModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _castingRepository.Add(model.Casting);
                List<string> subCategories = CastingServices.ConverteStringSubCategoriesToList(model.SubCategorySlug);
                foreach (var subCategory in subCategories)
                {
                    _castingSubCategoryRepository.Add(model.Casting.Id, subCategory);
                }
                return Ok(new { message = "Casting adicionado com sucesso!" });
            }

            return BadRequest(new { message = "Não foi possível adicionar o casting. Erro: ModelState inválido" });

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível adicionar o casting. Erro: {ex}" });
        }
    }
    
    [HttpPost]
    [Route("update")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] AddCastingModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                await _castingRepository.Update(model.Casting);
                _castingSubCategoryRepository.RemoveAll(model.Casting.Id);
                List<string> subCategories = CastingServices.ConverteStringSubCategoriesToList(model.SubCategorySlug);
                foreach (var subCategory in subCategories)
                {
                    _castingSubCategoryRepository.Add(model.Casting.Id, subCategory);
                }
                return Ok(new { message = "Casting atualizado com sucesso!" });
            }
            return BadRequest(new {message = "Não foi possível atualizar o casting. Erro: ModelState inválido"});
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível atualizar o casting. Erro: {ex.Message}" });
        }
    }
    
    [HttpPost]
    [Route("delete")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete([FromBody] Guid id)
    {
        try
        {
            if(id == null) return BadRequest(new { message = "Id não fornecido." });
            await _castingRepository.Delete(await _castingRepository.GetEntityById(id));
            return Ok(new { message = "Casting deletado com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível deletar o casting. Erro: {ex.Message}" });
        }
    }
    
    [HttpGet]
    [Route("search/{stringSearch}")]
    public async Task<IActionResult> GetBySearch([FromRoute] string stringSearch)
    {
        try
        {
            return Ok(await _castingRepository.SearchCastingByName(stringSearch));
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

    [HttpGet]
    [Route("list/all")]
    public async Task<IActionResult> All()
    {
        try
        {
            List<CastingModel> list = await _castingRepository.List();
            foreach (var item in list)
            {
                item.SubCategorys = await _subCategoryRepository.GetSubCategoriesByCasting(item.Id);
                foreach (var subcateog in item.SubCategorys)
                {
                    subcateog.Castings = null;
                    subcateog.Categories = await _categoryRepository.GetCategoriesBySubCategory(subcateog.Slug);
                    foreach (var category in subcateog.Categories)
                    {
                        category.SubCategories = null;
                    }
                }
            }
            return Ok(list);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar os castings. Erro: {ex.Message}" });
        }
    }
    
    [HttpGet]
    [Route("get-by-id/{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        try
        {
            CastingModel? casting = await _castingRepository.GetEntityById(id);
            if(casting == null) return BadRequest(new { message = "Casting não encontrado" });
            casting.SubCategorys = await _subCategoryRepository.GetSubCategoriesByCasting(casting.Id);
            casting.SubCategorys.ForEach(subCategory => subCategory.Castings = null);
            return Ok(casting);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível buscar o casting. Erro: {ex.Message}" });
        }
    }

    [HttpGet]
    [Route("list/geral/{page}")]
    public async Task<IActionResult> Geral([FromRoute] int page)
    {
        try
        {
            return Ok(await _castingRepository.ListGeral(page-1));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar os castings. Erro: {ex.Message}" });
        }
    }
    
    [HttpGet]
    [Route("count")]
    public async Task<IActionResult> Count()
    {
        try
        {
            return Ok(await _castingRepository.CountCasting());
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar os castings. Erro: {ex.Message}" });
        }
    }
}