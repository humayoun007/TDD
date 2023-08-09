//using EFFIK.Controllers;
//using EFFIK.Utility;
using EFFIK.Tests.Controller;
using EFFIK.Tests.Entity;
using EFFIK.Tests.Repository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Xunit;

namespace EFFIK.Tests
{
    public class EmployeeControllerTest
    {
        //[Fact]
        //public void TestFundwiseScoringRedirect()
        //{
        //    //Arrange
        //    var controller = new HomeController();

        //    var token = CacheManager.OneDayAliveTokenKey;
        //    //Act
        //    //var result = controller.FundwiseScoring(token) as ViewResult;
        //    var result = (RedirectToRouteResult)controller.FundwiseScoring(token);

        //    //Assert
        //    //Assert.Equal("Index", result.ViewName);
        //    //var product = (Product)result.ViewData.Model;
        //    //Assert.Equal("Laptop", product.Name);
        //    Assert.Equal("LogOn", result.RouteValues["action"]);
        //ModelState modelState = result.ViewData.ModelState[""];
        //Assert.NotNull(modelState);  
        //Assert.True(modelState.Errors.Any());  
        //Assert.Equal(exception, modelState.Errors[0].Exception); 
        //}


        [Fact]
        public void IndexView()
        {
            var empcontroller = GetEmployeeController(new InmemoryEmpRepository());
            ViewResult result = empcontroller.Index();
            Assert.IsType<ViewResult>(result);
            //var employees = Assert.IsType<List<Employee>>(viewResult.Model);
            //Assert.Equal(2, employees.Count);
            Assert.Equal("Index", result.ViewName);
          
        }

        public static EmployeeController GetEmployeeController(IEmployeeRepository employeeRepository)
        {
            EmployeeController empController = new EmployeeController(employeeRepository);
            empController.ControllerContext = new ControllerContext()
            {
                Controller = empController,
                RequestContext = new System.Web.Routing.RequestContext(new MockHttpContext(),
                new System.Web.Routing.RouteData())
            };
            return empController;
        }

        [Fact]
        public void GetAllEmployeeFromRepository()
        {
            Employee employee1 = GetEmployeeWithName(1, "ravi", 50000, 10);
            Employee employee2 = GetEmployeeWithName(2, "suri", 50000, 10);
            InmemoryEmpRepository emprepository = new InmemoryEmpRepository();
            emprepository.Add(employee1);
            emprepository.Add(employee2);
            var controller = GetEmployeeController(emprepository);
            var result = controller.Index();
            var datamodel = (IEnumerable<Employee>)result.ViewData.Model;

            //CollectionAsserts.Contains(datamodel.ToList(), employee1);
            Assert.Contains(datamodel.ToList(), item => item.EmployeeId == employee1.EmployeeId);
            Assert.Contains(datamodel.ToList(), item => item.EmployeeId == employee2.EmployeeId);



        }

        private Employee GetEmployeeWithName(int id, string name, int salary, int departmentId)
        {
            return new Employee() { EmployeeId = id, Name = name, Salary = salary, DepartmentId = departmentId };
        }

        [Fact]
        public void Details_View_Should_Fetch_From_MemoryRepo_And_Return_Specific_EmployeeById()
        {
            var _repository = new Mock<IEmployeeRepository>();

            var expectedEmployee = new Employee() { EmployeeId = 1, Name = "hk", Salary = 5000, DepartmentId = 1 };

            var mockContext = new Mock<ControllerContext>();

            _repository.Setup(x => x.GetEmployeeById(It.IsAny<int>())).Returns(expectedEmployee);

            var controller = new EmployeeController(_repository.Object) { ControllerContext = mockContext.Object };

            var result = controller.Details(1) as ViewResult;

            var resultData = (Employee)result.ViewData.Model;

            Assert.Equal("Details", result.ViewName);

            Assert.Equal(expectedEmployee.EmployeeId, resultData.EmployeeId);


        }


        [Fact]
        public void Create_InvalidModelState_CreateEmployeeNeverExecutes()
        {
            var _repository = new Mock<IEmployeeRepository>();
            var mockContext = new Mock<ControllerContext>();
            var _controller = new EmployeeController(_repository.Object) { ControllerContext = mockContext.Object };

            _controller.ModelState.AddModelError("Name", "Name is required");

            var employee = new Employee { Salary = 5000 };

            _controller.Create(employee);

            _repository.Verify(x => x.CreateNewEmployee(It.IsAny<Employee>()), Times.Never);
        }

        [Fact]
        public void Create_ModelStateValid_CreateEmployeeCalledOnce()
        {
            Employee emp = null;
            var _mockRepo = new Mock<IEmployeeRepository>();
            var mockContext = new Mock<ControllerContext>();
            var _controller = new EmployeeController(_mockRepo.Object) { ControllerContext = mockContext.Object };
            _mockRepo.Setup(r => r.CreateNewEmployee(It.IsAny<Employee>()))
                .Callback<Employee>(x => emp = x);
            var employee = new Employee
            {
                Name = "Test Employee",
                Salary = 5000,
                DepartmentId = 1
            };
            _controller.Create(employee);
            _mockRepo.Verify(x => x.CreateNewEmployee(It.IsAny<Employee>()), Times.Once);
            Assert.Equal(emp.Name, employee.Name);
            Assert.Equal(emp.Salary, employee.Salary);
            Assert.Equal(emp.DepartmentId, employee.DepartmentId);

            //var result = _controller.Create(employee);
            //var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            //Assert.Equal("Index", redirectToActionResult.ActionName);
        }


    }

    public class MockHttpContext : HttpContextBase
    {
        private readonly IPrincipal _user = new GenericPrincipal(new GenericIdentity("someuser"), null);

        public override IPrincipal User { get => _user; set => base.User = value; }
    }
}
