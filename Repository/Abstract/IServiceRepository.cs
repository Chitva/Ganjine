using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace RepositoryLayer.Abstract
{
    public interface IServiceRepository
    {
        IQueryable<ServiceGroup> ServiceGroups { get; }
        void SaveServiceGroup(ServiceGroup ServiceGroup);
        ServiceGroup DetailsServiceGroup(int Id);
        void DeleteServiceGroup(ServiceGroup ServiceGroup);

        IQueryable<ServiceTab> ServiceTabs { get; }
        void SaveServiceTab(ServiceTab ServiceTab);
        ServiceTab DetailsServiceTab(int Id);
        void DeleteServiceTab(ServiceTab ServiceTab);

        IQueryable<ServiceTabFile> ServiceTabFiles { get; }
        bool SaveServiceTabFile(ServiceTabFile ServiceTabFile);
        ServiceTabFile DetailsServiceTabFile(int Id);
        void DeleteServiceTabFile(ServiceTabFile ServiceTabFile);

        //Azizi
        bool IsLastLevelNodeInGroup(int serviceGroupId);
        //Azizi
        string TitleOfFirstTab(int serviceGroupId);

        //Azizi
        bool HasNotAnyTab(int serviceGroupId);
        //Azizi
        int TabsCountOfSpecificserviceGroup(int serviceGroupId);

        //Azizi
        bool HasAnyOrder(int serviceGroupId);

        //azizi
        ServiceGroup GetTopmostLeavefromTree(bool FirstTime, ServiceGroup node,int? languageId);
    }
}

