using CaliburnTraining.Services;
using CaliburnTraining.Services.Interfaces;
using CaliburnTraining.ViewModels;
using Ninject;
using Ninject.Parameters;

namespace CaliburnTraining
{
    public class SubjectProvider
    {
        static SubjectProvider instance;

        StandardKernel kernel;

        public static SubjectProvider Instance => instance ?? (instance = new SubjectProvider());

        SubjectProvider()
        {
            kernel = new StandardKernel();
            kernel.Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<INavigator>().To<Navigator>().InSingletonScope();
            kernel.Bind<UsersListViewModel>().ToSelf().InSingletonScope();
            kernel.Bind<UserDetailsViewModel>().ToSelf();
            kernel.Bind<DataManager>().ToSelf().InSingletonScope();
        }

        public T Create<T>()
        {
            return kernel.Get<T>();
        }

        public T Create<T>(string propertyName, object value)
        {
            return kernel.Get<T>(new ConstructorArgument(propertyName, value));
        }
    }
}
