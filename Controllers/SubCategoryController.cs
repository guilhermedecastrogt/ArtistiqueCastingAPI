﻿using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository;
using ArtistiqueCastingAPI.Repository.MapsRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtistiqueCastingAPI.Controllers;

[Route("api/v1/subcategory")]
public class SubCategoryController : Controller
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubCategoryCategoryRepository _subCategoryCategoryRepository;
    private readonly ICastingRepository _castingRepository;

    public SubCategoryController()
    {
        _castingRepository = new CastingRepository();
        _categoryRepository = new CategoryRespository();
        _subCategoryRepository = new SubCategoryRepository();
        _subCategoryCategoryRepository = new SubCategoryCategoryRepository();
    }
    
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> List(GetListCastingModel model)
    {
        try
        {
            List<SubCategoryModel> list = await _subCategoryRepository.List();
            foreach (var item in list)
            {
                item.Categories = await _subCategoryCategoryRepository.GetCategoriesBySubCategory(item.Slug);
            }
            return Ok(list);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar as subcategorias. Erro: {ex.Message}" });
        }
    }
    
    [HttpPost]
    [Route("add")]
    public async Task<IActionResult> Add([FromBody] AddSubCategoryModel model)
    {
        try
        {
            if (ModelState.IsValid)
            {
                CategoryModel? category = await _categoryRepository.GetBySlug(model.SlugCategory);
                await _subCategoryRepository.Add(model.SubCategory);
                _subCategoryCategoryRepository.Add(model.SubCategory.Slug, model.SlugCategory);
                return Ok(new { message = "Subcategoria adicionada com sucesso!" });
            }
            return BadRequest(new{message = "Erro ao adicionar SubCategorias. ModelState inválido."});
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível adicionar a SubCategoria. Erro: {ex.Message}" });
        }
    }
    
    [HttpPost]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] UpdateSubCategoryModel model)
    {
        try
        {
            SubCategoryModel? subCategory = await _subCategoryRepository.GetBySlug(model.beforeSlug);
            if(subCategory == null) return BadRequest(new { message = "Subcategoria não encontrada." });
            if(model.beforeSlug == null) return BadRequest(new { message = "Slug novo slug não pode ser null." });

            
            //Get all castings and all categories of subcategory
            List<CategoryModel>? categories = await _categoryRepository.GetCategoriesBySubCategory(model.beforeSlug);
            List<CastingModel>? castings = await _castingRepository
                .FilterByCategoryAndSubCategory(categories[0].Slug, model.beforeSlug);
            
            // Remove subcategory of all castings
            foreach (var item in castings)
            {
                _castingRepository.DeleteSubCategory(item.Id, model.beforeSlug);
            }
            
            // Remove all categories of subcategory
            _subCategoryCategoryRepository.DeleteAllCategoriesOfSubCategory(model.beforeSlug);
            
            await _subCategoryRepository.Delete(await _subCategoryRepository.GetBySlug(model.beforeSlug));
            
            // Add new subcategory
            subCategory.Slug = model.slug;
            subCategory.Name = model.name;
            
            await _subCategoryRepository.Add(subCategory);
            
            // Update category of subcategory
            if (model.categorySlug != null)
            {
                //_subCategoryCategoryRepository.Delete(model.beforeSlug, model.categorySlug);
                _subCategoryCategoryRepository.Add(subCategory.Slug, model.categorySlug);
            }
            
            // Add subcategory to before castings
            foreach (var item in castings)
            {
                _castingRepository.AddSubCategory(item.Id, model.slug);
            }
            
            return Ok(new { message = "Subcategoria atualizada com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível atualizar a subcategoria. Erro: {ex}" });
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
            SubCategoryModel? subCategory = await _subCategoryRepository.GetBySlug(slug);
            if(subCategory == null) return BadRequest(new { message = "Subcategoria não encontrada." });
            await _subCategoryRepository.Delete(subCategory);
            return Ok(new { message = "Subcategoria removida com sucesso!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível remover a subcategoria. Erro: {ex.Message}" });
        }
    }

    [HttpGet]
    [Route("getbycategory/{slugCategory}")]
    public async Task<IActionResult> GetListByCategory([FromRoute]string slugCategory)
    {
        try
        {
            CategoryModel? category = await _categoryRepository.GetBySlug(slugCategory);
            if(category == null) return BadRequest(new { message = "Categoria não encontrada." });
            List<SubCategoryModel> list = await _subCategoryRepository.GetByCategory(slugCategory);
            foreach (var item in list)
            {
                item.Categories = null;
            }
            return Ok(list);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar as subcategorias. Erro: {ex.Message}" });
        }
    }    
    
    [HttpGet]
    [Route("get-by-slug/{slug}")]
    public async Task<IActionResult> GetBySlug([FromRoute] string slug)
    {
        try
        {
            SubCategoryModel? subcategory = await _subCategoryRepository.GetBySlug(slug);
            SubCategoryGetBySlugModel model = new SubCategoryGetBySlugModel();
            if(subcategory != null) {
                model.name = subcategory.Name;
                model.slug = subcategory.Slug;
                List<CategoryModel>? category = await _categoryRepository.GetCategoriesBySubCategory(subcategory.Slug);
                if(category != null) {
                    model.categorySlug = category[0].Slug;
                    model.categoryName = category[0].Name;
                }
                return Ok(model);
            }
            return BadRequest(new { message = "Subcategoria não encontrada." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar a subcategoria. Erro: {ex.Message}" });
        }
    }
}