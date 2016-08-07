using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace RepositoryLayer.Abstract
{
    public interface IQuestionRepository
    {
        IQueryable<Question> Questions { get; }
        Question DetailsQuestion(int Id);
        void SaveQuestion(Question Question);
        void DeleteQuestion(Question Question);

    }
}
