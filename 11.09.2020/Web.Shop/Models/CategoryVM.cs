using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Web.Shop.Entities;

namespace Web.Shop.Models
{

    public class CategoryVM
    {
        public int Page { get; set; }
        public int maxPage { get; set; }
        public List<Category> list { get; set; }
        public CategoryFilterVM CategoryFilter { get; set; }
    }
    public class CategoryAddVM
    {
        [Display(Name = "Назва категорії")]
        [Required(ErrorMessage ="Вкажіть назву")]
        public string Name { get; set; }

        [Display(Name = "URL Slug")]
        [Required(ErrorMessage = "Вкажіть slug")]
        public string UrlSlug { get; set; }

        [Display(Name = "Фото")]
        [Required(ErrorMessage = "Оберіть фото")]
        public IFormFile Image { get; set; }
    }

    public class CategoryEditVM
    {
        [Display(Name = "Назва категорії")]
        [Required(ErrorMessage = "Вкажіть назву")]
        public string Name { get; set; }

        [Display(Name = "URL Slug")]
        [Required(ErrorMessage = "Вкажіть slug")]
        public string UrlSlug { get; set; }

        [Display(Name = "Вибрати нове фото")]
        public IFormFile ImageUpload { get; set; }

        [Display(Name = "Фото")]
        public string Image { get; set; }
    }

    public class CategoryFilterVM
    {
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Назва категорії")]
        public string Name { get; set; }

    }
}
