using ArtistiqueCastingAPI.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace ArtistiqueCastingAPI.ModelBinders;

public class CustomModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(CategoryModel))
            return new CategoryModelBinder(); 
        return null;
    }
}
public class CategoryModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
            throw new ArgumentNullException(nameof(bindingContext));
        //if the content type is "application/x-www-form-urlencoded", get the value from the Request Form
        if (bindingContext.HttpContext.Request.ContentType== "application/x-www-form-urlencoded")
        {
            var category = new CategoryModel();
            if(bindingContext.HttpContext.Request.Form.ContainsKey("Slug"))
                category.Slug = bindingContext.HttpContext.Request.Form["Slug"].ToString();
            if (bindingContext.HttpContext.Request.Form.ContainsKey("Name"))
                category.Name = bindingContext.HttpContext.Request.Form["Name"].ToString();
            bindingContext.Result = ModelBindingResult.Success(category);
        }
        else if(bindingContext.HttpContext.Request.ContentType== "application/json")
        {  //If the content type is "application/json", get the value from the request body.
            string valueFromBody = string.Empty;
            using (var sr = new StreamReader(bindingContext.HttpContext.Request.Body))
            {
                valueFromBody = sr.ReadToEnd();
                if (string.IsNullOrEmpty(valueFromBody))
                {
                    return Task.CompletedTask;
                }
                var student = JsonConvert.DeserializeObject<CategoryModel>(valueFromBody);
                if (student != null)
                {
                    bindingContext.Result = ModelBindingResult.Success(student);
                }
            }
        } 
        return Task.CompletedTask;
    }
}