﻿using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository;
using ArtistiqueCastingAPI.Repository.MapsRepository;
using Microsoft.AspNetCore.Mvc;

namespace ArtistiqueCastingAPI.Controllers;

[Route("api/v1/subcategory")]
public class SubCategoryController : Controller
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ISubCategoryCategoryRepository _subCategoryCategoryRepository;

    public SubCategoryController()
    {
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
    
    [HttpPut]
    [Route("update")]
    public async Task<IActionResult> Update([FromBody] UpdateSubCategoryModel model)
    {
        try
        {
            SubCategoryModel subCategory = await _subCategoryRepository.GetBySlug(model.beforeSlug);
            if(subCategory == null) return BadRequest(new { message = "Subcategoria não encontrada." });
            
            await _subCategoryRepository.Delete(await _subCategoryRepository.GetBySlug(model.beforeSlug));
            
            subCategory.Slug = model.slug;
            subCategory.Name = model.name;
            
            await _subCategoryRepository.Add(subCategory);
            
            _subCategoryCategoryRepository.Add(subCategory.Slug, model.categorySlug);
            
            return Ok(new { message = "Subcategoria atualizada com sucesso!" });
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
    
    [HttpGet]
    [Route("getbycategory/{slugCategory}")]
    public async Task<IActionResult> GetByCategory([FromRoute]string slugCategory)
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