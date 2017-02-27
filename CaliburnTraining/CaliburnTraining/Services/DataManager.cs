using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Utility;
using Model;
using Newtonsoft.Json;

namespace CaliburnTraining.Services
{
    public class DataManager
    {
        #region Variables

        static readonly string containerPath;

        #endregion

        #region Construct & Destruct

        static DataManager()
        {
            containerPath = PathHelper.MakeAbsPath("usersList.json");
        }

        #endregion

        #region Properties


        #endregion

        #region  Private Methods

        public List<UserModel> LoadModel()
        {
            List<UserModel> usersList = new List<UserModel>();

            if (File.Exists(containerPath))
            {
                usersList = JsonConvert.DeserializeObject<List<UserModel>>(GetUserContent());
            }

            return usersList;
        }

        public void SaveModel(List<UserModel> users)
        {
            var json = JsonConvert.SerializeObject(users);

            if (!string.IsNullOrEmpty(json))
            {
                File.WriteAllText(containerPath, json);
            }
        }

        string GetUserContent()
        {
            return File.ReadAllText(containerPath);
        }

        #endregion
    }
}
