using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

using StructureMap;

using _928.Core;

namespace _928.Web.Mvc {
    public class StructureMapControllerFactory : DefaultControllerFactory {

        private IContainer container; 

        public StructureMapControllerFactory(IContainer container)
            : base() {
                this.container = container;
        }

        public override IController CreateController(RequestContext requestContext, string controllerName) {
            try {
                return container.GetInstance(base.GetControllerType(requestContext, controllerName)) as IController;
            } catch (StructureMapException ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception("Error in CreateController: " + ex.Message);
            } catch (ArgumentNullException) {
                throw new HttpException(404, "Page Not Found. Path: {0}".FormatWith(requestContext.HttpContext.Request.Path));
            }
        }

        protected override IController GetControllerInstance(RequestContext requestContext,
            Type controllerType) {
            try {
                if ((requestContext == null) || (controllerType == null))
                    return null;

                return container.GetInstance(controllerType) as IController;

            } catch (StructureMapException ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                throw new Exception("Error in GetControllerInstance: " + ex.Message);
            }
        }
        public override void ReleaseController(IController controller) {
            IDisposable dispose = controller as IDisposable; if (dispose != null) {
                dispose.Dispose();
            }
        }
    }
}

