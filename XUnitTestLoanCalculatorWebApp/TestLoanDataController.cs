using System;
using Xunit;
using LoanCalculatorWebApp.Controllers;
using LoanCalculatorWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using LoanCalculatorWebApp.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTestLoanCalculatorWebApp
{
    public class TestLoanDataController
    {
        //[STAThread]
        //static void Main() { }

        private LoanDataController loanDataController;

        public TestLoanDataController() {

            var loanDataRepo = new Mock<IRepository<LoanData>>();
            loanDataController = new LoanDataController(loanDataRepo.Object);



            loanDataRepo.Setup(context => context.List).Returns(GetQueryableMockDbSet(getTestLoanData()));

            //dbContext.Setup(
            //context => context.LoanData.ToListAsync<LoanData>(It.IsAny<System.Threading.CancellationToken>()))
            //.Returns(GetQueryableMockDbSet(getTestLoanData()));

        }

        private static DbSet<T> GetQueryableMockDbSet<T>(IEnumerable<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSet.Object;
        }

        [Fact]
        public void LoanDataControllerInstantiation()
        {
            Assert.NotNull(loanDataController);
        }

        [Fact]
        public void TestCreateLoanData() {
        }

        [Fact]
        public void TestCalculateLoanData() {
            var testLoanData = new LoanData()
            {
                Id = Guid.NewGuid(),
                LoanAmount = 100000.00,
                InterestRates = 5.0,
                LoanTerm = 12
            };
            testLoanData.SetTotalCost();
            testLoanData.SetMonthlyBill();
            double testDouble = 8333.75;

            Assert.Equal(testDouble, testLoanData.MonthlyBill);

        }
        
        public static List<LoanData> getTestLoanData() {

            List<LoanData> testLoanDataCollection = new List<LoanData> {
                new LoanData()
                {
                    Id = Guid.NewGuid(),
                    LoanAmount = 100000.00,
                    InterestRates = 5.0,
                    LoanTerm = 12
                },
                new LoanData()
                {
                    Id = Guid.NewGuid(),
                    LoanAmount = 250000.00,
                    InterestRates = 7.2,
                    LoanTerm = 36
                },
                  new LoanData()
                {
                    Id = Guid.NewGuid(),
                    LoanAmount = 35000.00,
                    InterestRates = 6.5,
                    LoanTerm = 120
                }
            };
            return testLoanDataCollection;
        }

        public static IEnumerable<object[]> getData() {
            return (IEnumerable<object[]>) getTestLoanData();
        }

        [Fact]
        public async void TestLoanDataIndex() {

            var result = await loanDataController.Index().ConfigureAwait(false);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<LoanData>>(viewResult.ViewData.Model);
        }

        [Fact]
        public void TesLoanDataEdit() { }

        [Fact]
        public void TestLoanDataDelete() { }

    }
}
