using Ninject;
using System.Reflection;

namespace Project.Service
{
    public class Kernel
    {
        public static T Inject<T>()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            return kernel.Get<T>();
        }
    }
}
