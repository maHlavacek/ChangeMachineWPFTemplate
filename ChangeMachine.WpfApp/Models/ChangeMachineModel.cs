using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace ChangeMachine.WpfApp.Models
{
    public class ChangeMachineModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int[] moneyValues = { 1, 2, 5, 10, 20, 50, 100 };


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Properties

        private Logic.ChangeMachine ChangeMachine { get; set; }


        private int[] valuesInDepot;
        private int[] valuesInInsert;
        private int[] valuesInSelect;
        private int[] valuesInEject;

        public int[] ValuesInDepot
        {
            get { return valuesInDepot; }
            set { valuesInDepot = value; OnPropertyChanged(nameof(ValuesInDepot)); }
        }

        public int[] ValuesInInsert
        {
            get { return valuesInInsert; }
            set { valuesInInsert = value; OnPropertyChanged(nameof(ValuesInInsert)); }
        }

        public int[] ValuesInSelect
        {
            get { return valuesInSelect; }
            set { valuesInSelect = value; OnPropertyChanged(nameof(ValuesInSelect)); }
        }

        public int[] ValuesInEject
        {
            get { return valuesInEject; }
            set { valuesInEject = value; OnPropertyChanged(nameof(ValuesInEject)); }
        }


        /// <summary>
        /// Get the amount of depot money
        /// </summary>
        public int DepotMoney => ChangeMachine.DepotMoney;

        /// <summary>
        /// Get the amount of inserted money
        /// </summary>
        public int InsertedMoney => ChangeMachine.InsertedMoney;

        /// <summary>
        /// Get the amount of selected money
        /// </summary>
        public int SelectedMoney => ChangeMachine.SelectedMoney;

        /// <summary>
        /// Get the amount of ejection money
        /// </summary>
        public int EjectionMoney => ChangeMachine.EjectionMoney;

        /// <summary>
        /// returns moneyValues for the combobox as a string
        /// </summary>
        public string[] MoneyValuesAsString => moneyValues.Select(s => s.ToString()).ToArray();

        #endregion

        #region Constructor
        public ChangeMachineModel()
        {
            ChangeMachine = new Logic.ChangeMachine();
            ValuesInDepot = new int[moneyValues.Length];
            ValuesInInsert = new int[moneyValues.Length];
            ValuesInSelect = new int[moneyValues.Length];
            ValuesInEject = new int[moneyValues.Length];
            Update();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Fill the properties with the current values
        /// </summary>
        private void Update()
        {
            SetDepot();
            SetInsert();
            SetSelect();
            SetEject();
        }

        /// <summary>
        /// Puts the value into the Logic insertMoney and returns true if the process was successful
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool InsertValue(int value)
        {
            bool result;
            result = ChangeMachine.InsertMoney(value);
            Update();
            return result;
        }

        /// <summary>
        /// Cancels the changing process
        /// </summary>
        public void Cancel()
        {
            ChangeMachine.CancelOrder();
            Update();
        }

        /// <summary>
        /// Eject the changed money
        /// </summary>
        public void Eject()
        {
            ChangeMachine.EmptyEjection();
            Update();
        }

        /// <summary>
        /// starts the changing process
        /// </summary>
        public void Change()
        {
            ChangeMachine.Change();
            Update();
        }

        /// <summary>
        /// increase the value for selected change
        /// </summary>
        /// <param name="value"></param>
        public void IncreaseSelect(int value)
        {
            ChangeMachine.IncreseCounterForSelected(value);
            Update();
        }

        /// <summary>
        /// decrease the value for selected change
        /// </summary>
        /// <param name="value"></param>
        public void DecreaseSelect(int value)
        {
            ChangeMachine.DecreseCounterForSelected(value);
            Update();
        }
        #endregion

        #region SetPropertyMethods

        /// <summary>
        /// Set amount of money values in the depot
        /// </summary>
        private void SetDepot()
        {
            int[] counterArray = new int[moneyValues.Length];
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForDepot(moneyValues[i], out int counter);
                counterArray[i] = counter;
            }
            ValuesInDepot = counterArray;
        }

        /// <summary>
        /// Set amount of money values in the insert
        /// </summary>
        private void SetInsert()
        {
            int[] counterArray = new int[moneyValues.Length];
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForInsert(moneyValues[i], out int counter);
                counterArray[i] = counter;
            }
            ValuesInInsert = counterArray;
        }

        /// <summary>
        /// Set amount of money values in the select
        /// </summary>
        private void SetSelect()
        {
            int[] counterArray = new int[moneyValues.Length];
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForSelected(moneyValues[i], out int counter);
                counterArray[i] = counter;
            }
            ValuesInSelect = counterArray;
        }

        /// <summary>
        /// Set amount of money values in the eject
        /// </summary>
        private void SetEject()
        {
            int[] counterArray = new int[moneyValues.Length];
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForEjection(moneyValues[i], out int counter);
                counterArray[i] = counter;
            }
            ValuesInEject = counterArray;
        }
        #endregion
    }
}
