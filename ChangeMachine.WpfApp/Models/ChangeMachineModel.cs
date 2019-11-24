using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ChangeMachine.WpfApp.Models
{
    class ChangeMachineModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));
        }

        #region Fields
        private int[] moneyValues = { 1, 2, 5, 10, 20, 50, 100 };

        #endregion

        #region Properties
        private Logic.ChangeMachine ChangeMachine { get; set; }

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
        /// Set amount of money values in the depot
        /// </summary>
        private void SetDepot()
        {
            int counter;
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForDepot(moneyValues[i], out counter);
                ValuesInDepot[i] = counter;
            }
        }



        #endregion

        #region Insert

        private int[] valuesInInsert;

        public int[] ValuesInInsert
        {
            get { return valuesInInsert; }
            set
            {
                valuesInDepot = value;
                OnPropertyChanged(nameof(ValuesInInsert));
            }
        }

        /// <summary>
        /// Set amount of money values in the insert
        /// </summary>
        private void SetInsert()
        {
            int counter;
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForDepot(moneyValues[i], out counter);
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
        /// Set amount of money values in the select
        /// </summary>
        private void SetSelect()
        {
            int counter;
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForDepot(moneyValues[i], out counter);
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
                valuesInSelect = value;
                OnPropertyChanged(nameof(ValuesInEject));
            }
        }

        /// <summary>
        /// Set amount of money values in the eject
        /// </summary>
        private void SetEject()
        {
            int counter;
            for (int i = 0; i < moneyValues.Length; i++)
            {
                ChangeMachine.GetCounterForDepot(moneyValues[i], out counter);
                ValuesInEject[i] = counter;
            }
        }

        #endregion

        public ChangeMachineModel()
        {
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
