using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskFrontEnd
{
    public static class SD
    {
        public static string APIBasedUrl = "https://localhost:44335/";
        public static string DesignationApiPath = APIBasedUrl + "api/Dsg/";
        public static string DepartmentApiPath = APIBasedUrl + "api/Dep/";
        public static string EmployeeApiPath = APIBasedUrl + "api/EmpDep/";
      //  public static string GetAllEmployees = APIBasedUrl + "api/EmpDep/GetAllEmployees/";
        public static string EmployeeApiPathGetByID = APIBasedUrl + "api/EmpDep/GetEmployeeByid/"; 
        

        public static string UserRegister = APIBasedUrl + "api/login/register/";
        public static string UserLogin = APIBasedUrl + "api/login/authenticate/";


        public const string UserDetails = "JWToken";
        //public const string Cookiesdata = "JWToken";
        public const string newtoken = "key";



    }
}
