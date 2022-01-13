
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.GenericRepository.Utility;
using MongoDB.GenericRepository.ViewModel;
using System;
using System.Collections.Generic;

namespace MongoDB.GenericRepository.Model
{
    public class AddQuestion
    {
        public AddQuestion(AddQuestionViewModel UR)
        {
            Id = Guid.NewGuid();
            UnqueID = CommonMethods.GetUniqueIDShorter(); //new Random().Next(10, 500).ToString();
            DateStart = DateTime.Now.ToString("dd-MM-yyyy"); // UR.DateStart;
            id = UR.id;
        name = UR.name;
        description = UR.description;
         questions = UR.questions;

        }

    public AddQuestion(string updateUniqueaID, AddQuestionViewModel UR)
        {
            //Id = id;
            UnqueID = updateUniqueaID;
            DateStart = DateTime.Now.ToString("dd-MM-yyyy");  // UR.DateStart;
            id = UR.id;
            name = UR.name;
            description = UR.description;
            questions = UR.questions;
        }
        //[BsonId]
        //[BsonIgnoreIfDefault]
        public Guid Id { get; set; }
        public string UnqueID { get; set; }

        public string DateStart { get; set; } = new DateTime().ToShortDateString();
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public List<Question> questions { get; set; }

    }

    public class Option
    {
        public int id { get; set; }
        public int questionId { get; set; }
        public string name { get; set; }
        public bool isAnswer { get; set; }
    }

    public class QuestionType
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
    }

    public class Question
    {
        public int id { get; set; }
        public string name { get; set; }
        public int questionTypeId { get; set; }
        public List<Option> options { get; set; }
        public QuestionType questionType { get; set; }
    }



}





