using Ninject;
using System.Reflection;

namespace Project.Service
{
    public class KernelNinject
    {
        public static T ReturnInjectedResult<T>()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            return kernel.Get<T>();           
        }
    }
}
