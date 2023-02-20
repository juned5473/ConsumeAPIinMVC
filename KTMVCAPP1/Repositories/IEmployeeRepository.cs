using KTMVCAPP1.Models;

namespace KTMVCAPP1.Repositories
{
    public interface IEmployeeRepository
    {

        List<Employee> GetAll();
       
        Employee GetEmployeeByID(int empId);
        void InsertEmployee(Employee emp);
        void DeleteEmployee(Employee emp);
        void UpdateEmployee(Employee emp);



    }
}
