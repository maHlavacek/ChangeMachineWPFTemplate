﻿using System;
using System.Linq;

namespace ChangeMachine.Logic
{
    public class ChangeMachine
    {
        #region Fields

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

        #endregion

        #region Properties
  
        /// <summary>
        /// return the sum of money values 
        /// </summary>
        public int MoneyInDepot => valuesInDepot.Sum();


        /// <summary>
        /// return the amount of inserted money
        /// </summary>
        public int InsertedMoney => CalculateMoney(insertedValues);

        /// <summary>
        /// return the amount of the money in depot
        /// </summary>
        public int DepotMoney => CalculateMoney(valuesInDepot);

        /// <summary>
        /// return the amount of the selected money
        /// </summary>
        public int SelectedMoney => CalculateMoney(selectedValues);

        /// <summary>
        /// return the amount of the ejection money
        /// </summary>
        public int EjectionMoney => CalculateMoney(ejectionValues);

        #endregion

        #region Constructors

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

            values.CopyTo(valuesInDepot, 0);
        }
        #endregion

        #region Methods
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
                insertedValues[index]++;
                result = true;
            }
            return result;
        }


        /// <summary>
        /// Calculates the ejectionValues and put the inserted money in the depot,
        /// </summary>
        public void Change()
        {
            int insert = InsertedMoney;

            AddIntArrays(valuesInDepot, insertedValues);
            AddIntArrays(ejectionValues, selectedValues);

            for (int i = moneyValues.Length - 1; i >= 0; i--)
            {
                //Automatic change
                if (moneyValues[i] <= insert && selectedValues.All(e => e == 0))
                {
                    while (insert >= moneyValues[i] && valuesInDepot[i] > 0)
                    {
                        insert -= moneyValues[i];
                        valuesInDepot[i]--;
                        ejectionValues[i]++;
                    }
                }
                //Selected values change
                else if (selectedValues.Any(e => e > 0) && valuesInDepot[i] > ejectionValues[i])
                {
                    valuesInDepot[i] -= ejectionValues[i];
                }
            }
            EmptyIntArray(selectedValues);
            EmptyDepot();
        }

        /// <summary>
        /// Put inserted money in ejection and empty insertedValues
        /// </summary>
        public void CancelOrder()
        {
            insertedValues.CopyTo(ejectionValues, 0);
            EmptyIntArray(insertedValues);
        }

        /// <summary>
        /// increase the selected value
        /// </summary>
        /// <param name="value"></param>
        public void IncreseCounterForSelected(int value)
        {
            if (moneyValues.Any(m => m == value) && SelectedMoney + value <= InsertedMoney)
            {
                int index = Array.IndexOf(moneyValues, value);
                if (selectedValues[index] < valuesInDepot[index])
                {
                    selectedValues[index]++;
                }
            }
        }

        /// <summary>
        /// decrease the selected value
        /// </summary>
        /// <param name="value"></param>
        public void DecreseCounterForSelected(int value)
        {
            if (moneyValues.Any(m => m == value))
            {
                int index = Array.IndexOf(moneyValues, value);
                if (selectedValues[index] > 0)
                {
                    selectedValues[index]--;
                }
            }
        }

        /// <summary>
        /// Empty ejectionValues and returns them
        /// </summary>
        /// <returns></returns>
        public int[] EmptyEjection()
        {
            int[] result = new int[moneyValues.Length];
            ejectionValues.CopyTo(result, 0);
            EmptyIntArray(ejectionValues);
            return result;  
        }

        /// <summary>
        /// Empty insertedValues and returns valuesInDepot
        /// </summary>
        /// <returns></returns>
        public int[] EmptyDepot()
        {
            int[] result = new int[moneyValues.Length];
            valuesInDepot.CopyTo(result, 0);
            EmptyIntArray(insertedValues);
            return result;
        }

        #endregion

        #region CounterMethods

        /// <summary>
        /// get the counter for Depot and returns true if the value is valid
        /// </summary>
        /// <param name="value"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public bool GetCounterForDepot(int value, out int counter)
        {
          return GetCounter(valuesInDepot, value, out counter);
        }

        /// <summary>
        /// get the counter of insert and returns true if the value is valid
        /// </summary>
        /// <param name="value"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public bool GetCounterForInsert(int value, out int counter)
        {
            return GetCounter(insertedValues, value, out counter);
        }

        /// <summary>
        /// get the counter of insert and returns true if the value is valid
        /// </summary>
        /// <param name="value"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public bool GetCounterForSelected(int value, out int counter)
        {
            return GetCounter(selectedValues, value, out counter);
        }

        /// <summary>
        /// get the counter of insert and returns true if the value is valid
        /// </summary>
        /// <param name="value"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public bool GetCounterForEjection(int value, out int counter)
        {
            return GetCounter(ejectionValues, value, out counter);
        }

        #endregion

        #region helper

        /// <summary>
        /// get the counter with an out parameter for the array which is passed in, and returns true if it was successful
        /// </summary>
        /// <param name="valueArray"></param>
        /// <param name="value"></param>
        /// <param name="counter"></param>
        /// <returns></returns>
        private bool GetCounter(int[] valueArray, int value, out int counter)
        {
            counter = 0;
            bool result = false;
            if (value < 0)
                throw new ArgumentException(nameof(value) + $" can not be less than zero");

            if (moneyValues.Any(m => m == value))
            {
                int index = Array.IndexOf(moneyValues, value);
                counter = valueArray[index];
                result = true;
            }
            return result;
        }

        /// <summary>
        /// Helpermethod to add array values
        /// </summary>
        /// <param name="result"></param>
        /// <param name="other"></param>
        private void AddIntArrays(int[] result, int[] other)
        {
            if (result.Length != other.Length)
                throw new ArgumentException(nameof(result) + $" and " + nameof(other) + $" have not the same length");
            for (int i = 0; i < result.Length; i++)
            {
                result[i] += other[i];
            }
        }

        /// <summary>
        /// Helpermethod to set an int array to zero on each position
        /// </summary>
        /// <param name="array"></param>
        private void EmptyIntArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = 0;
            }
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
    #endregion
}
