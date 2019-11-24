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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));
        }

        #region Fields
        public int[] moneyValues = { 1, 2, 5, 10, 20, 50, 100 };


        #endregion

        #region Properties
        private Logic.ChangeMachine ChangeMachine { get; set; }

        public string[] MoneyValuesAsString => moneyValues.Select(s => s.ToString()).ToArray();

        #endregion

        #region Depot

        private int[] valuesInDepot;

        public int[] ValuesInDepot
        {
            get { return valuesInDepot; }
            set
            {
                valuesInDepot = value;
                OnPropertyChanged(nameof(ValuesInDepot));
            }
        }

        /// <summary>
        /// Get the amount of depot money
        /// </summary>
        public int DepotMoney => ChangeMachine.DepotMoney;

        public bool InsertValue(int value)
        {
            bool result;
            result = ChangeMachine.InsertMoney(value);
            Update();
            return result;
        }

        public void Cancel()
        {
            ChangeMachine.CancelOrder();
            Update();
        }


        /// <summary>
        /// Set amount of money values in the depot
        /// </summary>
        private void SetDepot()
        {
            int counter;
            for (int i = 0; i < moneyValues.Length; i++)
            {
                counter = 0;
                ChangeMachine.GetCounterForDepot(moneyValues[i], out counter);
                ValuesInDepot[i] = counter;
            }
        }

        internal void Eject()
        {
            ChangeMachine.EmptyEjection();
            Update();
        }

        internal void Change()
        {
            ChangeMachine.Change();
            Update();
        }




        #endregion

        #region Insert

        private int[] valuesInInsert;

        public int[] ValuesInInsert
        {
            get { return valuesInInsert; }
            set
            {
                valuesInInsert = value;
                OnPropertyChanged(nameof(ValuesInInsert));
            }
        }

        /// <summary>
        /// Get the amount of inserted money
        /// </summary>
        public int InsertedMoney => ChangeMachine.InsertedMoney;

        /// <summary>
        /// Set amount of money values in the insert
        /// </summary>
        private void SetInsert()
        {
            int counter;
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForInsert(moneyValues[i], out counter);
                ValuesInInsert[i] = counter;
            }
        }

        #endregion

        #region Select

        private int[] valuesInSelect;

        public int[] ValuesInSelect
        {
            get { return valuesInSelect; }
            set
            {
                valuesInSelect = value;
                OnPropertyChanged(nameof(ValuesInSelect));
            }
        }

        /// <summary>
        /// Get the amount of selected money
        /// </summary>
        public int SelectedMoney => ChangeMachine.SelectedMoney;



        /// <summary>
        /// Set amount of money values in the select
        /// </summary>
        private void SetSelect()
        {
            int counter;
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForSelected(moneyValues[i], out counter);
                ValuesInSelect[i] = counter;
            }
        }


        #endregion

        #region Eject

        private int[] valuesInEject;

        public int[] ValuesInEject
        {
            get { return valuesInEject; }
            set
            {
                valuesInEject = value;
                OnPropertyChanged(nameof(ValuesInEject));
            }
        }

        /// <summary>
        /// Get the amount of ejection money
        /// </summary>
        public int EjectionMoney => ChangeMachine.EjectionMoney;


        /// <summary>
        /// Set amount of money values in the eject
        /// </summary>
        private void SetEject()
        {
            int counter;
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForEjection(moneyValues[i], out counter);
                ValuesInEject[i] = counter;
            }
        }

        #endregion

        public ChangeMachineModel()
        {
            ChangeMachine = new Logic.ChangeMachine();
            ValuesInDepot = new int[moneyValues.Length];
            ValuesInInsert = new int[moneyValues.Length];
            ValuesInSelect = new int[moneyValues.Length];
            ValuesInEject = new int[moneyValues.Length];
            Update();
        }


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
    }
}
