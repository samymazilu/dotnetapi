using MyAPI.Entities;
namespace MyAPI.Services;
public class ArticleRepository : Repository<Article>, IArticleRepository{
        public ArticleRepository(MyDbContext context):base(context){

        }

}