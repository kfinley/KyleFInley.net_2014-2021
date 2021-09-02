using System;
using System.Dynamic;
using System.Web.Mvc;

using AutoMapper;

using _928.Commands;
using _928.Web.Mvc;
using _928.Web.Mvc.ModelBinders;

using KyleFinley.Commands;
using KyleFinley.Models;
using _928.Entities.Models;

namespace KyleFinley.Web.App_Start
{
    public class ModelBinderConfig
    {
        protected static ICommandDispatcher dispatcher = new CommandDispatcher(new _928.Web.HttpContextWrapper());

        public static void Configure()
        {
            ModelBinders.Binders.Add(typeof(AuthorViewData<Article>), new PartialModelBinder<AuthorViewData<Article>>
            {
                LoadExistingModel = (context, postedModel) =>
                {
                    if (context.HttpContext.Request.Form["Page.Id"] != null)
                    {
                        // Setup Commands
                        var getArticle = CommandFactory.Create<GetPage<Article>>();
                        getArticle.Id = Guid.Parse(context.HttpContext.Request.Form["Page.Id"].ToString());
                        getArticle.EntityCommand = CommandFactory.Create<GetArticle>();

                        // Run Commands
                        dispatcher.Run(getArticle);

                        // Return Result
                        var viewData = new AuthorViewData<Article>();
                        viewData.Page = getArticle.Data;
                        viewData.Url = getArticle.Data.Path;
                        
                        var mappingConfig = new MapperConfiguration(cfg =>
                        {
                            cfg.CreateMap<ExpandoObject, AuthorViewData<Article>>().ForAllExpandoMembers();
                            cfg.CreateMap<ExpandoObject, Page<Article>>().ForAllExpandoMembers();
                        });

                        var model = mappingConfig.CreateMapper().Map(postedModel, viewData);

                        return model;
                    }
                    return null;
                }
            });
        }
    }
}
