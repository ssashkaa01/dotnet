﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Shop.Entities;
using Web.Shop.Models;

namespace Web.Shop.Services.Interfaces
{
    public interface INewsService
    {
        NewsVM GetNews(NewsFilterVM NewsFilter, int page);
        void AddNews(NewsAddVM newsAdd);
        void EditNews(NewsEditVM newsAdd, int id);
        void Delete(int id);
        News Get(int id);
    }
}
