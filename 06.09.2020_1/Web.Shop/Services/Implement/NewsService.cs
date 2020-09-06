using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Web.Shop.Entities;
using Web.Shop.Models;
using Web.Shop.Repo.Interafaces;
using Web.Shop.Services.Interfaces;

namespace Web.Shop.Services.Implement
{
    public class NewsService : INewsService
    {
        private readonly INewsRepo _newsRepo;
        public NewsService(INewsRepo newsRepo)
        {
            _newsRepo = newsRepo;
        }

        public void AddNews(NewsAddVM newsAdd)
        {
            News news = new News
            {
                Name = newsAdd.Name,
                Image = newsAdd.Image,
                UrlSlug = newsAdd.UrlSlug,
                Description = newsAdd.Description,
                DateCreate = DateTime.Now
            };
            _newsRepo.Add(news);
        }

        public void EditNews(NewsEditVM newsAdd, int id)
        {
            News news = _newsRepo.GetById(id);

            news.Name = newsAdd.Name;
            news.Image = newsAdd.Image;
            news.UrlSlug = newsAdd.UrlSlug;
            news.Description = newsAdd.Description;
            news.DateCreate = DateTime.Now;
            
            _newsRepo.Save();
        }
        public void Delete(int id)
        {
            _newsRepo.Delete(id);
        }

        public News Get(int id)
        {
            return _newsRepo.GetById(id);
        }

        public NewsVM GetNews(NewsFilterVM filter, int page)
        {
            NewsVM model = new NewsVM();
            int pageSize = 2;
            var query = _newsRepo.GetAll();

            if (filter.Date != null)
                query = query.Where(x => x.DateCreate.Date == filter.Date);

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(x => x.Name.Contains(filter.Name));
            if (filter.Id != 0)
                query = query.Where(x => x.Id == filter.Id);

            int pageNo = page-1;
            model.list = query.OrderBy(x => x.Id)
                .Skip(pageNo * pageSize)
                .Take(pageSize)
                .ToList();

            int allCount = query.Count();
            model.Page = page;
            model.maxPage = (int)Math.Ceiling((double)allCount / pageSize);

            model.NewsFilter = filter;

            return model;
        }
    }
}
