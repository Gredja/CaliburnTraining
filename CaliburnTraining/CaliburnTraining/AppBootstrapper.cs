using Caliburn.Micro;

namespace CaliburnTraining
{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            var config = new TypeMappingConfiguration
            {
                DefaultSubNamespaceForViewModels = "ViewModels",
                DefaultSubNamespaceForViews = "Views"
            };
            ViewModelLocator.ConfigureTypeMappings(config);
            ViewLocator.ConfigureTypeMappings(config);
            Initialize();
        }
    }
}
