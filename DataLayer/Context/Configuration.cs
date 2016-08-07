using Domain.Entities;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace DataLayer.Context
{
    public class Configuration : DbMigrationsConfiguration<EFDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EFDbContext context)
        {
            GenerateTasks(context);
            base.Seed(context);
        }
        private static void GenerateTasks(EFDbContext context)
        {

            //context.Roles.AddOrUpdate(_ => _.Id,
            //   new Role() { Id = 1, RoleName = "Admin" },
            //   new Role() { Id = 2, RoleName = "User" });

            //context.Languages.AddOrUpdate(_ => _.Id,
            //  new Language() { Id = 1, LanguageName = "Persian" },
            //  new Language() { Id = 2, LanguageName = "English" });

            //context.SaveChanges();

            //var dt = DateTime.Now.Date;                                                                               //432
            //context.UserAccounts.AddOrUpdate(_ => _.Id, new UserAccount() { Email = "admin@admin.com", RoleId = 1, IsActive = true, EncrypedPass = "UUvewJ1G0FTZu/WcwlC2Ig==", CreateDate = dt });

            //context.Settings.AddOrUpdate(_ => _.LanguageId, new Setting()
            //{
            //    AboutUs = "",
            //    Awards = "",
            //    Certificates = "",
            //    CompanyHistory = "",
            //    CompanyIntroduce = "",
            //    ContactUs = "",
            //    LanguageId = 1,
            //    MissionStatement = "",
            //    RajiGazSpecifications = "",
            //    RajiStory = ""
            //});

            //context.Settings.AddOrUpdate(_ => _.LanguageId, new Setting()
            //{
            //    AboutUs = "",
            //    Awards = "",
            //    Certificates = "",
            //    CompanyHistory = "",
            //    CompanyIntroduce = "",
            //    ContactUs = "",
            //    LanguageId = 2,
            //    MissionStatement = "",
            //    RajiGazSpecifications = "",
            //    RajiStory = ""
            //});
            //context.ContactInfos.AddOrUpdate(_ => _.LanguageId, new ContactInfo()
            //{
            //    Address = " ",
            //    Tell = " ",
            //    Map = " ",
            //    LanguageId = 1,
            //    Email = " ",
            //    Fax = " ",
            //    FormTopText = " "

            //});

            //context.ContactInfos.AddOrUpdate(_ => _.LanguageId, new ContactInfo()
            //{
            //    Address = " ",
            //    Tell = " ",
            //    Map = " ",
            //    LanguageId = 2,
            //    Email = " ",
            //    Fax = " ",
            //    FormTopText = " "

            //});

            //context.Menus.AddOrUpdate(_ => _.Name, new Menu() { Name = "بخش های سایت", ParentID = 0, Action = 202, icon = "SitePart.png" },
            //    new Menu() { Name = "لیست اطلاعات نمایندگان", ParentID = 1, Action = 52, icon = "1382975035_reseller_programm.png" },
            //    new Menu() { Name = "لیست اطلاعات پنل ", ParentID = 1, Action = 53, icon = "1383670533_stock_show-all.png" },
            //    new Menu() { Name = "لیست خدمات جدید", ParentID = 1, Action = 54, icon = "NewService.png" },
            //    new Menu() { Name = "تعیین عنوان بخش ها", ParentID = 1, Action = 55, icon = "Title.png" });


            //context.FooterColumnNames.AddOrUpdate(_ => _.LanguageId, new FooterColumnName() { LanguageId = 1 });


            //context.SaveChanges();
        }
    }
    }
