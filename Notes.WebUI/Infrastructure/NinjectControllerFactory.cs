using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace Notes.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            //ninjectKernel.Bind<Bd_course_work.Domain.Abstract.IFootballTeamRepository>().To<EFFootballTeamRepository>();
            //ninjectKernel.Bind<Bd_course_work.Domain.Abstract.IFootbalGameRepository>().To<EFFootbalGameRepository>().InSingletonScope();
            //ninjectKernel.Bind<UnituOfWork>().To<UnituOfWork>().InSingletonScope();
        }
    }
}