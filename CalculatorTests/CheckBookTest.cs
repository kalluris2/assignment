using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.CheckBook;
using System.Linq;
using System.Collections.ObjectModel;

namespace CalculatorTests
{
    [TestClass]
    public class CheckBookTest
    {
        [TestMethod]
        public void FillsUpProperly()
        {
            var ob = new CheckBookVM();

            Assert.IsNull(ob.Transactions);

            ob.Fill();

            Assert.AreEqual(12, ob.Transactions.Count);
        }

        [TestMethod]
        public void CountofEqualsMoshe()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var count = ob.Transactions.Where(t => t.Payee == "Moshe").Count();

            Assert.AreEqual(4, count);
        }

        [TestMethod]
        public void SumOfMoneySpentOnFood()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var category = "Food";

            var food = ob.Transactions.Where(t => t.Tag == category);

            var total = food.Sum(t => t.Amount);

            Assert.AreEqual(261, total);

        }

        [TestMethod]
        public void avgoftags()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var avg = ob.Transactions.GroupBy(t => t.Tag).Select(g => new { g.Key, Avge = g.Average(t => t.Amount) });
            Assert.AreEqual(32.625, avg.First().Avge);
            Assert.AreEqual(75, avg.Last().Avge);
        }

        [TestMethod]

        public void payforfood()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var category = "Food";
            var sum = ob.Transactions.Where(t => t.Payee == "Moshe" & t.Tag == category).Sum(t => t.Amount);

            Assert.AreEqual(130, sum);

            var sum1 = ob.Transactions.Where(t => t.Payee == "Tim" & t.Tag == category).Sum(t => t.Amount);
            Assert.AreEqual(0, sum1);

            var sum2 = ob.Transactions.Where(t => t.Payee == "Bracha" & t.Tag == category).Sum(t => t.Amount);
            Assert.AreEqual(131, sum2);
        }

        [TestMethod]

        public void payforpayee()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var sum = ob.Transactions.Where(t => t.Payee == "Moshe").Sum(t => t.Amount);

            Assert.AreEqual(130, sum);

            var sum1 = ob.Transactions.Where(t => t.Payee == "Tim").Sum(t => t.Amount);
            Assert.AreEqual(300, sum1);

            var sum2 = ob.Transactions.Where(t => t.Payee == "Bracha").Sum(t => t.Amount);
            Assert.AreEqual(131, sum2);

        }

        [TestMethod]

        public void apriltransactions()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var date = DateTime.Parse("4/5/2015");
            var date1 = DateTime.Parse("4/8/2015");
            var trans = ob.Transactions.Where(t => (t.Date >= date.Date) & (t.Date < date1.Date)).Count();
            Assert.AreEqual(6, trans);
        }

  

    [TestMethod]

        public void moneyonauto()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var category = "Auto";

            var auto = ob.Transactions.Where(t => t.Tag == category & t.Account == "Checking");

            var total = auto.Sum(t => t.Amount);

            Assert.AreEqual(150, total);
            var auto1 = ob.Transactions.Where(t => t.Tag == category & t.Account == "Credit");

            var total1 = auto1.Sum(t => t.Amount);

            Assert.AreEqual(150, total1);
            Assert.IsTrue(total == total1);
    }
        [TestMethod]

        public void transactionofaccounts()
    {
        var ob = new CheckBookVM();
       ob.Fill();
        
        var date = DateTime.Parse("4/5/2015");
        var date1 = DateTime.Parse("4/8/2015");
        var trans = ob.Transactions.Where(t => (t.Date >= date.Date) & (t.Date < date1.Date) & t.Account == "Checking").Count();

        Assert.AreEqual(3, trans);
        var trans1 = ob.Transactions.Where(t => (t.Date >= date.Date) & (t.Date < date1.Date) & t.Account == "Credit").Count();
        Assert.AreEqual(3, trans1);

    }
        [TestMethod]
        public void datesaccountsused()
        {
            var ob = new CheckBookVM();
            ob.Fill();
            var trans1 = ob.Transactions.Where(t => t.Account == "Checking");
            var date1 = trans1.OrderBy(t => t.Date).Count();
            Assert.AreEqual(6, date1);
            var trans2 = ob.Transactions.Where(t => t.Account == "Credit");
            var date2 = trans2.OrderBy(t => t.Date).Count();
            Assert.AreEqual(6, date2);

        }

        [TestMethod]

        public void Group()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var total = ob.Transactions.GroupBy(t => t.Tag).Select(g => new { g.Key, Sum = g.Sum(t => t.Amount) });

            Assert.AreEqual(261, total.First().Sum);
            Assert.AreEqual(300, total.Last().Sum);
        }
    }
}

