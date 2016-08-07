using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using Domain.Entities;
using RepositoryLayer.Abstract;
using System.Security.Cryptography;
namespace RepositoryLayer.Concrete
{
    public class EFServiceRepository : IServiceRepository
    {
        IUnitOfWork _uow;
        IDbSet<ServiceGroup> _RServiceGroup;
        IDbSet<ServiceTab> _RServiceTab;
        IDbSet<ServiceTabFile> _RServiceTabFile;
        IDbSet<ProductOrder> _ROrders;
        public EFServiceRepository(IUnitOfWork uow)
        {
            _uow = uow;

            _RServiceGroup = _uow.Set<ServiceGroup>();
            _RServiceTab = _uow.Set<ServiceTab>();
            _RServiceTabFile = _uow.Set<ServiceTabFile>();
            _ROrders = _uow.Set<ProductOrder>();
        }
        public IQueryable<ServiceGroup> ServiceGroups
        {
            get { return _RServiceGroup; }
        }
        public void SaveServiceGroup(ServiceGroup ServiceGroup)
        {
            try
            {
                 if (ServiceGroup.Id == 0)
                {
                    _RServiceGroup.Add(ServiceGroup);
                }
                else
                {
                    _uow.Entry(ServiceGroup).State = EntityState.Modified;
                }
            _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
          
        }
        public ServiceGroup DetailsServiceGroup(int Id)
        {
            return _RServiceGroup.Find(Id);
        }
        public void DeleteServiceGroup(ServiceGroup ServiceGroup)
        {
            _RServiceGroup.Remove(ServiceGroup);
            _uow.SaveChanges();
        }

        public IQueryable<ServiceTab> ServiceTabs
        {
            get { return _RServiceTab; }
        }
        public void SaveServiceTab(ServiceTab ServiceTab)
        {
            if (ServiceTab.Id == 0)
            {
              _RServiceTab.Add(ServiceTab);
            }
            else
            {
                _uow.Entry(ServiceTab).State = EntityState.Modified;
            }
            _uow.SaveChanges();
        }
        public ServiceTab DetailsServiceTab(int Id)
        {
            return _RServiceTab.Find(Id);
        }
        public void DeleteServiceTab(ServiceTab ServiceTab)
        {
            _RServiceTab.Remove(ServiceTab);
            _uow.SaveChanges();
        }


        public IQueryable<ServiceTabFile> ServiceTabFiles
        {
            get { return _RServiceTabFile; }
        }
        public bool SaveServiceTabFile(ServiceTabFile ServiceTabFile)
        {
            if (ServiceTabFile.Id == 0)
            {
                _RServiceTabFile.Add(ServiceTabFile);
            }
            else
            {
                _uow.Entry(ServiceTabFile).State = EntityState.Modified;
            }
            var result = _uow.SaveChanges() > 0;

            return result;
        }
        public ServiceTabFile DetailsServiceTabFile(int Id)
        {
            return _RServiceTabFile.Find(Id);
        }
        public void DeleteServiceTabFile(ServiceTabFile ServiceTabFile)
        {
            _RServiceTabFile.Remove(ServiceTabFile);
            _uow.SaveChanges();
        }

        //Azizi
        public bool IsLastLevelNodeInGroup(int serveiceGroupId)
        {
            //if there isnt any row in serviceGroup table which has a child and also 
            //it means this service Group is the last level node in the tree
            if (!_RServiceGroup.Any(s => s.ParentID == serveiceGroupId))
            {
                return true;
            }
            return false;

        }

        //azizi
        public string TitleOfFirstTab(int serviceGroupId)
        {
           return  _RServiceTab.FirstOrDefault(_ => _.ServiceGroupId == serviceGroupId).Title;
        }

        //Azizi
        public bool HasNotAnyTab(int serveiceGroupId)
        {
            //if there isnt any row in serviceTab table which its serviceGroupID is equal to this serviceGroupID
            //it means it hasnt any registered tab yet
            if (!_RServiceTab.Any(s => s.ServiceGroupId == serveiceGroupId))
            {
                return true;
            }
            return false;

        }


        //Azizi
        public bool HasAnyOrder(int serviceGroupId)
        {
            if(_ROrders.Any(_=>_.ServiceGroupId == serviceGroupId))
            {
                return true;
            }
            return false;
        }


        //Azizi
        public int TabsCountOfSpecificserviceGroup(int serveiceGroupId)
        {
            return _RServiceTab.Count(_ => _.ServiceGroupId == serveiceGroupId);
        }

       //written By Azizi 94_09_10
        public ServiceGroup GetTopmostLeavefromTree(bool firstTime, ServiceGroup node,int? languageId)
        {
            var group = new ServiceGroup();
            var childs = new List<ServiceGroup>();
            if (firstTime)
            {
                var roots = _RServiceGroup.Where(_ => _.ParentID == null && _.LanguageId == languageId);
                if (roots.Any())
                {
                    var firstPrirityNode = roots.FirstOrDefault(_ => _.Priority == 1);
                    if (firstPrirityNode != null)
                    {
                        return GetTopmostLeavefromTree(false, firstPrirityNode,null);
                    }
                    return group;
                }
                return group;
            }
                childs = _RServiceGroup.Where(_ => _.ParentID == node.Id).ToList();
                if (childs.Any())
                {
                    group = childs.FirstOrDefault(_ => _.Priority == 1);
                    return GetTopmostLeavefromTree(false, group,null);
                }
                return node;
        }
        
    }
}
