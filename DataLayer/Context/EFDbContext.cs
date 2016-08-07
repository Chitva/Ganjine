using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using DataLayer.Mapping;
using System.Data.Entity.Infrastructure;

namespace DataLayer.Context
{
    public class EFDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Role> Roles { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<Customer>  Customers { get; set; }
        public DbSet<ContactUs> ContactUs { get; set; }
        public DbSet<NewsLetterEmails> NewsLetterEmails { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsGallery> NewsGalleries { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<SeoMng> SeoMngs { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<ServiceGroup> ServiceGroups { get; set; }
        public DbSet<ServiceTab> ServiceTabs { get; set; }
        public DbSet<ServiceTabFile> ServiceTabFiles { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleGallery> ArticleGalleries { get; set; }
        public DbSet<WorkExperienceGroup> WorkExperienceGroups { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<WorkExperienceGallery> WorkExperienceGalleries { get; set; }
        public DbSet<FooterColumnName> FooterColumnNames { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<StoryGallery> StoryGalleries { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<SaleAgency> SaleAgencies { get; set; }

        #region IUnitOfWork Members
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public new DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return base.Entry(entity);
        }
        #endregion
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new MenuConfig());
            modelBuilder.Configurations.Add(new BannerConfig());
            modelBuilder.Configurations.Add(new ContactInfoConfig());
            modelBuilder.Configurations.Add(new NewsConfig());
            modelBuilder.Configurations.Add(new NewsGalleryConfig());
            modelBuilder.Configurations.Add(new StoryConfig());
            modelBuilder.Configurations.Add(new StoryGalleryConfig());

            modelBuilder.Configurations.Add(new ServiceGroupConfig());
            modelBuilder.Configurations.Add(new ServiceTabConfig());
            modelBuilder.Configurations.Add(new ServiceTabFileConfig());

            modelBuilder.Configurations.Add(new WorkExperienceGroupConfig());
            modelBuilder.Configurations.Add(new WorkExperienceConfig());
            modelBuilder.Configurations.Add(new WorkExperienceGalleryConfig());



            modelBuilder.Configurations.Add(new FooterColumnNameConfig());
            modelBuilder.Configurations.Add(new FooterConfig());
            modelBuilder.Configurations.Add(new ContactUsConfig());
            modelBuilder.Configurations.Add(new QuestionConfig());
            modelBuilder.Configurations.Add(new UploadConfig());



            modelBuilder.Entity<UserAddress>()
                .HasRequired(_ => _.City)
                .WithMany()
                .HasForeignKey(_ => _.CityId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .HasRequired(_ => _.UserAccount)
                .WithMany()
                .HasForeignKey(_ => _.UserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
