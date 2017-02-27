using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Base;
using Core.Utility;

namespace Model
{
    public class UserModel :  BaseModel
    {
        string firstName;
        string secondName;
        DateTime birthDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);
        string photoPath;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (Equals(value, firstName) == false)
                {
                    firstName = value;
                    OnPropertyChanged(x => this.FirstName);
                }
            }
        }

        public string SecondName
        {
            get { return secondName; }
            set
            {
                if (Equals(value, secondName) == false)
                {
                    secondName = value;
                    OnPropertyChanged(x => this.SecondName);
                }
            }
        }


        public DateTime BirthDate
        {
            get { return birthDate; }
            set
            {
                if (Equals(value, birthDate) == false)
                {
                    birthDate = value;
                    OnPropertyChanged(x => this.BirthDate);
                }
            }
        }

        public string PhotoPath
        {
            get { return photoPath; }
            set
            {
                if (Equals(value, photoPath))
                {
                    photoPath = value;
                    OnPropertyChanged(x => this.PhotoPath);
                }
            }
        }

        protected override string Validate(string fieldName)
        {
            string message = string.Empty;

            switch (fieldName)
            {
                case nameof(this.FirstName):
                    {
                        message = ValidationFunctions.ValidateRequiredField(this.FirstName);
                        break;
                    }
                case nameof(this.SecondName):
                    {
                        message = ValidationFunctions.ValidateRequiredField(this.SecondName);
                        break;
                    }
                case nameof(this.BirthDate):
                    {
                        message = ValidationFunctions.BirthDateValidete(this.BirthDate);
                        break;
                    }
            }

            return message;
        }

        protected override void FillErrorsStorage()
        {
            errorsStorage = new List<string>
            {
                MembersOf<UserModel>.GetName(x => x.FirstName),
                MembersOf<UserModel>.GetName(x => x.SecondName),
                MembersOf<UserModel>.GetName(x => x.BirthDate),

            };
        }
    }
}
