using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using Domain.Entities;
using RepositoryLayer.Abstract;
namespace RepositoryLayer.Concrete
{
    public class EFSettingRepository : ISettingRepository
    {
        IUnitOfWork _uow;
        IDbSet<Setting> _RSetting;
        IDbSet<Menu> _RMenu;
        public EFSettingRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RSetting = _uow.Set<Setting>();
            _RMenu = _uow.Set<Menu>();
        }
        public void SaveMenu(Menu Menu)
        {
            if (Menu.Id == 0)
            {
                _RMenu.Add(Menu);
            }
            else
            {
                _uow.Entry(Menu).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }

        public Menu DetailsMenu(int Id)
        {
            return _RMenu.Find(Id);
        }
     
        public IQueryable<Setting> Settings
        {
            get { return _RSetting; }
        }
        public void SaveSetting(Setting Setting)
        {
            if (Setting.Id == 0)
            {

                _RSetting.Add(Setting);
            }
            else
            {
                _uow.Entry(Setting).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }

    }
}
