using System;
using System.Collections.Generic;
using System.Linq;
using SilverlineSolutionsInterviewApp.Models;

namespace SilverlineSolutionsInterviewApp.Controllers
{
    public class DatabaseAccess
    {
        public List<Recipe> GetAllRecipes()
        {
            using (var db = new RecipeList_SilverlineSolutionsEntities())
                return db.Recipes.ToList();
        }

        public bool AddRecipe(string name, string image, string ingredients)
        {
            try
            {
                using (var db = new RecipeList_SilverlineSolutionsEntities())
                {
                    db.Recipes.Add(new Recipe() { 
                        Name = name,
                        ImageFile = image,
                        Ingredients = ingredients
                    });
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool EditRecipe(int ID, string name, string ingredients, string image)
        {
            try
            {
                using (var db = new RecipeList_SilverlineSolutionsEntities())
                {
                    var recipe = db.Recipes.Find(ID);
                    recipe.Name = name;
                    if (!string.IsNullOrEmpty(image)) recipe.ImageFile = image;
                    recipe.Ingredients = ingredients;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteRecipe(int ID)
        {
            try
            {
                using (var db = new RecipeList_SilverlineSolutionsEntities())
                {
                    db.Recipes.Remove(db.Recipes.Find(ID));
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}