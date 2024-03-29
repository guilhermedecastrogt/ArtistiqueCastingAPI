﻿using ArtistiqueCastingAPI.Models;
using ArtistiqueCastingAPI.Repository.Generics;

namespace ArtistiqueCastingAPI.Repository;

public interface ICastingRepository : IGenericsRepository<CastingModel>
{
    Task<List<CastingModel>> FilterByCategoryAndSubCategory(string? categorySlug, string? subCategorySlug);
    Task<List<CastingModel>> SearchCastingByName(string modelSearchByName);
    Task<List<CastingModel>> GetExclusives();
    Task<List<CastingTableModel>> ListCastingTable();
    Task<List<CastingGeralModel>> ListGeral(int page);
    Task<int> CountCasting();
    void DeleteSubCategory(Guid castingId, string subCategorySlug);
    void AddSubCategory(Guid castingId, string subCategorySlug);
}