//using Domain.Entities;
//using Repository.Abstract;
//using RepositoryLayer.Abstract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web;
//namespace WebUI.Infrastructure.Extentions.Admin
//{
//    public class QuestionExtensions
//    {
//        IQuestionRepository _RQuestion;
//        IUserAccountRepository _RDefineUser;
//        UserAccountExtentions EDefineUser;
//        public QuestionExtensions(IQuestionRepository QuestionRepository, IUserAccountRepository DefineUserRepository)
//        {
//            _RQuestion = QuestionRepository;
//            _RDefineUser = DefineUserRepository;
//            EDefineUser = new UserAccountExtentions(_RDefineUser);
//        }

//        //public IQueryable<Question> QuestionAdmin()
//        //{
//        //    return _RQuestion.Questions;
//        //}
//        public IQueryable<Question> GetQuestionAdmin(int Start, int End,int LanguageId)
//        {
//            return _RQuestion.Questions.Where(x=>x.LanguageId==LanguageId).OrderByDescending(x => x.Id).Skip(Start).Take(End);
//        }
//        public int GetCountQuestionAdmin(int LanguageId)
//        {

//            return _RQuestion.Questions.Where(x => x.LanguageId == LanguageId).Count();
//        }
        
//    }
//}