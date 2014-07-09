using NNCQ.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNCQ.Domain.Organization
{
    /// <summary>
    /// 扩展 Department 的数据持久化方法或者其他的方法
    /// </summary>
    public static class DepartmentExtensions
    {
        public static Department GetDepartment(this IEntityRepository<Department> departmentRepository, Guid id) 
        {
            var department = departmentRepository.GetSingleBy(x => x.ID == id);

            var typeCollection = departmentRepository.GetAllRelevance<DepartmentTypeInDepartment>().Where(x => x.Department.ID == id).OrderBy(s => s.SortCode);
            foreach (var item in typeCollection)
                department.Types.Add(item.DepartmentType);

            var leaderCollection = departmentRepository.GetAllRelevance<DepartmentLeader>().Where(x => x.Department.ID == id).OrderBy(s => s.SortCode);
            foreach (var item in leaderCollection)
                department.Leaders.Add(item.Person);

            var personCollection = departmentRepository.GetAllRelevance<PersonsInDepartment>().Where(x => x.Department.ID == id).OrderBy(s => s.SortCode);
            foreach (var item in personCollection)
                department.Perosns.Add(item.Person);

            var jobLeveClollection = departmentRepository.GetAllRelevance<JobLevelInDepartment>().Where(x => x.Department.ID == id).OrderBy(s => s.SortCode);
            foreach (var item in jobLeveClollection)
                department.JobLevels.Add(item.JobLevel);

            var jobTitleCollection = departmentRepository.GetAllRelevance<JobTitleInDepartment>().Where(x => x.Department.ID == id).OrderBy(s => s.SortCode);
            foreach (var item in jobTitleCollection)
                department.JobTitles.Add(item.JobTitle);

            return department;
        }

        public static void SaveDepartment(this IEntityRepository<Department> departmentRepository, Department dept)
        {
            departmentRepository.AddOrEdit(dept);

            var typeCollection = departmentRepository.GetAllRelevance<DepartmentTypeInDepartment>().Where(x => x.Department.ID == dept.ID).OrderBy(s => s.SortCode);
            foreach (var item in typeCollection)
                departmentRepository.DeleteRelevance<DepartmentTypeInDepartment>(item);
            
            var jobLeveClollection = departmentRepository.GetAllRelevance<JobLevelInDepartment>().Where(x => x.Department.ID == dept.ID).OrderBy(s => s.SortCode);
            foreach (var item in jobLeveClollection)
                departmentRepository.DeleteRelevance<JobLevelInDepartment>(item);

            var jobTitleCollection = departmentRepository.GetAllRelevance<JobTitleInDepartment>().Where(x => x.Department.ID == dept.ID).OrderBy(s => s.SortCode);
            foreach (var item in jobTitleCollection)
                departmentRepository.DeleteRelevance<JobTitleInDepartment>(item);

            foreach(var item in dept.Types)
            {
                var tItem = new DepartmentTypeInDepartment() { Name = "", Department = dept, DepartmentType = item };
                departmentRepository.AddRelevance<DepartmentTypeInDepartment>(tItem);
            }
            foreach (var item in dept.JobLevels)
            {
                var tItem = new JobLevelInDepartment() { Name = "", Department = dept, JobLevel=item };
                departmentRepository.AddRelevance<JobLevelInDepartment>(tItem);
            }
            foreach (var item in dept.JobTitles)
            {
                var tItem = new JobTitleInDepartment() { Name = "", Department = dept, JobTitle=item};
                departmentRepository.AddRelevance<JobTitleInDepartment>(tItem);
            }

            departmentRepository.Save();

        }

        /// <summary>
        /// 根据部门的ID，直接提取部门内部的人员对象集合。
        /// </summary>
        /// <param name="customerRepository"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public static List<Person> GetPersons(this IEntityRepository<Department> departmentRepository,Guid deptID)
        {
            var persons = new List<Person>();
            var tempCollection = departmentRepository.GetAllRelevance<PersonsInDepartment>().Where(x => x.Department.ID == deptID).OrderBy(s => s.SortCode);
            foreach (var item in tempCollection) 
            {
                persons.Add(item.Person);
            }
            return persons;
        }

        /// <summary>
        /// 根据部门和人员ID，为部门添加关联人员
        /// </summary>
        /// <param name="departmentRepository"></param>
        /// <param name="deptID"></param>
        /// <param name="personID"></param>
        /// <returns></returns>
        public static bool AddPerson(this IEntityRepository<Department> departmentRepository, Guid deptID, Guid personID) 
        {
            var dept = departmentRepository.GetSingle(deptID);
            var person = departmentRepository.GetSingleRelevance<Person>(personID);
            var tempRelevance = departmentRepository.GetSingleRelevanceBy<PersonsInDepartment>(x=>x.Person.ID== personID && x.Department.ID==deptID);
            if (tempRelevance != null)
                return false;
            else
            {
                tempRelevance = new PersonsInDepartment() { Department = dept, Person = person };
                departmentRepository.AddAndSaveRelevance<PersonsInDepartment>(tempRelevance);
                return true;
            }
        }

        /// <summary>
        /// 为部门批量添加人员
        /// </summary>
        /// <param name="departmentRepository"></param>
        /// <param name="deptID"></param>
        /// <param name="personIDs"></param>
        public static void AddPersons(this IEntityRepository<Department> departmentRepository, Guid deptID, List<Guid> personIDs) 
        {
            var dept = departmentRepository.GetSingle(deptID);
            foreach (var item in personIDs) 
            {
                var person = departmentRepository.GetSingleRelevance<Person>(item);
                var tempRelevance = departmentRepository.GetSingleRelevanceBy<PersonsInDepartment>(x => x.Person.ID == item && x.Department.ID == deptID);
                if (tempRelevance == null)
                {
                    tempRelevance = new PersonsInDepartment() { Department = dept, Person = person };
                    departmentRepository.AddRelevance<PersonsInDepartment>(tempRelevance);
                }
            }
            departmentRepository.Save();
        }

        /// <summary>
        /// 将人员从指定的的部门移除
        /// </summary>
        /// <param name="departmentRepository"></param>
        /// <param name="deptID"></param>
        /// <param name="personID"></param>
        public static void RemovePerson(this IEntityRepository<Department> departmentRepository, Guid deptID, Guid personID) 
        {
            var tempRelevance = departmentRepository.GetSingleRelevanceBy<PersonsInDepartment>(x => x.Person.ID == personID && x.Department.ID == deptID);
            if (tempRelevance != null)
                departmentRepository.DeleteAndSaveRelevance<PersonsInDepartment>(tempRelevance);

        }

        /// <summary>
        /// 批量移除人员
        /// </summary>
        /// <param name="departmentRepository"></param>
        /// <param name="deptID"></param>
        /// <param name="personIDs"></param>
        public static void RemovePersons(this IEntityRepository<Department> departmentRepository, Guid deptID, List<Guid> personIDs) 
        {
            foreach (var item in personIDs)
            {
                var tempRelevance = departmentRepository.GetSingleRelevanceBy<PersonsInDepartment>(x => x.Person.ID == item && x.Department.ID == deptID);
                if (tempRelevance != null)
                    departmentRepository.DeleteRelevance<PersonsInDepartment>(tempRelevance);

            }
            departmentRepository.Save();

        }

    }
}
