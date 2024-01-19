﻿using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ArtistiqueCastingAPI.Controllers;

[Route("api/v1/subcategory")]
public class SubCategoryController : Controller
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly ICategoryRepository _categoryRepository;

    public SubCategoryController()
    {
        _categoryRepository = new CategoryRespository();
        _subCategoryRepository = new SubCategoryRepository();
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
                //if(category != null) model.SubCategory.Category = category;
                await _subCategoryRepository.Add(model.SubCategory);
                return Ok(new { message = "Subcategoria adicionada com sucesso!" });
            }
            return BadRequest(new{message = "Erro ao adicionar sebcategorias. ModelState inválido."});
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível adicionar a subcategoria. Erro: {ex}" });
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
    
    [HttpGet]
    [Route("getbycategory/{slug}")]
    public async Task<IActionResult> GetByCategory(string slugCategory)
    {
        try
        {
            CategoryModel? category = await _categoryRepository.GetBySlug(slugCategory);
            if(category == null) return BadRequest(new { message = "Categoria não encontrada." });
            List<SubCategoryModel> list = await _subCategoryRepository.GetByCategory(slugCategory);
            return Ok(list);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Não foi possível listar as subcategorias. Erro: {ex.Message}" });
        }
    }    
}