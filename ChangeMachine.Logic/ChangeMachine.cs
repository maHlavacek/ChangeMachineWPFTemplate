using System;
using System.Linq;

namespace ChangeMachine.Logic
{
    public class ChangeMachine
    {
        /// <summary>
        /// valid money values (1 EUR, 2EUR, 5 EUR, 10 EUR.....)
        /// </summary>
        private int[] moneyValues;

        /// <summary>
        /// sum of each money value in depot
        /// </summary>
        private int[] valuesInDepot;

        /// <summary>
        /// sum of each inserted money value
        /// </summary>
        private int[] insertedValues;

        /// <summary>
        /// sum of each selected money value
        /// </summary>
        private int[] selectedValues;

        /// <summary>
        /// sum of each money value that will be ejected
        /// </summary>
        private int[] ejectionValues;


        /// <summary>
        /// return the sum of money values 
        /// </summary>
        public int MoneyInDepot => valuesInDepot.Sum();


        #region MoneyCalculation
        public int InsertValue => CalculateMoney(insertedValues);

        public int DepotValue => CalculateMoney(valuesInDepot);

        public int SelectedValue => CalculateMoney(selectedValues);

        public int EjectionValue => CalculateMoney(ejectionValues);

        #endregion



        public ChangeMachine()
        {
            moneyValues = new int[] { 1, 2, 5, 10, 20, 50, 100 };
            valuesInDepot = new int[moneyValues.Length];
            for (int i = 0; i < valuesInDepot.Length; i++)
            {
                valuesInDepot[i] = 30;
            }
            insertedValues = new int[moneyValues.Length];
            selectedValues = new int[moneyValues.Length];
            ejectionValues = new int[moneyValues.Length];
        }

        public ChangeMachine(int[] values) :this()
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (values.Length != moneyValues.Length)
                throw new ArgumentException(nameof(values) + $" length is invalid");

            if (values.Any(v => v < 0))
                throw new ArgumentException(nameof(values) + $" can not be negative");

            for (int i = 0; i < moneyValues.Length; i++)
            {
                valuesInDepot[i] = values[i];
            }
        }

        /// <summary>
        /// Inserted money will be inserted if it is a valid value.
        /// Returns False when the inserted value is not a valid MoneyValue,
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InsertMoney(int value)
        {
            bool result = false;

            //if (value < 0)
            //    throw new ArgumentException(nameof(value) + $" can not be negative");

            if(moneyValues.Contains(value))
            {
                int index = Array.IndexOf(moneyValues, value);
                moneyValues[index] += value;
                result = true;
            }
            return result;
        }

        public void Change()
        {
            throw new NotImplementedException();
        }

        public int[] EmptyEjection()
        {
            throw new NotImplementedException();
        }

        public int[] EmptyDepot()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Helpermethod to calculate the amout of money
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private int CalculateMoney(int[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Length != moneyValues.Length)
                throw new ArgumentException(nameof(values) + "$ length is invalid");

            int result = 0;
            for (int i = 0; i < moneyValues.Length; i++)
            {
                result += values[i] * moneyValues[i];
            }
            return result;
        }
    }
}
