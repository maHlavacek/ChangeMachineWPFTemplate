using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ChangeMachine.LogicUnitTest
{
    [TestClass]
    public class ChangeMachineUnitTest
    {
        /// <summary>
        /// Vergleich zweier Arrays gleichen Typs mit vergleichbaren Elementen
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static bool CompareArrays<T>(T[] first, T[] second) where T : IComparable<T>
        {
            if (first.Length != second.Length)
            {
                return false;
            }
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i].CompareTo(second[i]) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        [TestMethod()]
        public void DefaultConstructor_01()
        {
            Logic.ChangeMachine changeMachine = new Logic.ChangeMachine();
            Assert.AreEqual(210, changeMachine.MoneyInDepot, "Je Wert sollen 30 Einheiten im Depot sein");
        }

        [TestMethod()]
        public void OwnConstructor_01()
        {
            int[] moneyInDepot = { 1, 2, 3, 4, 5, 6, 7 };
            Logic.ChangeMachine changeMachine = new Logic.ChangeMachine(moneyInDepot);
            Assert.AreEqual(28, changeMachine.MoneyInDepot, "1-7 Einheiten sollen im Depot sein");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OwnConstructor_02()
        {
            int[] moneyInDepot = null;
            new Logic.ChangeMachine(moneyInDepot);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void OwnConstructor_03()
        {
            int[] moneyInDepot = { 1, 2, 3, 4, 5, 6 };
            new Logic.ChangeMachine(moneyInDepot);
        }

        [TestMethod()]
        public void OwnConstructor_04()
        {
            int[] moneyInDepot = { 1, 2, 3, 4, 5, 6, 7 };
            Logic.ChangeMachine changeMachine = new Logic.ChangeMachine(moneyInDepot);
            moneyInDepot[0] = 10;
            Assert.AreEqual(28, changeMachine.MoneyInDepot, "1-7 Einheiten sollen im Depot sein");
        }

        [TestMethod()]
        public void InsertMoney_01()
        {
            int[] moneyUnits = { 1, 2, 5, 10, 20, 50, 100 };
            int[] moneyInDepot = { 1, 2, 3, 4, 5, 6, 7 };
            Logic.ChangeMachine changeMachine = new Logic.ChangeMachine(moneyInDepot);
            for (int i = 0; i < moneyUnits.Length; i++)
            {
                Assert.AreEqual(true, changeMachine.InsertMoney(moneyUnits[i]), $"{moneyUnits[i]} is valid");
            }
        }

        [TestMethod()]
        public void InsertMoney_02()
        {
            int[] moneyUnits = { 1, 2, 5, 10, 20, 50, 100, 0, -1, 199, 201 };
            int[] moneyInDepot = { 1, 2, 3, 4, 5, 6, 7 };
            Logic.ChangeMachine changeMachine = new Logic.ChangeMachine(moneyInDepot);
            for (int i = 0; i < moneyUnits.Length; i++)
            {
                if (i < 7)
                {
                    Assert.AreEqual(true, changeMachine.InsertMoney(moneyUnits[i]), $"{moneyUnits[i]} is valid");
                }
                else
                {
                    Assert.AreEqual(false, changeMachine.InsertMoney(moneyUnits[i]), $"{moneyUnits[i]} is invalid");
                }
            }
        }

        [TestMethod()]
        public void NormalChange_01()
        {
            int[] moneyInDepot =   { 001, 002, 005, 010, 020, 050, 100 };
            int[] changeExpected = { 000, 000, 000, 001, 000, 000, 000 };
            int[] depotExpected =  { 001, 002, 007, 009, 020, 050, 100 };
            Logic.ChangeMachine changeMachine = new Logic.ChangeMachine(moneyInDepot);
            bool ok = changeMachine.InsertMoney(5);
            Assert.IsTrue(ok, "InsertMoney sollte funktionieren");
            ok = changeMachine.InsertMoney(5);
            Assert.IsTrue(ok, "InsertMoney sollte funktionieren");
            changeMachine.Change();
            Assert.IsTrue(CompareArrays(changeMachine.EmptyEjection(), changeExpected), "Wechselgeld stimmt nicht!");
            Assert.IsTrue(CompareArrays(changeMachine.EmptyDepot(), depotExpected), "Wechselgeld stimmt nicht!");
        }

        [TestMethod()]
        public void NormalChange_02()
        {
            int[] moneyInDepot = { 001, 002, 005, 010, 020, 050, 100 };
            int[] changeExpected = { 000, 000, 000, 000, 001, 000, 000 };
            int[] depotExpected = { 001, 002, 005, 012, 019, 050, 100 };
            Logic.ChangeMachine changeMachine = new Logic.ChangeMachine(moneyInDepot);
            bool ok = changeMachine.InsertMoney(10);
            Assert.IsTrue(ok, "InsertMoney sollte funktionieren");
            ok = changeMachine.InsertMoney(10);
            Assert.IsTrue(ok, "InsertMoney sollte funktionieren");
            changeMachine.Change();
            Assert.IsTrue(CompareArrays(changeMachine.EmptyEjection(), changeExpected), "Wechselgeld stimmt nicht!");
            Assert.IsTrue(CompareArrays(changeMachine.EmptyDepot(), depotExpected), "Wechselgeld stimmt nicht!");
        }

        [TestMethod()]
        public void NormalChange_03()
        {
            int[] moneyInDepot = { 001, 002, 005, 010, 020, 050, 100 };
            int[] changeExpected = { 000, 000, 001, 001, 000, 000, 000 };
            int[] depotExpected = { 001, 002, 007, 009, 020, 050, 100 };
            Logic.ChangeMachine changeMachine = new Logic.ChangeMachine(moneyInDepot);
            bool ok = changeMachine.InsertMoney(5);
            Assert.IsTrue(ok, "InsertMoney sollte funktionieren");
            ok = changeMachine.InsertMoney(5);
            Assert.IsTrue(ok, "InsertMoney sollte funktionieren");
            ok = changeMachine.InsertMoney(5);
            Assert.IsTrue(ok, "InsertMoney sollte funktionieren");
            changeMachine.Change();
            Assert.IsTrue(CompareArrays(changeMachine.EmptyEjection(), changeExpected), "Wechselgeld stimmt nicht!");
            Assert.IsTrue(CompareArrays(changeMachine.EmptyDepot(), depotExpected), "Wechselgeld stimmt nicht!");
        }
    }
}
