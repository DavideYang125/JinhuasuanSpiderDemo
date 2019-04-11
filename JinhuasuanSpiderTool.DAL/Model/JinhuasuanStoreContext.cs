using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace JinhuasuanSpiderTool.DAL.Model
{
    public class JinhuasuanStoreContext : DbContext
    {
        public IConfiguration Configuration { get; }
        public JinhuasuanStoreContext()
            : base()
        {

        }

        public virtual DbSet<JinhuasuanStore> JinhuasuanStore { get; set; }
        public virtual DbSet<Citys> Citys { get; set; }

        public virtual DbSet<Provinces> Provinces { get; set; }
        public virtual DbSet<Districts> Districts { get; set; }

        private static ILoggerFactory Mlogger => new LoggerFactory();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //配置连接字符串
            optionsBuilder.UseMySql("");
        }
    }
}
