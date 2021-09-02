using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _928.Core.Wrappers;
using _928.Core;
using _928.Entities.Models;

namespace KyleFinley.Models {
    public static class MapperBootstrapper {
        public static void Run() {
         
            MyMapper.CreateMap<Page, Page<Home>>();
            MyMapper.CreateMap<Page<Home>, Page>();

            MyMapper.CreateMap<Page, Page<Article>>();
            MyMapper.CreateMap<Page<Article>, Page>();

            MyMapper.CreateMap<Page, Page<Category>>();
            MyMapper.CreateMap<Page<Category>, Page>();
        }
    }
}
