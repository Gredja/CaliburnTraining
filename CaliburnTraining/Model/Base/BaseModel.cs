using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Model.Base
{
    public abstract class BaseModel : INotifyPropertyChanged, IDataErrorInfo, ICloneable
    {
        List<string> errorFieldList = new List<string>();
        protected List<string> errorsStorage;

        protected BaseModel()
        {
            FillErrorsStorage();
        }

        protected abstract string Validate(string fieldName);

        public string this[string propertyName] => GetValidationError(propertyName);

        string IDataErrorInfo.Error => null;

        public bool IsValid
        {
            get
            {
                return this.ValidateProperties == null || this.ValidateProperties.All(property => string.IsNullOrEmpty(GetValidationError(property)));
            }
        }

        protected void OnPropertyChanged<T>(Expression<Func<object, T>> propertyExpression)
        {
            string propertyName = ((MemberExpression)propertyExpression.Body).Member.Name;
            OnPropertyChangedCore(propertyName);
        }

        protected void OnPropertyChangedCore(string propertyName)
        {
            if (this.IsValid)
            {
                SaveModel();
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void FillErrorsStorage() { }

        void SaveModel()
        {
            SaveEvent?.Invoke(this, EventArgs.Empty);
        }

        string[] ValidateProperties => errorFieldList?.ToArray();

        string GetValidationError(string propertyName)
        {
            string error = null;
            if (errorFieldList != null && errorFieldList.Any())
            {
                foreach (string fieldName in errorFieldList)
                {
                    if (fieldName == propertyName)
                    {
                        error = Validate(fieldName);
                        break;
                    }
                }
            }
            return error;
        }

        void AddOneValidationError(string propertyName)
        {
            if (errorsStorage != null)
            {
                foreach (string name in errorsStorage)
                {
                    if (propertyName == name)
                    {
                        if (!errorFieldList.Contains(name))
                        {
                            errorFieldList.Add(name);
                            GetValidationError(name);
                        }

                        break;
                    }
                }
            }
        }

        public void AddAllValidationErrors()
        {
            if (errorsStorage != null)
            {
                foreach (string name in errorsStorage)
                {
                    if (!errorFieldList.Contains(name))
                    {
                        errorFieldList.Add(name);
                    }
                }

                CallAllErrorMessages();
            }
        }

        public void RemoveAllValidationErrors()
        {
            errorFieldList.Clear();
        }

        protected void CallAllErrorMessages()
        {
            foreach (string error in errorFieldList)
            {
                OnPropertyChangedCore(error);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler SaveEvent;
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
