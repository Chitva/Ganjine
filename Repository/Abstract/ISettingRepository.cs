using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Abstract
{
    public interface ISettingRepository
    {
        IQueryable<Setting> Settings { get; }
        void SaveSetting(Setting Setting);
        void SaveMenu(Menu Menu);
        Menu DetailsMenu(int Id);

    }
}
