using AmsmProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmsmProject.DAL
{
    public class AmsmInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<AmsmContext>
    {
       
        protected override void Seed(AmsmContext context)
        {

            var candidates = new List<Candidate>
            {
            new Candidate{firstName="Ангела",parentName="Тони",lastName="Трпковска" ,category="Б", drivingSchool="Нова",instructor="Сања Петрова",EMBG="1005993455011",registered=false},
            new Candidate{firstName="Ана",parentName="Коста",lastName="Костоска" ,category="Б", drivingSchool="АвтошколаОхрид",instructor="Соња Куззмановска",EMBG="1685934512123",registered=false},
            new Candidate{firstName="Слободанка",parentName="Љупчо",lastName="Ценевска" ,category="Б", drivingSchool="АМСМ",instructor="Петар Смилев",EMBG="0808081111111",registered=false},
            new Candidate{firstName="Моника",parentName="Тони",lastName="Трпковска" ,category="Б", drivingSchool="Нова",instructor="Петар Јованов",EMBG="2608990455000",registered=false},
            new Candidate{firstName="Владимир",parentName="Драгослав",lastName="Радески" ,category="Б", drivingSchool="АМСМ",instructor="Амир Исмаилович",EMBG="0705993889966",registered=false},
            new Candidate{firstName="Марија",parentName="Сашо",lastName="Ристевска" ,category="Б", drivingSchool="Зебра",instructor="Марјан Петров",EMBG="0909990458912",registered=false},
            new Candidate{firstName="Петар",parentName="Љупчо",lastName="Петрушевски" ,category="Б", drivingSchool="Кекец",instructor="Марија Стојковска",EMBG="0303991458520",registered=false},
            new Candidate{firstName="Соња",parentName="Миро",lastName="Смилкова" ,category="Б", drivingSchool="Нова",instructor="Сања Петрова",EMBG="0808992457841",registered=false},
            new Candidate{firstName="Коста",parentName="Драган",lastName="Петровски" ,category="Б", drivingSchool="АМСМ",instructor="Амир Исмаилович",EMBG="1108993451020",registered=false},
            new Candidate{firstName="Мирјана",parentName="Александар",lastName="Алексова" ,category="Б", drivingSchool="Зебра",instructor="Марјан Петров",EMBG="0107986122011",registered=false},
            new Candidate{firstName="Виктор",parentName="Миле",lastName="Крстевски" ,category="Б", drivingSchool="АМСМ",instructor="Амир Исмаилович",EMBG="2506991457896",registered=false},
            new Candidate{firstName="Симона",parentName="Перо",lastName="Стојчева" ,category="Б", drivingSchool="АМСМ",instructor="Амир Исмаилович",EMBG="2205993451022",registered=false}};

            candidates.ForEach(s => context.Candidates.Add(s));
            context.SaveChanges();


            var termini = new List<AmsmInfo>
            {
            new AmsmInfo{date=DateTime.Parse("25.08.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("25.08.2014"),hour=10,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("26.08.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("26.08.2014"),hour=10,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("27.08.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("27.08.2014"),hour=10,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("29.08.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("29.08.2014"),hour=10,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("01.09.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("01.09.2014"),hour=10,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("02.09.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("02.09.2014"),hour=10,candidatesTP=0,candidatesPP1=0},

            new AmsmInfo{date=DateTime.Parse("03.09.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("03.09.2014"),hour=10,candidatesTP=0,candidatesPP1=0},


            new AmsmInfo{date=DateTime.Parse("04.09.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("05.09.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("08.09.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("09.09.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("10.09.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("11.09.2014"),hour=8,candidatesTP=0,candidatesPP1=0},
            new AmsmInfo{date=DateTime.Parse("12.09.2014"),hour=8,candidatesTP=0,candidatesPP1=0}};

            termini.ForEach(s => context.Informations.Add(s));
            context.SaveChanges();


            var terminiPP2 = new List<AmsmInfoPP2>
            {
            new AmsmInfoPP2{date=DateTime.Parse("25.08.2014"),hour=10,candidatesPP2=0},
          
            new AmsmInfoPP2{date=DateTime.Parse("26.08.2014"),hour=10,place="Паркинг-РесторанБисера",candidatesPP2=0},
            new AmsmInfoPP2{date=DateTime.Parse("26.08.2014"),hour=13,place="Паркинг-РесторанБисера",candidatesPP2=0},
            new AmsmInfoPP2{date=DateTime.Parse("27.08.2014"),hour=10,place="Паркинг-РесторанБисера",candidatesPP2=0},
            new AmsmInfoPP2{date=DateTime.Parse("27.08.2014"),hour=13,place="Паркинг-РесторанБисера",candidatesPP2=0},
            new AmsmInfoPP2{date=DateTime.Parse("29.08.2014"),hour=10,place="Паркинг-РесторанБисера",candidatesPP2=0},
            new AmsmInfoPP2{date=DateTime.Parse("29.08.2014"),hour=13,place="Паркинг-РесторанБисера",candidatesPP2=0},
            new AmsmInfoPP2{date=DateTime.Parse("01.09.2014"),hour=10,place="Паркинг-РесторанБисера",candidatesPP2=0}};

            terminiPP2.ForEach(s => context.AmsmInfoPP2.Add(s));
            context.SaveChanges();

           
        }

       

    }
}