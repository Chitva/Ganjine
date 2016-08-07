using System;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Context;
using Domain.Entities;
using RepositoryLayer.Abstract;
using System.Web;

namespace RepositoryLayer.Concrete
{
    public class EFQuestionRepository : IQuestionRepository
    {
        IUnitOfWork _uow;
        IDbSet<Question> _RQuestion;
        public EFQuestionRepository(IUnitOfWork uow)
        {
            _uow = uow;
            _RQuestion = _uow.Set<Question>();
        }

        public IQueryable<Question> Questions
        {
            get { return _RQuestion; }
        }
        public Question DetailsQuestion(int Id)
        {
            return _RQuestion.Find(Id);
        }
        public void SaveQuestion(Question Question)
        {
            try
            {
                if (Question.Id == 0)
                {
                    _RQuestion.Add(Question);
                }
                else
                {
                    _uow.Entry(Question).State = EntityState.Modified;

                }
                _uow.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
            
           
        }
        public void DeleteQuestion(Question Question)
        {
            _RQuestion.Remove(Question);
            _uow.SaveChanges();
        }
    }
}
