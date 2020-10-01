using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Модель данных получена командой
 * Scaffold-DbContext "Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=E:\Users\Lena\NET\DB\STUD_20.mdf;Integrated Security=True;Connect Timeout=30" Microsoft.EntityFrameworkCore.SqlServer -Context STUD_20Context -OutputDir Models -Tables STUDENTS, GROUPS, SUBJECTS, TUTORS, CHAIRS, CURRICULUM
 * Команда не отработает, если есть ошибки компиляции
 * 
 * https://www.thecodebuzz.com/efcore-scaffold-dbcontext-commands-orm-net-core/#install-efcore-tools
 * 
 */

    

namespace LabLINQ_M
{
    class DataAccess
    {

        Models.STUD_20Context context = new Models.STUD_20Context();

        public DataAccess()
        {

        }//DataAccess()

        /* TODO:
         * Исправить путь к файлу БД в строке соединения в контексте данных (Models.STUD_20Context.cs)
         * Если есть проблемы с БД - пересоздать её, используя сценарий Stud_20.sql
         * Выполнять задания в этом файле в соответсвии с вариантом
         */


        /* Пример обращения к нескольким сущностям в одном запросе.
         * Запрос обращается к связаннім сущностям Tutors и Chairs,
         * но в инстукции from имеется только обращение к Tutors (2).
         * Данные Chairs получены при помощи навигационного свойства 
         * Также обратите внимание на использование навигационных свойств 
         * для вычисления агрегатных функций (3),
         * а также на использование подзапроса (4)
         * 
         * Задача для примера: извлечь преподавателей,
         * (1) для которых указан новмер кафедры
         * (2) и в группах, которые они курируют, количество занятий больше 
         *     среднего количества занятий во всех группах;
         *    для каждого преподавателя указать:
         * (3) ФИО преподавателя,
         * (4) номер кафедры, на которой он работает 
         * (5) количество групп, которые он курирует 
         * (6) группу, имеющую наибольшее количество занятий из групп той кафедры,
         *     на которой работает данный преподаватель;
         * (7) упорядочить по убыванию номера кафедры, а затем - по имени преподавателя   
         */

        public object Query1Example()
        {
            return
            (
            from t in context.Tutors
            where
              //(1)
              (t.Chair.ChairNumber != "") &&
              //(2)
              (t.CuratorOfGroups.Average(gr => gr.StudyHours) > context.Groups.Average(gr => gr.StudyHours))
            //(7)
            orderby t.Chair.ChairNumber descending, t.NameFio
            select new
            {
                //(3)
                Name = t.NameFio,

                // (4) получаем информацию о кафедре через навигационное свойство
                Chair = t.Chair.ChairNumber,

                // (5) подсчитываем количество курируемых групп 
                CuratorGroupCount = t.CuratorOfGroups.Count(),

                // (6) подзапрос
                MaxStudyHoursGroup =
                (
                    from gr in t.Chair.Groups
                    where gr.StudyHours == t.Chair.Groups.Max(ggr => ggr.StudyHours)
                    select gr.GroupNumber
                ).FirstOrDefault()

            }
            ).ToList();  // ToList() нужен, т.к. EF Core не получит данные, пока мы не попытаемся их прочитать
        }//Query1Example()

        public object Query1()
        {
            return
                null;
        }//Query1()

        public object Query2()
        {
            return
                null;
        }//Query2()

        public object Query3()
        {
            return
                null;
        }//Query3()

        public object Query4()
        {
            return
                null;
        }//Query4()

        public object Query5()
        {
            return
                null;
        }//Query4()


        /* TODO 6.1 Пример группировки
         * Запрос извлекает преподавателей, группируя их по первой букве фамилии
         * длинные ФИО (более 12 символов) не включаются;
         * ФИО на буквы М и Ж тоже не вкоючаются.
         * 
         * Обратите внимание на две инструкции where:
         * первая фильтрует отдельные элементы до группировки,
         * вторая - уже сформированные группы
         */

        public IOrderedEnumerable<IGrouping<string, Models.Tutors>> Task6Example()
        {
            return 
            
                from tut in context.Tutors.AsEnumerable()  // AsEnumerable() нужно добавлять, т.к. EF Core не будет группировать данные до их чтения
                where tut.NameFio.Length <= 12   // фильтрация элементов данных
                group tut by tut.NameFio.Substring(0, 1) into gr
                where (gr.Key != "М") && (gr.Key != "Ж")        // фильтрация групп
                orderby gr.Key descending
                select gr;
                
        }//Task6Example()

        /* TODO 6.2 
         * Допишите код метода Task6() для получения данных в соответствии с вариантом
         * Обратите внимание, что ключ группировки имеет тип string
         * Если у вас по заданию ключ числовой - преобразуйте в строку
         * Это связано с настройкой вывода на форму, на самом деле допустим любой тип данных
         * ToString() не помомжет, т.к. его невозможно преобразовать в SQL
         * Вместо этого используйте SqlFunctions.StringConvert(), например так:
         * SqlFunctions.StringConvert( (double)st.ABSENCES)
         * Подробности:
         * https://docs.microsoft.com/ru-ru/dotnet/api/system.data.objects.sqlclient.sqlfunctions?view=netframework-4.8
         */

        public IOrderedEnumerable<IGrouping<string, Models.Students>> Task6()
        {
            return
                null;
        }//Task6()

        /* TODO 7.1 Пример группировки с подсчётом агрегатных функций
         * Функции Count(), Min(), Max(), Average(), Sum()
         * подсчитываются по каждой группе данных
        */

        public object Task7Example()
        {
            return
                (
                from tut in context.Tutors.AsEnumerable()
                group tut by tut.NameFio.Substring(0, 1) into gr
                orderby gr.Key descending
                select new
                {
                    Number = gr.Key,
                    Count = gr.Count(),
                    minLength = gr.Min(t => t.NameFio.Length),
                    maxLength = gr.Max(t => t.NameFio.Length),
                    avgLength = gr.Average(t => t.NameFio.Length),
                    sumLength = gr.Sum(t => t.NameFio.Length)
                }
                ).ToList();
        }//Task7Example

        /* TODO 7.2
         * Напишите запрос с группировкой и подсчётом агрегатных функций
         * в соответсвии с вариантом задания
         */

        public object Task7()
        {
            return
                null;
        }//Task7


    }//class DataAccess}
}
